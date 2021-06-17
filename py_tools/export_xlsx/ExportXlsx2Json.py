from export_xlsx.ExportXlsxUtil import *
from export_xlsx.ExportXlsxConst import *
from pythoncat.util.FileUtil import *
import json

class ExportXlsx2Json(object):
  @staticmethod
  def ResetAll():
    FileUtil.RemoveDir(ExportXlsxConst.Export_2_Json_Dir_Path)
    FileUtil.RemoveFile(ExportXlsxConst.Export_2_JsonFilePathes_File_Path)

  @staticmethod
  def ExportSheet(sheet, export_relative_dir_path):
    export_file_path = ExportXlsxConst.Export_2_Json_Dir_Path + export_relative_dir_path + ExportXlsxUtil.GetExportSheetName(sheet) +".json"
    json_dict = {}
    json_dict["data_list"] = ExportXlsx2Json.GetSheetDataList(sheet)
    json_dict["index_dict"] = ExportXlsx2Json.GetSheetIndexDict(sheet,json_dict["data_list"])
    FileUtil.WriteFile(export_file_path, json.dumps(json_dict, ensure_ascii=False, indent=2))

    json_file_path = export_file_path.replace("..\\..\\","").replace("\\\\","/").replace("\\","/")
    ExportXlsx2Json.WriteToJsonFilePathes(json_file_path)
    return json_dict

  @staticmethod
  def WriteToJsonFilePathes(json_file_path):
    if os.path.exists(ExportXlsxConst.Export_2_JsonFilePathes_File_Path):
      FileUtil.WriteFile(ExportXlsxConst.Export_2_JsonFilePathes_File_Path,"\n"+json_file_path,"a")
    else:
      FileUtil.WriteFile(ExportXlsxConst.Export_2_JsonFilePathes_File_Path, json_file_path)

  @staticmethod
  def GetSheetDataList(sheet):
    max_row = sheet.max_row
    fieldInfo_list = ExportXlsxUtil.GetExportSheetFiledInfoList(sheet)
    data_list = []
    for row in range(ExportXlsxConst.Sheet_Data_Start_Row, max_row + 1):
      data = {}
      for fieldInfo in fieldInfo_list:
        column = fieldInfo["column"]
        cell = sheet.cell(row=row, column=column)
        cell_value = cell.value
        fileInfo_type = fieldInfo["type"]
        fileInfo_name = fieldInfo["name"]
        cell_value = ExportXlsxUtil.GetExportValueOrDefault(cell_value, fileInfo_type)
        if fileInfo_type == ExportXlsxConst.Sheet_FieldInfo_Type_Array or fileInfo_type == ExportXlsxConst.Sheet_FieldInfo_Type_Json:
          data[fileInfo_name] = json.loads(cell_value)
        else:
          data[fileInfo_name] = cell_value
      data_list.append(data)
    return data_list

  @staticmethod
  def GetSheetIndexDict(sheet, data_list):
    json_index_dict = {}
    index_dict = ExportXlsxUtil.GetExportSheetIndexDict(sheet)
    for i in range(0, len(data_list)):
      data = data_list[i]
      for index_group in index_dict.keys():
        for index_key_list in index_dict[index_group]:
          index_field_key = "_and_".join(index_key_list)
          specific_key_list = []
          for index_key in index_key_list:
            specific_key_list.append(str(data[index_key]))
          specific_key = ".".join(specific_key_list)
          if index_group == ExportXlsxConst.Sheet_Unique_Tag:
            unique_dict = DictUtil.GetOrAddDefault(json_index_dict, ExportXlsxConst.Sheet_Unique_Tag, {})
            unique_specific_dict = DictUtil.GetOrAddDefault(unique_dict, index_field_key, {})
            unique_specific_dict[specific_key] = i
          elif index_group == ExportXlsxConst.Sheet_Multiple_Tag:
            multiple_dict = DictUtil.GetOrAddDefault(json_index_dict, ExportXlsxConst.Sheet_Multiple_Tag, {})
            multiple_specific_dict = DictUtil.GetOrAddDefault(multiple_dict, index_field_key, {})
            DictUtil.GetOrAddDefault(multiple_specific_dict, specific_key, []).append(i)
    return json_index_dict
