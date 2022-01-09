using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class AnimatorComp
	{
		public string cur_animation_name;

		public Dictionary<Animator, Dictionary<string, AnimatorParameterInfo>> animators_parameterInfo_dict =
		  new Dictionary<Animator, Dictionary<string, AnimatorParameterInfo>>();

		public void Destroy()
		{
			animators_parameterInfo_dict.Clear();
			cur_animation_name = null;
		}


		public void OnBuild()
		{

		}

		public void OnBuildOk(GameObject gameObject)
		{
			var animators = gameObject.GetComponentsInChildren<Animator>();
			foreach (var animator in animators)
				this.SaveAnimator(animator);
		}

		public void SaveAnimator(Animator animator)
		{
			var parameters = animator.parameters;
			var animatorParameterInfo_dict = new Dictionary<string, AnimatorParameterInfo>();
			foreach (var parameter in parameters)
				animatorParameterInfo_dict[parameter.name] = new AnimatorParameterInfo(animator, parameter);
			this.animators_parameterInfo_dict[animator] = animatorParameterInfo_dict;
		}

		public void PlayAnimation(string animation_name, object parameter_value = null, float speed = 1)
		{
			if ("die".Equals(this.cur_animation_name))
				return;
			bool is_changed = false;
			foreach (var animator in animators_parameterInfo_dict.Keys)
			{
				var animatorParameterInfo_dict = animators_parameterInfo_dict[animator];
				//停掉上一个动画
				if (!this.cur_animation_name.IsNullOrWhiteSpace()
					&& animatorParameterInfo_dict.ContainsKey(cur_animation_name)
					&& animatorParameterInfo_dict[cur_animation_name].animatorControllerParameterType ==
					AnimatorControllerParameterType.Bool)
					animatorParameterInfo_dict[this.cur_animation_name].SetValue(false);
				//设置更改的动画
				if (animatorParameterInfo_dict.ContainsKey(animation_name))
				{
					animator.speed = speed;
					animatorParameterInfo_dict[animation_name].SetValue(parameter_value);
					is_changed = true;
				}

				if (is_changed)
					this.cur_animation_name = animation_name;
			}
		}
	}
}