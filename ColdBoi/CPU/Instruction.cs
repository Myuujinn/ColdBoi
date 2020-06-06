namespace ColdBoi.CPU
{
    public abstract class Instruction : IInstruction
    {
        protected Processor processor;
        public byte OpCode { get; }

        public string Name { get; }
        public byte OpCodeLength => sizeof(byte);

        public byte OperandLength { get; }
        public byte Length => (byte) (sizeof(byte) + this.OperandLength);
        public byte Cycles { get; protected set; }

        public Instruction(Processor processor, byte opCode, byte operandLength, byte cycles, string name)
        {
            this.processor = processor;
            this.OpCode = opCode;
            this.OperandLength = operandLength;
            this.Cycles = cycles;
            Name = name;
        }

        public abstract void Execute(params byte[] operands);
    }
}