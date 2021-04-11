import sys
import os
sys.path.append(os.path.dirname(os.path.realpath(__file__ + "/..")))
############作用######################################
## 将protos下的文件转为对应的语言的类
######################################################
from pythoncat.util.FileUtil import *

cs_output_base_dir = "../../Assets/CsCat/Script/" #cs输出的目录

def ToCS(from_file_path):
  form_file_dir = os.path.dirname(from_file_path)
  abs_from_file_path = os.path.abspath(from_file_path)
  abs_from_file_dir = os.path.abspath(form_file_dir)
  to_file_dir = (cs_output_base_dir + "{}").format(form_file_dir)
  abs_to_file_dir = os.path.abspath(to_file_dir)
  if not StringUtil.IsNoneOrEmpty(abs_to_file_dir) and not os.path.exists(abs_to_file_dir):  # 检查文件夹是否存在,不存在则创建该目录
    os.mkdir(abs_to_file_dir)
  abs_protoc_exe_path = os.path.abspath("lib/protoc.exe")
  cmd = "{} -I={} --csharp_out={} {}".format(abs_protoc_exe_path,abs_from_file_dir,abs_to_file_dir,abs_from_file_path)
  # print(cmd)
  os.system(cmd)

def main():
  file_path_list = FileUtil.GetFilePathList("protos/")
  for file_path in file_path_list:
    if "descriptor.proto" not in file_path:
      ToCS(file_path)
  print("finish")

main()
