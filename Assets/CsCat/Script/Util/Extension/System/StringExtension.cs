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
            return Regex.IsMatch(self, StringConst.String_Regex_Integer) ||
                   Regex.IsMatch(self, StringConst.String_Regex_Float);
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
            using (var scope = new StringBuilderScope())
            {
                for (var i = 0; i < n; i++)
                    scope.stringBuilder.Append(self);
                return scope.stringBuilder.ToString();
            }
        }

        public static void RemoveFiles(this string self)
        {
            StdioUtil.RemoveFiles(self);
        }

        public static string Random(this string self, int count, bool isUnique)
        {
            return new string(self.ToCharArray().RandomArray(count, isUnique));
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

        public static string FileName(this string filePath, string lastSeparator = StringConst.String_Slash)
        {
            var index = filePath.LastIndexOf(lastSeparator);
            if (index < 0)
                index = 0;
            else
                index += lastSeparator.Length;
            return filePath.Substring(index);
        }

        public static string DirPath(this string filePath, string lastSeparator = StringConst.String_Slash)
        {
            var index = filePath.LastIndexOf(lastSeparator);
            var length = index < 0 ? filePath.Length : index + lastSeparator.Length;
            return filePath.Substring(0, length);
        }


        public static int IndexEndOf(this string self, string value, int startIndex, int count)
        {
            count = startIndex + count > self.Length ? self.Length - startIndex : count;
            var index = self.IndexOf(value, startIndex, count);
            return index == -1 ? index : index + value.Length - 1;
        }

        public static int IndexEndOf(this string self, string value, int startIndex = 0)
        {
            return IndexEndOf(self, value, startIndex, self.Length - startIndex);
        }

        public static int LastIndexEndOf(this string self, string value, int startIndex, int count)
        {
            count = startIndex + count > self.Length ? self.Length - startIndex : count;
            var index = self.LastIndexOf(value, startIndex, count);
            return index == -1 ? index : index + value.Length - 1;
        }

        public static int LastIndexEndOf(this string self, string value, int startIndex = 0)
        {
            return LastIndexEndOf(self, value, startIndex, self.Length - startIndex);
        }

        public static bool IsNetURL(this string self)
        {
            return self.StartsWith(StringConst.String_http, StringComparison.CurrentCultureIgnoreCase) ||
                   self.StartsWith(StringConst.String_ftp, StringComparison.CurrentCultureIgnoreCase);
        }

        public static string WWWURLHandle(this string self)
        {
            return self.IsNetURL()
                ? self
                : self.IndexOf(StringConst.String_File_Url_Prefix, StringComparison.CurrentCultureIgnoreCase) == -1
                    ? self.WithRootPath(StringConst.String_File_Url_Prefix)
                    : self;
        }

        public static string ReplaceDirectorySeparatorChar(this string self, char separator = CharConst.Char_Slash)
        {
            return self.Replace(Path.DirectorySeparatorChar, separator);
        }

        public static string WithoutAllSuffix(this string self)
        {
            var index = self.IndexOf(CharConst.Char_Dot);
            return index != -1 ? self.Substring(0, index) : self;
        }

        public static string WithoutSuffix(this string self)
        {
            var index = self.LastIndexOf(CharConst.Char_Dot);
            return index != -1 ? self.Substring(0, index) : self;
        }

        public static string ToLuaRequirePath(this string self)
        {
            return self.Replace(CharConst.Char_Slash, CharConst.Char_Dot);
        }


        public static bool ContainTags(this string self, params string[] checkTags)
        {
            var tags = self.Split(MultiTagsUtil.TAG_SEPARATOR);
            foreach (var checkTag in checkTags)
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

        public static string GetPreString(this string self, string splitContent)
        {
            var index = self.IndexOf(splitContent);
            return index == -1 ? self : self.Substring(0, index);
        }

        public static string GetPostString(this string self, string splitContent)
        {
            var index = self.IndexEndOf(splitContent);
            return index == -1 ? self : self.Substring(index + 1);
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

        public static void SetRichTextFontSize(this string self, int fontSize)
        {
            RichTextUtil.SetFontSize(self, fontSize);
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
        public static long ToLong(this string self, int fromBase)
        {
            return _X2H(self, fromBase);
        }

        #endregion


        #region 各种转换 ToXX

        public static Vector2 ToVector2(this string self, string split = StringConst.String_Comma,
            string trimLeft = StringConst.String_LeftRoundBrackets,
            string trimRight = StringConst.String_RightRoundBrackets)
        {
            var elementList = self.ToList<string>(split, trimLeft, trimRight);
            var x = elementList[0].To<float>();
            var y = elementList[1].To<float>();
            return new Vector2(x, y);
        }

        public static Vector2 ToVector2OrDefault(this string self, string toDefaultString = null,
            Vector2 defaultValue = default)
        {
            return ObjectUtil.Equals(self, toDefaultString) ? defaultValue : self.ToVector2();
        }

        public static Vector3 ToVector3(this string s, string split = StringConst.String_Comma,
            string trimLeft = StringConst.String_LeftRoundBrackets,
            string trimRight = StringConst.String_RightRoundBrackets)
        {
            var elementList = s.ToList<string>(split, trimLeft, trimRight);
            var x = elementList[0].To<float>();
            var y = elementList[1].To<float>();
            var z = elementList[2].To<float>();
            return new Vector3(x, y, z);
        }

        public static Vector2Int ToVector2IntOrDefault(this string self, string toDefaultString = null,
            Vector2Int defaultValue = default)
        {
            return self.ToVector2OrDefault(toDefaultString, defaultValue).ToVector2Int();
        }


        public static Vector3 ToVector3OrDefault(this string self, string toDefaultString = null,
            Vector3 defaultValue = default)
        {
            return ObjectUtil.Equals(self, toDefaultString) ? defaultValue : self.ToVector3();
        }

        public static Vector3Int ToVector3IntOrDefault(this string self, string toDefaultString = null,
            Vector3Int defaultValue = default)
        {
            return self.ToVector3OrDefault(toDefaultString, defaultValue).ToVector3Int();
        }

        public static Vector4 ToVector4(this string self, string split = StringConst.String_Comma,
            string trimLeft = StringConst.String_LeftRoundBrackets,
            string trimRight = StringConst.String_RightRoundBrackets)
        {
            var elementList = self.ToList<string>(split, trimLeft, trimRight);
            var x = elementList[0].To<float>();
            var y = elementList[1].To<float>();
            var z = elementList[2].To<float>();
            var w = elementList[3].To<float>();
            return new Vector4(x, y, z, w);
        }

        public static Vector3 ToVector4OrDefault(this string self, string toDefaultString = null,
            Vector4 defaultValue = default)
        {
            return ObjectUtil.Equals(self, toDefaultString) ? defaultValue : self.ToVector4();
        }

        public static Matrix4x4 ToMatrix4x4(this string self, string split = StringConst.String_SlashN,
            string trimLeft = StringConst.String_LeftRoundBrackets,
            string trimRight = StringConst.String_RightRoundBrackets)
        {
            self = self.Replace(StringConst.String_SlashR, StringConst.String_Empty);
            var elementList = self.ToList<string>(split, trimLeft, trimRight);
            var row0 = elementList[0].ToVector4(StringConst.String_Tab);
            var row1 = elementList[1].ToVector4(StringConst.String_Tab);
            var row2 = elementList[2].ToVector4(StringConst.String_Tab);
            var row3 = elementList[3].ToVector4(StringConst.String_Tab);

            var column0 = new Vector4(row0.x, row1.x, row2.x, row3.x);
            var column1 = new Vector4(row0.y, row1.y, row2.y, row3.y);
            var column2 = new Vector4(row0.z, row1.z, row2.z, row3.z);
            var column3 = new Vector4(row0.w, row1.w, row2.w, row3.w);
            return new Matrix4x4(column0, column1, column2, column3);
        }

        public static Matrix4x4 ToMatrix4x4OrDefault(this string self, string toDefaultString = null,
            Matrix4x4 defaultValue = default(Matrix4x4))
        {
            return ObjectUtil.Equals(self, toDefaultString) ? defaultValue : self.ToMatrix4x4();
        }


        public static float ToFloat(this string self)
        {
            return float.Parse(self);
        }

        public static int ToInt(this string self)
        {
            return int.Parse(self);
        }

        public static Quaternion ToQuaternion(this string self, string split = StringConst.String_Comma,
            string trimLeft = StringConst.String_LeftRoundBrackets,
            string trimRight = StringConst.String_RightRoundBrackets)
        {
            var elementList = self.ToList<string>(split, trimLeft, trimRight);
            if (elementList.Count == 4)
            {
                var x = elementList[0].To<float>();
                var y = elementList[1].To<float>();
                var z = elementList[2].To<float>();
                var w = elementList[3].To<float>();
                return new Quaternion(x, y, z, w);
            }
            else //欧拉角，三个系数
            {
                var x = elementList[0].To<float>();
                var y = elementList[1].To<float>();
                var z = elementList[2].To<float>();
                return Quaternion.Euler(x, y, z);
            }
        }

        /// <summary>
        /// split 忽略ignore_left,ignore_right包裹的东西
        /// </summary>
        /// <param name="self"></param>
        /// <param name="split"></param>
        /// <param name="ignoreLeft">注意转移字符，需要加上\,例如忽略",则需要输入\\\"</param>
        /// <param name="ignoreRight"></param>
        /// <returns></returns>
        public static string[] SplitIgnore(this string self, string split = StringConst.String_Comma,
            string ignoreLeft = StringConst.String_Regex_DoubleQuotes,
            string ignoreRight = null)
        {
            if (ignoreRight == null)
                ignoreRight = ignoreLeft;
            var resultList = new List<string>();
            //https://blog.csdn.net/scrOUT/article/details/90517304
            //    var regex = new Regex("(" + split + ")" + "(?=([^\\\"]*\\\"[^\\\"]*\\\")*[^\\\"]*$)"); //双引号内的逗号不分割  双引号外的逗号进行分割
            string pattern = string.Format(StringConst.String_Regex_Format_SplitIgnore, split, ignoreLeft, ignoreLeft,
                ignoreRight, ignoreRight, ignoreRight);
            var regex = new Regex(pattern);
            var startIndex = -1;
            var matchCollection = regex.Matches(self);
            foreach (Match match in matchCollection)
            {
                var element = self.Substring(startIndex + 1, match.Index - (startIndex + 1));
                resultList.Add(element);
                startIndex = match.Index;
            }

            if (startIndex <= self.Length - 1)
            {
                if (self.Length == 0)
                    resultList.Add(StringConst.String_Empty);
                else if (startIndex == self.Length - split.Length && self.Substring(startIndex).Equals(split))
                    resultList.Add(StringConst.String_Empty);
                else
                    resultList.Add(self.Substring(startIndex + 1));
            }

            return resultList.ToArray();
        }

        public static List<T> ToList<T>(this string self, string split = StringConst.String_Comma,
            string trimLeft = StringConst.String_LeftSquareBrackets,
            string trimRight = StringConst.String_RightSquareBrackets)
        {
            var list = new List<T>();
            if (self.StartsWith(trimLeft))
                self = self.Substring(1);
            if (self.EndsWith(trimRight))
                self = self.Substring(0, self.Length - 1);
            if (self.IsNullOrWhiteSpace())
                return list;
            var elements = self.SplitIgnore(split);
            foreach (var element in elements) list.Add(element.To<T>());
            return list;
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this string self,
            string split = StringConst.String_Comma,
            string subSeparator = StringConst.String_Colon, string trimLeft = StringConst.String_LeftCurlyBrackets,
            string trimRight = StringConst.String_RightCurlyBrackets,
            string elementIgnoreLeft = StringConst.String_Regex_DoubleQuotes,
            string elementIgnoreRight = null)
        {
            var elementList = self.ToList<string>(split, trimLeft, trimRight);
            var dict = new Dictionary<TKey, TValue>();
            foreach (var element in elementList)
            {
                var ss = element.SplitIgnore(subSeparator, elementIgnoreLeft, elementIgnoreRight);
                var keyString = ss[0];
                var valueString = ss[1];

                dict[keyString.To<TKey>()] = valueString.To<TValue>();
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
        public static Color ToColor(this string self, string trimLeft = StringConst.String_NumberSign)
        {
            self = self.TrimLeft(trimLeft);
            self = self.Replace(StringConst.String_0x, StringConst.String_Empty);
            ColorUtility.TryParseHtmlString(self, out var color);
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

        public static Color ToColorOrDefault(this string self, string toDefaultString = null,
            Color defaultValue = default)
        {
            return ObjectUtil.Equals(self, toDefaultString) ? defaultValue : self.ToColor();
        }

        #endregion

        #region Enum

        public static T ToEnum<T>(this string self)
        {
            return (T) Enum.Parse(typeof(T), self);
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
        public static string[] Split(this string self, int sliceLength)
        {
            if (self == null)
                return null;
            if (sliceLength <= 0 || self.Length <= sliceLength)
                return new[] {self};
            var list = new List<string>();
            for (var i = 0; i < self.Length / sliceLength; i++)
                list.Add(self.Substring(i * sliceLength, (i + 1) * sliceLength));
            if (self.Length % sliceLength != 0)
                list.Add(self.Substring(self.Length - self.Length % sliceLength));
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
                return new[] {self};

            var sepLength = sep.Length;
            var sepIndex = self.IndexOf(sep, StringComparison.Ordinal);
            if (sepIndex == -1) return new[] {self};
            var list = new List<string> {self.Substring(0, sepIndex)};
            var subIndex = sepIndex + sepLength;
            while ((sepIndex = self.IndexOf(sep, subIndex)) != -1)
            {
                list.Add(self.Substring(subIndex, sepIndex - subIndex));
                subIndex = sepIndex + sepLength;
            }

            list.Add(self.Substring(subIndex));


            return list.ToArray();
        }

        #endregion

        #region Warp

        /// <summary>
        ///   将对象列表的每一项成员用左右字符串括起来
        /// </summary>
        public static string Wrap(this string self, string leftWrap, string rightWrap)
        {
            return leftWrap + self + rightWrap;
        }

        public static string WarpBoth(this string self, string wrap)
        {
            return self.Wrap(wrap, wrap);
        }

        public static string WarpWithDoubleQuotes(this string self) //双引号
        {
            return self.WarpBoth(StringConst.String_DoubleQuotes);
        }

        //pos在left之后第一个字母的index
        public static int WrapEndIndex(this string s, string left, string right, int pos = 0)
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
                    stack--;
                    pos = pos + right.Length;
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
        public static string Trim(this string self, string trimLeft, string trimRight,
            bool isTrimAll = true)
        {
            if (!trimLeft.IsNullOrWhiteSpace())
                while (true)
                {
                    var beginIndex = self.IndexOf(trimLeft);
                    if (beginIndex == 0)
                    {
                        self = self.Substring(trimLeft.Length);
                        if (!isTrimAll)
                            break;
                    }
                    else
                        break;
                }

            if (trimRight.IsNullOrWhiteSpace()) return self;
            while (true)
            {
                var beginIndex = self.LastIndexOf(trimLeft);
                if (beginIndex != -1 && beginIndex + trimLeft.Length == self.Length)
                {
                    self = self.Substring(0, beginIndex);
                    if (!isTrimAll)
                        break;
                }
                else
                    break;
            }

            return self;
        }

        /// <summary>
        ///   整理字符串,去掉两边的指定字符trimChars
        /// </summary>
        public static string Trim(this string self, string trimString)
        {
            return Trim(self, trimString, trimString);
        }

        /// <summary>
        ///   整理字符串,去掉左边的指定字符trimChars
        /// </summary>
        public static string TrimLeft(this string self, string trimString)
        {
            return Trim(self, trimString, StringConst.String_Empty);
        }

        /// <summary>
        ///   整理字符串,去掉右边的指定字符trimChars
        /// </summary>
        public static string TrimRight(this string self, string trimString)
        {
            return Trim(self, StringConst.String_Empty, trimString);
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
            using (var scope = new StringBuilderScope())
            {
                for (var i = 0; i < len - self.Length; i++)
                    scope.stringBuilder.Append(c);
                scope.stringBuilder.Append(self);
                return scope.stringBuilder.ToString();
            }
        }

        /// <summary>
        ///   后补齐字符串.若src长度不足len，则在src后面用c补足len长度，否则直接返回src
        /// </summary>
        public static string FillEnd(this string self, int len, char c)
        {
            if (self.Length >= len)
                return self;
            using (var scope = new StringBuilderScope())
            {
                scope.stringBuilder.Append(self);
                for (var i = 0; i < len - self.Length; i++)
                    scope.stringBuilder.Append(c);
                return scope.stringBuilder.ToString();
            }
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
            using (var scope = new StringBuilderScope(self.Length))
            {
                foreach (var t in self)
                {
                    if (!char.IsDigit(t))
                        break;
                    scope.stringBuilder.Append(t);
                }

                return scope.stringBuilder.ToString();
            }
        }

        /// <summary>
        ///   获取src的第一个数字（可能由多个字符组成）
        ///   如：123df58f，则返回123;abc123则返回dv
        /// </summary>
        public static long GetDigitStart(this string self, long defaultValue)
        {
            return GetDigitStart(self).ToLongOrToDefault(defaultValue);
        }

        /// <summary>
        ///   由末尾向前，获取src的第一个数字（可能由多个字符组成）
        ///   如：fg125abc456，则得出来是"456";fg125abc456fd，则得出来是""
        /// </summary>
        public static string GetDigitEnd(this string self)
        {
            self = self.Trim();
            using (var scope = new StringBuilderScope(self.Length))
            {
                for (var i = self.Length - 1; i >= 0; i--)
                {
                    if (!char.IsDigit(self[i]))
                        break;
                    scope.stringBuilder.Insert(0, self[i]);
                }

                return scope.stringBuilder.ToString();
            }
        }

        /// <summary>
        ///   由末尾向前，获取src的第一个数字（可能由多个字符组成）
        ///   如：fg125abc456，则得出来是456;fg125abc456fd，则得出来是dv
        /// </summary>
        public static long GetDigitEnd(this string self, long defaultValue)
        {
            return GetDigitEnd(self).ToLongOrToDefault(defaultValue);
        }


        public static string ToGuid(this string self, object o)
        {
            return self + o.GetHashCode();
        }

        public static string GetId(this string self)
        {
            var index = self.IndexOf(IdConst.Rid_Infix);
            return index == -1 ? self : self.Substring(0, index);
        }

        public static string GetUITextOfColorAndFontSize(this string self, Color? color = null, int? fontSize = null)
        {
            if (color.HasValue)
            {
                self = self.Replace(StringConst.String_Regex_Text_Color_WarpEnd, StringConst.String_Empty);
                self = Regex.Replace(self, StringConst.String_Regex_Text_Color_WarpStart, StringConst.String_Empty);
                self = string.Format(StringConst.String_Format_Text_Color, color.Value.ToHtmlStringRGBA(), self);
            }

            if (fontSize.HasValue)
            {
                self = self.Replace(StringConst.String_Regex_Text_FontSize_WarpEnd, StringConst.String_Empty);
                self = Regex.Replace(self, StringConst.String_Regex_Text_FontSize_WarpStart, StringConst.String_Empty);
                self = string.Format(StringConst.String_Format_Text_FontSize, fontSize.Value, self);
            }

            return self;
        }

        public static string ReplaceAll(this string s, string pattern, Func<string, string> replaceFunc = null)
        {
            using (var scope = new StringBuilderScope())
            {
                MatchCollection matchCollection = Regex.Matches(s, pattern);
                int lastEndIndex = -1;
                for (int i = 0; i < matchCollection.Count; i++)
                {
                    int startIndex = matchCollection[i].Index;
                    string value = matchCollection[i].Value;
                    int endIndex = startIndex + value.Length - 1;
                    value = replaceFunc == null ? value : replaceFunc(value);
                    scope.stringBuilder.Append(s.Substring(lastEndIndex + 1, startIndex - (lastEndIndex + 1)));
                    scope.stringBuilder.Append(value);
                    lastEndIndex = endIndex;
                }

                if (lastEndIndex != s.Length - 1)
                    scope.stringBuilder.Append(s.Substring(lastEndIndex + 1));
                return scope.stringBuilder.ToString();
            }
        }

        #endregion

        #region 私有函数

        private static long _X2H(string value, int fromBase)
        {
            value = value.Trim();
            if (string.IsNullOrEmpty(value))
                return 0L;
            var constChars = CharConst.DigitsAndCharsBig;
            var digits = new string(constChars, 0, fromBase);
            long result = 0;
            value = value.ToUpper(); // 2


            for (var i = 0; i < value.Length; i++)
            {
                if (!digits.Contains(value[i].ToString()))
                    throw new ArgumentException(string.Format("The argument \"{0}\" is not in {1} system.", value[i],
                        fromBase));
                try
                {
                    result += (long) Math.Pow(fromBase, i) *
                              _GetCharIndex(constChars, value[value.Length - i - 1]); //   2
                }
                catch
                {
                    throw new OverflowException("运算溢出.");
                }
            }

            return result;
        }

        private static int _GetCharIndex(char[] chars, char value)
        {
            for (var i = 0; i < chars.Length; i++)
                if (chars[i] == value)
                    return i;
            return 0;
        }

        #endregion
    }
}