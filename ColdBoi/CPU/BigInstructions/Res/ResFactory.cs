using System;
using System.Collections.Generic;

namespace ColdBoi.CPU.BigInstructions
{
    public class ResFactory
    {
        private struct ResInstructionData
        {
            public ushort opCode;
            public RegisterPair register;
            public bool isHigherByte;
            public int bitNumber;
            public string registerName;

            public ResInstructionData(ushort opCode, RegisterPair register, bool isHigherByte, int bitNumber,
                string registerName)
            {
                this.opCode = opCode;
                this.register = register;
                this.isHigherByte = isHigherByte;
                this.bitNumber = bitNumber;
                this.registerName = registerName;
            }
        }

        private const ushort RES_BASE_OPCODE = 0xcb80;

        private Processor processor;

        private Tuple<RegisterPair, bool, string>[] registers;
        private List<ResInstructionData> instructionData;
        private Tuple<ushort, int>[] addressInstructionData;

        public ResFactory(Processor processor)
        {
            this.processor = processor;
            
            Initialize();
        }

        private void Initialize()
        {
            this.registers = new[]
            {
                new Tuple<RegisterPair, bool, string>(this.processor.Registers.BC, true, "b"),
                new Tuple<RegisterPair, bool, string>(this.processor.Registers.BC, false, "c"),
                new Tuple<RegisterPair, bool, string>(this.processor.Registers.DE, true, "d"),
                new Tuple<RegisterPair, bool, string>(this.processor.Registers.DE, false, "e"),
                new Tuple<RegisterPair, bool, string>(this.processor.Registers.HL, true, "h"),
                new Tuple<RegisterPair, bool, string>(this.processor.Registers.HL, false, "l"),
                new Tuple<RegisterPair, bool, string>(this.processor.Registers.AF, true, "a")
            };

            this.instructionData = new List<ResInstructionData>();

            for (var bitNumber = 0; bitNumber < 8; bitNumber++)
            {
                for (var registerIndex = 0; registerIndex < 7; registerIndex++)
                {
                    var offset = registerIndex == this.registers.Length - 1 ? 1 : 0;
                    this.instructionData.Add(new ResInstructionData(
                        (ushort) (RES_BASE_OPCODE + bitNumber * 8 + registerIndex + offset),
                        this.registers[registerIndex].Item1, this.registers[registerIndex].Item2, bitNumber,
                        this.registers[registerIndex].Item3));
                }
            }

            this.addressInstructionData = new[]
            {
                new Tuple<ushort, int>(0xcb86, 0),
                new Tuple<ushort, int>(0xcb8e, 1),
                new Tuple<ushort, int>(0xcb96, 2),
                new Tuple<ushort, int>(0xcb9e, 3),
                new Tuple<ushort, int>(0xcba6, 4),
                new Tuple<ushort, int>(0xcbae, 5),
                new Tuple<ushort, int>(0xcbb6, 6),
                new Tuple<ushort, int>(0xcbbe, 7)
            };
        }

        public void Populate(Dictionary<ushort, BigInstruction> instructionMap)
        {
            foreach (var instruction in this.instructionData)
            {
                instructionMap.Add(instruction.opCode,
                    new Res(this.processor, instruction.opCode, instruction.register, instruction.isHigherByte,
                        instruction.bitNumber, instruction.registerName));
            }

            foreach (var (opCode, bitNumber) in addressInstructionData)
            {
                instructionMap.Add(opCode, new ResHl(this.processor, opCode, bitNumber));
            }
        }
    }
}