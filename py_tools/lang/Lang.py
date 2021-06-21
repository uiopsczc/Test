############作用######################################
## 收集需要切换多语言字符串
## 将收集需要切换多语言字符串写到export_lang_file_path中
######################################################
import sys
import os
sys.path.append(os.path.dirname(os.path.realpath(__file__ + "/..")))#一定要放到这里（值为py_tools文件夹），不然会import不到module的
from lang.LangConst import *
from pythoncat.util.ExcelUtil import *
from pythoncat.util.FileUtil import *
from pythoncat.util.StringUtil import *
from export_xlsx.ExportXlsxConst import *
from export_xlsx.ExportXlsxUtil import *

sys.path.append(os.path.dirname(os.path.realpath(__file__ + "/..")))

#收集需要转换的字符串
def CollectLangIds():
  lang_id_set = set()
  CollectLangId(LangConst.Cs_Lang_Root_Dir_Path, FilterCSFile, LangConst.Cs_Match_Pattern_List, lang_id_set)
  CollectLangId(LangConst.Lua_Lang_Root_Dir_Path, FilterLuaFile, LangConst.Lua_Match_Pattern_List, lang_id_set)
  CollectExcelLangId(LangConst.Excel_Lang_Root_Dir_Path, FilterExcelFile, lang_id_set)
  CollectPythonExcelLangIds(lang_id_set, LangConst.UI_String_File_Path)
  CollectPythonExcelLangIds(lang_id_set, LangConst.Custom_String_File_Path)
  return lang_id_set

def FilterCSFile(file_path):
  if not file_path.endswith(".cs"):
    return True
  for ignore_lang_file_path in LangConst.Ignore_Lang_File_Path_Dict:
    if file_path.find(ignore_lang_file_path) != -1:
      return True
  return False

def FilterLuaFile(file_path):
  if not file_path.endswith(".lua.txt"):
    return True
  for ignore_lang_file_path in LangConst.Ignore_Lang_File_Path_Dict:
    if file_path.find(ignore_lang_file_path) != -1:
      return True
  return False

def FilterExcelFile(file_path):
  if not file_path.endswith(".xlsx") or os.path.basename(file_path).startswith("~$"):
    return True
  return False

#收集文件中需要转换的字符串
def CollectLangId(lang_root_dir_path, FilterFile, match_pattern_list, lang_id_set):
  file_path_list = FileUtil.GetFilePathList(lang_root_dir_path, FilterFile)
  # print(file_path_list)
  for file_path in file_path_list:
    # if file_path.find("Assets\Lua\luacat\Client.lua.txt") ==-1:
    #   continue
    content = FileUtil.ReadFile(file_path)
    # print(content)
    lang_id_list = GetLangIdList(content, match_pattern_list)
    if(len(lang_id_list) == 0):
      continue
    for lang_id in lang_id_list:
      lang_id = StringUtil.Escape(lang_id)
      lang_id_set.add(lang_id)
  # print(lang_id_set)

#收集Excel文件中需要转换的字符串
def CollectExcelLangId(lang_root_dir_path, FilterFile, lang_id_set):
  file_path_list = FileUtil.GetFilePathList(lang_root_dir_path, FilterFile)
  # print(file_path_list)
  fieldInfo_type_row = ExportXlsxConst.Sheet_FieldInfo_Type_Row
  data_start_row = ExportXlsxConst.Sheet_Data_Start_Row
  for file_path in file_path_list:
    wrokbook = load_workbook(file_path, read_only=True, data_only=True)
    sheet_count = len(wrokbook.sheetnames)
    for sheet_index in range(0, sheet_count):
      sheet = wrokbook.worksheets[sheet_index]
      if ExportXlsxUtil.IsExportSheet(sheet):
        fieldInfo_type_line = ExcelUtil.ReadExcelAsLine(sheet, fieldInfo_type_row)
        data_line_list = ExcelUtil.ReadExcelAsLineList(sheet, data_start_row)
        for column in range(0, len(fieldInfo_type_line)):  # 列
          type = fieldInfo_type_line[column]
          if type is None or str.lower(type) != ExportXlsxConst.Sheet_FieldInfo_Type_Lang:  # 只收集type为lang的字段
            continue
          for row in range(0, len(data_line_list)):  # 行
            field_value = data_line_list[row][column]
            if field_value is None or StringUtil.IsNoneOrEmpty(field_value) or field_value == "none":
              continue
            lang_id = field_value
            lang_id_set.add(lang_id)

#用match_pattern_list收集content中的多语言字符串
def GetLangIdList(content, match_pattern_list):
  match_dict = {}
  length = len(content)
  index = 0
  lang_id_list = []
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
        lang_id_list.append(pure_str)
        # print(pure_str)
        # print("===========================")
      # print(content[min_match.start():min_match.end()])
      # print("=================================")
    else:
      break
  return lang_id_list



def CollectPythonExcelLangIds(lang_id_set, xx_string_file_path):
  line_list = ExcelUtil.ReadExcelAsLineListFromFilePath(xx_string_file_path)
  for line in line_list:
    lang_id_set.add(line[0])


#将lang_ids导出到export_lang_file_path中
def ExportLangIds(lang_ids):
  data_start_row = ExportXlsxConst.Sheet_Data_Start_Row # 数据开始的行号
  workbook = load_workbook(LangConst.Export_Lang_File_Path, data_only=True)#data_only,读取公式的结果，而不是公式本身
  sheet = workbook.worksheets[0]
  ExcelUtil.ClearExcelEmptyRows(sheet,data_start_row)
  row = sheet.max_row + 1
  org_line_list = ExcelUtil.ReadExcelAsLineList(sheet, data_start_row)

  lang_dict = {}
  for line in org_line_list:
    lang_id = line[0]
    lang_dict[lang_id] = True

  for lang_id in lang_ids:
    if lang_id in lang_dict:
      continue
    sheet.cell(row, 1).value = lang_id
    lang_dict[lang_id] = True
    row += 1
  workbook.save(LangConst.Export_Lang_File_Path)

def main():
  lang_id_set = CollectLangIds()
  ExportLangIds(lang_id_set)
  os.system("explorer /select, " + LangConst.Export_Lang_File_Path)
  print("Lang Export finished")
main()
