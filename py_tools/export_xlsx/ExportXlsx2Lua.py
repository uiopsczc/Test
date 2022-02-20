from export_xlsx.ExportXlsxUtil import *
from export_xlsx.ExportXlsxConst import *
from pythoncat.util.FileUtil import *


class ExportXlsx2Lua(object):
  @staticmethod
  def ResetAll():
    FileUtil.RemoveDir(ExportXlsxConst.Export_2_Lua_Dir_Path)


  @staticmethod
  def ExportSheet(sheet, json_dict, export_relative_dir_path, export_relative_file_path):
    export_file_path = ExportXlsxConst.Export_2_Lua_Dir_Path + export_relative_dir_path + ExportXlsx2Lua.GetCfgName(sheet) + ".lua.txt"
    indent = 0
    content = ""
    content += "--AutoGen. DO NOT EDIT!!!\n"
    content += "--ExportFrom %s[%s]\n" % (export_relative_file_path, sheet.title)
    content += ExportXlsx2Lua.ExportCfgDataComments(sheet, indent)
    content += "\n\n\n"
    content += ExportXlsx2Lua.ExportDataList(sheet, json_dict["data_list"], indent)
    content += "\n"
    content += ExportXlsx2Lua.ExportIndexDict(sheet, json_dict["index_dict"], indent)
    content += "\n"

    content += ExportXlsx2Lua.ExportCfg(sheet,json_dict["index_dict"], indent)
    FileUtil.WriteFile(export_file_path, content)
    ExportXlsx2Lua.Export2CfgRequire(sheet, export_relative_dir_path)

  @staticmethod
  def Export2CfgRequire(sheet, export_relative_dir_path):
    require_path = ExportXlsxConst.Export_2_Lua_Require_Root_Dir_Path + export_relative_dir_path.replace("\\\\","\\").replace("\\",".")+ ExportXlsxUtil.GetCfgName(sheet)
    require_path = "%s = require(\"%s\")"%(ExportXlsxUtil.GetCfgName(sheet), require_path)
    if os.path.exists(ExportXlsxConst.Export_2_Lua_RequireCfgPaths):
      FileUtil.WriteFile(ExportXlsxConst.Export_2_Lua_RequireCfgPaths, "\n" + require_path, "a")
    else:
      require_path = "--AutoGen. DO NOT EDIT!!!\n" + require_path
      FileUtil.WriteFile(ExportXlsxConst.Export_2_Lua_RequireCfgPaths, require_path)



  @staticmethod
  def ExportCfgDataComments(sheet, indent):
    content = ""
    fieldInfo_list = ExportXlsxUtil.GetExportSheetFiledInfoList(sheet)
    content += "%s---@class %s\n" % (StringUtil.GetSpace(indent),ExportXlsxUtil.GetCfgDataName(sheet))
    for fieldInfo in fieldInfo_list:
      content += "%s---@field %s\n" % (StringUtil.GetSpace(indent),fieldInfo["name"])
    content += "%slocal _\n"%(StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def ExportDataList(sheet, data_list, indent):
    content = ""
    fieldInfo_dict = ExportXlsxUtil.GetExportSheetFiledInfoDict(sheet)
    content += "%s---@type %s[]\n"%(StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgDataName(sheet))
    content += "%slocal data_list = {\n" % (StringUtil.GetSpace(indent))
    indent+=1
    for data in data_list:
      content += "%s{\n"%(StringUtil.GetSpace(indent))
      indent += 1
      for key in data.keys():
        content +="%s%s = %s,\n"%(StringUtil.GetSpace(indent), key, ExportXlsxUtil.GetExportLuaValueOrDefault(data[key],fieldInfo_dict[key]["type"]))
      indent -= 1
      content += "%s},\n" % (StringUtil.GetSpace(indent))
    indent -= 1
    content += "%s}\n" % (StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def ExportIndexDict(sheet, index_dict, indent):
    content = ""
    content += "%slocal index_dict = {\n" % (StringUtil.GetSpace(indent))
    indent += 1
    for index_group in index_dict.keys():
      content += "%s%s = {\n" % (StringUtil.GetSpace(indent), index_group)
      indent += 1
      for specific_index_key in index_dict[index_group].keys():
        if index_group == ExportXlsxConst.Sheet_Unique_Tag:
          content += "%s%s = {\n"%(StringUtil.GetSpace(indent),specific_index_key)
          indent += 1
          for key in index_dict[index_group][specific_index_key].keys():
            content += "%s[\n%s[=[%s]=]\n%s] = %s,\n"%(StringUtil.GetSpace(indent),StringUtil.GetSpace(indent+1), key,StringUtil.GetSpace(indent), index_dict[index_group][specific_index_key][key]+1)#lua是从1开始
          indent -= 1
          content += "%s},\n" % (StringUtil.GetSpace(indent))
        elif index_group == ExportXlsxConst.Sheet_Multiple_Tag:
          content += "%s%s = {\n" % (StringUtil.GetSpace(indent), specific_index_key)
          indent += 1
          for key in index_dict[index_group][specific_index_key].keys():
            content += "%s[\n%s[=[%s]=]\n%s] = {\n" % (StringUtil.GetSpace(indent),StringUtil.GetSpace(indent+1), key,StringUtil.GetSpace(indent))
            indent +=1
            for index in index_dict[index_group][specific_index_key][key]:
              content += "%s%s,\n"%(StringUtil.GetSpace(indent), index+1)#lua是从1开始
            indent -=1
            content += "%s},\n" % (StringUtil.GetSpace(indent))
          indent -= 1
          content += "%s},\n" % (StringUtil.GetSpace(indent))
      indent -=1
      content += "%s},\n" % (StringUtil.GetSpace(indent))
    indent -= 1
    content += "%s}\n" % (StringUtil.GetSpace(indent))

    return content

  @staticmethod
  def ExportCfg(sheet,index_dict, indent):
    content = ""
    content += "%slocal cfg = {}\n" % (StringUtil.GetSpace(indent))
    content += "%s\n"%(StringUtil.GetSpace(indent))

    content += "%sfunction cfg.All()\n"%(StringUtil.GetSpace(indent))
    indent +=1
    content += "%sreturn data_list\n"%(StringUtil.GetSpace(indent))
    indent -= 1
    content += "%send\n" % (StringUtil.GetSpace(indent))
    content += "%s\n" % (StringUtil.GetSpace(indent))

    content += "%sfunction cfg.Get(index)\n" % (StringUtil.GetSpace(indent))
    indent += 1
    content += "%sreturn data_list[index]\n" % (StringUtil.GetSpace(indent))
    indent -= 1
    content += "%send\n" % (StringUtil.GetSpace(indent))
    content += "%s\n" % (StringUtil.GetSpace(indent))

    fieldInfo_dict = ExportXlsxUtil.GetExportSheetFiledInfoDict(sheet)
    for index_group in index_dict.keys():
      for index_specific_key in index_dict[index_group].keys():
        index_specific_keys = index_specific_key.split("_and_")
        index_specific_keys_of_method_name = "And".join(StringUtil.UpperFirstLetterOfArray(index_specific_keys))
        args = ""
        keys = ""
        for arg_key in index_specific_keys:
          fieldInfo = fieldInfo_dict[arg_key]
          args += "%s," % (fieldInfo["name"])
          keys += "tostring(%s)," % (fieldInfo["name"])
        args = args[0:len(args) - 1]
        keys = keys[0:len(keys) - 1]
        # GetByXXX
        content += "%sfunction cfg.GetBy%s(%s)\n" % (
          StringUtil.GetSpace(indent), index_specific_keys_of_method_name, args)
        indent += 1
        if len(index_specific_keys) > 1:
          content += "%slocal keys = {%s}\n" % (StringUtil.GetSpace(indent), keys)
          content += "%slocal key = table.concat(keys, \".\")\n" % (StringUtil.GetSpace(indent))
        else:
          content += "%slocal key = tostring(%s)\n" % (StringUtil.GetSpace(indent), index_specific_key)

        if index_group == ExportXlsxConst.Sheet_Unique_Tag:
          content += "%sreturn cfg.Get(index_dict.%s.%s[key])\n" % (
            StringUtil.GetSpace(indent), index_group, index_specific_key)
        elif index_group == ExportXlsxConst.Sheet_Multiple_Tag:
          content += "%s---@type %s[]\n" %(StringUtil.GetSpace(indent),ExportXlsxUtil.GetCfgDataName(sheet))
          content += "%slocal result = {}\n" % (StringUtil.GetSpace(indent))
          content += "%slocal indexes = index_dict.%s.%s[key]\n" % (
          StringUtil.GetSpace(indent), index_group, index_specific_key)
          content += "%sfor _,index in ipairs(indexes) do  table.insert(result, cfg.Get(index)) end\n" % (
            StringUtil.GetSpace(indent))
          content += "%sreturn result\n" % (StringUtil.GetSpace(indent))
        indent -= 1
        content += "%send\n" % (StringUtil.GetSpace(indent))
        content += "%s\n" % (StringUtil.GetSpace(indent))

        # contain_key_by_xxxxx
        content += "%sfunction cfg.IsContainsKeyBy%s(%s)\n" % (StringUtil.GetSpace(indent), index_specific_keys_of_method_name, args)
        indent += 1
        if len(index_specific_keys) > 1:
          content += "%slocal keys = {%s}\n" % (StringUtil.GetSpace(indent), keys)
          content += "%slocal key = table.concat(keys, \".\")\n" % (StringUtil.GetSpace(indent))
        else:
          content += "%slocal key = tostring(%s)\n" % (StringUtil.GetSpace(indent), index_specific_key)

        content += "%sreturn index_dict.%s.%s[key] ~= nil\n" % (
          StringUtil.GetSpace(indent), index_group, index_specific_key)
        indent -= 1
        content += "%send\n" % (StringUtil.GetSpace(indent))
        content += "%s\n" % (StringUtil.GetSpace(indent))

    content += "%sreturn cfg\n" % (StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def GetDataName(sheet):
    return "%sData"%(ExportXlsx2Lua.GetCfgName(sheet))

  @staticmethod
  def GetCfgName(sheet):
    return ExportXlsxUtil.GetExportSheetName(sheet)


