namespace ColdBoi.CPU
{
    public delegate void OnMemoryEventHandler(object sender, ushort address, ref byte value);
}