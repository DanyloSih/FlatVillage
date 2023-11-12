namespace FlatVillage.Maps
{
    public interface ILayerProvider<TLayerData, TLayerObjectInfo> : ILayerProviderBase
        where TLayerObjectInfo : class, IIndexableObject
    {
        public Map<TLayerData, TLayerObjectInfo> Map { get; }
    }
}
