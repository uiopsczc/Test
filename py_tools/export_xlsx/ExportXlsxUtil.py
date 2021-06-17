from export_xlsx.ExportXlsxConst import *
from pythoncat.util.StringUtil import *
from pythoncat.util.DictUtil import *

class ExportXlsxUtil(object):
  @staticmethod
  def IsExportSheet(sheet):
    file_name = sheet.cell(row=ExportXlsxConst.Sheet_CfgName_Cell_Row, column=ExportXlsxConst.Sheet_CfgName_Cell_Column).value
    return file_name.find(ExportXlsxConst.Sheet_CfgName_Tag) != -1

  @staticmethod
  def GetExportSheetName(sheet):
    file_name = sheet.cell(row=ExportXlsxConst.Sheet_CfgName_Cell_Row, column=ExportXlsxConst.Sheet_CfgName_Cell_Column).value
    file_name = file_name.replace(ExportXlsxConst.Sheet_CfgName_Tag, "")
    return file_name

  @staticmethod
  def GetExportSheetFiledInfoList(sheet):
    fieldInfo_list = []
    max_column = sheet.max_column
    for column in range(1, max_column + 1):
      fieldInfo_type = sheet.cell(row=ExportXlsxConst.Sheet_FieldInfo_Type_Row, column=column).value
      fieldInfo_name = sheet.cell(row=ExportXlsxConst.Sheet_FieldInfo_Name_Row, column=column).value
      if StringUtil.IsNoneOrEmpty(fieldInfo_type) or StringUtil.IsNoneOrEmpty(fieldInfo_name):
        continue
      fieldInfo = {}
      fieldInfo["column"] = column
      fieldInfo["type"] = fieldInfo_type.lower()
      fieldInfo["name"] = fieldInfo_name
      fieldInfo["name_chinese"] = sheet.cell(row=ExportXlsxConst.Sheet_FieldInfo_Name_Chinese_Row, column=column).value
      fieldInfo_list.append(fieldInfo)
    return fieldInfo_list

  @staticmethod
  def GetExportTypeDefaultValue(type):
    if type == ExportXlsxConst.Sheet_FieldInfo_Type_Int:
      return 0
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Float:
      return 0.0
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Bool:
      return False
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Array:
      return "[]"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Json:
      return "{}"
    else: #string等
      return ""

  @staticmethod
  def GetExportCsType(type):
    if type == ExportXlsxConst.Sheet_FieldInfo_Type_Int:
      return "int"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Float:
      return "float"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Bool:
      return "bool"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Array:
      return "LitJson.JsonData"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Json:
      return "LitJson.JsonData"
    else: #string等
      return "string"

  @staticmethod
  def GetExportValueOrDefault(value, type):
    if value is None:
      return ExportXlsxUtil.GetExportTypeDefaultValue(type)
    return value

  @staticmethod
  def GetExportSheetIndexDict(sheet):
    index_dict = {}
    max_column = sheet.max_column
    for column in range(1, max_column + 1):
      cell_value = sheet.cell(row=ExportXlsxConst.Sheet_Index_Row, column=column).value
      if StringUtil.IsNoneOrEmpty(cell_value):
        continue
      if cell_value.find(ExportXlsxConst.Sheet_Index_Unique_Tag) != -1:
        keys = cell_value.replace(ExportXlsxConst.Sheet_Index_Unique_Tag,"").split(" ")
        list = DictUtil.GetOrAddDefault(index_dict,ExportXlsxConst.Sheet_Unique_Tag,[])
        index_key_list = []
        for key in keys:
          index_key_list.append(key)
        list.append(index_key_list)
      elif cell_value.find(ExportXlsxConst.Sheet_Index_Multiple_Tag) != -1:
        keys = cell_value.replace(ExportXlsxConst.Sheet_Index_Multiple_Tag, "").split(" ")
        list = DictUtil.GetOrAddDefault(index_dict, ExportXlsxConst.Sheet_Multiple_Tag, [])
        index_key_list = []
        for key in keys:
          index_key_list.append(key)
        list.append(index_key_list)
    return index_dict
