from export_xlsx.ExportXlsxUtil import *
from export_xlsx.ExportXlsxConst import *
from pythoncat.util.FileUtil import *


class ExportXlsx2Lua(object):
  @staticmethod
  def ResetAll():
    FileUtil.RemoveDir(ExportXlsxConst.Export_2_Lua_Dir_Path)


  @staticmethod
  def ExportSheet(sheet, jsonDict, sheetCfg, exportRelativeFilePath):
    exportFilePath = ExportXlsxConst.Export_2_Lua_Dir_Path + sheetCfg.GetOutputDir() + sheetCfg.GetTableName() + ".lua.txt"
    dataList = jsonDict[ExportXlsxConst.Name_DataList]
    indexDict = jsonDict[ExportXlsxConst.Name_IndexDict]
    indent = 0
    content = ""
    content += "--AutoGen. DO NOT EDIT!!!\n"
    content += "--ExportFrom %s[%s]\n" % (exportRelativeFilePath, sheet.title)
    content += ExportXlsx2Lua.ExportCfgDataComments(sheetCfg, indent)
    content += "\n\n\n"
    content += ExportXlsx2Lua.ExportDataList(dataList, sheetCfg, indent)
    content += "\n"
    content += ExportXlsx2Lua.ExportIndexDict(indexDict, indent)
    content += "\n"

    content += ExportXlsx2Lua.ExportCfg(indexDict, sheetCfg, indent)
    FileUtil.WriteFile(exportFilePath, content)
    ExportXlsx2Lua.Export2CfgRequire(sheetCfg)

  @staticmethod
  def Export2CfgRequire(sheetCfg):
    requirePath = ExportXlsxConst.Export_2_Lua_Require_Root_Dir_Path + sheetCfg.GetOutputDir().replace("\\\\", "\\").replace("\\", ".").replace("/", ".") + sheetCfg.GetTableName()
    requirePath = "%s = require(\"%s\")" % (sheetCfg.GetTableName(), requirePath)
    if os.path.exists(ExportXlsxConst.Export_2_Lua_RequireCfgPaths):
      FileUtil.WriteFile(ExportXlsxConst.Export_2_Lua_RequireCfgPaths, "\n" + requirePath, "a")
    else:
      requirePath = "--AutoGen. DO NOT EDIT!!!\n" + requirePath
      FileUtil.WriteFile(ExportXlsxConst.Export_2_Lua_RequireCfgPaths, requirePath)

  @staticmethod
  def ExportCfgDataComments(sheetCfg, indent):
    content = ""
    fieldInfoList = sheetCfg.GetFieldInfoList()
    content += "%s---@class %s\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgDataName())
    for fieldInfo in fieldInfoList:
      content += "%s---@field %s\n" % (StringUtil.GetSpace(indent), fieldInfo["name"])
    content += "%slocal _\n" % (StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def ExportDataList(dataList, sheetCfg, indent):
    content = ""
    fieldInfoDict = sheetCfg.GetFieldInfoDict()
    content += "%s---@type %s[]\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgDataName())
    content += "%slocal %s = {\n" % (StringUtil.GetSpace(indent), ExportXlsxConst.Name_DataList)
    indent += 1
    for data in dataList:
      content += "%s{\n" % (StringUtil.GetSpace(indent))
      indent += 1
      for key in data.keys():
        content +="%s%s = %s,\n" % (StringUtil.GetSpace(indent), key, ExportXlsxUtil.GetExportLuaValueOrDefault(data[key], fieldInfoDict[key]["type"]))
      indent -= 1
      content += "%s},\n" % (StringUtil.GetSpace(indent))
    indent -= 1
    content += "%s}\n" % (StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def ExportIndexDict(indexDict, indent):
    content = ""
    content += "%slocal %s = {\n" % (StringUtil.GetSpace(indent), ExportXlsxConst.Name_IndexDict)
    indent += 1
    for indexTag in indexDict.keys():
      content += "%s%s = {\n" % (StringUtil.GetSpace(indent), indexTag)
      indent += 1
      for specificIndexKey in indexDict[indexTag].keys():
        if indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_UniqueIndexesList:
          content += "%s%s = {\n" % (StringUtil.GetSpace(indent), specificIndexKey)
          indent += 1
          for key in indexDict[indexTag][specificIndexKey].keys():
            content += "%s[\n%s[=[%s]=]\n%s] = %s,\n" % (StringUtil.GetSpace(indent), StringUtil.GetSpace(indent+1), key, StringUtil.GetSpace(indent), indexDict[indexTag][specificIndexKey][key] + 1)#lua是从1开始
          indent -= 1
          content += "%s},\n" % (StringUtil.GetSpace(indent))
        elif indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList:
          content += "%s%s = {\n" % (StringUtil.GetSpace(indent), specificIndexKey)
          indent += 1
          for key in indexDict[indexTag][specificIndexKey].keys():
            content += "%s[\n%s[=[%s]=]\n%s] = {\n" % (StringUtil.GetSpace(indent), StringUtil.GetSpace(indent+1), key, StringUtil.GetSpace(indent))
            indent += 1
            for index in indexDict[indexTag][specificIndexKey][key]:
              content += "%s%s,\n" % (StringUtil.GetSpace(indent), index+1)#lua是从1开始
            indent -= 1
            content += "%s},\n" % (StringUtil.GetSpace(indent))
          indent -= 1
          content += "%s},\n" % (StringUtil.GetSpace(indent))
      indent -= 1
      content += "%s},\n" % (StringUtil.GetSpace(indent))
    indent -= 1
    content += "%s}\n" % (StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def ExportCfg(indexDict, sheetCfg, indent):
    content = ""
    content += "%slocal cfg = {}\n" % (StringUtil.GetSpace(indent))
    content += "%s\n" % (StringUtil.GetSpace(indent))

    content += "%s---@return %s[]\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgDataName())
    content += "%sfunction cfg.All()\n" % (StringUtil.GetSpace(indent))
    indent += 1
    content += "%sreturn %s\n" % (StringUtil.GetSpace(indent), ExportXlsxConst.Name_DataList)
    indent -= 1
    content += "%send\n" % (StringUtil.GetSpace(indent))
    content += "%s\n" % (StringUtil.GetSpace(indent))

    content += "%s---@return %s\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgDataName())
    content += "%sfunction cfg.Get(index)\n" % (StringUtil.GetSpace(indent))
    indent += 1
    content += "%sreturn %s[index]\n" % (StringUtil.GetSpace(indent), ExportXlsxConst.Name_DataList)
    indent -= 1
    content += "%send\n" % (StringUtil.GetSpace(indent))
    content += "%s\n" % (StringUtil.GetSpace(indent))

    fieldInfoDict = sheetCfg.GetFieldInfoDict()
    for indexTag in indexDict.keys():
      for indexSpecificKey in indexDict[indexTag].keys():
        indexSpecificKeys = indexSpecificKey.split("_and_")
        indexSpecificKeysOfMethodName = "And".join(StringUtil.UpperFirstLetterOfArray(indexSpecificKeys))
        args = ""
        keys = ""
        for argKey in indexSpecificKeys:
          fieldInfo = fieldInfoDict[argKey]
          args += "%s," % (fieldInfo["name"])
          keys += "tostring(%s)," % (fieldInfo["name"])
        args = args[0:len(args) - 1]
        keys = keys[0:len(keys) - 1]
        # GetByXXX
        if indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_UniqueIndexesList:
          content += "%s---@return %s\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgDataName())
        elif indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList:
          content += "%s---@return %s[]\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgDataName())

        content += "%sfunction cfg.GetBy%s(%s)\n" % (
          StringUtil.GetSpace(indent), indexSpecificKeysOfMethodName, args)
        indent += 1
        if len(indexSpecificKeys) > 1:
          content += "%slocal keys = {%s}\n" % (StringUtil.GetSpace(indent), keys)
          content += "%slocal key = table.concat(keys, \".\")\n" % (StringUtil.GetSpace(indent))
        else:
          content += "%slocal key = tostring(%s)\n" % (StringUtil.GetSpace(indent), indexSpecificKey)

        if indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_UniqueIndexesList:
          content += "%sreturn cfg.Get(%s.%s.%s[key])\n" % (
            StringUtil.GetSpace(indent), ExportXlsxConst.Name_IndexDict, indexTag, indexSpecificKey)
        elif indexTag == ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList:
          dictName = "_%s_%sDict" % (ExportXlsxConst.FieldName_Sheet_Cfg_MultiplyIndexesList, indexSpecificKeysOfMethodName)
          content += "%sif not self.%s then\n" % (StringUtil.GetSpace(indent), dictName)
          content += "%sself.%s = {}\n" % (StringUtil.GetSpace(indent + 1), dictName)
          content += "%send\n" % (StringUtil.GetSpace(indent))
          content += "%sif self.%s[key] then\n" % (StringUtil.GetSpace(indent), dictName)
          content += "%sreturn self.%s[key]\n" % (StringUtil.GetSpace(indent + 1), dictName)
          content += "%send\n" % (StringUtil.GetSpace(indent))
          content += "%s---@type %s[]\n" % (StringUtil.GetSpace(indent), sheetCfg.GetCfgDataName())
          content += "%slocal result = {}\n" % (StringUtil.GetSpace(indent))
          content += "%slocal indexes = %s.%s.%s[key]\n" % (StringUtil.GetSpace(indent), ExportXlsxConst.Name_IndexDict, indexTag, indexSpecificKey)
          content += "%sfor _, index in ipairs(indexes) do\n" % (StringUtil.GetSpace(indent))
          content += "%stable.insert(result, cfg.Get(index))\n" % (StringUtil.GetSpace(indent + 1))
          content += "%send\n" % (StringUtil.GetSpace(indent))
          content += "%sself.%s[key] = result\n" % (StringUtil.GetSpace(indent), dictName)
          content += "%sreturn self.%s[key]\n" % (StringUtil.GetSpace(indent), dictName)
        indent -= 1
        content += "%send\n" % (StringUtil.GetSpace(indent))
        content += "%s\n" % (StringUtil.GetSpace(indent))

        # contain_key_by_xxxxx
        content += "%sfunction cfg.IsContainsKeyBy%s(%s)\n" % (StringUtil.GetSpace(indent), indexSpecificKeysOfMethodName, args)
        indent += 1
        if len(indexSpecificKeys) > 1:
          content += "%slocal keys = {%s}\n" % (StringUtil.GetSpace(indent), keys)
          content += "%slocal key = table.concat(keys, \".\")\n" % (StringUtil.GetSpace(indent))
        else:
          content += "%slocal key = tostring(%s)\n" % (StringUtil.GetSpace(indent), indexSpecificKey)

        content += "%sreturn %s.%s.%s[key] ~= nil\n" % (StringUtil.GetSpace(indent), ExportXlsxConst.Name_IndexDict, indexTag, indexSpecificKey)
        indent -= 1
        content += "%send\n" % (StringUtil.GetSpace(indent))
        content += "%s\n" % (StringUtil.GetSpace(indent))

    content += "%sreturn cfg\n" % (StringUtil.GetSpace(indent))
    return content


