namespace BookShelf.BL
{
    public class ReadersContext:IAbstractReader
    {
        private IAbstractReader _concretereader;

        public ReadersContext(IAbstractReader reader)
        {
            _concretereader = reader;
        }

        public void Read(string filepath)
        {
            _concretereader.Read(filepath);
        }
    }
}
