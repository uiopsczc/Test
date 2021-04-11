using UnityEngine;
using System.Collections;

namespace CsCat
{
  public class CoroutineUtil
  {

    public static IEnumerator WaitForSeconds(float seconds)
    {
      float passed_seconds = 0;
      if (seconds > 0)
      {
        while (true)
        {
          //while (GameData.instance.gameState == GameState.Pause)
          //    yield return 0;

          passed_seconds += Time.deltaTime;
          if (passed_seconds >= seconds)
            yield break;
          else
            yield return 0;
        }
      }
      else
      {
        //while (GameData.instance.gameState == GameState.Pause)
        //    yield return 0;

        yield break;
      }
    }

  }
}
