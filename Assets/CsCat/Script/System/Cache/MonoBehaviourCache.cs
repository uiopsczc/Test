
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
		public GameObject gameObject => dict.GetOrAddDefault2(typeof(GameObject), () => owner.gameObject);

		public Rigidbody rigidbody => dict.GetOrAddDefault2(typeof(Rigidbody), () => owner.GetComponent<Rigidbody>());

		public Rigidbody2D rigidbody2D => dict.GetOrAddDefault2(typeof(Rigidbody2D), () => owner.GetComponent<Rigidbody2D>());

		public Camera camera => dict.GetOrAddDefault2(typeof(Camera), () => owner.GetComponent<Camera>());

		public Light light => dict.GetOrAddDefault2(typeof(Light), () => owner.GetComponent<Light>());

		public Animation animation => dict.GetOrAddDefault2(typeof(Animation), () => owner.GetComponent<Animation>());

		public ConstantForce constantForce => dict.GetOrAddDefault2(typeof(ConstantForce), () => owner.GetComponent<ConstantForce>());

		public Renderer renderer => dict.GetOrAddDefault2(typeof(Renderer), () => owner.GetComponent<Renderer>());

		public AudioSource audio => dict.GetOrAddDefault2(typeof(AudioSource), () => owner.GetComponent<AudioSource>());

		//  public GUIElement guiElement { get { return dict.GetOrAddDefault(typeof(GUIElement), () => { return owner.GetComponent<GUIElement>(); }); } }
		public Collider collider => dict.GetOrAddDefault2(typeof(Collider), () => owner.GetComponent<Collider>());

		public Collider2D collider2D => dict.GetOrAddDefault2(typeof(Collider2D), () => owner.GetComponent<Collider2D>());

		public HingeJoint hingeJoint => dict.GetOrAddDefault2(typeof(HingeJoint), () => owner.GetComponent<HingeJoint>());

		public Transform transform => dict.GetOrAddDefault2(typeof(Transform), () => owner.GetComponent<Transform>());

		public ParticleSystem particleSystem => dict.GetOrAddDefault2(typeof(ParticleSystem), () => owner.GetComponent<ParticleSystem>());

		public RectTransform rectTransform => dict.GetOrAddDefault2(typeof(RectTransform), () => owner.GetComponent<RectTransform>());

		public Animator animator => dict.GetOrAddDefault2(typeof(Animator), () => owner.GetComponent<Animator>());

		public BoxCollider boxCollider => dict.GetOrAddDefault2(typeof(BoxCollider), () => owner.GetComponent<BoxCollider>());

		public SpriteRenderer spriteRenderer => dict.GetOrAddDefault2(typeof(SpriteRenderer), () => owner.GetComponent<SpriteRenderer>());

		#endregion

		#region ctor

		public MonoBehaviourCache(MonoBehaviour owner)
		{
			this.owner = owner;
		}

		#endregion



	}
}