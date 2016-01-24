namespace BookShelf.BL
{
    //Implementation of strategy pattern 
    public interface IAbstractReader
    {
        void Read(string filepath);
    }
}
