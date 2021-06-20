using System.Collections.Generic;

namespace CsCat
{
  public class LangConst
  {
    public static string Json_File_Path= "Assets/Cfg/lang.json";
    private static Dictionary<char, bool> _char_exclude_dict;

    public static Dictionary<char, bool> char_exclude_dict
    {
      get
      {
        if (_char_exclude_dict == null)
        {
          _char_exclude_dict = new Dictionary<char, bool>();
          _char_exclude_dict[' '] = true;
          _char_exclude_dict['0'] = true;
          _char_exclude_dict['1'] = true;
          _char_exclude_dict['2'] = true;
          _char_exclude_dict['3'] = true;
          _char_exclude_dict['4'] = true;
          _char_exclude_dict['5'] = true;
          _char_exclude_dict['6'] = true;
          _char_exclude_dict['7'] = true;
          _char_exclude_dict['8'] = true;
          _char_exclude_dict['9'] = true;
          // 英文符号
          _char_exclude_dict[','] = true;
          _char_exclude_dict['<'] = true;
          _char_exclude_dict['.'] = true;
          _char_exclude_dict['>'] = true;
          _char_exclude_dict['/'] = true;
          _char_exclude_dict['?'] = true;
          _char_exclude_dict[';'] = true;
          _char_exclude_dict[':'] = true;
          _char_exclude_dict['\''] = true;
          _char_exclude_dict['"'] = true;
          _char_exclude_dict['['] = true;
          _char_exclude_dict['{'] = true;
          _char_exclude_dict[']'] = true;
          _char_exclude_dict['}'] = true;
          _char_exclude_dict['\\'] = true;
          _char_exclude_dict['|'] = true;
          _char_exclude_dict['`'] = true;
          _char_exclude_dict['~'] = true;
          _char_exclude_dict['!'] = true;
          _char_exclude_dict['@'] = true;
          _char_exclude_dict['#'] = true;
          _char_exclude_dict['$'] = true;
          _char_exclude_dict['%'] = true;
          _char_exclude_dict['^'] = true;
          _char_exclude_dict['&'] = true;
          _char_exclude_dict['*'] = true;
          _char_exclude_dict['('] = true;
          _char_exclude_dict[')'] = true;
          _char_exclude_dict['-'] = true;
          _char_exclude_dict['_'] = true;
          _char_exclude_dict['='] = true;
          _char_exclude_dict['+'] = true;
          //中文符号
          _char_exclude_dict['，'] = true;
          _char_exclude_dict['《'] = true;
          _char_exclude_dict['。'] = true;
          _char_exclude_dict['》'] = true;
          _char_exclude_dict['、'] = true;
          _char_exclude_dict['？'] = true;
          _char_exclude_dict['；'] = true;
          _char_exclude_dict['：'] = true;
          _char_exclude_dict['‘'] = true;
          _char_exclude_dict['’'] = true;
          _char_exclude_dict['“'] = true;
          _char_exclude_dict['”'] = true;
          _char_exclude_dict['【'] = true;
          _char_exclude_dict['｛'] = true;
          _char_exclude_dict['】'] = true;
          _char_exclude_dict['｝'] = true;
          _char_exclude_dict['、'] = true;
          _char_exclude_dict['|'] = true;
          _char_exclude_dict['·'] = true;
          _char_exclude_dict['~'] = true;
          _char_exclude_dict['！'] = true;
          _char_exclude_dict['@'] = true;
          _char_exclude_dict['#'] = true;
          _char_exclude_dict['￥'] = true;
          _char_exclude_dict['%'] = true;
          _char_exclude_dict['…'] = true;
          _char_exclude_dict['&'] = true;
          _char_exclude_dict['*'] = true;
          _char_exclude_dict['（'] = true;
          _char_exclude_dict['）'] = true;
          _char_exclude_dict['-'] = true;
          _char_exclude_dict['—'] = true;
          _char_exclude_dict['='] = true;
          _char_exclude_dict['+'] = true;
        }

        return _char_exclude_dict;
      }
    }
  }
}