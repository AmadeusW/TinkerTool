namespace Analyzer.Library
{
    public interface IRepository
    {
        void Initialize();
        void Pull();
        object Get(string property);
        void Set(string property, object value);
    }
}