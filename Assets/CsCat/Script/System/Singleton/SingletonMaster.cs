using UnityEngine;
using UnityEngine.Audio;

namespace CsCat
{
  public class SingletonMaster : MonoBehaviour, ISingleton
  {
    public AudioMixer audioMixer;
    public GameObject[] inActive_gameObjects;
    public static SingletonMaster instance => SingletonFactory.instance.GetMono<SingletonMaster>();

    private void Awake()
    {
      DontDestroyOnLoad(gameObject);
    }
  }
}