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
        public Interrupts Interrupts { get; }
        public Graphics Graphics { get; }
        public Input Input { get; }

        private ControlUnit ControlUnit { get; }

        public int cyclesExecuted;
        private ushort walkerIndex;

        public Processor(Screen screen)
        {
            this.Memory = new Memory();
            this.Registers = new Registers();
            this.Stack = new Stack(this.Memory, this.Registers.SP);

            this.Interrupts = new Interrupts(this);
            this.Graphics = new Graphics(this, screen);
            this.Input = new Input(this.Memory);
            this.ControlUnit = new ControlUnit(this);

            this.cyclesExecuted = 0;

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

        public void Tick(int timesToTick)
        {
            if (test >= 4)
                return;
            else if (test >= 2)
            {
                Console.Error.WriteLine("writing tileset");
                this.Graphics.RenderTileSet();
                test += 2;
                return;
            }
            
            this.cyclesExecuted = 0;

            while (this.cyclesExecuted < timesToTick)
            {
                Cycle();
            }
        }

        private int test;

        private void Cycle()
        {
            if (this.Registers.PC.Value == 0x27ac)
            {
                this.test += 1;
            }

            // Just to be sure
            this.walkerIndex = this.Registers.PC.Value;

            var instruction = this.ControlUnit.Fetch();
            var operands = this.ControlUnit.FetchOperands(instruction.OperandLength);

#if DEBUG
            this.Registers.Dump();
#endif

            this.ControlUnit.Execute(instruction, operands);

            this.Registers.PC.Value += instruction.Length;
            this.cyclesExecuted += instruction.Cycles;

            this.Graphics.Tick(instruction.Cycles);

            this.Interrupts.Process();

            this.Input.Update();
        }
    }
}