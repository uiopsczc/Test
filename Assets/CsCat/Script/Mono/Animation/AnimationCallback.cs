using UnityEngine;
using UnityEngine.Events;

namespace CsCat
{
  public class AnimationCallback : MonoBehaviour
  {
    public UnityEvent myEvent;

    public void DestroyOnFinish()
    {
      gameObject.Destroy();
    }

    public void DeactiveOnFinish()
    {
      gameObject.SetActive(false);
    }

    public void DespawnOnFinish()
    {
      gameObject.Despawn();
    }

    public void OnFinish()
    {
      myEvent?.Invoke();
    }
  }
}