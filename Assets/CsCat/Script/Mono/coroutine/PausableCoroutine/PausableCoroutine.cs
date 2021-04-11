using System.Collections;

namespace CsCat
{
  public class PausableCoroutine
  {
    public ICoroutineYield current_yield = new YieldDefault();
    public IEnumerator routine;
    public string routine_unique_hash;
    public string owner_unique_hash;
    public string method_name = "";

    public int owner_hash;
    public string owner_type;

    public bool is_finished = false;
    public bool is_paused = false;

    public PausableCoroutine(IEnumerator routine, int owner_hash, string owner_type)
    {
      this.routine = routine;
      this.owner_hash = owner_hash;
      this.owner_type = owner_type;
      owner_unique_hash = owner_hash + "_" + owner_type;

      if (routine != null)
      {
        string[] split = routine.ToString().Split('<', '>');
        if (split.Length == 3)
          this.method_name = split[1];
      }

      routine_unique_hash = owner_hash + "_" + owner_type + "_" + method_name;
    }

    public PausableCoroutine(string methodName, int owner_hash, string owner_type)
    {
      method_name = methodName;
      this.owner_hash = owner_hash;
      this.owner_type = owner_type;
      owner_unique_hash = owner_hash + "_" + owner_type;
      routine_unique_hash = owner_hash + "_" + owner_type + "_" + method_name;
    }

    public void SetIsPaused(bool is_paused)
    {
      this.is_paused = is_paused;
    }
  }
}