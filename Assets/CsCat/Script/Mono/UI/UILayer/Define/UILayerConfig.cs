namespace CsCat
{
	public class UILayerConfig
	{
		public EUILayerName name;
		public int orderInLayer;
		public UILayerRule uiLayerRule;

		public UILayerConfig(EUILayerName name, int orderInLayer, UILayerRule uiLayerRule)
		{
			this.name = name;
			this.orderInLayer = orderInLayer;
			this.uiLayerRule = uiLayerRule;
		}



	}
}