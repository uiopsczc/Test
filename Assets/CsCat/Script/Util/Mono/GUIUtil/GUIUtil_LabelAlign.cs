using UnityEngine;

namespace CsCat
{
	public partial class GUIUtil
	{
		public static GUILabelAlignScope LabelAlign(TextAnchor textAnchor)
		{
			return new GUILabelAlignScope(textAnchor);
		}
	}
}