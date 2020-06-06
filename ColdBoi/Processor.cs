using System;
using ColdBoi.CPU;

namespace ColdBoi
{
    public class Processor : ITickable
    {
        public const int CLOCK_SPEED = 4194304; // in Hertz
        private const ushort ENTRY_POINT = 0x100;

        public Memory Memory { get; }
        public Registers Registers { get; }
        public Stack Stack { get; }
        private ControlUnit ControlUnit { get; }

        private byte cyclesToWait;
        private ushort walkerIndex;

        public Processor()
        {
            this.Memory = new Memory();
            this.Registers = new Registers();
            this.Stack = new Stack(this.Memory, this.Registers.SP);
            this.ControlUnit = new ControlUnit(this);

            this.cyclesToWait = 0;

            Initialize();
        }

        private void Initialize()
        {
            this.Registers.PC.Value = ENTRY_POINT;
        }

        public byte Walk()
        {
            return this.Memory.Content[this.walkerIndex++];
        }

        public void Tick()
        {
            if (this.cyclesToWait > 0)
            {
                this.cyclesToWait -= 1;
                return;
            }

            // Just to be sure
            this.walkerIndex = this.Registers.PC.Value;

            var instruction = this.ControlUnit.Fetch();
            var operands = this.ControlUnit.FetchOperands(instruction.OperandLength);

#if DEBUG
            //this.Registers.Dump();
#endif
            
            this.ControlUnit.Execute(instruction, operands);

            if (this.Registers.PC.Value == 0x282a)
            {
                this.Registers.Dump();
                Environment.Exit(1);
            }
                
            this.Registers.PC.Value += instruction.Length;
            this.cyclesToWait = instruction.Cycles;
        }
    }
}