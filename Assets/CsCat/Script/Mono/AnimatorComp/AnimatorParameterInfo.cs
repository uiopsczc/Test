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
			switch (this.animatorControllerParameterType)
			{
				case UnityEngine.AnimatorControllerParameterType.Bool:
					return this.animator.GetBool(this.name);
				case UnityEngine.AnimatorControllerParameterType.Float:
					return this.animator.GetFloat(this.name);
				case UnityEngine.AnimatorControllerParameterType.Int:
					return this.animator.GetInteger(this.name);
				case UnityEngine.AnimatorControllerParameterType.Trigger:
					return null;
				default:
					throw new Exception("no animatorControllerParameterType");
			}
		}

		public void SetValue(object value = null)
		{
			switch (this.animatorControllerParameterType)
			{
				case UnityEngine.AnimatorControllerParameterType.Bool:
					this.animator.SetBool(this.name, value.To<bool>());
					break;
				case UnityEngine.AnimatorControllerParameterType.Float:
					this.animator.SetFloat(this.name, value.To<float>());
					break;
				case UnityEngine.AnimatorControllerParameterType.Int:
					this.animator.SetInteger(this.name, value.To<int>());
					break;
				case UnityEngine.AnimatorControllerParameterType.Trigger:
					this.animator.SetTrigger(this.name);
					break;
				default:
					throw new Exception("no animatorControllerParameterType");
			}

			this.animator.Update(0);
		}

	}
}