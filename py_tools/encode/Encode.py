import sys
import os

sys.path.append(os.path.dirname(os.path.realpath(__file__ + "/..")))
############作用######################################
## 将root_file_path目录下的文件编码格式改为change_encode_to
######################################################
from pythoncat.util.FileUtil import *

#root_file_path = r"E:\WorkSpace\Java\yuxian"
root_dir_path = r"E:\WorkSpace\Unity\Test\Test\Assets"
# root_dir_path = r"E:\WorkSpace\Java\utf8"

change_encode_to = "utf-8" #utf-8 gb2312

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


def Convert(file_path, target_encode):
  file_content = ReadFile(file_path)
  original_encode = FileUtil.GetFileEncoding(file_path)
  file_decode = file_content.decode(original_encode, 'ignore')
  file_encode = file_decode.encode(target_encode)
  WriteFile(file_encode, file_path)



def main():
  file_path_list = FileUtil.GetFilePathList(root_dir_path, FilterFile)
  for file_path in file_path_list:
    # print(file_path)
    Convert(file_path, change_encode_to)

main()
