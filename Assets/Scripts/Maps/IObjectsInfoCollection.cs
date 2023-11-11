namespace FlatVillage.Maps
{
    public interface IObjectsInfoCollection<T>
        where T : class, IObjectInfo
    {
        T GetByID(int id);
        T GetByName(string tileName);
        int GetObjectsCount();
    }
}