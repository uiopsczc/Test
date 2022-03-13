from export_xlsx.ExportXlsxConst import *


class SheetCfgFieldInfoDict(object):
  _sheet = None
  _fieldInfoDict = {}

  def __init__(self, sheet):
    self._sheet = sheet
    self.Parse()

  def Parse(self):
    for rowIndex in range(1, ExportXlsxConst.Sheet_Cfg_FieldName_Max_Row_Index + 1):
      curValue = self._sheet.cell(row=rowIndex, column=ExportXlsxConst.Sheet_Cfg_FieldName_Column_Index).value
      if curValue is None:
        continue
      isContainsKey = ExportXlsxConst.Sheet_Cfg_FieldNameDict.get(curValue)
      if isContainsKey:
        self._fieldInfoDict[curValue] = rowIndex

  def GetRowIndexOfIsOutput(self):
    return self._fieldInfoDict[ExportXlsxConst.FieldName_Sheet_Cfg_IsOutput]

  def GetRowIndexOfTableName(self):
    return self._fieldInfoDict[ExportXlsxConst.FieldName_Sheet_Cfg_TableName]

  def GetRowIndexOfOutputDir(self):
    return self._fieldInfoDict[ExportXlsxConst.FieldName_Sheet_Cfg_OutputDir]

  def GetRowIndexOfUniqueIndexesList(self):
    return self._fieldInfoDict[ExportXlsxConst.FieldName_Sheet_Cfg_UniqueIndexesList]

  def GetRowIndexOfMultiplyIndexesList(self):
    return self._fieldInfoDict[ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList]

  def GetRowIndexOfHeadCommentRowIndex(self):
    return self._fieldInfoDict[ExportXlsxConst.FieldName_Sheet_Cfg_HeadCommentRowIndex]

  def GetRowIndexOfHeadRowIndex(self):
    return self._fieldInfoDict[ExportXlsxConst.FieldName_Sheet_Cfg_HeadRowIndex]

  def GetRowIndexOfDataStartRowIndex(self):
    return self._fieldInfoDict[ExportXlsxConst.FieldName_Sheet_Cfg_DataStartRowIndex]