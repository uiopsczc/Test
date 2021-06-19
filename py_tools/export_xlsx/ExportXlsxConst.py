class ExportXlsxConst(object):
  Xlsx_Dir_Path = r"..\..\Excels\\"
  CsCat_Namespace = "CsCat"
  Export_2_Cs_Dir_Path = r"..\..\Assets\CsCat\Script\game\Cfg\AutoGen\\"
  Export_2_Json_Dir_Path = r"..\..\Assets\Cfg\\"
  Export_2_Lua_Dir_Path = r"..\..\Assets\Lua\game\Cfg\AutoGen\\"
  Export_2_Lua_Require_Root_Dir_Path = r"game.Cfg.AutoGen."
  Export_2_Lua_RequireCfgPathes = Export_2_Lua_Dir_Path + "RequireCfgPathes.lua.txt"

  Export_2_JsonFilePathes_File_Path = r"..\..\Assets\Cfg\JsonFilePathes.txt"

  Sheet_CfgName_Cell_Row = 1
  Sheet_CfgName_Cell_Column = 2
  Sheet_CfgName_Tag = "#name "
  Sheet_Cfg_Tag = "Cfg"

  Sheet_Index_Row = 1

  Sheet_Index_Tag = "#index"
  Sheet_Unique_Tag = "unique"
  Sheet_Multiple_Tag = "multiple"

  Sheet_Index_Unique_Tag = Sheet_Index_Tag+" " +Sheet_Unique_Tag+" "
  Sheet_Index_Multiple_Tag = Sheet_Index_Tag+" " +Sheet_Multiple_Tag+" "


  Sheet_FieldInfo_Type_Row = 2
  Sheet_FieldInfo_Name_Row = 3
  Sheet_FieldInfo_Name_Chinese_Row = 4

  Sheet_Data_Start_Row = 5

  Sheet_FieldInfo_Type_Int = "int"
  Sheet_FieldInfo_Type_Float = "float"
  Sheet_FieldInfo_Type_Bool = "bool"
  Sheet_FieldInfo_Type_String = "string"
  Sheet_FieldInfo_Type_Array = "array"
  Sheet_FieldInfo_Type_Json = "json"
  Sheet_FieldInfo_Type_Ends_With_Array = "[]"
  Sheet_FieldInfo_Type_Starts_With_Dict = "dict"





