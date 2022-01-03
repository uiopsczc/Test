using System;
using UnityEngine;

namespace CsCat
{
	public class GUIStyleMarginScope : IDisposable
	{
		private readonly RectOffset _margin; //边缘，在GUILayout类函数下起作用，和其他控件的距离
		private readonly RectOffset _overflow; //溢出区域，也就是在margin(和其他控件的距离)固定的情况下，背景部分再画多出去多少
		private readonly RectOffset _padding; //内容和控件大小(也就是背景)的距离
		private readonly GUIStyle _guiStyle;

		public GUIStyleMarginScope(GUIStyle guiStyle, RectOffset margin) : this(guiStyle, margin, guiStyle.padding,
			guiStyle.overflow)
		{
		}

		public GUIStyleMarginScope(GUIStyle guiStyle, RectOffset margin, RectOffset padding) : this(guiStyle, margin,
			padding,
			guiStyle.overflow)
		{
		}

		public GUIStyleMarginScope(GUIStyle guiStyle, RectOffset margin, RectOffset padding, RectOffset overflow)
		{
			this._guiStyle = guiStyle;
			this._margin = margin;
			this._padding = padding;
			this._overflow = overflow;
			guiStyle.margin = this._margin;
			guiStyle.padding = this._padding;
			guiStyle.overflow = this._overflow;
		}

		public void Dispose()
		{
			_guiStyle.margin = _margin;
			_guiStyle.padding = _padding;
			_guiStyle.overflow = _overflow;
		}
	}
}