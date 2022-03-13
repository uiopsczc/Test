from export_xlsx.ExportXlsxUtil import *
from export_xlsx.ExportXlsxConst import *
from pythoncat.util.FileUtil import *
import json


class ExportXlsx2Json(object):
  @staticmethod
  def ResetAll():
    FileUtil.RemoveDir(ExportXlsxConst.Export_2_Json_Dir_Path)
    FileUtil.RemoveFile(ExportXlsxConst.Export_2_JsonFilePaths_File_Path)

  @staticmethod
  def ExportSheet(sheet, sheetCfg):
    exportFilePath = ExportXlsxConst.Export_2_Json_Dir_Path + sheetCfg.GetOutputDir() + sheetCfg.GetTableName() + ".json"
    jsonDict = {}
    dataList = ExportXlsx2Json.ExportDataList(sheet, sheetCfg)
    jsonDict[ExportXlsxConst.Name_DataList] = dataList
    jsonDict[ExportXlsxConst.Name_IndexDict] = ExportXlsx2Json.ExportIndexDict(sheetCfg, dataList)
    FileUtil.WriteFile(exportFilePath, json.dumps(jsonDict, ensure_ascii=False, indent=2))

    jsonFilePath = exportFilePath.replace("..\\..\\", "").replace("\\\\", "/").replace("\\", "/")
    ExportXlsx2Json.WriteToJsonFilePaths(jsonFilePath)
    return jsonDict

  @staticmethod
  def WriteToJsonFilePaths(jsonFilePath):
    if os.path.exists(ExportXlsxConst.Export_2_JsonFilePaths_File_Path):
      FileUtil.WriteFile(ExportXlsxConst.Export_2_JsonFilePaths_File_Path, "\n" + jsonFilePath, "a")
    else:
      FileUtil.WriteFile(ExportXlsxConst.Export_2_JsonFilePaths_File_Path, jsonFilePath)

  @staticmethod
  def ExportDataList(sheet, sheetCfg):
    maxRowIndex = sheet.max_row
    fieldInfoList = sheetCfg.GetFieldInfoList()
    dataStartRowIndex = sheetCfg.GetDataStartRowIndex()
    dataList = []
    for rowIndex in range(dataStartRowIndex, maxRowIndex + 1):
      if sheet.cell(rowIndex, 1).value is None:
        continue
      data = {}
      for fieldInfo in fieldInfoList:
        columnIndex = fieldInfo["columnIndex"]
        fileInfoType = fieldInfo["type"]
        fileInfoName = fieldInfo["name"]
        cellValue = ExportXlsxUtil.GetExportJsonValueOrDefault(sheet, rowIndex, columnIndex, fileInfoType)
        try:
          if fileInfoType == ExportXlsxConst.Sheet_FieldInfo_Type_Array or fileInfoType == ExportXlsxConst.Sheet_FieldInfo_Type_Json or fileInfoType.endswith(
            ExportXlsxConst.Sheet_FieldInfo_Type_Ends_With_Array) or fileInfoType.startswith(
            ExportXlsxConst.Sheet_FieldInfo_Type_Starts_With_Dict):
            data[fileInfoName] = json.loads(cellValue)
          else:
            data[fileInfoName] = cellValue
        except Exception as e:
          print("cell[%s,%s] has error" % (rowIndex, columnIndex))
          raise e
      dataList.append(data)
    return dataList

  @staticmethod
  def ExportIndexDict(sheetCfg, dataList):
    jsonIndexDict = {}
    indexDict = sheetCfg.GetIndexDict()
    for i in range(0, len(dataList)):
      data = dataList[i]
      for indexTag in indexDict.keys():
        for indexes in indexDict[indexTag]:
          combineIndexKey = "_and_".join(indexes)
          specificKeyList = []
          for index in indexes:
            specificKeyList.append(str(data[index]))
          specificKey = ".".join(specificKeyList)
          if indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_UniqueIndexesList:
            uniqueDict = DictUtil.GetOrAddDefault(jsonIndexDict, ExportXlsxConst.FieldName_Sheet_Cfg_UniqueIndexesList, {})
            uniqueSpecificDict = DictUtil.GetOrAddDefault(uniqueDict, combineIndexKey, {})
            uniqueSpecificDict[specificKey] = i
          elif indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList:
            multipleDict = DictUtil.GetOrAddDefault(jsonIndexDict, ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList, {})
            multipleSpecificDict = DictUtil.GetOrAddDefault(multipleDict, combineIndexKey, {})
            DictUtil.GetOrAddDefault(multipleSpecificDict, specificKey, []).append(i)
    return jsonIndexDict
