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
		private object _dstPropOwner;

		/// <summary>
		/// dstFieldInfo
		/// </summary>
		private FieldInfo _dstFieldInfo;

		/// <summary>
		/// dstPropInfo
		/// </summary>
		private PropertyInfo _dstPropInfo;

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
			if (this._dstFieldInfo != null)
			{
				this._dstFieldInfo.SetValue(this._dstPropOwner, newValue);
				return;
			}

			if (this._dstPropInfo != null)
				this._dstPropInfo.SetValue(this._dstPropOwner, newValue, null);
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
			this._dstPropOwner = dstPropOwner;
			Type dstType = this._dstPropOwner.GetType();
			this._dstFieldInfo = dstType.GetFieldInfo(dstPropName);
			if (this._dstFieldInfo == null)
				this._dstPropInfo = dstType.GetPropertyInfo(dstPropName);

			base.propBinder.Bind(srcPropOwner, srcPropName, _OnValueChanged);
			return this;
		}

		#endregion
	}
}