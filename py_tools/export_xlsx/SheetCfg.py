from export_xlsx.ExportXlsxUtil import *
from export_xlsx.SheetCfgFieldInfoDict import *


class SheetCfg(object):
  _languageType = None
  _sheet = None
  _isOutput = False
  _isExportSheet = False
  _tableName = None
  _outputDir = None
  _uniqueIndexesList = None
  _multiplyIndexesList = None
  _headCommentRowIndex = None
  _languageTypeColumnIndex = None
  _headRowIndex = None
  _dataStartRowIndex = None
  _fieldInfoList = None
  _indexDict = None
  _fieldInfoDict = None
  _cfgIndexDataName = None
  _cfgDataName = None
  _cfgRootName = None

  def __init__(self, sheet, languageType, sheetCfgFieldInfoDict):
    self._sheet = sheet
    self._languageType = languageType
    self._sheetCfgFieldInfoDict = sheetCfgFieldInfoDict
    self.Parse()

  def Parse(self):
    self._languageTypeColumnIndex = self._GetSheetCfgColumnIndexByLanguageType()
    if not self._languageTypeColumnIndex:
      return
    self._isOutput = self._GetSheetCfgIsOutput()
    if not self._isOutput:
      return
    self._tableName = self._GetSheetCfgTableName()
    self._outputDir = self._GetSheetCfgOutputDir()
    self._uniqueIndexesList = self._GetSheetCfgUniqueIndexesList()
    self._multiplyIndexesList = self._GetSheetCfgMultiplyIndexesList()
    self._headCommentRowIndex = self._GetSheetCfgHeadCommentRowIndex()
    self._headRowIndex = self._GetSheetCfgHeadRowIndex()
    self._dataStartRowIndex = self._GetSheetCfgDataStartRowIndex()
    self._fieldInfoList = self._GetFieldInfoList()
    self._indexDict = self._GetIndexDict()
    self._fieldInfoDict = self._GetFieldInfoDict()
    self._cfgIndexDataName = self._GetCfgIndexDataName()
    self._cfgDataName = self._GetCfgDataName()
    self._cfgRootName = self._GetCfgRootName()

  def _GetSheetCfgFieldValue(self, fieldInfoRowIndex):
    return ExportXlsxUtil.GetCellValue(self._sheet, fieldInfoRowIndex, self._languageTypeColumnIndex, ExportXlsxConst.Sheet_Cfg_FieldName_Default_Column_Index)

  def _GetSheetCfgColumnIndexByLanguageType(self):
    for columnIndex in range(1, ExportXlsxConst.Sheet_Cfg_FieldName_Max_Column_Index + 1):
      curLanguageType = self._sheet.cell(row=ExportXlsxConst.Sheet_Export_Language_Type_Row_Index, column=columnIndex).value
      if curLanguageType.lower() == self._languageType.lower():
        return columnIndex
    return None

  def _GetSheetCfgIsOutput(self):
    isOutput = self._sheet.cell(row=self._sheetCfgFieldInfoDict.GetRowIndexOfIsOutput(), column=self._languageTypeColumnIndex).value
    if isinstance(isOutput, int):
      return isOutput != 0
      if isinstance(isOutput, bool):
        return isOutput
      if isinstance(isOutput, str):
        return isOutput.lower() == "true"
      return False

  def _GetSheetCfgTableName(self):
    return self._GetSheetCfgFieldValue(self._sheetCfgFieldInfoDict.GetRowIndexOfTableName())

  def _GetSheetCfgOutputDir(self):
    outputDir = self._GetSheetCfgFieldValue(self._sheetCfgFieldInfoDict.GetRowIndexOfOutputDir())
    if outputDir is not None:
      if outputDir[-1] != '/':
        outputDir = outputDir + '/'
    else:
      outputDir = ""
    return outputDir

  def _GetSheetCfgUniqueIndexesList(self):
    uniqueIndexesListString = self._GetSheetCfgFieldValue(self._sheetCfgFieldInfoDict.GetRowIndexOfUniqueIndexesList())
    if StringUtil.IsNoneOrEmpty(uniqueIndexesListString):
      return [[ExportXlsxConst.Sheet_Cfg_UniqueIndexesList_Default]]
    result = []
    for match in re.finditer(ExportXlsxConst.Sheet_Cfg_IndexesList_Wrap_Pattern, uniqueIndexesListString):
      indexesListString = uniqueIndexesListString[match.start() + 1:match.end() - 1]
      result.append(indexesListString.split(","))
    return result

  def _GetSheetCfgMultiplyIndexesList(self):
    multiplyIndexesListString = self._GetSheetCfgFieldValue(self._sheetCfgFieldInfoDict.GetRowIndexOfMultiplyIndexesList())
    if StringUtil.IsNoneOrEmpty(multiplyIndexesListString):
      return []
    result = []
    for match in re.finditer(ExportXlsxConst.Sheet_Cfg_IndexesList_Wrap_Pattern, multiplyIndexesListString):
      indexesString = multiplyIndexesListString[match.start() + 1: match.end() - 1]
      result.append(indexesString.split(","))
    return result

  def _GetSheetCfgHeadCommentRowIndex(self):
    return self._GetSheetCfgFieldValue(self._sheetCfgFieldInfoDict.GetRowIndexOfHeadCommentRowIndex())

  def _GetSheetCfgHeadRowIndex(self):
    return self._GetSheetCfgFieldValue(self._sheetCfgFieldInfoDict.GetRowIndexOfHeadRowIndex())

  def _GetSheetCfgDataStartRowIndex(self):
    return self._GetSheetCfgFieldValue(self._sheetCfgFieldInfoDict.GetRowIndexOfDataStartRowIndex())

  def _GetFieldInfoList(self):
    fieldInfoList = []
    headCommentRowIndex = self.GetHeadCommentRowIndex()
    headRowIndex = self.GetHeadRowIndex()
    maxColumnIndex = self._sheet.max_column
    for columnIndex in range(1, maxColumnIndex + 1):
      headValue = self._sheet.cell(row=headRowIndex, column=columnIndex).value
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
      fieldInfo["comment"] = self._sheet.cell(row=headCommentRowIndex, column=columnIndex).value
      fieldInfoList.append(fieldInfo)
    return fieldInfoList

  def _GetIndexDict(self):
    indexDict = {}
    uniqueIndexesList = self.GetUniqueIndexesList()
    multiplyIndexesList = self.GetMultiplyIndexesList()
    DictUtil.GetOrAddDefault(indexDict, ExportXlsxConst.FieldName_Sheet_Cfg_UniqueIndexesList, uniqueIndexesList)
    DictUtil.GetOrAddDefault(indexDict, ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList, multiplyIndexesList)
    return indexDict

  def _GetFieldInfoDict(self):
    fieldInfoDict = {}
    for fieldInfo in self.GetFieldInfoList():
      fieldInfoDict[fieldInfo["name"]] = fieldInfo
    return fieldInfoDict

  def _GetCfgIndexDataName(self):
    return "%sIndexData" % self.GetTableName()

  def _GetCfgDataName(self):
    return "%sData" % self.GetTableName()

  def _GetCfgRootName(self):
    return "%sRoot" % self.GetTableName()

  #####################################################################################################
  def GetLanguageType(self):
    return self._languageType

  def GetSheet(self):
    return self._sheet

  def GetLanguageTypeColumnIndex(self):
    return self._languageTypeColumnIndex

  def IsOutput(self):
    return self._isOutput

  def GetTableName(self):
    return self._tableName

  def GetOutputDir(self):
    return self._outputDir

  def GetUniqueIndexesList(self):
    return self._uniqueIndexesList

  def GetMultiplyIndexesList(self):
    return self._multiplyIndexesList

  def GetHeadCommentRowIndex(self):
    return self._headCommentRowIndex

  def GetHeadRowIndex(self):
    return self._headRowIndex

  def GetDataStartRowIndex(self):
    return self._dataStartRowIndex

  def GetFieldInfoList(self):
    return self._fieldInfoList

  def GetIndexDict(self):
    return self._indexDict

  def GetFieldInfoDict(self):
    return self._fieldInfoDict

  def GetCfgIndexDataName(self):
    return self._cfgIndexDataName

  def GetCfgDataName(self):
    return self._cfgDataName

  def GetCfgRootName(self):
    return self._cfgRootName

  def GetCfgSpecificIndexDataName(self, specificType):
    return "%sIndex%sData" % (self.GetTableName(), StringUtil.UpperFirstLetter(specificType))

