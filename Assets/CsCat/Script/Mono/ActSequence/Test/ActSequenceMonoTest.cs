using System;
using System.Collections;
using UnityEngine;

namespace CsCat
{
  public class ActSequenceMonoTest : MonoBehaviour
  {
    public void Start()
    {
      var actSequenceMono = gameObject.GetOrAddComponent<ActSequenceMono>();
      actSequenceMono.sequence.Coroutine(Co());
      actSequenceMono.sequence.AppendActStart(thisAct => { Debug.LogError("hello"); }, true);
      actSequenceMono.sequence.WaitForSeconds(2);
      actSequenceMono.sequence.AppendActStart(thisAct =>
      {
        Debug.LogError("world");
        thisAct.Exit();
      });
      actSequenceMono.sequence.AppendActStart(thisAct => { StartCoroutine(IEStart(thisAct)); })
        .OnExit(next => { StartCoroutine(IEEnd(next)); });
      actSequenceMono.sequence.AppendActStart(thisAct => { Debug.LogError("world2"); }, true);
      actSequenceMono.sequence.AppendActStart(thisAct => { Debug.LogError("world3"); }, true);
      actSequenceMono.StartActs();
    }

    private IEnumerator Co()
    {
      Debug.LogError("e0");
      yield return new WaitForSeconds(1);
      Debug.LogError("e1");
      yield return new WaitForSeconds(1);
      Debug.LogError("e2");
      yield return new WaitForSeconds(1);
      Debug.LogError("e3");
      yield return new WaitForSeconds(1);
    }


    private IEnumerator IEStart(Act act)
    {
      Debug.LogError("start");
      yield return new WaitForSeconds(3);
      act.Exit();
    }

    private IEnumerator IEEnd(Action next)
    {
      Debug.LogError("end");
      yield return new WaitForSeconds(3);
      next();
    }
  }
}