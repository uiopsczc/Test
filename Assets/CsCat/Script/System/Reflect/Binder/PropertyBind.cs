using System;
using System.Reflection;

namespace CsCat
{
	/// <summary>
	/// 当绑定的srcPropOwner的srcPropName属性的值改变的时候，
	/// dstPropOwner的dstPropName属性的值也设置为跟srcPropOwner的srcPropName属性一样的值
	/// </summary>
	public class PropertyBind : BaseBind
	{
		#region field

		/// <summary>
		/// dstPropOwner
		/// </summary>
		private object dstPropOwner;

		/// <summary>
		/// dstFieldInfo
		/// </summary>
		private FieldInfo dstFieldInfo;

		/// <summary>
		/// dstPropInfo
		/// </summary>
		private PropertyInfo dstPropInfo;

		#endregion

		#region virtual method

		/// <summary>
		/// 当srcPropOwner的srcPropName属性的值改变的时候，
		/// dstPropOwner的dstPropName属性的值也设置为跟srcPropOwner的srcPropName属性一样的值
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="oldValue"></param>
		/// <param name="newValue"></param>
		internal override void _OnValueChanged(string propertyName, object oldValue, object newValue)
		{
			if (this.dstFieldInfo != null)
			{
				this.dstFieldInfo.SetValue(this.dstPropOwner, newValue);
				return;
			}

			if (this.dstPropInfo != null)
				this.dstPropInfo.SetValue(this.dstPropOwner, newValue, null);
		}

		/// <summary>
		/// 当srcPropOwner的srcPropName属性的值改变的时候，
		/// dstPropOwner的dstPropName属性的值也设置为跟srcPropOwner的srcPropName属性一样的值
		/// </summary>
		/// <param name="srcPropOwner"></param>
		/// <param name="srcPropName"></param>
		/// <param name="dstPropOwner"></param>
		/// <param name="dstPropName"></param>
		/// <returns></returns>
		public virtual BaseBind Bind(object srcPropOwner, string srcPropName, object dstPropOwner,
			string dstPropName)
		{
			this.dstPropOwner = dstPropOwner;
			Type dstType = this.dstPropOwner.GetType();
			this.dstFieldInfo = dstType.GetFieldInfo(dstPropName);
			if (this.dstFieldInfo == null)
				this.dstPropInfo = dstType.GetPropertyInfo(dstPropName);

			base.propBinder.Bind(srcPropOwner, srcPropName, _OnValueChanged);
			return this;
		}

		#endregion
	}
}