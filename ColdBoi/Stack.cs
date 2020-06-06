using ColdBoi.CPU;

namespace ColdBoi
{
    public class Stack
    {
        private const ushort INCREMENT = 2;

        private BigRegister stackPointer;
        private Memory memory;

        public Stack(Memory memory, BigRegister stackPointer)
        {
            this.memory = memory;
            this.stackPointer = stackPointer;
        }

        public void Push(ushort value)
        {
            this.stackPointer.Value -= INCREMENT;
            this.memory.Write(this.stackPointer.Value, value);
        }

        public ushort Peek()
        {
            return (ushort) (this.memory.Content[this.stackPointer.Value] +
                             (this.memory.Content[this.stackPointer.Value + 1] << 8));
        }

        public ushort Pop()
        {
            var value = Peek();
            this.stackPointer.Value += INCREMENT;
            return value;
        }
    }
}