namespace CsCat
{
	public class UILayerConfig
	{
		public EUILayerName name;
		public int orderInLayer;
		public UILayerRule uiLayerRule;

		public UILayerConfig(EUILayerName name, int order_in_layer, UILayerRule uiLayerRule)
		{
			this.name = name;
			this.orderInLayer = order_in_layer;
			this.uiLayerRule = uiLayerRule;
		}



	}
}