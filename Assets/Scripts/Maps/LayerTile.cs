namespace FlatVillage.Maps
{
    public struct LayerTile : ILayerTile
    {
        public string LayerName { get; }
        public IObjectInfo ObjectInfo { get; }

        public LayerTile(string layerName, IObjectInfo objectInfo)
        {
            LayerName = layerName;
            ObjectInfo = objectInfo;
        }
    }
}
