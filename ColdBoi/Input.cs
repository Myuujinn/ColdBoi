using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace ColdBoi
{
    public class Input
    {
        private const ushort INPUT_ADDRESS = 0xff00;
        private const byte DEFAULT_STATE = 0xc0;
        private const byte INPUT_MASK = 0x30;

        private Memory memory;
        private List<Tuple<byte, Keys, Keys>> inputMap;

        public Input(Memory memory)
        {
            this.memory = memory;
            
            this.inputMap = new List<Tuple<byte, Keys, Keys>>
            {
                new Tuple<byte, Keys, Keys>(0, Keys.A, Keys.Right),
                new Tuple<byte, Keys, Keys>(1, Keys.B, Keys.Left),
                new Tuple<byte, Keys, Keys>(2, Keys.Enter, Keys.Up),
                new Tuple<byte, Keys, Keys>(3, Keys.Back, Keys.Down)
            };
            
            
        }

        private byte Read()
        {
            return this.memory.Content[INPUT_ADDRESS];
        }

        private void Write(byte inputByte)
        {
            this.memory.Write(INPUT_ADDRESS, inputByte);
        }

        public void Update()
        {
            var keyboardState = Keyboard.GetState();
            var pollingBits = (byte) (Read() & INPUT_MASK);
            var inputByte = (byte) (DEFAULT_STATE | pollingBits);
            

            foreach (var (bitNumber, firstKey, secondKey) in this.inputMap)
            {
                var isPressed = keyboardState.IsKeyDown(firstKey) || keyboardState.IsKeyDown(secondKey);
                if (isPressed)
                    continue;

                inputByte |= (byte) (1 << bitNumber);
            }

            Write(inputByte);
        }
    }
}