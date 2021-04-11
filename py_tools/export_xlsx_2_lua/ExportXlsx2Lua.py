import sys
import os
sys.path.append(os.path.dirname(os.path.realpath(__file__ + "/..")))
############作用######################################
## 根据xlsx_dir_path目录下的.xlsx文件生成lua文件并移动到export_2_lua_dir_path目录
######################################################
import traceback
from pythoncat.util.ExcelUtil import *
from pythoncat.util.FileUtil import *
from pythoncat.util.StringUtil import *

xlsx_dir_path = r"..\..\Assets\Excels\\"
export_2_lua_dir_path = r"..\..\Assets\Lua\game\definition\export\\"
id_list_dict = {} # 所有的文件的id列表
IdDefinition_Name = "IdDefinition.lua.txt"
export_2_IdDefinition_path = export_2_lua_dir_path + IdDefinition_Name


def FilterFilePath(file_path):
  if not file_path.endswith(".xlsx") or file_path.find("~$") != -1: #"~$"临时打开的文件过滤掉
    return True
  return False

#根据file_path获取需要导出到的文件路径
def GetExport2LuaFilePath(file_path):
  relative_file_path = file_path.replace(xlsx_dir_path, "")
  file_name = file_path[file_path.rfind("-") + 1:]  #file_name只获取"-"之后的名称
  file_name = file_name.replace(".xlsx", ".lua.txt") #将file_name只获取"-"之后的名称
  slash_start_index = relative_file_path.rfind("\\")
  if slash_start_index == -1:
    result = file_name
  else:
    result = relative_file_path[:slash_start_index+1] + file_name
  result = export_2_lua_dir_path + result
  return result

#通过value和type获取对应的lua中的value的字符串
def GetValueContent(value, type):
  try:
    type = str.lower(type)
    if type == "int":
      return int(value)
    elif type == "float":
      return float(value)
    elif type == "string":
      return "[=[%s]=]" % (value)
    elif type == "translation":
      return "global.Translate([=[%s]=])" % (value)
    elif type == "vector3":
      return "Vector3(%s)"%(value)
    elif type == "boolean":
      if value == "是" or str.lower(value) == "true":
        return "true"
      else:
        return "false"
    elif type.endswith("[]"):
      type = type[:-2]
      if value.startswith("[") and value.startswith("]"):#去掉包裹value中的[]
        value = value[1,-1]
      values = StringUtil.SplitIgnore(value, ",")
      content = ""
      for value in values:
        content = content + "%s,"%GetValueContent(value, type)
      return "{%s}"%content
    elif type.startswith("dict"):
      type = type[5:-1] # dict<xxx,yyyy> 去掉dict<>
      types = str.split(type,",")
      sub_key_type = types[0]
      sub_value_type = types[1]
      content = ""
      if value.startswith("{") and value.endswith("}"): #去掉包裹value中的{}
        value = value[1, -1]
      entrys = StringUtil.SplitIgnore(value, ",")
      for entry in entrys:
        es = StringUtil.SplitIgnore(entry,":")
        key = es[0]
        value = es[1]
        content = content + "[\n%s\n] = %s,"%(GetValueContent(key,sub_key_type),GetValueContent(value,sub_value_type))
      return "{%s}"%(content)
    else:
      raise RuntimeError("not Support Type:%s"%(type))
  except Exception as e:
    raise e

#Exprot单个文件
def Export(file_path, line_list):
  file_name = os.path.basename(file_path)
  short_file_name = file_name[:file_name.find(".")]
  header_name_list = line_list[0]
  type_list = line_list[1]
  content = "---@class %s\n"%(short_file_name)
  for header_name in header_name_list:
    content = content + "---@field %s\n"%(header_name)
  content = content + "local _\n\n\n"
  content = content + "local definition = {\n\n"
  id_list_dict[short_file_name] = [] # 文件的id_list
  for row in range(2,len(line_list)): #行
    line_content = ""
    line = line_list[row]
    id_value = ""
    for column in range(0,len(type_list)): #列
      header_name = str(header_name_list[column])
      type = str(type_list[column])
      value = str(line[column])
      #忽略没用的列
      if StringUtil.IsNoneOrEmpty(header_name) or str.lower(header_name) == "none" or StringUtil.IsNoneOrEmpty(type) or str.lower(type) == "none" or StringUtil.IsNoneOrEmpty(value) or str.lower(value) == "none":
        continue
      try:
        value = GetValueContent(value, type)
        is_id = str.lower(header_name) == "id"
        if is_id:
          id_value = value
          id_list_dict[short_file_name].append(id_value) # 文件的id_list
        line_content = line_content + "%s = %s," % (header_name, value)
      except Exception as e:
        traceback.print_exc()
        print("Error in %s [header_name = %s, value = %s, type = %s]"%(file_name,header_name,value,type))
    content = content + "  [\n%s\n] = {%s},\n\n"%(id_value,line_content)
  content = content + "}\nreturn definition"
  # print(content)
  FileUtil.WriteFile(file_path,content)

#Export所有的文件
def ExportAll():
  file_path_list = FileUtil.GetFilePathList(xlsx_dir_path, FilterFilePath)
  start_row_num = 9
  for file_path in file_path_list:
    export_2_lua_file_path = GetExport2LuaFilePath(file_path)
    line_list = ExcelUtil.ReadExcelAsLineList(file_path, start_row_num)
    # if file_path.find("test") != -1:
    Export(export_2_lua_file_path, line_list)

def ExportIdDefinition():
  #id_list_dict
  file_name = IdDefinition_Name
  short_file_name = file_name[:file_name.find(".")]
  content = "---@class %s\nlocal definition = {\n\n"%(short_file_name)
  for key in id_list_dict:
    id_list = id_list_dict[key]
    id_list_content = ",".join(id_list)
    content = content +  "%s = {%s},\n\n"%(key,id_list_content)
  content = content + "}\nreturn definition"
  FileUtil.WriteFile(export_2_IdDefinition_path,content)

def main():
  ExportAll()
  ExportIdDefinition()
  print("finish")
main()