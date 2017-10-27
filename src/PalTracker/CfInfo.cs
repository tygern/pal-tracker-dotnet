namespace PalTracker
{
    public class CfInfo
    {
        public string Port { get; }
        public string MemoryLimit  { get; }
        public string CfInstanceIndex  { get; }
        public string CfInstanceAddr  { get; }

        public CfInfo(string port, string memoryLimit, string cfInstanceIndex, string cfInstanceAddr)
        {
            Port = port;
            MemoryLimit = memoryLimit;
            CfInstanceIndex = cfInstanceIndex;
            CfInstanceAddr = cfInstanceAddr;
        }
    }
}
