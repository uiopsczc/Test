using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace CsCat
{
	/// <summary>
	/// This class encodes and decodes JSON strings.
	/// Spec. details, see http://www.json.org/
	///
	/// JSON uses Arrays and Objects. These correspond here to the datatypes ArrayList and Hashtable.
	/// All numbers are parsed to doubles.
	/// </summary>
	public class MiniJson
	{
		#region field

		public const int TOKEN_NONE = 0;
		public const int TOKEN_CURLY_OPEN = 1;
		public const int TOKEN_CURLY_CLOSE = 2;
		public const int TOKEN_SQUARED_OPEN = 3;
		public const int TOKEN_SQUARED_CLOSE = 4;
		public const int TOKEN_COLON = 5;
		public const int TOKEN_COMMA = 6;
		public const int TOKEN_STRING = 7;
		public const int TOKEN_NUMBER = 8;
		public const int TOKEN_TRUE = 9;
		public const int TOKEN_FALSE = 10;
		public const int TOKEN_NULL = 11;



		private const int BUILDER_CAPACITY = 2000;

		#endregion

		#region static method

		#region public

		/// <summary>
		/// Parses the string json into a value
		/// </summary>
		/// <param name="json">A JSON string.</param>
		/// <returns>An ArrayList, a Hashtable, a double, a string, null, true, or false</returns>
		public static object JsonDecode(string json)
		{
			bool success = true;

			return JsonDecode(json, ref success);
		}

		/// <summary>
		/// Parses the string json into a value; and fills 'success' with the successfullness of the parse.
		/// </summary>
		/// <param name="json">A JSON string.</param>
		/// <param name="success">Successful parse?</param>
		/// <returns>An ArrayList, a Hashtable, a double, a string, null, true, or false</returns>
		public static object JsonDecode(string json, ref bool success)
		{
			success = true;
			if (json != null)
			{
				char[] charArray = json.ToCharArray();
				int index = 0;
				object value = _ParseValue(charArray, ref index, ref success);
				return value;
			}

			return null;
		}

		/// <summary>
		/// Converts a Hashtable / ArrayList object into a JSON string
		/// </summary>
		/// <param name="json">A Hashtable / ArrayList</param>
		/// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
		public static string JsonEncode(object json)
		{
			StringBuilder builder = new StringBuilder(BUILDER_CAPACITY);
			bool success = _SerializeValue(json, builder);
			return (success ? builder.ToString() : null);
		}

		#endregion

		#region protected

		protected static Hashtable _ParseObject(char[] json, ref int index, ref bool success)
		{
			Hashtable table = new Hashtable();

			// {
			_NextToken(json, ref index);

			bool done = false;
			while (!done)
			{
				var token = _LookAhead(json, index);
				if (token == MiniJson.TOKEN_NONE)
				{
					success = false;
					return null;
				}

				if (token == MiniJson.TOKEN_COMMA)
					_NextToken(json, ref index);
				else if (token == MiniJson.TOKEN_CURLY_CLOSE)
				{
					_NextToken(json, ref index);
					return table;
				}
				else
				{
					// name
					string name = _ParseString(json, ref index, ref success);
					if (!success)
					{
						success = false;
						return null;
					}

					// :
					token = _NextToken(json, ref index);
					if (token != MiniJson.TOKEN_COLON)
					{
						success = false;
						return null;
					}

					// value
					object value = _ParseValue(json, ref index, ref success);
					if (!success)
					{
						success = false;
						return null;
					}

					table[name] = value;
				}
			}

			return table;
		}

		protected static ArrayList _ParseArray(char[] json, ref int index, ref bool success)
		{
			ArrayList array = new ArrayList();

			// [
			_NextToken(json, ref index);

			bool done = false;
			while (!done)
			{
				int token = _LookAhead(json, index);
				if (token == MiniJson.TOKEN_NONE)
				{
					success = false;
					return null;
				}

				if (token == MiniJson.TOKEN_COMMA)
				{
					_NextToken(json, ref index);
				}
				else if (token == MiniJson.TOKEN_SQUARED_CLOSE)
				{
					_NextToken(json, ref index);
					break;
				}
				else
				{
					object value = _ParseValue(json, ref index, ref success);
					if (!success)
						return null;

					array.Add(value);
				}
			}

			return array;
		}

		protected static object _ParseValue(char[] json, ref int index, ref bool success)
		{
			switch (_LookAhead(json, index))
			{
				case MiniJson.TOKEN_STRING:
					return _ParseString(json, ref index, ref success);
				case MiniJson.TOKEN_NUMBER:
					return _ParseNumber(json, ref index, ref success);
				case MiniJson.TOKEN_CURLY_OPEN:
					return _ParseObject(json, ref index, ref success);
				case MiniJson.TOKEN_SQUARED_OPEN:
					return _ParseArray(json, ref index, ref success);
				case MiniJson.TOKEN_TRUE:
					_NextToken(json, ref index);
					return true;
				case MiniJson.TOKEN_FALSE:
					_NextToken(json, ref index);
					return false;
				case MiniJson.TOKEN_NULL:
					_NextToken(json, ref index);
					return null;
				case MiniJson.TOKEN_NONE:
					break;
			}

			success = false;
			return null;
		}

		protected static string _ParseString(char[] json, ref int index, ref bool success)
		{
			StringBuilder s = new StringBuilder(BUILDER_CAPACITY);

			_EatWhitespace(json, ref index);

			// "
			var c = json[index++];

			bool complete = false;
			while (!complete)
			{
				if (index == json.Length)
					break;

				c = json[index++];
				if (c == '"')
				{
					complete = true;
					break;
				}

				if (c == '\\')
				{
					if (index == json.Length)
						break;

					c = json[index++];
					if (c == '"')
						s.Append('"');
					else if (c == '\\')
						s.Append('\\');
					else if (c == '/')
						s.Append('/');
					else if (c == 'b')
						s.Append('\b');
					else if (c == 'f')
						s.Append('\f');
					else if (c == 'n')
						s.Append('\n');
					else if (c == 'r')
						s.Append('\r');
					else if (c == 't')
						s.Append('\t');
					else if (c == 'u')
					{
						int remainingLength = json.Length - index;
						if (remainingLength >= 4)
						{
							// parse the 32 bit hex into an integer codepoint
							if (!(success = UInt32.TryParse(new string(json, index, 4), NumberStyles.HexNumber,
								CultureInfo.InvariantCulture, out var codePoint)))
								return "";

							// convert the integer codepoint to a unicode char and add to string
							s.Append(Char.ConvertFromUtf32((int) codePoint));
							// skip 4 chars
							index += 4;
						}
						else
							break;
					}
				}
				else
					s.Append(c);
			}

			if (!complete)
			{
				success = false;
				return null;
			}

			return s.ToString();
		}

		protected static double _ParseNumber(char[] json, ref int index, ref bool success)
		{
			_EatWhitespace(json, ref index);

			int lastIndex = _GetLastIndexOfNumber(json, index);
			int charLength = (lastIndex - index) + 1;

			success = Double.TryParse(new string(json, index, charLength), NumberStyles.Any, CultureInfo.InvariantCulture,
			  out var number);

			index = lastIndex + 1;
			return number;
		}

		protected static int _GetLastIndexOfNumber(char[] json, int index)
		{
			int lastIndex;

			for (lastIndex = index; lastIndex < json.Length; lastIndex++)
			{
				if ("0123456789+-.eE".IndexOf(json[lastIndex]) == -1)
					break;
			}

			return lastIndex - 1;
		}

		protected static void _EatWhitespace(char[] json, ref int index)
		{
			for (; index < json.Length; index++)
			{
				if (" \t\n\r".IndexOf(json[index]) == -1)
					break;
			}
		}

		protected static int _LookAhead(char[] json, int index)
		{
			int saveIndex = index;
			return _NextToken(json, ref saveIndex);
		}

		protected static int _NextToken(char[] json, ref int index)
		{
			_EatWhitespace(json, ref index);

			if (index == json.Length)
				return MiniJson.TOKEN_NONE;

			char c = json[index];
			index++;
			switch (c)
			{
				case '{':
					return MiniJson.TOKEN_CURLY_OPEN;
				case '}':
					return MiniJson.TOKEN_CURLY_CLOSE;
				case '[':
					return MiniJson.TOKEN_SQUARED_OPEN;
				case ']':
					return MiniJson.TOKEN_SQUARED_CLOSE;
				case ',':
					return MiniJson.TOKEN_COMMA;
				case '"':
					return MiniJson.TOKEN_STRING;
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case '-':
					return MiniJson.TOKEN_NUMBER;
				case ':':
					return MiniJson.TOKEN_COLON;
			}

			index--;

			int remainingLength = json.Length - index;

			// false
			if (remainingLength >= 5)
			{
				if (json[index] == 'f' &&
					json[index + 1] == 'a' &&
					json[index + 2] == 'l' &&
					json[index + 3] == 's' &&
					json[index + 4] == 'e')
				{
					index += 5;
					return MiniJson.TOKEN_FALSE;
				}
			}

			// true
			if (remainingLength >= 4)
			{
				if (json[index] == 't' &&
					json[index + 1] == 'r' &&
					json[index + 2] == 'u' &&
					json[index + 3] == 'e')
				{
					index += 4;
					return MiniJson.TOKEN_TRUE;
				}
			}

			// null
			if (remainingLength >= 4)
			{
				if (json[index] == 'n' &&
					json[index + 1] == 'u' &&
					json[index + 2] == 'l' &&
					json[index + 3] == 'l')
				{
					index += 4;
					return MiniJson.TOKEN_NULL;
				}
			}

			return MiniJson.TOKEN_NONE;
		}

		protected static bool _SerializeValue(object value, StringBuilder builder)
		{
			bool success = true;

			if (value is string s)
				success = _SerializeString(s, builder);
			else if (value is Hashtable hashtable)
				success = _SerializeObject(hashtable, builder);
			else if (value is ArrayList list)
				success = _SerializeArray(list, builder);
			else if ((value is bool b) && b)
			{
				builder.Append("true");
			}
			else if ((value is bool b1) && (b1 == false))
				builder.Append("false");
			else if (value is ValueType)
			{
				// thanks to ritchie for pointing out ValueType to me
				success = _SerializeNumber(Convert.ToDouble(value), builder);
			}
			else if (value == null)
				builder.Append("null");
			else
				success = false;

			return success;
		}

		protected static bool _SerializeObject(Hashtable anObject, StringBuilder builder)
		{
			builder.Append("{");

			IDictionaryEnumerator e = anObject.GetEnumerator();
			bool first = true;
			while (e.MoveNext())
			{
				string key = e.Key.ToString();
				object value = e.Value;

				if (!first)
					builder.Append(", ");

				_SerializeString(key, builder);
				builder.Append(":");
				if (!_SerializeValue(value, builder))
					return false;

				first = false;
			}

			builder.Append("}");
			return true;
		}

		protected static bool _SerializeArray(ArrayList anArray, StringBuilder builder)
		{
			builder.Append("[");

			bool first = true;
			for (int i = 0; i < anArray.Count; i++)
			{
				object value = anArray[i];

				if (!first)
					builder.Append(", ");

				if (!_SerializeValue(value, builder))
					return false;

				first = false;
			}

			builder.Append("]");
			return true;
		}

		protected static bool _SerializeString(string aString, StringBuilder builder)
		{
			builder.Append("\"");

			char[] charArray = aString.ToCharArray();
			for (int i = 0; i < charArray.Length; i++)
			{
				char c = charArray[i];
				if (c == '"')
					builder.Append("\\\"");
				else if (c == '\\')
					builder.Append("\\\\");
				else if (c == '\b')
					builder.Append("\\b");
				else if (c == '\f')
					builder.Append("\\f");
				else if (c == '\n')
					builder.Append("\\n");
				else if (c == '\r')
					builder.Append("\\r");
				else if (c == '\t')
					builder.Append("\\t");
				else
				{
					int codepoint = Convert.ToInt32(c);
					if ((codepoint >= 32) && (codepoint <= 126))
						builder.Append(c);
					else
						builder.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
				}
			}

			builder.Append("\"");
			return true;
		}

		protected static bool _SerializeNumber(double number, StringBuilder builder)
		{
			builder.Append(Convert.ToString(number, CultureInfo.InvariantCulture));
			return true;
		}

		#endregion

		#endregion



	}
}

