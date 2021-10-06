using UnityEngine;
using UnityEngine.Audio;

namespace CsCat
{
    public class SingletonMaster : MonoBehaviour, ISingleton
    {
        public AudioMixer audioMixer;
        public GameObject[] inActiveGameObjects;
        public static SingletonMaster instance => SingletonFactory.instance.GetMono<SingletonMaster>();

        public void SingleInit()
        {
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}