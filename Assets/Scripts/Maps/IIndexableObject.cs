namespace FlatVillage.Maps
{
    public interface IIndexableObject
    {
        int ID { get; }

        void InitializeID(int id);
    }
}