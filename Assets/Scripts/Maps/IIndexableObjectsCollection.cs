namespace FlatVillage.Maps
{
    public interface IIndexableObjectsCollection<T>
        where T : class, IIndexableObject
    {
        T GetByID(int id);

        int GetObjectsCount();
    }
}