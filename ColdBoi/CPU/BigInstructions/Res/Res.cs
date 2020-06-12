using System;

namespace ColdBoi.CPU.BigInstructions
{
    public class Res : BigInstruction
    {
        public const string NAME = "res";
        
        private RegisterPair register;
        private bool isHigherByte;
        private int bitNumber;
        private string registerName;
        
        public Res(Processor processor, ushort opCode, RegisterPair register, bool isHigherByte, int bitNumber, string registerName) : base(processor, opCode, 0, 8, NAME)
        {
            this.register = register;
            this.isHigherByte = isHigherByte;
            this.bitNumber = bitNumber;
            this.registerName = registerName;
        }

        public override void Execute(params byte[] operands)
        {
            if (isHigherByte)
                this.register.HigherByte = Bit.Set(this.register.HigherByte, this.bitNumber, false);
            else
                this.register.LowerByte = Bit.Set(this.register.LowerByte, this.bitNumber, false);

#if DEBUG
            Console.WriteLine($"{this.processor.Registers.PC.Value:X4}: {this.Name} {this.bitNumber}, {this.registerName}");
#endif
        }
    }
}