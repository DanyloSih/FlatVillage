namespace FlatVillage.Maps
{
    public interface ILayerTile
    {
        public string LayerName { get; }
        public IObjectInfo ObjectInfo { get; }
    }
}
