using System.Collections;

namespace CsCat
{
	/// <summary>
	/// 状态
	/// </summary>
	public class CoroutineHFSMState : HFSMState
	{
		public virtual IEnumerator IEEnter(params object[] args)
		{
			base.Enter(args);
			yield break;
		}

		public virtual IEnumerator IEExit(params object[] args)
		{
			base.Exit(args);
			yield break;
		}

		public virtual IEnumerator IEExitLoopTo(CoroutineHFSM toHFSM, params object[] args)
		{
			yield return IEExit();
			var hfsm = parentHFSM as CoroutineHFSM;
			while (hfsm != toHFSM)
			{
				yield return hfsm.IEExit(args);
				hfsm = hfsm.parentHFSM as CoroutineHFSM;
			}
		}
	}
}