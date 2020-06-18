using System;

namespace ColdBoi.CPU.BigInstructions
{
    public class Bit : BigInstruction
    {
        public const string NAME = "bit";

        private RegisterPair register;
        private bool isHigherByte;
        private int bitNumber;
        private string registerName;

        public Bit(Processor processor, ushort opCode, RegisterPair register, bool isHigherByte, int bitNumber,
            string registerName) : base(processor, opCode, 0, 8, NAME)
        {
            this.register = register;
            this.isHigherByte = isHigherByte;
            this.bitNumber = bitNumber;
            this.registerName = registerName;
        }

        public override void Execute(params byte[] operands)
        {
            var registerByte = this.isHigherByte ? this.register.HigherByte : this.register.LowerByte;
            this.processor.Registers.Zero.Value = !global::ColdBoi.Bit.IsSet(registerByte, this.bitNumber);
            this.processor.Registers.Subtract.Value = false;
            this.processor.Registers.HalfCarry.Value = true;


#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} {this.bitNumber}, {this.registerName}");
#endif
        }
    }
}