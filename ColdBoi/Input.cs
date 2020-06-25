using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace ColdBoi
{
    public class Input
    {
        private const ushort INPUT_ADDRESS = 0xff00;
        private const byte DEFAULT_STATE = 0xc0;
        private const byte BIT_4_MASK = 0x10;
        private const byte BIT_5_MASK = 0x20;

        private Processor processor;
        private Memory Memory => this.processor.Memory;
        private readonly List<Tuple<byte, Keys, Keys>> inputMap;

        public Input(Processor processor)
        {
            this.processor = processor;
            
            this.inputMap = new List<Tuple<byte, Keys, Keys>>
            {
                new Tuple<byte, Keys, Keys>(0, Keys.Right, Keys.A),
                new Tuple<byte, Keys, Keys>(1, Keys.Left, Keys.B),
                new Tuple<byte, Keys, Keys>(2, Keys.Up, Keys.Back),
                new Tuple<byte, Keys, Keys>(3, Keys.Down, Keys.Enter)
            };
        }

        private byte Read()
        {
            return this.Memory.Content[INPUT_ADDRESS];
        }

        private void Write(byte inputByte)
        {
            this.Memory.Write(INPUT_ADDRESS, inputByte);
        }

        public void Update()
        {
            var keyboardState = Keyboard.GetState();
            var pollingBits = Read();
            byte inputByte = 0;

            if (!Bit.IsSet(pollingBits, 4))
            {
                foreach (var (bitNumber, firstKey, _) in this.inputMap)
                {
                    var bitValue = !keyboardState.IsKeyDown(firstKey);

                    inputByte = Bit.Set(inputByte, bitNumber, bitValue);
                }

                //inputByte = (byte) (DEFAULT_STATE | inputByte | BIT_4_MASK | BIT_5_MASK);
            }
            else if (!Bit.IsSet(pollingBits, 5))
            {
                foreach (var (bitNumber, _, secondKey) in this.inputMap)
                {
                    var bitValue = !keyboardState.IsKeyDown(secondKey);
                    
                    inputByte = Bit.Set(inputByte, bitNumber, bitValue);
                }
                
                //inputByte = (byte) (DEFAULT_STATE | inputByte | BIT_4_MASK | BIT_5_MASK);
            }
            else
            {
                inputByte = 0xff;
            }

            pollingBits ^= 0xff;
            inputByte |= 0xf0;
            inputByte &= pollingBits;
            
            Write(inputByte);
        }
    }
}