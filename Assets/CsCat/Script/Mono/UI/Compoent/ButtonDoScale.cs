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

    public float pressed_scale = 0.9f;
    public float animation_time = 0.1f;
    private Vector3 org_size;

    void Awake()
    {
      this.transform = this.GetComponent<Transform>();
      button = this.GetComponent<Button>();
      org_size = this.transform.localScale;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
      if (button.interactable)
      {
        transform.DOScale(org_size * pressed_scale, animation_time);
      }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
      transform.DOScale(org_size, animation_time);
    }
  }
}