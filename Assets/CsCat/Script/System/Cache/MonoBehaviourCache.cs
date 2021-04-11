
using UnityEngine;

namespace CsCat
{
  /// <summary>
  /// Mono的缓存数据
  /// 在对应的MonoBehaviour中需要有这个属性
  ///  private MonoBehaviourCache _MonoBehaviourCache;
  ///  public MonoBehaviourCache MonoBehaviourCache { get { if (_MonoBehaviourCache == null) _MonoBehaviourCache = new MonoBehaviourCache(this); return _MonoBehaviourCache; } }
  ///  </summary>
  public class MonoBehaviourCache : Cache
  {
    #region field

    protected MonoBehaviour owner;

    #endregion



    #region property

    //与Component中的过时组件对应
    public GameObject gameObject
    {
      get { return dict.GetOrAddDefault(typeof(GameObject), () => { return owner.gameObject; }); }
    }

    public Rigidbody rigidbody
    {
      get { return dict.GetOrAddDefault(typeof(Rigidbody), () => { return owner.GetComponent<Rigidbody>(); }); }
    }

    public Rigidbody2D rigidbody2D
    {
      get { return dict.GetOrAddDefault(typeof(Rigidbody2D), () => { return owner.GetComponent<Rigidbody2D>(); }); }
    }

    public Camera camera
    {
      get { return dict.GetOrAddDefault(typeof(Camera), () => { return owner.GetComponent<Camera>(); }); }
    }

    public Light light
    {
      get { return dict.GetOrAddDefault(typeof(Light), () => { return owner.GetComponent<Light>(); }); }
    }

    public Animation animation
    {
      get { return dict.GetOrAddDefault(typeof(Animation), () => { return owner.GetComponent<Animation>(); }); }
    }

    public ConstantForce constantForce
    {
      get { return dict.GetOrAddDefault(typeof(ConstantForce), () => { return owner.GetComponent<ConstantForce>(); }); }
    }

    public Renderer renderer
    {
      get { return dict.GetOrAddDefault(typeof(Renderer), () => { return owner.GetComponent<Renderer>(); }); }
    }

    public AudioSource audio
    {
      get { return dict.GetOrAddDefault(typeof(AudioSource), () => { return owner.GetComponent<AudioSource>(); }); }
    }

//  public GUIElement guiElement { get { return dict.GetOrAddDefault(typeof(GUIElement), () => { return owner.GetComponent<GUIElement>(); }); } }
    public Collider collider
    {
      get { return dict.GetOrAddDefault(typeof(Collider), () => { return owner.GetComponent<Collider>(); }); }
    }

    public Collider2D collider2D
    {
      get { return dict.GetOrAddDefault(typeof(Collider2D), () => { return owner.GetComponent<Collider2D>(); }); }
    }

    public HingeJoint hingeJoint
    {
      get { return dict.GetOrAddDefault(typeof(HingeJoint), () => { return owner.GetComponent<HingeJoint>(); }); }
    }

    public Transform transform
    {
      get { return dict.GetOrAddDefault(typeof(Transform), () => { return owner.GetComponent<Transform>(); }); }
    }

    public ParticleSystem particleSystem
    {
      get
      {
        return dict.GetOrAddDefault(typeof(ParticleSystem), () => { return owner.GetComponent<ParticleSystem>(); });
      }
    }

    public RectTransform rectTransform
    {
      get { return dict.GetOrAddDefault(typeof(RectTransform), () => { return owner.GetComponent<RectTransform>(); }); }
    }

    public Animator animator
    {
      get { return dict.GetOrAddDefault(typeof(Animator), () => { return owner.GetComponent<Animator>(); }); }
    }

    public BoxCollider boxCollider
    {
      get { return dict.GetOrAddDefault(typeof(BoxCollider), () => { return owner.GetComponent<BoxCollider>(); }); }
    }

    public SpriteRenderer spriteRenderer
    {
      get
      {
        return dict.GetOrAddDefault(typeof(SpriteRenderer), () => { return owner.GetComponent<SpriteRenderer>(); });
      }
    }

    #endregion

    #region ctor

    public MonoBehaviourCache(MonoBehaviour owner)
    {
      this.owner = owner;
    }

    #endregion



  }
}