using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif
namespace CsCat
{
  public static class StringExtension
  {
    public static GUIContent ToGUIContent(this string self)
    {
      return new GUIContent(self);
    }

    #region Equals

    public static bool EqualsIgnoreCase(this string self, string s2)
    {
      return self.ToLower().Equals(s2.ToLower());
    }

    #endregion

    public static bool IsNumber(this string self)
    {
      if (Regex.IsMatch(self, "^[+-]?\\d+$")) //整数
        return true;
      if (Regex.IsMatch(self, "^[+-]?((\\d+)|(\\d+\\.\\d*)|(\\d*\\.\\d+))([eE]\\d+)?$")) //浮点数
        return true;
      return false;
    }

    #region 判断是否为null或Empty或WhiteSpace

    /// <summary>
    ///   判断是否为null或Empty
    /// </summary>
    public static bool IsNullOrWhiteSpace(this string self)
    {
      return string.IsNullOrWhiteSpace(self);
    }

    public static bool IsNullOrEmpty(this string self)
    {
      return string.IsNullOrEmpty(self);
    }

    #endregion

    #region 加密

    public static string Encrypt(this string self)
    {
      return MD5Util.Encrypt(self);
    }

    #endregion

    /// <summary>
    ///   将s重复n次返回
    /// </summary>
    public static string Join(this string self, int n)
    {
      var stringBuilder = new StringBuilder();
      for (var i = 0; i < n; i++)
        stringBuilder.Append(self);
      return stringBuilder.ToString();
    }

    public static void RemoveFiles(this string self)
    {
      StdioUtil.RemoveFiles(self);
    }

    public static string Random(this string self, int count, bool is_unique, RandomManager randomManager = null)
    {
      return new string(self.ToCharArray().RandomArray(count, is_unique, randomManager));
    }

    public static int GetSubStringCount(this string self, string subString)
    {
      return Regex.Matches(self, subString).Count;
    }

#if UNITY_EDITOR
    /// <summary>
    ///   "xx11" comes after "xx2".
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static int CompareTo2(this string self, string other)
    {
      return EditorUtility.NaturalCompare(self, other);
    }
#endif
    //跟上面这个EditorUtility.NaturalCompare效果是一样的
    public static int AlphanumCompareTo(this string self, string other)
    {
      return ComparatorConst.AlphanumComparator.Compare(self, other);
    }

    public static string FileName(this string file_path, string last_separator = "/")
    {
      var index = file_path.LastIndexOf(last_separator);
      if (index < 0)
        index = 0;
      else
        index += last_separator.Length;
      return file_path.Substring(index);
    }

    public static string DirPath(this string file_path, string last_separator = "/")
    {
      var index = file_path.LastIndexOf(last_separator);
      var length = 0;
      if (index < 0)
        length = file_path.Length;
      else
        length = index + last_separator.Length;
      return file_path.Substring(0, length);
    }


    public static int IndexEndOf(this string self, string value, int start_index, int count)
    {
      count = start_index + count > self.Length ? self.Length - start_index : count;
      var index = self.IndexOf(value, start_index, count);
      if (index == -1)
        return index;
      return index + value.Length - 1;
    }

    public static int IndexEndOf(this string self, string value, int start_index = 0)
    {
      return IndexEndOf(self, value, start_index, self.Length - start_index);
    }

    public static int LastIndexEndOf(this string self, string value, int start_index, int count)
    {
      count = start_index + count > self.Length ? self.Length - start_index : count;
      var index = self.LastIndexOf(value);
      if (index == -1)
        return index;
      return index + value.Length - 1;
    }

    public static int LastIndexEndOf(this string self, string value, int start_index = 0)
    {
      return LastIndexEndOf(self, value, start_index, self.Length - start_index);
    }

    public static bool IsNetURL(this string self)
    {
      if (self.StartsWith("http", StringComparison.CurrentCultureIgnoreCase) ||
          self.StartsWith("ftp", StringComparison.CurrentCultureIgnoreCase))
        return true;
      return false;
    }

    public static string WWWURLHandle(this string self)
    {
      if (self.IsNetURL())
        return self;
      if (self.IndexOf(FilePathConst.File_Prefix, StringComparison.CurrentCultureIgnoreCase) == -1)
        return self.WithRootPath(FilePathConst.File_Prefix);
      return self;
    }

    public static string ReplaceDirectorySeparatorChar(this string self, char separator = '/')
    {
      return self.Replace(Path.DirectorySeparatorChar, separator);
    }

    public static string WithoutAllSuffix(this string self)
    {
      var index = self.IndexOf(".");
      return index != -1 ? self.Substring(0, index) : self;
    }

    public static string WithoutSuffix(this string self)
    {
      var index = self.LastIndexOf(".");
      return index != -1 ? self.Substring(0, index) : self;
    }

    public static string ToLuaRequirePath(this string self)
    {
      return self.Replace("/", ".");
    }
		

    public static bool ContainTags(this string self, params string[] check_tags)
    {
      var tags = self.Split(MultiTagsUtil.TAG_SEPARATOR);
      foreach (var checkTag in check_tags)
        if (!tags.Contains(checkTag))
          return false;
      return true;
    }

    public static string GetMainAssetPath(this string self)
    {
      return self.GetAssetPathInfo().main_asset_path;
    }

    public static string GetSubAssetPath(this string self)
    {
      return self.GetAssetPathInfo().sub_asset_path;
    }

    public static AssetPathInfo GetAssetPathInfo(this string self)
    {
      return new AssetPathInfo(self);
    }

    public static string GetPreString(this string self, string split_content)
    {
      int index = self.IndexOf(split_content);
      if (index == -1)
        return self;
      else
        return self.Substring(0, index);
    }

    public static string GetPostString(this string self, string split_content)
    {
      int index = self.IndexEndOf(split_content);
      if (index == -1)
        return self;
      else
        return self.Substring(index + 1);
    }

    public static string Format(this string format, params object[] args)
    {
      return string.Format(format, args);
    }
    #region RichText
    public static void SetRichTextColor(this string self, Color color)
    {
      RichTextUtil.SetColor(self, color);
    }
    public static void SetRichTextIsBold(this string self)
    {
      RichTextUtil.SetIsBold(self);
    }
    public static void SetIsItalic(this string self)
    {
      RichTextUtil.SetIsItalic(self);
    }
    public static void SetRichTextFontSize(this string self, int font_size)
    {
      RichTextUtil.SetFontSize(self, font_size);
    }
    #endregion

    #region 编码

    /// <summary>
    ///   字符串的bytes，默认编码是当前系统编码
    /// </summary>
    public static byte[] GetBytes(this string self, Encoding encoding = null)
    {
      if (encoding == null)
        encoding = Encoding.UTF8;

      return encoding.GetBytes(self);
    }

    /// <summary>
    ///   从字符串的X进制编码的字符串转为十进制的数字（long类型）
    /// </summary>
    public static long ToLong(this string self, int from_base)
    {
      return X2H(self, from_base);
    }

    #endregion


    #region 各种转换 ToXX

    public static Vector2 ToVector2(this string self, string split = ",", string trim_left = "(",
      string trim_right = ")")
    {
      var element_list = self.ToList<string>(split, trim_left, trim_right);
      var x = element_list[0].To<float>();
      var y = element_list[1].To<float>();
      return new Vector2(x, y);
    }

    public static Vector2 ToVector2OrDefault(this string self, string to_default_string = null,
      Vector2 default_value = default(Vector2))
    {
      if (ObjectUtil.Equals(self, to_default_string))
        return default_value;
      return self.ToVector2();
    }

    public static Vector3 ToVector3(this string s, string split = ",", string trim_left = "(", string trim_right = ")")
    {
      var element_list = s.ToList<string>(split, trim_left, trim_right);
      var x = element_list[0].To<float>();
      var y = element_list[1].To<float>();
      var z = element_list[2].To<float>();
      return new Vector3(x, y, z);
    }

    public static Vector2Int ToVector2IntOrDefault(this string self, string to_default_string = null,
      Vector2Int default_value = default(Vector2Int))
    {
      return self.ToVector2OrDefault(to_default_string, default_value).ToVector2Int();
    }


    public static Vector3 ToVector3OrDefault(this string self, string to_default_string = null,
      Vector3 default_value = default(Vector3))
    {
      if (ObjectUtil.Equals(self, to_default_string))
        return default_value;
      return self.ToVector3();
    }

    public static Vector3Int ToVector3IntOrDefault(this string self, string to_default_string = null,
      Vector3Int default_value = default(Vector3Int))
    {
      return self.ToVector3OrDefault(to_default_string, default_value).ToVector3Int();
    }

    public static Vector4 ToVector4(this string self, string split = ",", string trim_left = "(",
      string trim_right = ")")
    {
      var element_list = self.ToList<string>(split, trim_left, trim_right);
      var x = element_list[0].To<float>();
      var y = element_list[1].To<float>();
      var z = element_list[2].To<float>();
      var w = element_list[3].To<float>();
      return new Vector4(x, y, z, w);
    }

    public static Vector3 ToVector4OrDefault(this string self, string to_default_string = null,
      Vector4 default_value = default(Vector4))
    {
      if (ObjectUtil.Equals(self, to_default_string))
        return default_value;
      return self.ToVector4();
    }

    public static Matrix4x4 ToMatrix4x4(this string self, string split = "\n", string trim_left = "(",
      string trim_right = ")")
    {
      self = self.Replace("\r", "");
      var element_list = self.ToList<string>(split, trim_left, trim_right);
      var row0 = element_list[0].ToVector4("\t");
      var row1 = element_list[1].ToVector4("\t");
      var row2 = element_list[2].ToVector4("\t");
      var row3 = element_list[3].ToVector4("\t");

      var column0 = new Vector4(row0.x, row1.x, row2.x, row3.x);
      var column1 = new Vector4(row0.y, row1.y, row2.y, row3.y);
      var column2 = new Vector4(row0.z, row1.z, row2.z, row3.z);
      var column3 = new Vector4(row0.w, row1.w, row2.w, row3.w);
      return new Matrix4x4(column0, column1, column2, column3);
    }

    public static Matrix4x4 ToMatrix4x4OrDefault(this string self, string to_default_string = null,
      Matrix4x4 default_value = default(Matrix4x4))
    {
      if (ObjectUtil.Equals(self, to_default_string))
        return default_value;
      return self.ToMatrix4x4();
    }


    public static float ToFloat(this string self)
    {
      return float.Parse(self);
    }

    public static int ToInt(this string self)
    {
      return int.Parse(self);
    }

    public static Quaternion ToQuaternion(this string self, string split = ",", string trim_left = "(",
      string trim_right = ")")
    {
      var element_list = self.ToList<string>(split, trim_left, trim_right);
      if (element_list.Count == 4)
      {
        var x = element_list[0].To<float>();
        var y = element_list[1].To<float>();
        var z = element_list[2].To<float>();
        var w = element_list[3].To<float>();
        return new Quaternion(x, y, z, w);
      }
      else //欧拉角，三个系数
      {
        var x = element_list[0].To<float>();
        var y = element_list[1].To<float>();
        var z = element_list[2].To<float>();
        return Quaternion.Euler(x, y, z);
      }
    }

    /// <summary>
    /// split 忽略ignore_left,ignore_right包裹的东西
    /// </summary>
    /// <param name="self"></param>
    /// <param name="split"></param>
    /// <param name="ignore_left">注意转移字符，需要加上\,例如忽略",则需要输入\\\"</param>
    /// <param name="ignore_right"></param>
    /// <returns></returns>
    public static string[] SplitIgnore(this string self, string split = ",", string ignore_left = "\\\"",
      string ignore_right = null)
    {
      if (ignore_right == null)
        ignore_right = ignore_left;
      var result_list = new List<string>();
      //https://blog.csdn.net/scrOUT/article/details/90517304
      //    var regex = new Regex("(" + split + ")" + "(?=([^\\\"]*\\\"[^\\\"]*\\\")*[^\\\"]*$)"); //双引号内的逗号不分割  双引号外的逗号进行分割
      string pattern = string.Format("({0})(?=([^{1}]*{2}[^{3}]*{4})*[^{5}]*$)", split, ignore_left, ignore_left,
        ignore_right, ignore_right, ignore_right);
      var regex = new Regex(pattern);
      var start_index = -1;
      var matchCollection = regex.Matches(self);
      foreach (Match match in matchCollection)
      {
        var element = self.Substring(start_index + 1, match.Index - (start_index + 1));
        result_list.Add(element);
        start_index = match.Index;
      }

      if (start_index <= self.Length - 1)
      {
        if (self.Length == 0)
          result_list.Add("");
        else if (start_index == self.Length - split.Length && self.Substring(start_index).Equals(split))
          result_list.Add("");
        else
          result_list.Add(self.Substring(start_index + 1));
      }

      return result_list.ToArray();
    }

    public static List<T> ToList<T>(this string self, string split = ",", string trim_left = "[",
      string trim_right = "]")
    {
      var list = new List<T>();
      if (self.StartsWith(trim_left))
        self = self.Substring(1);
      if (self.EndsWith(trim_right))
        self = self.Substring(0, self.Length - 1);
      if (self.IsNullOrWhiteSpace())
        return list;
      var elements = self.SplitIgnore(split);
      foreach (var element in elements) list.Add(element.To<T>());
      return list;
    }

    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this string self, string split = ",",
      string sub_separator = ":", string trim_left = "{", string trim_right = "}", string element_ignore_left = "\\\"",
      string element_ignore_right = null)
    {
      var element_list = self.ToList<string>(split, trim_left, trim_right);
      var dict = new Dictionary<TKey, TValue>();
      foreach (var element in element_list)
      {
        var ss = element.SplitIgnore(sub_separator, element_ignore_left, element_ignore_right);
        var key_string = ss[0];
        var value_string = ss[1];

        dict[key_string.To<TKey>()] = value_string.To<TValue>();
      }

      return dict;
    }


    /// <summary>
    ///   将s用pattern模式转换为DateTime，转换失败时返回默认值dv
    /// </summary>
    public static DateTime ToDateTime(this string self, string pattern)
    {
      var provider = CultureInfo.InvariantCulture;
      return DateTime.ParseExact(self, pattern, provider);
    }

    /// <summary>
    ///   形如:
    ///   #FF00FF00或者FF00FF00  含Alpha
    ///   或者#FF00FF或者FF00FF 不含Alpha
    /// </summary>
    public static Color ToColor(this string self, string trim_left = "#")
    {
      self = self.TrimLeft(trim_left);
      self = self.Replace("0x", "");
      Color color;
      ColorUtility.TryParseHtmlString(self, out color);
      return color;
      //int value = int.Parse(s, System.Globalization.NumberStyles.HexNumber);

      //if (s.Length == 6)
      //{
      //    byte R = Convert.ToByte((value >> 16) & 255);
      //    byte G = Convert.ToByte((value >> 8) & 255);
      //    byte B = Convert.ToByte((value >> 0) & 255);
      //    return new Color(R / 255f, G / 255f, B / 255f);
      //}
      //else
      //{
      //    byte R = Convert.ToByte((value >> 24) & 255);
      //    byte G = Convert.ToByte((value >> 16) & 255);
      //    byte B = Convert.ToByte((value >> 8) & 255);
      //    byte A = Convert.ToByte((value >> 0) & 255);
      //    return new Color(R / 255f, G / 255f, B / 255f,A/255f);
      //}
    }

    public static Color ToColorOrDefault(this string self, string to_default_string = null,
      Color default_value = default(Color))
    {
      if (ObjectUtil.Equals(self, to_default_string))
        return default_value;
      return self.ToColor();
    }

    #endregion

    #region Enum

    public static T ToEnum<T>(this string self)
    {
      return (T)Enum.Parse(typeof(T), self);
    }

    public static bool IsEnum<T>(this string self)
    {
      return Enum.IsDefined(typeof(T), self);
    }

    #endregion

    #region ToBytes

    /// <summary>
    ///   将s转换为bytes（bytes长度为len[当len比s转换出来的bytes更少的时候，用更少那个]）
    /// </summary>
    public static byte[] ToBytes(this string self, int len, Encoding encoding = null)
    {
      var bb = new byte[len];
      ByteUtil.ZeroBytes(bb);
      if (self.IsNullOrWhiteSpace())
        return null;
      var tBb = self.GetBytes(encoding);
      ByteUtil.BytesCopy(tBb, bb, Math.Min(len, tBb.Length));
      return bb;
    }

    public static byte[] ToBytes(this string self, Encoding encoding = null)
    {
      if (self.IsNullOrWhiteSpace())
        return null;
      var bb = self.GetBytes(encoding);
      return bb;
    }

    #endregion

    #region Split

    /// <summary>
    ///   将s按sliceLen长度进行分割
    /// </summary>
    public static string[] Split(this string self, int slice_len)
    {
      if (self == null)
        return null;
      if (slice_len <= 0 || self.Length <= slice_len)
        return new[] { self };
      var list = new List<string>();
      for (var i = 0; i < self.Length / slice_len; i++)
        list.Add(self.Substring(i * slice_len, (i + 1) * slice_len));
      if (self.Length % slice_len != 0)
        list.Add(self.Substring(self.Length - self.Length % slice_len));
      return list.ToArray();
    }

    /// <summary>
    ///   将s按字符串sep分隔符分割
    /// </summary>
    public static string[] Split(this string self, string sep)
    {
      if (self == null)
        return null;
      if (string.IsNullOrEmpty(sep))
        return new[] { self };

      var sep_len = sep.Length;
      var sep_index = self.IndexOf(sep, StringComparison.Ordinal);
      if (sep_index != -1)
      {
        var list = new List<string> { self.Substring(0, sep_index) };
        var sub_index = sep_index + sep_len;
        while ((sep_index = self.IndexOf(sep, sub_index)) != -1)
        {
          list.Add(self.Substring(sub_index, sep_index - sub_index));
          sub_index = sep_index + sep_len;
        }

        list.Add(self.Substring(sub_index));


        return list.ToArray();
      }

      return new[] { self };
    }

    #endregion

    #region Quote

    /// <summary>
    ///   将对象列表的每一项成员用左右字符串括起来
    /// </summary>
    public static string Quote(this string self, string left, string right)
    {
      return left + self + right;
    }

    public static string QuoteBoth(this string self, string quote)
    {
      return self.Quote(quote, quote);
    }

    public static string QuoteWithDouble(this string self) //双引号
    {
      return self.QuoteBoth("\"");
    }

    //pos在left之后第一个字母的index
    public static int QuoteEndIndex(this string s, string left, string right, int pos = 0)
    {
      int stack = 0;
      while (pos < s.Length)
      {
        if (s.IndexOf(left, pos) == pos)
        {
          stack++;
          pos = pos + left.Length;
        }
        else if (s.IndexOf(right, pos) == pos)
        {
          if (stack == 0)
            return pos;
          else
          {
            stack--;
            pos = pos + right.Length;
          }
        }
        else
          pos++;
      }

      return -1;
    }

    #endregion

    #region Trim

    /// <summary>
    ///   整理字符串,去掉两边的指定字符（trimLeftChars，trimRightChars）
    /// </summary>
    public static string Trim(this string self, string trim_left_chars, string trim_right_chars,
      bool is_trim_all = true)
    {
      if (!trim_left_chars.IsNullOrWhiteSpace())
        while (true)
        {
          var begin_index = self.IndexOf(trim_left_chars);
          if (begin_index == 0)
          {
            self = self.Substring(trim_left_chars.Length);
            if (!is_trim_all)
              break;
          }
          else
          {
            break;
          }
        }

      if (!trim_right_chars.IsNullOrWhiteSpace())
        while (true)
        {
          var begin_index = self.LastIndexOf(trim_left_chars);
          if (begin_index != -1 && begin_index + trim_left_chars.Length == self.Length)
          {
            self = self.Substring(0, begin_index);
            if (!is_trim_all)
              break;
          }
          else
          {
            break;
          }
        }

      return self;
    }

    /// <summary>
    ///   整理字符串,去掉两边的指定字符trimChars
    /// </summary>
    public static string Trim(this string self, string trim_chars)
    {
      return Trim(self, trim_chars, trim_chars);
    }

    /// <summary>
    ///   整理字符串,去掉左边的指定字符trimChars
    /// </summary>
    public static string TrimLeft(this string self, string trim_chars)
    {
      return Trim(self, trim_chars, "");
    }

    /// <summary>
    ///   整理字符串,去掉右边的指定字符trimChars
    /// </summary>
    public static string TrimRight(this string self, string trim_chars)
    {
      return Trim(self, "", trim_chars);
    }

    #endregion

    #region 大小写第一个字母

    public static string UpperFirstLetter(this string self)
    {
      return self.Substring(0, 1).ToUpper() + self.Substring(1);
    }

    public static string LowerFirstLetter(this string self)
    {
      return self.Substring(0, 1).ToLower() + self.Substring(1);
    }

    public static bool IsFirstLetterUpper(this string self)
    {
      return self[0].IsUpper();
    }

    public static bool IsFirstLetterLower(this string self)
    {
      return self[0].IsLower();
    }

    #endregion

    #region FillHead/FillEnd

    /// <summary>
    ///   前补齐字符串.若src长度不足len，则在src前面用c补足len长度，否则直接返回src
    /// </summary>
    public static string FillHead(this string self, int len, char c)
    {
      if (self.Length >= len)
        return self;
      var sb = new StringBuilder(len);
      for (var i = 0; i < len - self.Length; i++)
        sb.Append(c);
      sb.Append(self);
      return sb.ToString();
    }

    /// <summary>
    ///   后补齐字符串.若src长度不足len，则在src后面用c补足len长度，否则直接返回src
    /// </summary>
    public static string FillEnd(this string self, int len, char c)
    {
      if (self.Length >= len)
        return self;
      var sb = new StringBuilder(len);
      sb.Append(self);
      for (var i = 0; i < len - self.Length; i++)
        sb.Append(c);
      return sb.ToString();
    }

    #endregion

    #region GetDigit

    /// <summary>
    ///   获取src的第一个数字（可能由多个字符组成）
    ///   如：123df58f，则返回"123";abc123则返回""
    /// </summary>
    public static string GetDigitStart(this string self)
    {
      self = self.Trim();
      var sb = new StringBuilder(self.Length);
      foreach (var t in self)
      {
        if (!char.IsDigit(t))
          break;
        sb.Append(t);
      }

      return sb.ToString();
    }

    /// <summary>
    ///   获取src的第一个数字（可能由多个字符组成）
    ///   如：123df58f，则返回123;abc123则返回dv
    /// </summary>
    public static long GetDigitStart(this string self, long dv)
    {
      return GetDigitStart(self).ToLongOrToDefault(dv);
    }

    /// <summary>
    ///   由末尾向前，获取src的第一个数字（可能由多个字符组成）
    ///   如：fg125abc456，则得出来是"456";fg125abc456fd，则得出来是""
    /// </summary>
    public static string GetDigitEnd(this string self)
    {
      self = self.Trim();
      var sb = new StringBuilder(self.Length);
      for (var i = self.Length - 1; i >= 0; i--)
      {
        if (!char.IsDigit(self[i]))
          break;
        sb.Insert(0, self[i]);
      }

      return sb.ToString();
    }

    /// <summary>
    ///   由末尾向前，获取src的第一个数字（可能由多个字符组成）
    ///   如：fg125abc456，则得出来是456;fg125abc456fd，则得出来是dv
    /// </summary>
    public static long GetDigitEnd(this string self, long dv)
    {
      return GetDigitEnd(self).ToLongOrToDefault(dv);
    }


    public static string ToGuid(this string self, object o)
    {
      return self + o.GetHashCode();
    }

    public static string GetId(this string self)
    {
      var index = self.IndexOf(IdConst.Rid_Infix);
      if (index == -1)
        return self;
      return self.Substring(0, index);
    }

    public static string GetUITextOfColorAndFontSize(this string self, Color? color = null, int? font_size = null)
    {
      if (color.HasValue)
      {
        self = self.Replace("</color>", "");
        self = Regex.Replace(self, "<color.*?>", "");
        self = string.Format("<color=#{0}>{1}</color>", color.Value.ToHtmlStringRGBA(), self);
      }

      if (font_size.HasValue)
      {
        self = self.Replace("</size>", "");
        self = Regex.Replace(self, "<size.*?>", "");
        self = string.Format("<size={0}>{1}</size>", font_size.Value, self);
      }

      return self;
    }

    public static string ReplaceAll(this string s, string pattern, Func<string, string> replaceFunc = null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      MatchCollection matchCollection = Regex.Matches(s, pattern);
      int last_end_index = -1;
      for (int i = 0; i < matchCollection.Count; i++)
      {
        int start_index = matchCollection[i].Index;
        string value = matchCollection[i].Value;
        int end_index = start_index + value.Length - 1;
        value = replaceFunc == null ? value : replaceFunc(value);
        stringBuilder.Append(s.Substring(last_end_index + 1, start_index - (last_end_index + 1)));
        stringBuilder.Append(value);
        last_end_index = end_index;
      }

      if (last_end_index != s.Length - 1)
        stringBuilder.Append(s.Substring(last_end_index + 1));
      return stringBuilder.ToString();
    }


    #endregion

    #region 私有函数

    private static long X2H(string value, int from_base)
    {
      value = value.Trim();
      if (string.IsNullOrEmpty(value))
        return 0L;
      var const_chars = CharUtil.GetDigitsAndCharsBig();
      var digits = new string(const_chars, 0, from_base);
      long result = 0;
      value = value.ToUpper(); // 2


      for (var i = 0; i < value.Length; i++)
      {
        if (!digits.Contains(value[i] + ""))
          throw new ArgumentException(string.Format("The argument \"{0}\" is not in {1} system.", value[i],
            from_base));
        try
        {
          result += (long)Math.Pow(from_base, i) * GetCharIndex(const_chars, value[value.Length - i - 1]); //   2
        }
        catch
        {
          throw new OverflowException("运算溢出.");
        }
      }

      return result;
    }

    private static int GetCharIndex(char[] arr, char value)
    {
      for (var i = 0; i < arr.Length; i++)
        if (arr[i] == value)
          return i;
      return 0;
    }



    #endregion
  }
}