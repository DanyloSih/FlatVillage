namespace FlatVillage.Maps
{
    public interface ILayerProvider<TLayerData, TLayerObjectInfo> : ILayerProviderBase
        where TLayerObjectInfo : class, IObjectInfo
    {
        public Map<TLayerData, TLayerObjectInfo> Map { get; }
    }
}
