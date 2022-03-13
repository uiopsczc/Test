import json
import types

from export_xlsx.ExportXlsxConst import *
from pythoncat.util.StringUtil import *
from pythoncat.util.DictUtil import *
from pythoncat.util.NumberUtil import *


class ExportXlsxUtil(object):
  @staticmethod
  def IsCellValueNone(cellValue):
    if cellValue is None:
      return True
    return False

  @staticmethod
  def GetCellValue(sheet, rowIndex, columnIndex, backupColumnIndex):
    cellValue = sheet.cell(row=rowIndex, column=columnIndex).value
    if ExportXlsxUtil.IsCellValueNone(cellValue):
      cellValue = sheet.cell(row=rowIndex, column=backupColumnIndex).value
    return cellValue

  @staticmethod
  def IsExportSheet(sheet, languageType):
    columnIndex = ExportXlsxUtil.GetSheetCfgColumnIndexByLanguageType(sheet, languageType)
    if not columnIndex:
      return False
    return ExportXlsxUtil.GetSheetCfgIsOutput(sheet, languageType)

  @staticmethod
  def GetSheetCfgColumnIndexByLanguageType(sheet, languageType):
    for columnIndex in range(1, ExportXlsxConst.Sheet_Cfg_FieldName_Max_Column_Index + 1):
      curLanguageType = sheet.cell(row=ExportXlsxConst.Sheet_Export_Language_Type_Row_Index, column=columnIndex).value
      if curLanguageType.lower() == languageType.lower():
        return columnIndex
    return None

  @staticmethod
  def GetSheetCfgFieldNameRowIndex(sheet, fieldName):
    for rowIndex in range(1, ExportXlsxConst.Sheet_Cfg_FieldName_Max_Row_Index + 1):
      curValue = sheet.cell(row=rowIndex, column=ExportXlsxConst.Sheet_Cfg_FieldName_Column_Index).value
      if curValue.lower() == fieldName.lower():
        return rowIndex
    return None

  @staticmethod
  def GetSheetCfgFieldValue(sheet, languageType, fieldName):
    columnIndex = ExportXlsxUtil.GetSheetCfgColumnIndexByLanguageType(sheet, languageType)
    if not columnIndex:
      return None
    return ExportXlsxUtil.GetSheetCfgFieldValueWithColumnIndex(sheet, fieldName)





  @staticmethod
  def GetSheetCfgDataStartRowIndex(sheet, languageType):
    return ExportXlsxUtil.GetSheetCfgFieldValue(sheet, languageType, ExportXlsxConst.FieldName_Sheet_Cfg_DataStartRowIndex)

  @staticmethod
  def GetSheetCfgFieldInfoType(value):
    if StringUtil.IsNoneOrEmpty(value):
      return None
    index1 = value.find(ExportXlsxConst.Sheet_Cfg_FieldInfoType_Left_Wrap_Char)
    if index1 == -1:
      return None
    index2 = value.find(ExportXlsxConst.Sheet_Cfg_FieldInfoType_Right_Wrap_Char)
    if index2 == -1:
      return None
    return value[index1 + 1:index2]

  @staticmethod
  def GetSheetCfgFieldInfoName(value):
    if StringUtil.IsNoneOrEmpty(value):
      return None
    index1 = value.find(ExportXlsxConst.Sheet_Cfg_FieldInfoType_Left_Wrap_Char)
    if index1 == -1:
      return None
    return value[:index1]

  @staticmethod
  def GetExportSheetFiledInfoList(sheet, languageType):
    fieldInfoList = []
    headCommentRowIndex = ExportXlsxUtil.GetSheetCfgHeadCommentRowIndex(languageType)
    headRowIndex = ExportXlsxUtil.GetSheetCfgHeadRowIndex(languageType)
    maxColumnIndex = sheet.max_column
    for columnIndex in range(1, maxColumnIndex + 1):
      headValue = sheet.cell(row=headRowIndex, column=columnIndex).value
      fieldInfoType = ExportXlsxUtil.GetSheetCfgFieldInfoType(headValue)
      if not fieldInfoType:
        continue
      fieldInfoName = ExportXlsxUtil.GetSheetCfgFieldInfoName(headValue)
      if not fieldInfoName:
        continue
      fieldInfo = {}
      fieldInfo["columnIndex"] = columnIndex
      fieldInfo["type"] = fieldInfoType
      fieldInfo["name"] = fieldInfoName
      fieldInfo["comment"] = sheet.cell(row=headCommentRowIndex, column=columnIndex).value
      fieldInfoList.append(fieldInfo)
    return fieldInfoList

  @staticmethod
  def GetExportSheetFiledInfoDict(sheet):
    fieldInfoList = ExportXlsxUtil.GetExportSheetFiledInfoList(sheet)
    fieldInfoDict = {}
    for fieldInfo in fieldInfoList:
      fieldInfoDict[fieldInfo["name"]] = fieldInfo
    return fieldInfoDict

  @staticmethod
  def GetExportJsonTypeDefaultValue(cell, row, column, type):
    if type == ExportXlsxConst.Sheet_FieldInfo_Type_Int:
      return 0
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Float:
      return 0.0
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Bool:
      return False
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_String:
      return ""
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Lang:
      return ""
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Array:
      return "[]"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Json:
      return "{}"
    elif type.endswith(ExportXlsxConst.Sheet_FieldInfo_Type_Ends_With_Array):
      return "[]"
    elif type.startswith(ExportXlsxConst.Sheet_FieldInfo_Type_Starts_With_Dict):
      return "{}"
    else:
      raise Exception(("error:cell[%s,%s] is not define default value for %s") % (row, column, type))


  # 是否是 特殊的Cs Type
  @staticmethod
  def IsSpecialCsType(type):
    if type.endswith(ExportXlsxConst.Sheet_FieldInfo_Type_Ends_With_Array):
      return True
    elif type.startswith(ExportXlsxConst.Sheet_FieldInfo_Type_Starts_With_Dict):
      return True
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Lang:
      return True
    else:
      return False

  #特殊的Cs Type
  @staticmethod
  def GetSpecialCsType(type):
    if type == ExportXlsxConst.Sheet_FieldInfo_Type_Int:
      return "int"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Float:
      return "float"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Bool:
      return "bool"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_String:
      return "string"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Lang:
      return "string"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Array:
      return "LitJson.JsonData"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Json:
      return "LitJson.JsonData"
    elif type.endswith(ExportXlsxConst.Sheet_FieldInfo_Type_Ends_With_Array):
      sub_type = type[0:-2]
      return "%s[]" % (ExportXlsxUtil.GetSpecialCsType(sub_type))
    elif type.startswith(ExportXlsxConst.Sheet_FieldInfo_Type_Starts_With_Dict):
      sub_type = type[4:]
      pos = sub_type.index(",")
      sub_key_type = sub_type[1:pos]
      sub_value_type = sub_type[pos + 1:-1]
      return "Dictionary<%s,%s>" % (
      ExportXlsxUtil.GetSpecialCsType(sub_key_type), ExportXlsxUtil.GetSpecialCsType(sub_value_type))
    else:
      raise Exception("not define Special CsType for %s"%(type))



  @staticmethod
  def GetExportCsType(type):
    if type == ExportXlsxConst.Sheet_FieldInfo_Type_Int:
      return "int"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Float:
      return "float"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Bool:
      return "bool"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_String:
      return "string"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Lang:
      return "string"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Array:
      return "LitJson.JsonData"
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Json:
      return "LitJson.JsonData"
    elif type.endswith(ExportXlsxConst.Sheet_FieldInfo_Type_Ends_With_Array):
      return "LitJson.JsonData"
    elif type.startswith(ExportXlsxConst.Sheet_FieldInfo_Type_Starts_With_Dict):
      return "LitJson.JsonData"
    else:
      raise Exception("not define CsType for %s"%(type))

  @staticmethod
  def GetExportLuaValueOrDefault(value, type):
    if type == ExportXlsxConst.Sheet_FieldInfo_Type_Array:
      return "json:decode([=[%s]=])"%(json.dumps(value,ensure_ascii=False))
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Json:
      return "json:decode([=[%s]=])"%(json.dumps(value,ensure_ascii=False))
    elif type.endswith(ExportXlsxConst.Sheet_FieldInfo_Type_Ends_With_Array):
      return "json:decode([=[%s]=])"%(json.dumps(value,ensure_ascii=False))
    elif type.startswith(ExportXlsxConst.Sheet_FieldInfo_Type_Starts_With_Dict):
      return "json:decode([=[%s]=])"%(json.dumps(value,ensure_ascii=False))
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_String:
      return "[=[%s]=]"%(value)
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Lang:
      return "[=[%s]=]"%(str(value).replace("{0}","%s").replace("{1}","%s").replace("{2}","%s").replace("{3}","%s").replace("{4}","%s").replace("{5}","%s"))
    elif type == ExportXlsxConst.Sheet_FieldInfo_Type_Bool:
      if value:
        return "true"
      else:
        return "false"
    else:
      return value

  @staticmethod
  def IsStringType(type):
    if type == ExportXlsxConst.Sheet_FieldInfo_Type_String or type == ExportXlsxConst.Sheet_FieldInfo_Type_Lang:
      return True
    return False

  @staticmethod
  def GetExportJsonValueOrDefault(sheet, row, column, targetType):
    cell = sheet.cell(row=row, column=column)
    if cell.value is None:
      return ExportXlsxUtil.GetExportJsonTypeDefaultValue(cell, row, column, targetType)
    # if ExportXlsxUtil.IsStringType(targetType) and (NumberUtil.IsNumber(cell.value)):
    #   return cell.value
    return cell.value