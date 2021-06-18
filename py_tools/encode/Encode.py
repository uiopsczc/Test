############作用######################################
## 将root_file_path目录下的文件编码格式改为change_encode_to
######################################################
import sys
import os
from pythoncat.util.FileUtil import *
from encode.EncodeConst import *

sys.path.append(os.path.dirname(os.path.realpath(__file__ + "/..")))

def Convert(file_path, target_encode):
  file_content = ReadFile(file_path)
  original_encode = FileUtil.GetFileEncoding(file_path)
  file_decode = file_content.decode(original_encode, 'ignore')
  file_encode = file_decode.encode(target_encode)
  WriteFile(file_encode, file_path)


def FilterFile(file_path):
  if not file_path.endswith(".cs"):
    return True
  return False


def ReadFile(file_path):
  with open(file_path, 'rb') as f:
    return f.read()


def WriteFile(content, file_path):
  with open(file_path, 'wb') as f:
    f.write(content)


def main():
  file_path_list = FileUtil.GetFilePathList(EncodeConst.Root_Dir_Path, FilterFile)
  for file_path in file_path_list:
    # print(file_path)
    Convert(file_path, EncodeConst.Change_Encode_To)

main()
