namespace CsCat
{
  public class UILayerConfig
  {
    public EUILayerName name;
    public int order_in_layer;
    public float plane_distance;


    public UILayerConfig(EUILayerName name, int order_in_layer, float plane_distance)
    {
      this.name = name;
      this.order_in_layer = order_in_layer;
      this.plane_distance = plane_distance;
    }
  }
}