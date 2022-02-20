import sys
import os

sys.path.append(os.path.dirname(os.path.realpath(__file__ + "/..")))  # 一定要放到这里（值为py_tools文件夹），不然会import不到module的
from openpyxl import load_workbook
from export_xlsx.ExportXlsx2Cs import *
from export_xlsx.ExportXlsx2Json import *
from export_xlsx.ExportXlsx2Lua import *
from export_xlsx.ExportXlsxConst import *
from export_xlsx.ExportXlsxUtil import *
from pythoncat.util.FileUtil import *


def FilterFilePath(file_path):
  if not file_path.endswith(".xlsx") or file_path.find("~$") != -1:  # "~$"临时打开的文件过滤掉
    return True
  return False


# Export所有的文件
def ResetAll():
  ExportXlsx2Cs.ResetAll()
  ExportXlsx2Json.ResetAll()
  ExportXlsx2Lua.ResetAll()


# 根据file_path获取需要导出到的文件路径
def GetExportRelativeDirPath(file_path):
  relative_dir_path = file_path.replace(ExportXlsxConst.Xlsx_Dir_Path, "")
  slash_start_index = relative_dir_path.rfind("\\")
  if slash_start_index == -1:
    result = ""
  else:
    result = relative_dir_path[0:slash_start_index + 1]
  return result


# Export所有的文件
def ExportAll():
  file_path_list = FileUtil.GetFilePathList(ExportXlsxConst.Xlsx_Dir_Path, FilterFilePath)
  for file_path in file_path_list:
    export_relative_file_path = file_path.replace(ExportXlsxConst.Xlsx_Dir_Path, "")
    print("正在导出%s" % export_relative_file_path)
    export_relative_dir_path = GetExportRelativeDirPath(file_path)
    workbook = load_workbook(file_path, read_only=True, data_only=True)
    ExportWorkbook(workbook, export_relative_dir_path, export_relative_file_path)


def ExportWorkbook(wrokbook, export_relative_dir_path, export_relative_file_path):
  sheet_count = len(wrokbook.sheetnames)
  for sheet_index in range(0, sheet_count):
    sheet = wrokbook.worksheets[sheet_index]
    ExportSheet(sheet, export_relative_dir_path, export_relative_file_path)


def ExportSheet(sheet, export_relative_dir_path, export_relative_file_path):
  if ExportXlsxUtil.IsExportSheet(sheet):
    print("  正在导出[%s]" % (export_relative_dir_path + ExportXlsxUtil.GetExportSheetName(sheet)))
    json_dict = ExportXlsx2Json.ExportSheet(sheet, export_relative_dir_path, export_relative_file_path)
    ExportXlsx2Cs.ExportSheet(sheet,json_dict, export_relative_dir_path, export_relative_file_path)
    ExportXlsx2Lua.ExportSheet(sheet, json_dict, export_relative_dir_path, export_relative_file_path)


def main():
  ResetAll()
  ExportAll()
  print("ExportXlsx finished")


main()
