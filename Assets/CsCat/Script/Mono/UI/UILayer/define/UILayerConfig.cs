namespace CsCat
{
	public class UILayerConfig
	{
		public EUILayerName name;
		public int order_in_layer;
		public UILayerRule uiLayerRule;

		public UILayerConfig(EUILayerName name, int order_in_layer, UILayerRule uiLayerRule)
		{
			this.name = name;
			this.order_in_layer = order_in_layer;
			this.uiLayerRule = uiLayerRule;
		}



	}
}