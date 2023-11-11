namespace FlatVillage.Maps
{
    public interface IObjectInfo
    {
        int ID { get; }
        string Name { get; }

        void InitializeID(int id);
    }
}