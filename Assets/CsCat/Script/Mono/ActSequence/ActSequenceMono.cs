using UnityEngine;

namespace CsCat
{
  public class ActSequenceMono : MonoBehaviour
  {
    private ActSequence _sequence;

    public ActSequence sequence => _sequence ?? (_sequence = new ActSequence(this));


    public void StartActs()
    {
      sequence.Start();
    }

    public void Update()
    {
      sequence.Update();
    }
  }
}