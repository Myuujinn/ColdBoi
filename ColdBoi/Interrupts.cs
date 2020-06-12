using System;
using System.Collections.Generic;

namespace ColdBoi
{
    public class Interrupts
    {
        public enum Type
        {
            VBlank = 0,
            Lcd = 1,
            Timer = 2,
            Serial = 3,
            Joypad = 4
        }

        private ushort[] ISRLocations; // Interrupt Service Routine

        private const int INTERRUPT_ASSERTED = 0xff0f;
        private const int INTERRUPT_ENABLED = 0xffff;

        public bool Master { get; set; }

        public byte Asserted
        {
            get => this.Memory.Content[INTERRUPT_ASSERTED];
            set => this.Memory.Write(INTERRUPT_ASSERTED, (byte) (value | 0xe0));
        }

        public byte Enabled
        {
            get => this.Memory.Content[INTERRUPT_ENABLED];
            set => this.Memory.Write(INTERRUPT_ENABLED, value);
        }

        public bool VBlank
        {
            get => Bit.IsSet(this.Asserted, (int) Type.VBlank);
            set => this.Asserted = Bit.Set(this.Asserted, (int) Type.VBlank, value);
        }

        public bool Lcd
        {
            get => Bit.IsSet(this.Asserted, (int) Type.Lcd);
            set => this.Asserted = Bit.Set(this.Asserted, (int) Type.Lcd, value);
        }

        public bool Timer
        {
            get => Bit.IsSet(this.Asserted, (int) Type.Timer);
            set => this.Asserted = Bit.Set(this.Asserted, (int) Type.Timer, value);
        }

        public bool Serial
        {
            get => Bit.IsSet(this.Asserted, (int) Type.Serial);
            set => this.Asserted = Bit.Set(this.Asserted, (int) Type.Serial, value);
        }

        public bool Joypad
        {
            get => Bit.IsSet(this.Asserted, (int) Type.Joypad);
            set => this.Asserted = Bit.Set(this.Asserted, (int) Type.Joypad, value);
        }

        private readonly Processor processor;
        private Memory Memory => this.processor.Memory;

        public Interrupts(Processor processor)
        {
            this.processor = processor;

            this.ISRLocations = new ushort[] {0x40, 0x48, 0x50, 0x58, 0x60};
        }

        public void Trigger(Type type)
        {
            if (!this.Master)
                return;
            
            this.Asserted = Bit.Set(this.Asserted, (int) type, true);
        }

        public void Process()
        {
            if (!this.Master || this.Asserted == 0)
                return;

            var asserted = this.Asserted;
            var enabled = this.Enabled;

            foreach (Type type in Enum.GetValues(typeof(Type)))
            {
                var bit = (int) type;
                if (Bit.IsSet(asserted, bit) && Bit.IsSet(enabled, bit))
                    Service(type);
            }
        }

        private void Service(Type type)
        {
            this.Master = false;
            this.Asserted = Bit.Set(this.Asserted, (byte) type, false);
            
            this.processor.Stack.Push(this.processor.Registers.PC.Value);
            
            this.processor.Registers.PC.Value = this.ISRLocations[(int) type];
        }
    }
}