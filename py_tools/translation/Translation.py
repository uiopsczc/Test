import sys
import os
sys.path.append(os.path.dirname(os.path.realpath(__file__ + "/..")))
############作用######################################
## 收集需要切换多语言字符串
## 将收集需要切换多语言字符串写到export_translation_file_path中
######################################################
from pythoncat.util.ExcelUtil import *
from pythoncat.util.FileUtil import *
from pythoncat.util.StringUtil import *

export_translation_file_path = r"..\..\Assets\Excels\D 多语言表-TranslationDefinition.xlsx" #最终输出到问价路径(给策划翻译使用)
# export_translation_file_path = r"F:\WorkSpace\Unity\Test\Assets\cscat\test"
excel_translation_root_dir_path = r"..\..\Assets\Excels"
cs_translation_root_dir_path = r"..\..\Assets"  #cs需要切换多语言字符串的根目录
lua_translation_root_dir_path = r"..\..\Assets\Lua"  #lua需要切换多语言字符串的根目录
cs_string_file_path = r"excel\cs_string.xlsx" #收集到的cs的多语言字符串输出到的文件路径
lua_string_file_path = r"excel\lua_string.xlsx" #收集到的lua的多语言字符串输出到的文件路径
ui_string_file_path = r"excel\ui_string.xlsx"  #收集到的ui的多语言字符串输出到的文件路径
custom_string_file_path = r"excel\custom_string.xlsx"  #收集到的custom(自定义的)的多语言字符串输出到的文件路径
excel_string_file_path = r"excel\excel_string.xlsx"  #收集到的excel的多语言字符串输出到的文件路径

#不用收集文件路径
ignore_translate_file_path_dict = {
  # "\\translation\\Translation.cs":True,
  r"game\define\export":True,
  r"TranslationDefinition.lua.txt":True,
}

#cs文件的收集字符串使用的pattern
cs_match_pattern_list = [
  ("comment0", re.compile(r"//[^\r\n]*",re.S)),
  ("comment1", re.compile(r"/\*.*?\*/",re.S)),
  ("str0", re.compile(r"Translation\.GetText\(\"(.*?)(\"\))(?=([^\"]*\"[^\"]*\")*[^\"]*$)")),#双引号必须成对出现，否则match并不是想要的
]

#lua文件的收集字符串使用的pattern
lua_match_pattern_list = [
  ("comment0", re.compile(r"--(?!\[=*\[)[^\r\n]*",re.S)),
  ("comment1", re.compile(r"--\[\[.*?]]",re.S)),
  ("comment2", re.compile(r"--\[=\[.*?]=]",re.S)),
  ("str0", re.compile(r"global\.Translate\(\"(.*?)(\"\))(?=([^\"]*\"[^\"]*\")*[^\"]*$)")),#双引号必须成对出现，否则match并不是想要的
]

def FilterCSFile(file_path):
  if not file_path.endswith(".cs"):
    return True
  for ignore_translate_file_path in ignore_translate_file_path_dict:
    if file_path.find(ignore_translate_file_path) != -1:
      return True
  return False

def FilterLuaFile(file_path):
  if not file_path.endswith(".lua.txt"):
    return True
  for ignore_translate_file_path in ignore_translate_file_path_dict:
    if file_path.find(ignore_translate_file_path) != -1:
      return True
  return False

def FilterExcelFile(file_path):
  if not file_path.endswith(".xlsx") or os.path.basename(file_path).startswith("~$"):
    return True
  return False

#收集需要转换的字符串
def CollectTranslationIds():
  CollectTranslationId(cs_translation_root_dir_path, FilterCSFile, cs_match_pattern_list, cs_string_file_path)
  CollectTranslationId(lua_translation_root_dir_path, FilterLuaFile, lua_match_pattern_list, lua_string_file_path)
  CollectExcelTranslationId(excel_translation_root_dir_path,FilterExcelFile,excel_string_file_path)

#收集文件中需要转换的字符串
def CollectTranslationId(translation_root_dir_path, FilterFile,match_pattern_list,xx_string_file_path):
  file_path_list = FileUtil.GetFilePathList(translation_root_dir_path, FilterFile)
  # print(file_path_list)
  translation_id_set = set()
  for file_path in file_path_list:
    # if file_path.find("Assets\Lua\luacat\Client.lua.txt") ==-1:
    #   continue
    content = FileUtil.ReadFile(file_path)
    # print(content)
    translation_id_list = GetTranslationIdList(content,match_pattern_list)
    if(len(translation_id_list) == 0):
      continue
    for translation_id in translation_id_list:
      translation_id = StringUtil.Escape(translation_id)
      translation_id_set.add(translation_id)
  # print(translation_id_set)
  line_list = []
  for translation_id in translation_id_set:
    line_list.append([translation_id])
  ExcelUtil.WriteExcelFromLineList(xx_string_file_path, line_list)

#收集Excel文件中需要转换的字符串
def CollectExcelTranslationId(translation_root_dir_path, FilterFile,xx_string_file_path):
  file_path_list = FileUtil.GetFilePathList(translation_root_dir_path, FilterFile)
  # print(file_path_list)
  start_row = 10
  translation_id_set = set()
  for file_path in file_path_list:
    line_list = ExcelUtil.ReadExcelAsLineList(file_path,start_row)
    type_list = line_list[0]
    for column in range(0, len(type_list)): #列
      type = type_list[column]
      if type is None or str.lower(type) != "translation": #只收集type为translation的字段
        continue
      for row in range(1, len(line_list)): #行
        field_value = line_list[row][column]
        if StringUtil.IsNoneOrEmpty(field_value) or field_value == "none":
          continue
        translation_id = field_value
        translation_id_set.add(translation_id)
  line_list = []
  for translation_id in translation_id_set:
    line_list.append([translation_id])
  ExcelUtil.WriteExcelFromLineList(xx_string_file_path, line_list)

#用match_pattern_list收集content中的多语言字符串
def GetTranslationIdList(content, match_pattern_list):
  match_dict = {}
  length = len(content)
  index = 0
  translation_id_list = []
  #先搜索一次
  for (key, pattern) in match_pattern_list:
    match = pattern.search(content)
    match_dict[key] = match

  while True:
    min_match_key = None
    min_match_start_index = length
    for (key, pattern) in match_pattern_list:
      match = match_dict[key]

      if not match:
        continue
      if match.start() < index:
        match = pattern.search(content,index)
        match_dict[key] = match
      if match and match.start() < min_match_start_index:
        min_match_key = key
        min_match_start_index = match.start()
    if min_match_key:
      min_match = match_dict[min_match_key]
      index = min_match.end()
      if min_match_key.startswith("str"):  #匹配到str开头的match_pattern
        pure_str = min_match.group(1)
        translation_id_list.append(pure_str)
        # print(pure_str)
        # print("===========================")
      # print(content[min_match.start():min_match.end()])
      # print("=================================")
    else:
      break
  return translation_id_list

#合并所有的多语言字段
def CombineTranslationIds():
  translation_id_set = set()
  __CombineTranslationIds(translation_id_set, cs_string_file_path)
  __CombineTranslationIds(translation_id_set, lua_string_file_path)
  __CombineTranslationIds(translation_id_set, ui_string_file_path)
  __CombineTranslationIds(translation_id_set, excel_string_file_path)
  __CombineTranslationIds(translation_id_set, custom_string_file_path)
  return translation_id_set

def __CombineTranslationIds(translation_id_set,xx_string_file_path):
  line_list = ExcelUtil.ReadExcelAsLineList(xx_string_file_path)
  for line in line_list:
    translation_id_set.add(line[0])


#将translation_ids导出到export_translation_file_path中
def ExportTranslationIds(translation_ids):
  data_start_row = 11 # 数据开始的行号
  ExcelUtil.ClearExcelEmptyRows(export_translation_file_path, data_start_row) #去掉多余的行
  workbook = load_workbook(export_translation_file_path, data_only=True)#data_only,读取公式的结果，而不是公式本身
  sheet = workbook.worksheets[0]
  row = sheet.max_row + 1
  org_line_list = ExcelUtil.ReadExcelAsLineList(export_translation_file_path)

  translation_dict = {}
  for line in org_line_list:
    translation_id = line[0]
    translation_dict[translation_id] = True

  for translation_id in translation_ids:
    if translation_id in translation_dict:
      continue
    sheet.cell(row, 1).value = translation_id
    translation_dict[translation_id] = True
    row += 1
  workbook.save(export_translation_file_path)

def main():
  CollectTranslationIds()
  translation_id_set = CombineTranslationIds()
  ExportTranslationIds(translation_id_set)
  os.system("explorer /select, " + export_translation_file_path)
  print("finish")
main()
