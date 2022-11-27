using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class TextProxy
	{
		private Text _textComponent;
		private Outline _outlineComponent;
		private string _text;
		private Color? _color;
		private Color? _outlineColor;

		public void ApplyToText(Text textComponent)
		{
			this._textComponent = textComponent;
			this._outlineComponent = textComponent.GetComponent<Outline>();
			textComponent.text = _text;

			if (_color.HasValue)
				textComponent.color = _color.Value;
			else
				_color = textComponent.color;

			if (_outlineComponent != null)
			{
				if (_outlineColor.HasValue)
					_outlineComponent.effectColor = _outlineColor.Value;
				else
					_outlineColor = _outlineComponent.effectColor;
			}
		}

		public void SetText(string text)
		{
			this._text = text;
			if (this._textComponent)
				_textComponent.text = text;
		}

		public string GetText()
		{
			return this._text;
		}

		public void SetColor(Color color)
		{
			this._color = color;
			if (this._textComponent)
				_textComponent.color = color;
		}

		public Color GetColor()
		{
			return this._color.Value;
		}

		public void SetOutlineColor(Color outlineColor)
		{
			this._outlineColor = outlineColor;
			if (this._outlineComponent)
				_outlineComponent.effectColor = outlineColor;
		}

		public Color GetOutlineColor()
		{
			return this._outlineColor.Value;
		}


		public void Reset()
		{
			this._textComponent = null;
			this._outlineComponent = null;
			this._text = null;
			this._color = null;
			this._outlineColor = null;
		}
	}
}