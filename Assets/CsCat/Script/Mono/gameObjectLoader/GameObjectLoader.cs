using UnityEngine;

namespace CsCat
{
    public partial class GameObjectLoader : MonoBehaviour, ISingleton
    {
        public static GameObjectLoader instance => SingletonFactory.instance.GetMono<GameObjectLoader>();

        [SerializeField] public TextAsset textAsset;


        private ResLoad resLoad = new ResLoad();

        public void SingleInit()
        {
        }

        public void Clear()
        {
            gameObject.DestroyChildren();
            resLoad.Destroy();
        }
    }
}