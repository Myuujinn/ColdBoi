using System.Collections.Generic;
using System.IO;

namespace ColdBoi
{
    public class GameBoy
    {
        public Timer Timer { get; }
        public Processor Processor { get; }
        public Graphics Graphics { get; }
        public Screen Screen { get; }
        public Input Input { get; }

        public GameBoy(string romPath)
        {
            this.Timer = new Timer(Processor.CLOCK_SPEED);
            this.Processor = new Processor();
            this.Graphics = new Graphics(this.Processor.Memory);
            this.Screen = new Screen();
            this.Input = new Input(this.Processor.Memory);

            this.Timer.AddAction(this.Input.Update);
            this.Timer.AddAction(this.Processor.Tick);
            this.Timer.AddAction(this.Graphics.Tick);

            LoadRom(romPath);
            SetDefaultBootValues();
        }

        // As seen here: http://bgb.bircd.org/pandocs.htm#powerupsequence
        private void SetDefaultBootValues()
        {
            this.Processor.Registers.AF.Value = 0x01b0;
            this.Processor.Registers.BC.Value = 0x0013;
            this.Processor.Registers.DE.Value = 0x00d8;
            this.Processor.Registers.HL.Value = 0x014d;
            this.Processor.Registers.SP.Value = 0xfffe;

            var memoryValues = new Dictionary<ushort, byte>
            {
                {0xff05, 0x00},
                {0xff06, 0x00},
                {0xff07, 0x00},
                {0xff10, 0x80},
                {0xff11, 0xbf},
                {0xff12, 0xf3},
                {0xff14, 0xbf},
                {0xff16, 0x3f},
                {0xff17, 0x00},
                {0xff19, 0xbf},
                {0xff1a, 0x7f},
                {0xff1b, 0xff},
                {0xff1c, 0x9f},
                {0xff1e, 0xbf},
                {0xff20, 0xff},
                {0xff21, 0x00},
                {0xff22, 0x00},
                {0xff23, 0xbf},
                {0xff24, 0x77},
                {0xff25, 0xf3},
                {0xff26, 0xf1},
                {0xff40, 0x91},
                {0xff42, 0x00},
                {0xff43, 0x00},
                {0xff45, 0x00},
                {0xff47, 0xfc},
                {0xff48, 0xff},
                {0xff49, 0xff},
                {0xff4a, 0x00},
                {0xff4b, 0x00},
                {0xffff, 0x00},
            };

            foreach (var (address, value) in memoryValues)
            {
                this.Processor.Memory.Write(address, value);
            }
        }

        private void LoadRom(string romPath)
        {
            var romData = File.ReadAllBytes(romPath);
            this.Processor.Memory.LoadRomData(romData);
        }

        public void Update(double timeElapsed)
        {
            this.Timer.Update(timeElapsed);
        }
    }
}