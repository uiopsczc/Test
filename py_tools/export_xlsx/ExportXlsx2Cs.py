from export_xlsx.ExportXlsxUtil import *
from export_xlsx.ExportXlsxConst import *
from pythoncat.util.FileUtil import *


class ExportXlsx2Cs(object):
  @staticmethod
  def ResetAll():
    FileUtil.RemoveDir(ExportXlsxConst.Export_2_Cs_Dir_Path)

  @staticmethod
  def ExportSheet(sheet, json_dict, export_relative_dir_path):
    export_file_path = ExportXlsxConst.Export_2_Cs_Dir_Path + export_relative_dir_path + ExportXlsxUtil.GetCfgName(sheet) + ".cs"
    indent = 0
    content = ""
    content += "//AutoGen. DO NOT EDIT!!!\n"
    content += "using System;\n"
    content += "using System.Collections.Generic;\n"
    content += "using LitJson;\n"
    content += "namespace %s{\n" % (ExportXlsxConst.CsCat_Namespace)
    indent += 1
    content += ExportXlsx2Cs.ExportCfg(sheet, json_dict["index_dict"], indent)
    content += ExportXlsx2Cs.ExportCfgRoot(sheet, indent)
    content += ExportXlsx2Cs.ExportCfgData(sheet, indent)
    content += ExportXlsx2Cs.ExportCfgIndexDict(sheet, json_dict["index_dict"], indent)
    indent -= 1
    content += "}"
    FileUtil.WriteFile(export_file_path, content)

  @staticmethod
  def ExportCfg(sheet, index_dict, indent):
    content = ""
    content += "%spublic class %s {\n" % (StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgName(sheet))
    indent += 1
    content += "%sprotected %s () {}\n" % (StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgName(sheet))
    content += "%spublic static %s Instance => instance;\n" % (
    StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgName(sheet))
    content += "%sprotected static %s instance = new %s();\n" % (
    StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgName(sheet), ExportXlsxUtil.GetCfgName(sheet))
    content += "%sprotected %s root;\n" % (StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgRootName(sheet))
    content += "%spublic void Parse(string jsonStr) { this.root=JsonMapper.ToObject<%s>(jsonStr);}\n" % (
    StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgRootName(sheet))
    content += "%spublic List<%s> All(){ return this.root.data_list; }\n" % (
    StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgDataName(sheet))
    content += "%spublic %s Get(int index){ return this.root.data_list[index]; }\n" % (
    StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgDataName(sheet))

    fieldInfo_dict = ExportXlsxUtil.GetExportSheetFiledInfoDict(sheet)
    for index_group in index_dict.keys():
      cs_data_type = ""
      if index_group == ExportXlsxConst.Sheet_Unique_Tag:
        cs_data_type = ExportXlsxUtil.GetCfgDataName(sheet)
      elif index_group == ExportXlsxConst.Sheet_Multiple_Tag:
        cs_data_type = "List<%s>"%(ExportXlsxUtil.GetCfgDataName(sheet))
      for index_specific_key in index_dict[index_group].keys():
        index_specific_keys = index_specific_key.split("_and_")
        args_with_type = ""
        keys = ""
        for arg_key in index_specific_keys:
          fieldInfo = fieldInfo_dict[arg_key]
          args_with_type += "%s %s," % (ExportXlsxUtil.GetExportCsType(fieldInfo["type"]), fieldInfo["name"])
          keys += "%s.ToString()," % (fieldInfo["name"])
        args_with_type = args_with_type[0:len(args_with_type) - 1]
        keys = keys[0:len(keys) - 1]
        # get_by_xxxxx
        content += "%spublic %s get_by_%s(%s){\n" % (
        StringUtil.GetSpace(indent), cs_data_type, index_specific_key, args_with_type)
        indent += 1
        if len(index_specific_keys)>1:
          content += "%sstring[] keys = {%s};\n" % (StringUtil.GetSpace(indent), keys)
          content += "%sstring key = string.Join(\".\", keys);\n" % (StringUtil.GetSpace(indent))
        else:
          content += "%sstring key = %s.ToString();\n" % (StringUtil.GetSpace(indent), index_specific_key)

        if index_group == ExportXlsxConst.Sheet_Unique_Tag:
          content += "%sreturn this.Get(this.root.index_dict.%s.%s[key]);\n" % (
            StringUtil.GetSpace(indent), index_group,index_specific_key)
        elif index_group == ExportXlsxConst.Sheet_Multiple_Tag:
          content += "%sList<%s> result = new List<%s>();\n" % (StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgDataName(sheet), ExportXlsxUtil.GetCfgDataName(sheet))
          content += "%sList<int> indexes = this.root.index_dict.%s.%s[key];\n" % (StringUtil.GetSpace(indent),index_group,index_specific_key)
          content += "%sforeach(int index in indexes) { result.Add(this.Get(index)); }\n"%(StringUtil.GetSpace(indent))
          content += "%sreturn result;\n"%(StringUtil.GetSpace(indent))
        indent -= 1
        content += "%s}\n" % (StringUtil.GetSpace(indent))

        # contain_key_by_xxxxx
        content += "%spublic bool contain_key_by_%s(%s){\n" % (StringUtil.GetSpace(indent), index_specific_key, args_with_type)
        indent += 1
        if len(index_specific_keys) > 1:
          content += "%sstring[] keys = {%s};\n" % (StringUtil.GetSpace(indent), keys)
          content += "%sstring key = string.Join(\".\", keys);\n" % (StringUtil.GetSpace(indent))
        else:
          content += "%sstring key = %s.ToString();\n" % (StringUtil.GetSpace(indent), index_specific_key)

        content += "%sreturn this.root.index_dict.%s.%s.ContainsKey(key);\n" % (
          StringUtil.GetSpace(indent), index_group, index_specific_key)
        indent -= 1
        content += "%s}\n" % (StringUtil.GetSpace(indent))

    indent -= 1
    content += "%s}\n" % (StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def ExportCfgRoot(sheet, indent):
    content = ""
    content += "%spublic class %s{\n" % (StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgRootName(sheet))
    indent += 1
    content += "%spublic List<%s> data_list { get; set; }\n" % (
    StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgDataName(sheet))
    content += "%spublic %s index_dict { get; set; }\n" % (
    StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgIndexDataName(sheet))
    indent -= 1
    content += "%s}\n" % (StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def ExportCfgData(sheet, indent):
    content = ""
    content += "%spublic partial class %s {\n" % (StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgDataName(sheet))
    indent += 1
    fieldInfo_list = ExportXlsxUtil.GetExportSheetFiledInfoList(sheet)
    for fieldInfo in fieldInfo_list:
      fieldInfo_type = fieldInfo["type"]
      fieldInfo_name = fieldInfo["name"]
      content += "%s/*%s*/\n" % (StringUtil.GetSpace(indent), fieldInfo["name_chinese"])
      content += "%spublic %s %s { get; set; }\n" % (StringUtil.GetSpace(indent),ExportXlsxUtil.GetExportCsType(fieldInfo_type) ,fieldInfo_name)
      if ExportXlsxUtil.IsSpecialCsType(fieldInfo_type):
        fieldInfo_speical_cs_type = ExportXlsxUtil.GetSpecialCsType(fieldInfo_type)
        content += "%sprivate %s __%s;\n"%(StringUtil.GetSpace(indent),fieldInfo_speical_cs_type, fieldInfo_name)
        content += "%spublic %s _%s {\n"%(StringUtil.GetSpace(indent),fieldInfo_speical_cs_type, fieldInfo_name)
        indent += 1
        content += "%sget{\n" % (StringUtil.GetSpace(indent))
        indent += 1
        content += "%sif(__%s == default(%s)) __%s = %s.To<%s>();\n" % (StringUtil.GetSpace(indent), fieldInfo_name,fieldInfo_speical_cs_type,fieldInfo_name, fieldInfo_name, fieldInfo_speical_cs_type)
        content += "%sreturn __%s;\n"%(StringUtil.GetSpace(indent), fieldInfo_name)
        indent -= 1
        content += "%s}\n" % (StringUtil.GetSpace(indent))
        indent -= 1
        content += "%s}\n" % (StringUtil.GetSpace(indent))
    indent -= 1
    content += "%s}\n" % (StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def ExportCfgIndexDict(sheet, index_dict, indent):
    content = ""
    content += ExportXlsx2Cs.ExportCfgIndexData(sheet, index_dict, indent)
    content += ExportXlsx2Cs.ExportCfgSpecificIndexData(sheet, index_dict, indent)
    return content

  @staticmethod
  def ExportCfgIndexData(sheet, index_dict, indent):
    content = ""
    content += "%spublic class %s {\n" % (StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgIndexDataName(sheet))
    indent += 1
    for index_group in index_dict.keys():
      content += "%spublic %s %s{ get; set; }\n" % (
        StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgSpecificIndexDataName(sheet, index_group), index_group)
    indent -= 1
    content += "%s}\n" % (StringUtil.GetSpace(indent))
    return content

  @staticmethod
  def ExportCfgSpecificIndexData(sheet, index_dict, indent):
    content = ""
    for index_group in index_dict.keys():
      if index_group == ExportXlsxConst.Sheet_Unique_Tag:
        content += "%spublic class %s {\n" % (
          StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgSpecificIndexDataName(sheet, ExportXlsxConst.Sheet_Unique_Tag))
        indent += 1
        for specific_index_key in index_dict[index_group].keys():
          content += "%spublic Dictionary<string, int> %s { get; set; } \n" % (
            StringUtil.GetSpace(indent), specific_index_key)
        indent -= 1
        content += "%s}\n" % (StringUtil.GetSpace(indent))
      elif index_group == ExportXlsxConst.Sheet_Multiple_Tag:
        content += "%spublic class %s {\n" % (
        StringUtil.GetSpace(indent), ExportXlsxUtil.GetCfgSpecificIndexDataName(sheet, ExportXlsxConst.Sheet_Multiple_Tag))
        indent += 1
        for specific_index_key in index_dict[index_group].keys():
          content += "%spublic Dictionary<string,List<int>> %s { get; set; } \n" % (
          StringUtil.GetSpace(indent), specific_index_key)
        indent -= 1
        content += "%s}\n" % (StringUtil.GetSpace(indent))
    return content


