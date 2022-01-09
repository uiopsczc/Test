using System;
using UnityEngine;

namespace CsCat
{
	public class AnimatorParameterInfo
	{
		public Animator animator;
		public AnimatorControllerParameter animatorControllerParameter;
		public string name;
		public object value;
		public AnimatorControllerParameterType animatorControllerParameterType;

		public AnimatorParameterInfo(Animator animator, AnimatorControllerParameter animatorControllerParameter)
		{
			this.animator = animator;
			this.animatorControllerParameter = animatorControllerParameter;
			this.name = this.animatorControllerParameter.name;
			this.value = this.GetValue();
			this.animatorControllerParameterType = animatorControllerParameter.type;
		}

		public object GetValue()
		{
			if (this.animatorControllerParameterType == UnityEngine.AnimatorControllerParameterType.Bool)
				return this.animator.GetBool(this.name);
			else if (this.animatorControllerParameterType == UnityEngine.AnimatorControllerParameterType.Float)
				return this.animator.GetFloat(this.name);
			else if (this.animatorControllerParameterType == UnityEngine.AnimatorControllerParameterType.Int)
				return this.animator.GetInteger(this.name);
			else if (this.animatorControllerParameterType == UnityEngine.AnimatorControllerParameterType.Trigger)
				return null;
			else
				throw new Exception("no animatorControllerParameterType");
		}

		public void SetValue(object value = null)
		{
			if (this.animatorControllerParameterType == UnityEngine.AnimatorControllerParameterType.Bool)
				this.animator.SetBool(this.name, value.To<bool>());
			else if (this.animatorControllerParameterType == UnityEngine.AnimatorControllerParameterType.Float)
				this.animator.SetFloat(this.name, value.To<float>());
			else if (this.animatorControllerParameterType == UnityEngine.AnimatorControllerParameterType.Int)
				this.animator.SetInteger(this.name, value.To<int>());
			else if (this.animatorControllerParameterType == UnityEngine.AnimatorControllerParameterType.Trigger)
				this.animator.SetTrigger(this.name);
			else
				throw new Exception("no animatorControllerParameterType");
			this.animator.Update(0);
		}

	}
}