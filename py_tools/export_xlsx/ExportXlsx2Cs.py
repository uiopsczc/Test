from export_xlsx.ExportXlsxUtil import *
from export_xlsx.ExportXlsxConst import *
from pythoncat.util.FileUtil import *


class ExportXlsx2Cs(object):
  @staticmethod
  def ResetAll():
    FileUtil.RemoveDir(ExportXlsxConst.Export_2_Cs_Dir_Path)

  @staticmethod
  def ExportSheet(sheet, jsonDict, sheetCfg, exportRelativeFilePath):
    exportFilePath = ExportXlsxConst.Export_2_Cs_Dir_Path + sheetCfg.GetOutputDir() + sheetCfg.GetTableName() + ".cs"
    indent = 0
    content = ""
    content += "//AutoGen. DO NOT EDIT!!!\n"
    content += "//ExportFrom %s[%s]\n" % (exportRelativeFilePath, sheet.title)
    content += "using System;\n"
    content += "using System.Collections.Generic;\n"
    content += "using LitJson;\n"
    content += "namespace %s{\n" % (ExportXlsxConst.CsCat_Namespace)
    indent += 1
    indexDict = jsonDict[ExportXlsxConst.Name_IndexDict]
    content += ExportXlsx2Cs.ExportCfg(indexDict, sheetCfg, indent)
    content += ExportXlsx2Cs.ExportCfgRoot(sheetCfg, indent)
    content += ExportXlsx2Cs.ExportCfgData(sheetCfg, indent)
    content += ExportXlsx2Cs.ExportCfgIndexDict(sheet, indexDict, sheetCfg, indent)
    indent -= 1
    content += "}"
    FileUtil.WriteFile(exportFilePath, content)

  @staticmethod
  def ExportCfg(indexDict, sheetCfg, indent):
    content = ""
    content += "%spublic class %s {\n" % (StringUtil.GetSpace(indent), sheetCfg.GetTableName())
    indent += 1
    content += "%sprotected %s () {}\n" % (StringUtil.GetSpace(indent), sheetCfg.GetTableName())
    content += "%spublic static %s Instance => instance;\n" % (StringUtil.GetSpace(indent), sheetCfg.GetTableName())
    content += "%sprotected static %s instance = new %s();\n" % (StringUtil.GetSpace(indent), sheetCfg.GetTableName(), sheetCfg.GetTableName())
    content += "%sprotected %s root;\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgRootName())

    content += "%spublic void Parse(string jsonStr) { this.root=JsonMapper.ToObject<%s>(jsonStr);}\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgRootName())
    content += "%spublic List<%s> All(){ return this.root.%s; }\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgDataName(), ExportXlsxConst.Name_DataList)
    content += "%spublic %s Get(int index){ return this.root.%s[index]; }\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgDataName(), ExportXlsxConst.Name_DataList)

    fieldInfoDict = sheetCfg.GetFieldInfoDict()
    for indexTag in indexDict.keys():
      csDataType = ""
      if indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_UniqueIndexesList:
        csDataType = sheetCfg.GetCfgDataName()
      elif indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList:
        csDataType = "List<%s>" % (sheetCfg.GetCfgDataName())
      for indexSpecificKey in indexDict[indexTag].keys():
        indexSpecificKeys = indexSpecificKey.split("_and_")
        indexSpecificKeysOfMethodName = "And".join(StringUtil.UpperFirstLetterOfArray(indexSpecificKeys))
        argsWithType = ""
        keys = ""
        for argKey in indexSpecificKeys:
          fieldInfo = fieldInfoDict[argKey]
          argsWithType += "%s %s," % (ExportXlsxUtil.GetExportCsType(fieldInfo["type"]), fieldInfo["name"])
          keys += "%s.ToString()," % (fieldInfo["name"])
        argsWithType = argsWithType[0:len(argsWithType) - 1]
        keys = keys[0:len(keys) - 1]
        # GetByXXX
        if indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList:
          dictName = "%s_%sDict" % (ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList, indexSpecificKeysOfMethodName)
          dictType = "Dictionary<string, List<%s>>" % (sheetCfg.GetCfgDataName())
          content += "%sprivate %s %s = new %s();\n" % (StringUtil.GetSpace(indent), dictType, dictName, dictType)
        content += "%spublic %s GetBy%s(%s){\n" % (StringUtil.GetSpace(indent), csDataType, indexSpecificKeysOfMethodName, argsWithType)
        indent += 1
        if len(indexSpecificKeys) > 1:
          content += "%sstring[] keys = {%s};\n" % (StringUtil.GetSpace(indent), keys)
          content += "%sstring key = string.Join(\".\", keys);\n" % (StringUtil.GetSpace(indent))
        else:
          content += "%sstring key = %s.ToString();\n" % (StringUtil.GetSpace(indent), indexSpecificKey)

        if indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_UniqueIndexesList:
          content += "%sreturn this.Get(this.root.%s.%s.%s[key]);\n" % (StringUtil.GetSpace(indent), ExportXlsxConst.Name_IndexDict, indexTag, indexSpecificKey)
        elif indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList:
          content += "%sif(%s.TryGetValue(key, out var cacheValue))\n" % (StringUtil.GetSpace(indent), dictName)
          content += "%sreturn cacheValue;\n" % (StringUtil.GetSpace(indent + 1))
          content += "%sList<%s> result = new List<%s>();\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgDataName(), sheetCfg.GetCfgDataName())
          content += "%sList<int> indexes = this.root.%s.%s.%s[key];\n" % (StringUtil.GetSpace(indent), ExportXlsxConst.Name_IndexDict, indexTag, indexSpecificKey)
          content += "%sfor(int i = 1; i < indexes.Count; i++) \n" % (StringUtil.GetSpace(indent))
          content += "%s{\n" % (StringUtil.GetSpace(indent))
          content += "%svar index = indexes[i];\n" % (StringUtil.GetSpace(indent + 1))
          content += "%sresult.Add(this.Get(index));\n" % (StringUtil.GetSpace(indent + 1))
          content += "%s}\n" % (StringUtil.GetSpace(indent))

          # content += "%sforeach(int index in indexes) {  }\n" % (StringUtil.GetSpace(indent))
          content += "%s%s[key] = result;\n" % (StringUtil.GetSpace(indent), dictName)
          content += "%sreturn result;\n" % (StringUtil.GetSpace(indent))
        indent -= 1
        content += "%s}\n" % (StringUtil.GetSpace(indent))

        # contain_key_by_xxxxx
        content += "%spublic bool IsContainsKeyBy%s(%s){\n" % (StringUtil.GetSpace(indent), indexSpecificKeysOfMethodName, argsWithType)
        indent += 1
        if len(indexSpecificKeys) > 1:
          content += "%sstring[] keys = {%s};\n" % (StringUtil.GetSpace(indent), keys)
          content += "%sstring key = string.Join(\".\", keys);\n" % (StringUtil.GetSpace(indent))
        else:
          content += "%sstring key = %s.ToString();\n" % (StringUtil.GetSpace(indent), indexSpecificKey)

        content += "%sreturn this.root.%s.%s.%s.ContainsKey(key);\n" % (StringUtil.GetSpace(indent), ExportXlsxConst.Name_IndexDict, indexTag, indexSpecificKey)
        indent -= 1
        content += "%s}\n" % (StringUtil.GetSpace(indent))

    indent -= 1
    content += "%s}\n" % (StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def ExportCfgRoot(sheetCfg, indent):
    content = ""
    content += "%spublic class %s{\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgRootName())
    indent += 1
    content += "%spublic List<%s> %s { get; set; }\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgDataName(), ExportXlsxConst.Name_DataList)
    content += "%spublic %s %s { get; set; }\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgIndexDataName(), ExportXlsxConst.Name_IndexDict)
    indent -= 1
    content += "%s}\n" % (StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def ExportCfgData(sheetCfg, indent):
    content = ""
    content += "%spublic partial class %s {\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgDataName())
    indent += 1
    fieldInfoList = sheetCfg.GetFieldInfoList()
    for fieldInfo in fieldInfoList:
      fieldInfoType = fieldInfo["type"]
      fieldInfoName = fieldInfo["name"]
      content += "%s/*%s*/\n" % (StringUtil.GetSpace(indent), fieldInfo["comment"])
      isSpecialCsType = ExportXlsxUtil.IsSpecialCsType(fieldInfoType)
      if isSpecialCsType:
        fieldInfoSpecialCsType = ExportXlsxUtil.GetSpecialCsType(fieldInfoType)
        content += "%sprivate %s _%s;\n" % (StringUtil.GetSpace(indent), fieldInfoSpecialCsType, fieldInfoName)
        content += "%spublic %s %s {\n" % (StringUtil.GetSpace(indent), fieldInfoSpecialCsType, fieldInfoName)
        indent += 1
        content += "%sget{\n" % (StringUtil.GetSpace(indent))
        indent += 1
        content += "%sif(_%s == default(%s)) _%s = %s.To<%s>();\n" % (StringUtil.GetSpace(indent), fieldInfoName, fieldInfoSpecialCsType, fieldInfoName, fieldInfoName, fieldInfoSpecialCsType)
        content += "%sreturn _%s;\n" % (StringUtil.GetSpace(indent), fieldInfoName)
        indent -= 1
        content += "%s}\n" % (StringUtil.GetSpace(indent))
        indent -= 1
        content += "%s}\n" % (StringUtil.GetSpace(indent))
      else:
        content += "%spublic %s %s { get; set; }\n" % (StringUtil.GetSpace(indent), ExportXlsxUtil.GetExportCsType(fieldInfoType),fieldInfoName)
    indent -= 1
    content += "%s}\n" % (StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def ExportCfgIndexDict(sheet, indexDict, sheetCfg, indent):
    content = ""
    content += ExportXlsx2Cs.ExportCfgIndexData(indexDict, sheetCfg, indent)
    content += ExportXlsx2Cs.ExportCfgSpecificIndexData(sheet, indexDict, sheetCfg, indent)
    return content

  @staticmethod
  def ExportCfgIndexData(indexDict, sheetCfg, indent):
    content = ""
    content += "%spublic class %s {\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgIndexDataName())
    indent += 1
    for indexTag in indexDict.keys():
      content += "%spublic %s %s{ get; set; }\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgSpecificIndexDataName(indexTag), indexTag)
    indent -= 1
    content += "%s}\n" % (StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def ExportCfgSpecificIndexData(sheet, indexDict, sheetCfg, indent):
    content = ""
    for indexTag in indexDict.keys():
      if indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_UniqueIndexesList:
        content += "%spublic class %s {\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgSpecificIndexDataName(ExportXlsxConst.FieldName_Sheet_Cfg_UniqueIndexesList))
        indent += 1
        for specificIndexKey in indexDict[indexTag].keys():
          content += "%spublic Dictionary<string, int> %s { get; set; } \n" % (StringUtil.GetSpace(indent), specificIndexKey)
        indent -= 1
        content += "%s}\n" % (StringUtil.GetSpace(indent))
      elif indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList:
        content += "%spublic class %s {\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgSpecificIndexDataName(ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList))
        indent += 1
        for specificIndexKey in indexDict[indexTag].keys():
          content += "%spublic Dictionary<string,List<int>> %s { get; set; } \n" % (StringUtil.GetSpace(indent), specificIndexKey)
        indent -= 1
        content += "%s}\n" % (StringUtil.GetSpace(indent))
    return content


