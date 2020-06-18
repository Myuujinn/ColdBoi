using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ColdBoi
{
    public class Memory
    {
        private const int ROM_START = 0x0000;
        private const int ROM_END = 0x7fff;
        private const int MEMORY_SIZE = 0xffff + 1;
        private const int MEMORY_ECHO_OFFSET = 0x2000;
        private const int ROM_SIZE = 0x8000;
        private const int GAME_TITLE = 0x0134;
        private const int IO_START = 0xff00;
        private const int INTERRUPT_ASSERTED = 0xff0f;
        private const int INTERRUPT_ENABLED = 0xffff;
        private const int WRAM_START = 0xc000;
        private const int WRAM_STOP = 0xdfff;
        private const int ECHO_MEMORY_STOP = 0xfdff;
        private const int OAM_DMA_SOURCE_ADDRESS = 0xff46;
        
        private readonly Tuple<int, int> InternalRamRange;
        private readonly Tuple<int, int> EchoInternalRamRange;

        public byte[] Content { get; }

        public string RomName
        {
            get =>
                System.Text.Encoding.UTF8.GetString(this.Content[GAME_TITLE..(GAME_TITLE + 16)]);
        }

        public Memory()
        {
            this.Content = new byte[MEMORY_SIZE];
            this.InternalRamRange = new Tuple<int, int>(WRAM_START, WRAM_STOP);
            this.EchoInternalRamRange = new Tuple<int, int>(
                InternalRamRange.Item1 + MEMORY_ECHO_OFFSET,
                ECHO_MEMORY_STOP);

            Initialize();
        }

        private void Initialize()
        {
            SetDefaultIoValues();
        }

        private void SetDefaultIoValues()
        {
            var ioValues = new byte[]
            {
                0x0F, 0x00, 0x7C, 0xFF, 0x00, 0x00, 0x00, 0xF8, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x01,
                0x80, 0xBF, 0xF3, 0xFF, 0xBF, 0xFF, 0x3F, 0x00, 0xFF, 0xBF, 0x7F, 0xFF, 0x9F, 0xFF, 0xBF, 0xFF,
                0xFF, 0x00, 0x00, 0xBF, 0x77, 0xF3, 0xF1, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF,
                0x91, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFC, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x7E, 0xFF, 0xFE,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x3E, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xC0, 0xFF, 0xC1, 0x00, 0xFE, 0xFF, 0xFF, 0xFF,
                0xF8, 0xFF, 0x00, 0x00, 0x00, 0x8F, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xCE, 0xED, 0x66, 0x66, 0xCC, 0x0D, 0x00, 0x0B, 0x03, 0x73, 0x00, 0x83, 0x00, 0x0C, 0x00, 0x0D,
                0x00, 0x08, 0x11, 0x1F, 0x88, 0x89, 0x00, 0x0E, 0xDC, 0xCC, 0x6E, 0xE6, 0xDD, 0xDD, 0xD9, 0x99,
                0xBB, 0xBB, 0x67, 0x63, 0x6E, 0x0E, 0xEC, 0xCC, 0xDD, 0xDC, 0x99, 0x9F, 0xBB, 0xB9, 0x33, 0x3E,
                0x45, 0xEC, 0x52, 0xFA, 0x08, 0xB7, 0x07, 0x5D, 0x01, 0xFD, 0xC0, 0xFF, 0x08, 0xFC, 0x00, 0xE5,
                0x0B, 0xF8, 0xC2, 0xCE, 0xF4, 0xF9, 0x0F, 0x7F, 0x45, 0x6D, 0x3D, 0xFE, 0x46, 0x97, 0x33, 0x5E,
                0x08, 0xEF, 0xF1, 0xFF, 0x86, 0x83, 0x24, 0x74, 0x12, 0xFC, 0x00, 0x9F, 0xB4, 0xB7, 0x06, 0xD5,
                0xD0, 0x7A, 0x00, 0x9E, 0x04, 0x5F, 0x41, 0x2F, 0x1D, 0x77, 0x36, 0x75, 0x81, 0xAA, 0x70, 0x3A,
                0x98, 0xD1, 0x71, 0x02, 0x4D, 0x01, 0xC1, 0xFF, 0x0D, 0x00, 0xD3, 0x05, 0xF9, 0x00, 0x0B, 0x00
            };

            for (ushort i = 0; i < ioValues.Length; i++)
            {
                Write(i + IO_START, ioValues[i]);
            }
        }

        private void WriteInRam(int address, byte value)
        {
            if (address < this.InternalRamRange.Item1 || address > this.EchoInternalRamRange.Item2)
                return;

            var offset = address < this.InternalRamRange.Item2 ? MEMORY_ECHO_OFFSET : -MEMORY_ECHO_OFFSET;
            var echoAddress = address + offset;

            if (echoAddress > this.EchoInternalRamRange.Item2)
                return;

            this.Content[echoAddress] = value;
        }

        public void Write(int address, byte value)
        {
            if (address >= ROM_START && address <= ROM_END) // can't write in ROM
                return;
            
            WriteInRam(address, value);
            InterceptScanlineWrite(address, ref value);
            this.Content[address] = value;
            
            if (address == OAM_DMA_SOURCE_ADDRESS)
                OamDma(value);
        }

        private void OamDma(byte value)
        {
            var baseAddress = value << 8;

            for (var i = 0; i < 0xa0; i++)
            {
                this.Content[Graphics.OAM_START + i] = this.Content[baseAddress + i];
            }
        }

        private void InterceptScanlineWrite(int address, ref byte value)
        {
            if (address == Graphics.SCANLINE)
                value = 0;
        }

        public void Write(int address, ushort value)
        {
            var lowestByte = (byte) value;
            var highestByte = (byte) (value >> 8);
            
            Write(address, lowestByte);
            Write(address + 1, highestByte);
        }

        public void LoadRomData(byte[] data)
        {
            if (data.Length != ROM_SIZE)
                throw new InvalidDataException("ROM is not the right size.");

            for (var i = 0; i < ROM_SIZE; i++)
            {
                this.Content[i] = data[i];
            }
        }

        public void Dump()
        {
            Console.WriteLine("-- Memory Dump --");

            for (var i = 0; i < MEMORY_SIZE; i += 8)
            {
                var bytes = this.Content[i..(i + 8)];
                // don't take non printable characters, feel free to improve if there is a better way
                // while achieving the same results
                var chars = bytes.Select(Convert.ToChar).Select(c => c < 32 || c > 127 ? '.' : c)
                    .ToArray();
                var text = new string(chars);
                text = Regex.Replace(text, @"\p{C}+", ".");

                Console.WriteLine("{0:X4}: {1:X2} {2:X2} {3:X2} {4:X2} {5:X2} {6:X2} {7:X2} {8:X2}  {9}", i,
                    bytes[0], bytes[1], bytes[2], bytes[3], bytes[4],
                    bytes[5], bytes[6], bytes[7], text);
            }

            Console.WriteLine("-- End of Memory Dump --");
        }
    }
}