using UnityEngine;

namespace CsCat
{
  public partial class GameObjectLoader : MonoBehaviour, ISingleton
  {
    public static GameObjectLoader instance
    {
      get { return SingletonFactory.instance.GetMono<GameObjectLoader>(); }
    }

    [SerializeField] public TextAsset textAsset;


    private ResLoad resLoad = new ResLoad();

    public void Clear()
    {
      gameObject.DestroyChildren();
      resLoad.Destroy();
    }




  }

}


