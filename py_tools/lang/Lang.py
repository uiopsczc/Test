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
  langIdSet = set()
  CollectLangId(LangConst.Cs_Lang_Root_Dir_Path, FilterCSFile, LangConst.Cs_Match_Pattern_List, langIdSet)
  CollectLangId(LangConst.Lua_Lang_Root_Dir_Path, FilterLuaFile, LangConst.Lua_Match_Pattern_List, langIdSet)
  CollectExcelLangId(LangConst.Excel_Lang_Root_Dir_Path, FilterExcelFile, langIdSet)
  CollectPythonExcelLangIds(langIdSet, LangConst.UI_String_File_Path)
  CollectPythonExcelLangIds(langIdSet, LangConst.Custom_String_File_Path)
  return langIdSet

def FilterCSFile(filePath):
  if not filePath.endswith(".cs"):
    return True
  for ignoreLangFilePath in LangConst.Ignore_Lang_File_Path_Dict:
    if filePath.find(ignoreLangFilePath) != -1:
      return True
  return False

def FilterLuaFile(filePath):
  if not filePath.endswith(".lua.txt"):
    return True
  for ignoreLangFilePath in LangConst.Ignore_Lang_File_Path_Dict:
    if filePath.find(ignoreLangFilePath) != -1:
      return True
  return False

def FilterExcelFile(filePath):
  if not filePath.endswith(".xlsx") or os.path.basename(filePath).startswith("~$"):
    return True
  return False

#收集文件中需要转换的字符串
def CollectLangId(langRootDirPath, FilterFile, matchPatternList, langIdSet):
  filePathList = FileUtil.GetFilePathList(langRootDirPath, FilterFile)
  # print(file_path_list)
  for filePath in filePathList:
    # if file_path.find("Assets\Lua\luacat\Client.lua.txt") ==-1:
    #   continue
    content = FileUtil.ReadFile(filePath)
    # print(content)
    langIdList = GetLangIdList(content, matchPatternList)
    if len(langIdList) == 0:
      continue
    for langId in langIdList:
      langId = StringUtil.Escape(langId)
      langIdSet.add(langId)
  # print(lang_id_set)

#收集Excel文件中需要转换的字符串
def CollectExcelLangId(langRootDirPath, FilterFile, langIdSet):
  filePathList = FileUtil.GetFilePathList(langRootDirPath, FilterFile)
  # print(file_path_list)
  fieldInfoTypeRowIndex = ExportXlsxConst.Sheet_FieldInfo_Type_Row
  dataStartRowIndex = ExportXlsxConst.Sheet_Data_Start_Row
  for filePath in filePathList:
    workbook = load_workbook(filePath, read_only=True, data_only=True)
    sheetCount = len(workbook.sheetnames)
    for sheetIndex in range(0, sheetCount):
      sheet = workbook.worksheets[sheetIndex]
      if ExportXlsxUtil.IsExportSheet(sheet):
        fieldInfoTypeLine = ExcelUtil.ReadExcelAsLine(sheet, fieldInfoTypeRowIndex)
        dataLineList = ExcelUtil.ReadExcelAsLineList(sheet, dataStartRowIndex)
        for columnIndex in range(0, len(fieldInfoTypeLine)):  # 列
          type = fieldInfoTypeLine[columnIndex]
          if type is None or str.lower(type) != ExportXlsxConst.Sheet_FieldInfo_Type_Lang:  # 只收集type为lang的字段
            continue
          for rowIndex in range(0, len(dataLineList)):  # 行
            fieldValue = dataLineList[rowIndex][columnIndex]
            if fieldValue is None or StringUtil.IsNoneOrEmpty(fieldValue) or fieldValue == "none":
              continue
            langId = fieldValue
            langIdSet.add(langId)

#用match_pattern_list收集content中的多语言字符串
def GetLangIdList(content, matchPatternList):
  matchDict = {}
  length = len(content)
  index = 0
  langIdList = []
  #先搜索一次
  for (key, pattern) in matchPatternList:
    match = pattern.search(content)
    matchDict[key] = match

  while True:
    minMatchKey = None
    minMatchStartIndex = length
    for (key, pattern) in matchPatternList:
      match = matchDict[key]

      if not match:
        continue
      if match.start() < index:
        match = pattern.search(content,index)
        matchDict[key] = match
      if match and match.start() < minMatchStartIndex:
        minMatchKey = key
        minMatchStartIndex = match.start()
    if minMatchKey:
      minMatch = matchDict[minMatchKey]
      index = minMatch.end()
      if minMatchKey.startswith("str"):  #匹配到str开头的match_pattern
        pure_str = minMatch.group(1)
        langIdList.append(pure_str)
        # print(pure_str)
        # print("===========================")
      # print(content[min_match.start():min_match.end()])
      # print("=================================")
    else:
      break
  return langIdList



def CollectPythonExcelLangIds(langIdSet, xxStringFilePath):
  lineList = ExcelUtil.ReadExcelAsLineListFromFilePath(xxStringFilePath)
  for line in lineList:
    langIdSet.add(line[0])


#将lang_ids导出到export_lang_file_path中
def ExportLangIds(langIds):
  dataStartRowIndex = ExportXlsxConst.Sheet_Data_Start_Row # 数据开始的行号
  workbook = load_workbook(LangConst.Export_Lang_File_Path, data_only=True)#data_only,读取公式的结果，而不是公式本身
  sheet = workbook.worksheets[0]
  ExcelUtil.ClearExcelEmptyRows(sheet, dataStartRowIndex)
  row = sheet.max_row + 1
  orgLineList = ExcelUtil.ReadExcelAsLineList(sheet, dataStartRowIndex)

  langDict = {}
  for line in orgLineList:
    langId = line[0]
    langDict[langId] = True

  for langId in langIds:
    if langId in langDict:
      continue
    sheet.cell(row, 1).value = langId
    langDict[langId] = True
    row += 1
  workbook.save(LangConst.Export_Lang_File_Path)

def main():
  langIdSet = CollectLangIds()
  ExportLangIds(langIdSet)
  os.system("explorer /select, " + LangConst.Export_Lang_File_Path)
  print("Lang Export finished")
main()
