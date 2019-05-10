namespace Analyzer.Library
{
    /// <summary>
    /// A client side view of the data model
    /// </summary>
    public interface IRepository
    {
        void Initialize();
        void Pull();
        object Get(string property);
        void Set(string property, object value);
    }
}