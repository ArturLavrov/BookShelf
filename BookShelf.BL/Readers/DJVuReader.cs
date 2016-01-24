using System.Diagnostics;

namespace BookShelf.BL
{
    public class DJVuReader : IAbstractReader
    {
        public void Read(string filepath)
        {
            Process.Start(filepath);

        }
    }
}
