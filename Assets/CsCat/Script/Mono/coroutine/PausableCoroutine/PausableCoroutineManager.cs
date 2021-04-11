using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using System;
using System.Reflection;

namespace CsCat
{
  //https://github.com/marijnz/unity-editor-coroutines/edit/master/Assets/EditorCoroutines/Editor/EditorCoroutines.cs
  //不仅可以在运行模式下运行，还可以在Editor模式中运行
  public class PausableCoroutineManager : MonoBehaviour, ISingleton
  {
    Dictionary<string, List<PausableCoroutine>> coroutine_dict = new Dictionary<string, List<PausableCoroutine>>();
    List<List<PausableCoroutine>> temp_coroutine_list = new List<List<PausableCoroutine>>();
    Dictionary<string, Dictionary<string, PausableCoroutine>> coroutine_owner_dict =
      new Dictionary<string, Dictionary<string, PausableCoroutine>>();
    float previousTimeSinceStartup;
    private bool is_inited = false;
    private bool is_paused = false;


    public static PausableCoroutineManager instance
    {
      get
      {
        var result = SingletonFactory.instance.GetMono<PausableCoroutineManager>();
        result.Init();
        return result;
      }
    }

    void Init()
    {
      if (is_inited)
        return;
      previousTimeSinceStartup = Time.realtimeSinceStartup;
#if UNITY_EDITOR
      EditorApplication.update -= Update;
      if (!EditorApplication.isPlayingOrWillChangePlaymode)
      {
        EditorApplication.update += Update;
      }
#endif
      is_inited = true;
    }


    public void SetIsPaused(bool is_paused)
    {
      this.is_paused = is_paused;
    }

    /// <summary>Starts a coroutine.</summary>
    /// <param name="routine">The coroutine to start.</param>
    /// <param name="this_reference">Reference to the instance of the class containing the method.</param>
    public PausableCoroutine StartCoroutine(IEnumerator routine, object this_reference)
    {
      return instance.GoStartCoroutine(routine, this_reference);
    }

    /// <summary>Starts a coroutine.</summary>
    /// <param name="method_name">The name of the coroutine method to start.</param>
    /// <param name="this_reference">Reference to the instance of the class containing the method.</param>
    public new PausableCoroutine StartCoroutine(string method_name, object this_reference)
    {
      return StartCoroutine(method_name, null, this_reference);
    }

    /// <summary>Starts a coroutine.</summary>
    /// <param name="method_name">The name of the coroutine method to start.</param>
    /// <param name="value">The parameter to pass to the coroutine.</param>
    /// <param name="this_reference">Reference to the instance of the class containing the method.</param>
    public PausableCoroutine StartCoroutine(string method_name, object value, object this_reference)
    {
      MethodInfo methodInfo = this_reference.GetType()
        .GetMethodInfo2(method_name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      if (methodInfo == null)
        Debug.LogError("Coroutine '" + method_name + "' couldn't be started, the method doesn't exist!");

      object returnValue;

      returnValue = methodInfo.Invoke(this_reference, value == null ? null : new[] {value});

      if (returnValue is IEnumerator enumerator)
        return instance.GoStartCoroutine(enumerator, this_reference);

      Debug.LogError("Coroutine '" + method_name + "' couldn't be started, the method doesn't return an IEnumerator!");

      return null;
    }

    /// <summary>Stops all coroutines being the routine running on the passed instance.</summary>
    /// <param name="routine"> The coroutine to stop.</param>
    /// <param name="this_reference">Reference to the instance of the class containing the method.</param>
    public void StopCoroutine(IEnumerator routine, object this_reference)
    {
      instance.GoStopCoroutine(routine, this_reference);
    }

    /// <summary>
    /// Stops all coroutines named methodName running on the passed instance.</summary>
    /// <param name="method_name"> The name of the coroutine method to stop.</param>
    /// <param name="this_reference">Reference to the instance of the class containing the method.</param>
    public void StopCoroutine(string method_name, object this_reference)
    {
      instance.GoStopCoroutine(method_name, this_reference);
    }

    /// <summary>
    /// Stops all coroutines running on the passed instance.</summary>
    /// <param name="this_reference">Reference to the instance of the class containing the method.</param>
    public void StopAllCoroutines(object this_reference)
    {
      instance.GoStopAllCoroutines(this_reference);
    }

    //////////////////////////////////////////////////////////////////////
    // 私有方法
    //////////////////////////////////////////////////////////////////////
    void GoStopCoroutine(IEnumerator routine, object this_reference)
    {
      GoStopActualRoutine(CreateCoroutine(routine, this_reference));
    }

    void GoStopCoroutine(string methodName, object this_reference)
    {
      GoStopActualRoutine(CreateCoroutineFromString(methodName, this_reference));
    }

    void GoStopActualRoutine(PausableCoroutine routine)
    {
      if (!coroutine_dict.ContainsKey(routine.routine_unique_hash)) return;
      coroutine_owner_dict[routine.owner_unique_hash].Remove(routine.routine_unique_hash);
      coroutine_dict.Remove(routine.routine_unique_hash);
    }

    void GoStopAllCoroutines(object this_reference)
    {
      PausableCoroutine coroutine = CreateCoroutine(null, this_reference);
      if (!coroutine_owner_dict.ContainsKey(coroutine.owner_unique_hash)) return;
      foreach (var couple in coroutine_owner_dict[coroutine.owner_unique_hash])
        coroutine_dict.Remove(couple.Value.routine_unique_hash);

      coroutine_owner_dict.Remove(coroutine.owner_unique_hash);
    }

    PausableCoroutine GoStartCoroutine(IEnumerator routine, object this_reference)
    {
      if (routine == null)
        Debug.LogException(new Exception("IEnumerator is null!"), null);

      PausableCoroutine coroutine = CreateCoroutine(routine, this_reference);
      GoStartCoroutine(coroutine);
      return coroutine;
    }

    void GoStartCoroutine(PausableCoroutine coroutine)
    {
      if (!coroutine_dict.ContainsKey(coroutine.routine_unique_hash))
      {
        List<PausableCoroutine> new_coroutine_list = new List<PausableCoroutine>();
        coroutine_dict.Add(coroutine.routine_unique_hash, new_coroutine_list);
      }

      coroutine_dict[coroutine.routine_unique_hash].Add(coroutine);

      if (!coroutine_owner_dict.ContainsKey(coroutine.owner_unique_hash))
      {
        Dictionary<string, PausableCoroutine> new_coroutine_dict = new Dictionary<string, PausableCoroutine>();
        coroutine_owner_dict.Add(coroutine.owner_unique_hash, new_coroutine_dict);
      }

      // If the method from the same owner has been stored before, it doesn't have to be stored anymore,
      // One reference is enough in order for "StopAllCoroutines" to work
      if (!coroutine_owner_dict[coroutine.owner_unique_hash].ContainsKey(coroutine.routine_unique_hash))
      {
        coroutine_owner_dict[coroutine.owner_unique_hash].Add(coroutine.routine_unique_hash, coroutine);
      }

      MoveNext(coroutine);
    }

    PausableCoroutine CreateCoroutine(IEnumerator routine, object this_reference)
    {
      return new PausableCoroutine(routine, this_reference.GetHashCode(), this_reference.GetType().ToString());
    }

    PausableCoroutine CreateCoroutineFromString(string method_name, object this_reference)
    {
      return new PausableCoroutine(method_name, this_reference.GetHashCode(), this_reference.GetType().ToString());
    }

    void Update()
    {
//      LogCat.log("ggggg");
      float deltaTime = Time.deltaTime;
#if UNITY_EDITOR
      if (!EditorApplication.isPlayingOrWillChangePlaymode)
      {
        deltaTime = Time.realtimeSinceStartup - previousTimeSinceStartup;
        previousTimeSinceStartup = Time.realtimeSinceStartup;
      }
#endif
      if (this.is_paused)
        return;
      if (deltaTime == 0f)
        return;
      if (coroutine_dict.Count == 0)
      {
        return;
      }

      temp_coroutine_list.Clear();
      foreach (var pair in coroutine_dict)
        temp_coroutine_list.Add(pair.Value);

      for (var i = temp_coroutine_list.Count - 1; i >= 0; i--)
      {
        List<PausableCoroutine> coroutines = temp_coroutine_list[i];

        for (int j = coroutines.Count - 1; j >= 0; j--)
        {
          PausableCoroutine coroutine = coroutines[j];

          if (coroutine.is_paused)
          {
            continue;
          }

          if (!coroutine.current_yield.IsDone(deltaTime))
          {
            continue;
          }

          if (!MoveNext(coroutine))
          {
            coroutines.RemoveAt(j);
            coroutine.current_yield = null;
            coroutine.is_finished = true;
          }

          if (coroutines.Count == 0)
          {
            coroutine_dict.Remove(coroutine.owner_unique_hash);
          }
        }
      }
    }

    bool MoveNext(PausableCoroutine coroutine)
    {
      if (coroutine.routine.MoveNext())
      {
        return Process(coroutine);
      }

      return false;
    }

    // returns false if no next, returns true if OK
    bool Process(PausableCoroutine coroutine)
    {
      object current = coroutine.routine.Current;
      if (current == null)
      {
        coroutine.current_yield = new YieldDefault();
      }
      else if (current is WaitForSeconds)
      {
        coroutine.current_yield = new YieldWaitForSeconds(current.GetFieldValue<float>("m_Seconds"));
      }
      else if (current is CustomYieldInstruction customYield)
      {
        coroutine.current_yield = new YieldCustomYieldInstruction(customYield);
      }
      else if (current is WWW)
      {
        coroutine.current_yield = new YieldWWW((WWW)current);
      }
      else if (current is WaitForFixedUpdate || current is WaitForEndOfFrame)
      {
        coroutine.current_yield = new YieldDefault();
      }
      else if (current is AsyncOperation asyncOperation)
      {
        coroutine.current_yield = new YieldAsync(asyncOperation);
      }
      else if (current is PausableCoroutine co)
      {
        coroutine.current_yield = new YieldNestedCoroutine(co);
      }
      else
      {
        Debug.LogException(
          new Exception("<" + coroutine.method_name + "> yielded an unknown or unsupported type! (" + current.GetType() +
                        ")"),
          null);
        coroutine.current_yield = new YieldDefault();
      }

      return true;
    }

    void OnApplicationQuit()
    {
      is_inited = false;
    }
  }
}