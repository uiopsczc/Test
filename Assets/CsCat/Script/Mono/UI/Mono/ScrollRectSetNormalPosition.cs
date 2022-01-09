using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	[RequireComponent(typeof(ScrollRect))]
	public class ScrollRectSetNormalPosition : MonoBehaviour
	{
		private ScrollRect scrollRect;

		private const float defaultNormalizedPosition = -1;
		public float targetVerticalNormalizedPosition = defaultNormalizedPosition;
		public float targetHorizontalNormalizedPosition = defaultNormalizedPosition;

		public void Awake()
		{
			scrollRect = this.GetComponent<ScrollRect>();
		}

		void Update()
		{
			if (targetVerticalNormalizedPosition != defaultNormalizedPosition)
			{
				scrollRect.verticalNormalizedPosition = targetVerticalNormalizedPosition;
				if (scrollRect.verticalNormalizedPosition == targetVerticalNormalizedPosition)
					targetVerticalNormalizedPosition = defaultNormalizedPosition;
			}

			if (targetHorizontalNormalizedPosition != defaultNormalizedPosition)
			{
				scrollRect.horizontalNormalizedPosition = targetHorizontalNormalizedPosition;
				if (scrollRect.horizontalNormalizedPosition == targetHorizontalNormalizedPosition)
					targetHorizontalNormalizedPosition = defaultNormalizedPosition;
			}
		}
	}
}