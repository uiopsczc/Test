using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CsCat
{
	[RequireComponent(typeof(Button))]
	public class ButtonDoScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		private Button button;
		private new Transform transform;

		public float pressedScale = 0.9f;
		public float animationTime = 0.1f;
		private Vector3 orgSize;

		void Awake()
		{
			this.transform = this.GetComponent<Transform>();
			button = this.GetComponent<Button>();
			orgSize = this.transform.localScale;
		}

		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (button.interactable)
			{
				transform.DOScale(orgSize * pressedScale, animationTime);
			}
		}

		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			transform.DOScale(orgSize, animationTime);
		}
	}
}