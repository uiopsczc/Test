import re
class LangConst(object):
  Export_Lang_File_Path = r"..\..\Excels\YY语言表.xlsx"  # 最终输出到文件路径(给策划翻译使用)
  Excel_Lang_Root_Dir_Path = r"../../Excels"
  Cs_Lang_Root_Dir_Path = r"../../Assets"  # cs需要切换多语言字符串的根目录
  Lua_Lang_Root_Dir_Path = r"../../Assets/Lua"  # lua需要切换多语言字符串的根目录
  UI_String_File_Path = r"excel\ui_string.xlsx"  # 收集到的ui的多语言字符串输出到的文件路径
  Custom_String_File_Path = r"excel\custom_string.xlsx"  # 收集到的custom(自定义的)的多语言字符串输出到的文件路径

  # 不用收集文件路径
  Ignore_Lang_File_Path_Dict = {
    r"game\Cfg\AutoGen": True,
    r"CfgLang.lua.txt": True,
  }

  # cs文件的收集字符串使用的pattern
  Cs_Match_Pattern_List = [
    ("comment0", re.compile(r"//[^\r\n]*", re.S)),
    ("comment1", re.compile(r"/\*.*?\*/", re.S)),
    ("str0", re.compile(r"Lang\.GetText\(\"(.*?)(\"[\),])(?=([^\"]*\"[^\"]*\")*[^\"]*$)")),
    # 双引号必须成对出现，否则match并不是想要的
  ]

  # lua文件的收集字符串使用的pattern
  Lua_Match_Pattern_List = [
    ("comment0", re.compile(r"--(?!\[=*\[)[^\r\n]*", re.S)),
    ("comment1", re.compile(r"--\[\[.*?]]", re.S)),
    ("comment2", re.compile(r"--\[=\[.*?]=]", re.S)),
    ("str0", re.compile(r"global\.Lang.GetText\(\"(.*?)(\"[\),])(?=([^\"]*\"[^\"]*\")*[^\"]*$)")),  # 双引号必须成对出现，否则match并不是想要的
  ]
