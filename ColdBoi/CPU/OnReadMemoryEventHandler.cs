namespace ColdBoi.CPU
{
    public delegate void OnReadMemoryEventHandler(object sender, ushort address, ref byte value);
}