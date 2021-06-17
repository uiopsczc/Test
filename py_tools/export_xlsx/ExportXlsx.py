import sys
from openpyxl import load_workbook
from export_xlsx.ExportXlsx2Cs import *
from export_xlsx.ExportXlsx2Json import *
from export_xlsx.ExportXlsx2Lua import *
from pythoncat.util.FileUtil import *

sys.path.append(os.path.dirname(os.path.realpath(__file__ + "/..")))

def FilterFilePath(file_path):
  if not file_path.endswith(".xlsx") or file_path.find("~$") != -1: #"~$"临时打开的文件过滤掉
    return True
  return False

#Export所有的文件
def ResetAll():
  ExportXlsx2Cs.ResetAll()
  ExportXlsx2Json.ResetAll()
  ExportXlsx2Lua.ResetAll()

#根据file_path获取需要导出到的文件路径
def GetExportRelativeDirPath(file_path):
  relative_dir_path = file_path.replace(ExportXlsxConst.Xlsx_Dir_Path, "")
  slash_start_index = relative_dir_path.rfind("\\")
  if slash_start_index == -1:
    result = ""
  else:
    result = relative_dir_path[0:slash_start_index+1]
  return result

#Export所有的文件
def ExportAll():
  file_path_list = FileUtil.GetFilePathList(ExportXlsxConst.Xlsx_Dir_Path, FilterFilePath)
  for file_path in file_path_list:
    export_relative_dir_path = GetExportRelativeDirPath(file_path)
    wrokbook = load_workbook(file_path, read_only=True, data_only=True)
    ExportWorkbook(wrokbook, export_relative_dir_path)

def ExportWorkbook(wrokbook,export_relative_dir_path):
  sheet_count = len(wrokbook.sheetnames)
  for sheet_index in range(0, sheet_count):
    sheet = wrokbook.worksheets[sheet_index]
    ExportSheet(sheet, export_relative_dir_path)

def ExportSheet(sheet,export_relative_dir_path):
  if ExportXlsxUtil.IsExportSheet(sheet):
    json_dict = ExportXlsx2Json.ExportSheet(sheet, export_relative_dir_path)
    ExportXlsx2Cs.ExportSheet(sheet,json_dict, export_relative_dir_path)
    ExportXlsx2Lua.ExportSheet(sheet, export_relative_dir_path)


def main():
  ResetAll()
  ExportAll()
  # print("%s -- %s {set;get;}"%("1", 2))
  print("finished")
main()