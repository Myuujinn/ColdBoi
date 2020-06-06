namespace ColdBoi.CPU
{
    public abstract class BigInstruction : IInstruction
    {
        protected Processor processor;
        public ushort OpCode { get; }
        public string Name { get; }
        public byte OpCodeLength => sizeof(ushort);
        public byte OperandLength { get; }
        public byte Length => (byte) (sizeof(ushort) + this.OperandLength);
        public byte Cycles { get; }

        public BigInstruction(Processor processor, ushort opCode, byte operandLength, byte cycles, string name)
        {
            this.processor = processor;
            this.OpCode = opCode;
            this.OperandLength = operandLength;
            this.Cycles = cycles;
            this.Name = name;
        }

        public abstract void Execute(params byte[] operands);
    }
}