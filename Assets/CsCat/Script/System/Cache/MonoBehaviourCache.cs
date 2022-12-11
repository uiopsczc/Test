
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

		protected MonoBehaviour _owner;

		#endregion



		#region property

		//与Component中的过时组件对应
		public GameObject gameObject => dict.GetOrAddDefault2(typeof(GameObject), () => _owner.gameObject);

		public Rigidbody rigidbody => dict.GetOrAddDefault2(typeof(Rigidbody), () => _owner.GetComponent<Rigidbody>());

		public Rigidbody2D rigidbody2D => dict.GetOrAddDefault2(typeof(Rigidbody2D), () => _owner.GetComponent<Rigidbody2D>());

		public Camera camera => dict.GetOrAddDefault2(typeof(Camera), () => _owner.GetComponent<Camera>());

		public Light light => dict.GetOrAddDefault2(typeof(Light), () => _owner.GetComponent<Light>());

		public Animation animation => dict.GetOrAddDefault2(typeof(Animation), () => _owner.GetComponent<Animation>());

		public ConstantForce constantForce => dict.GetOrAddDefault2(typeof(ConstantForce), () => _owner.GetComponent<ConstantForce>());

		public Renderer renderer => dict.GetOrAddDefault2(typeof(Renderer), () => _owner.GetComponent<Renderer>());

		public AudioSource audio => dict.GetOrAddDefault2(typeof(AudioSource), () => _owner.GetComponent<AudioSource>());

		//  public GUIElement guiElement { get { return dict.GetOrAddDefault(typeof(GUIElement), () => { return owner.GetComponent<GUIElement>(); }); } }
		public Collider collider => dict.GetOrAddDefault2(typeof(Collider), () => _owner.GetComponent<Collider>());

		public Collider2D collider2D => dict.GetOrAddDefault2(typeof(Collider2D), () => _owner.GetComponent<Collider2D>());

		public HingeJoint hingeJoint => dict.GetOrAddDefault2(typeof(HingeJoint), () => _owner.GetComponent<HingeJoint>());

		public Transform transform => dict.GetOrAddDefault2(typeof(Transform), () => _owner.GetComponent<Transform>());

		public ParticleSystem particleSystem => dict.GetOrAddDefault2(typeof(ParticleSystem), () => _owner.GetComponent<ParticleSystem>());

		public RectTransform rectTransform => dict.GetOrAddDefault2(typeof(RectTransform), () => _owner.GetComponent<RectTransform>());

		public Animator animator => dict.GetOrAddDefault2(typeof(Animator), () => _owner.GetComponent<Animator>());

		public BoxCollider boxCollider => dict.GetOrAddDefault2(typeof(BoxCollider), () => _owner.GetComponent<BoxCollider>());

		public SpriteRenderer spriteRenderer => dict.GetOrAddDefault2(typeof(SpriteRenderer), () => _owner.GetComponent<SpriteRenderer>());

		#endregion

		#region ctor

		public MonoBehaviourCache(MonoBehaviour owner)
		{
			this._owner = owner;
		}

		#endregion



	}
}