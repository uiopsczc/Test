using UnityEngine.SceneManagement;

namespace CsCat
{
  public class SceneUtil
  {
    public static bool IsScene(string sceneName)
    {
        return SceneManager.GetActiveScene().name.Equals(sceneName);
    }

    public static void SetActiveSceneByPath(string path)
    {
      var sceneByPath = SceneManager.GetSceneByPath(path);
      if (sceneByPath.isLoaded)
        SceneManager.SetActiveScene(sceneByPath);
    }
  }
}