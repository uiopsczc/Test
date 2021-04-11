using UnityEngine.SceneManagement;

namespace CsCat
{
  public class SceneUtil
  {
    public static bool IsScene(string scene_name)
    {
      if (SceneManager.GetActiveScene().name == scene_name)
        return true;
      return false;
    }

    public static void SetActiveSceneByPath(string path)
    {
      var scene_by_path = SceneManager.GetSceneByPath(path);
      if (scene_by_path.isLoaded)
        SceneManager.SetActiveScene(scene_by_path);
    }
  }
}