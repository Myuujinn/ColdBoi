using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace ColdBoi
{
    public class Input
    {
        public const ushort INPUT_ADDRESS = 0xff00;
        private const byte DEFAULT_STATE = 0xc0;
        private const byte BIT_4_MASK = 0x10;
        private const byte BIT_5_MASK = 0x20;

        private readonly Processor processor;
        private Memory Memory => processor.Memory;
        private readonly List<Tuple<byte, Keys, Keys>> inputMap;

        private KeyboardState keyboardState;

        private bool IsPollingForButtons => !Bit.IsSet(Read(), 5);
        private bool IsPollingForDirections => !Bit.IsSet(Read(), 4);
        private bool IsPollingBoth => IsPollingForButtons && IsPollingForDirections;

        public Input(Processor processor)
        {
            this.processor = processor;
            
            inputMap = new List<Tuple<byte, Keys, Keys>>
            {
                new Tuple<byte, Keys, Keys>(0, Keys.Right, Keys.A),
                new Tuple<byte, Keys, Keys>(1, Keys.Left, Keys.B),
                new Tuple<byte, Keys, Keys>(2, Keys.Up, Keys.Back),
                new Tuple<byte, Keys, Keys>(3, Keys.Down, Keys.Enter)
            };
            
            Write(0xff);

            this.keyboardState = Keyboard.GetState();

            Memory.OnRead += OnMemoryRead;
        }

        private byte Read()
        {
            return Memory.Content[INPUT_ADDRESS];
        }

        private void Write(byte inputByte)
        {
            Memory.Write(INPUT_ADDRESS, inputByte);
        }

        private void OnMemoryRead(object sender, ushort address, ref byte value)
        {
            if (address == INPUT_ADDRESS)
                value = GetValueFromState();
        }

        private byte GetValueFromState()
        {
            byte inputByte = 0xf;

            if (IsPollingForButtons)
            {
                foreach (var (bitNumber, _, secondKey) in inputMap)
                {
                    if (this.keyboardState.IsKeyDown(secondKey)) 
                        inputByte = Bit.Set(inputByte, bitNumber, false);
                }

                return (byte) (DEFAULT_STATE | inputByte | 0x30);
            }

            if (IsPollingForDirections)
            {
                foreach (var (bitNumber, firstKey, _) in inputMap)
                {
                    if (this.keyboardState.IsKeyDown(firstKey))
                        inputByte = Bit.Set(inputByte, bitNumber, false);
                }
                
                return (byte) (DEFAULT_STATE | inputByte | 0x30);
            }

            if (IsPollingBoth)
            {
                return 0xff;
            }

            return 0;
        }

        public void Update(int frequency)
        {
            this.keyboardState = Keyboard.GetState();
        }
    }
}