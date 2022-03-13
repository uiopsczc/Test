class ExportXlsxConst(object):
  Is_Export_Lua = True
  Is_Export_Cs = True
  Xlsx_Dir_Path = r"..\..\Excels\\"

  Name_Default = "default"
  Name_Lua = "lua"
  Name_Json = "json"
  Name_Cs = "cs"


  Export_2_Lua_Dir_Path = r"..\..\Assets\Lua\Game\Cfg\AutoGen\\"
  Export_2_Lua_Require_Root_Dir_Path = r"Game.Cfg.AutoGen."
  Export_2_Lua_RequireCfgPaths = Export_2_Lua_Dir_Path + "RequireCfgPaths.lua.txt"
  CsCat_Namespace = "CsCat"
  Export_2_Cs_Dir_Path = r"..\..\Assets\CsCat\Script\Game\Cfg\AutoGen\\"
  Export_2_Json_Dir_Path = r"..\..\Assets\PatchResources\Cfg\Json\\"
  Export_2_JsonFilePaths_File_Path = Export_2_Json_Dir_Path + "CfgJsonFilePaths.txt"

  Sheet_Cfg_FieldName_Max_Row_Index = 15
  Sheet_Cfg_FieldName_Default_Column_Index = 2
  Sheet_Cfg_FieldName_Max_Column_Index = 5
  Sheet_Export_Language_Type_Row_Index = 1
  Sheet_Cfg_FieldName_Column_Index = 1
  FieldName_Sheet_Cfg_IsOutput = "isOutput"
  FieldName_Sheet_Cfg_TableName = "tableName"
  FieldName_Sheet_Cfg_OutputDir = "outputDir"
  FieldName_Sheet_Cfg_UniqueIndexesList = "uniqueIndexesList"
  FieldName_Sheet_Cfg_MultiplyIndexesList = "multiplyIndexesList"
  FieldName_Sheet_Cfg_HeadCommentRowIndex = "headCommentRowIndex"
  FieldName_Sheet_Cfg_HeadRowIndex = "headRowIndex"
  FieldName_Sheet_Cfg_DataStartRowIndex = "dataStartRowIndex"
  Sheet_Cfg_UniqueIndexesList_Default = 'id'
  Sheet_Cfg_IndexesList_Left_Wrap_Char = '['
  Sheet_Cfg_IndexesList_Right_Wrap_Char = ']'
  Sheet_Cfg_IndexesList_Wrap_Pattern = "\%s[^\%s]+\%s" % (Sheet_Cfg_IndexesList_Left_Wrap_Char, Sheet_Cfg_IndexesList_Right_Wrap_Char, Sheet_Cfg_IndexesList_Right_Wrap_Char)

  Sheet_Cfg_FieldNameDict = {
    FieldName_Sheet_Cfg_IsOutput: True,
    FieldName_Sheet_Cfg_TableName: True,
    FieldName_Sheet_Cfg_OutputDir: True,
    FieldName_Sheet_Cfg_UniqueIndexesList: True,
    FieldName_Sheet_Cfg_MultiplyIndexesList: True,
    FieldName_Sheet_Cfg_HeadCommentRowIndex: True,
    FieldName_Sheet_Cfg_HeadRowIndex: True,
    FieldName_Sheet_Cfg_DataStartRowIndex: True
  }

  Sheet_Cfg_FieldInfoType_Left_Wrap_Char = "("
  Sheet_Cfg_FieldInfoType_Right_Wrap_Char = ")"

  Name_DataList = "dataList"
  Name_IndexDict = "indexDict"

  Sheet_CfgName_Cell_Row = 1
  Sheet_CfgName_Cell_Column = 2
  Sheet_CfgName_Tag = "#name "
  Sheet_Cfg_Tag = "Cfg"

  Sheet_Index_Row = 1

  Sheet_Index_Tag = "#index"
  Name_UniqueIndexes = "uniqueIndexes"
  Name_MultipleIndexes = "multipleIndexes"

  Sheet_Index_Unique_Tag = Sheet_Index_Tag +" " + Name_UniqueIndexes + " "
  Sheet_Index_Multiple_Tag = Sheet_Index_Tag +" " + Name_MultipleIndexes + " "


  Sheet_FieldInfo_Type_Row = 2
  Sheet_FieldInfo_Name_Row = 3
  Sheet_FieldInfo_Name_Chinese_Row = 4

  Sheet_Data_Start_Row = 5

  Sheet_FieldInfo_Type_Int = "int"
  Sheet_FieldInfo_Type_Float = "float"
  Sheet_FieldInfo_Type_Bool = "bool"
  Sheet_FieldInfo_Type_String = "string"
  Sheet_FieldInfo_Type_Lang = "lang"
  Sheet_FieldInfo_Type_Array = "array"
  Sheet_FieldInfo_Type_Json = "json"
  Sheet_FieldInfo_Type_Ends_With_Array = "[]"
  Sheet_FieldInfo_Type_Starts_With_Dict = "dict"





