using System.Collections;

namespace CsCat
{
	public class PausableCoroutine
	{
		public ICoroutineYield currentYield = new YieldDefault();
		public IEnumerator routine;
		public string routineUniqueHash;
		public string ownerUniqueHash;
		public string methodName = "";

		public int ownerHashCode;
		public string ownerType;

		public bool isFinished = false;
		public bool isPaused = false;

		public PausableCoroutine(IEnumerator routine, int ownerHashCode, string ownerType)
		{
			this.routine = routine;
			this.ownerHashCode = ownerHashCode;
			this.ownerType = ownerType;
			ownerUniqueHash = ownerHashCode + "_" + ownerType;

			if (routine != null)
			{
				string[] split = routine.ToString().Split('<', '>');
				if (split.Length == 3)
					this.methodName = split[1];
			}

			routineUniqueHash = ownerHashCode + "_" + ownerType + "_" + methodName;
		}

		public PausableCoroutine(string methodName, int ownerHashCode, string ownerType)
		{
			this.methodName = methodName;
			this.ownerHashCode = ownerHashCode;
			this.ownerType = ownerType;
			ownerUniqueHash = ownerHashCode + "_" + ownerType;
			routineUniqueHash = ownerHashCode + "_" + ownerType + "_" + this.methodName;
		}

		public void SetIsPaused(bool isPaused)
		{
			this.isPaused = isPaused;
		}
	}
}