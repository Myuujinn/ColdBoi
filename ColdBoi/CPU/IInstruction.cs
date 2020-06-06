namespace ColdBoi.CPU
{
    public interface IInstruction
    {
        public string Name { get; }
        public byte OpCodeLength { get; }
        public byte OperandLength { get; }
        public byte Length { get; }
        public byte Cycles { get; }
        
        public void Execute(byte[] operands);
    }
}