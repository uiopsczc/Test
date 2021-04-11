using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  [RequireComponent(typeof(Text))]
  [ExecuteInEditMode]
  public class UITranslation : MonoBehaviour
  {
    private MonoBehaviourCache _monoBehaviourCache;

    public MonoBehaviourCache monoBehaviourCache
    {
      get
      {
        if (_monoBehaviourCache == null) _monoBehaviourCache = new MonoBehaviourCache(this);
        return _monoBehaviourCache;
      }
    }

    private Text text
    {
      get { return monoBehaviourCache.GetOrAddDefault("text", () => this.GetComponent<Text>()); }
    }

    [TextArea] public string translation_id;

    public void Awake()
    {

    }

    void Update()
    {
      if (!Application.isPlaying)
        Refresh();
    }

    public void Refresh()
    {
      if (translation_id != null)
        text.text = Translation.GetText(translation_id);
    }
  }
}