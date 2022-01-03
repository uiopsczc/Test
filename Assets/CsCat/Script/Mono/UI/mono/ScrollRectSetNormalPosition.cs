using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	[RequireComponent(typeof(ScrollRect))]
	public class ScrollRectSetNormalPosition : MonoBehaviour
	{
		private ScrollRect scrollRect;

		private const float default_normalizedPosition = -1;
		public float target_verticalNormalizedPosition = default_normalizedPosition;
		public float target_horizontalNormalizedPosition = default_normalizedPosition;

		public void Awake()
		{
			scrollRect = this.GetComponent<ScrollRect>();
		}

		void Update()
		{
			if (target_verticalNormalizedPosition != default_normalizedPosition)
			{
				scrollRect.verticalNormalizedPosition = target_verticalNormalizedPosition;
				if (scrollRect.verticalNormalizedPosition == target_verticalNormalizedPosition)
					target_verticalNormalizedPosition = default_normalizedPosition;
			}

			if (target_horizontalNormalizedPosition != default_normalizedPosition)
			{
				scrollRect.horizontalNormalizedPosition = target_horizontalNormalizedPosition;
				if (scrollRect.horizontalNormalizedPosition == target_horizontalNormalizedPosition)
					target_horizontalNormalizedPosition = default_normalizedPosition;
			}
		}
	}
}