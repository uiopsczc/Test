from export_xlsx.ExportXlsxUtil import *
from export_xlsx.ExportXlsxConst import *
from pythoncat.util.FileUtil import *


class ExportXlsx2Lua(object):
  @staticmethod
  def ResetAll():
    FileUtil.RemoveDir(ExportXlsxConst.Export_2_Lua_Dir_Path)


  @staticmethod
  def ExportSheet(sheet, export_relative_dir_path):
    export_file_path = ExportXlsxConst.Export_2_Lua_Dir_Path + export_relative_dir_path + ExportXlsxUtil.GetExportSheetName(sheet)
