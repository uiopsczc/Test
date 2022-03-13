import sys
import os
sys.path.append(os.path.dirname(os.path.realpath(__file__ + "/..")))
############作用######################################
## 将protos下的文件转为对应的语言的类
######################################################
from pythoncat.util.FileUtil import *

Cs_Output_Base_Dir = "../../Assets/CsCat/Script/" #cs输出的目录

def ToCS(fromFilePath):
  fromFileDir = os.path.dirname(fromFilePath)
  absFromFilePath = os.path.abspath(fromFilePath)
  absFromFileDir = os.path.abspath(fromFileDir)
  toFileDir = (Cs_Output_Base_Dir + "{}").format(fromFileDir)
  absToFileDir = os.path.abspath(toFileDir)
  if not StringUtil.IsNoneOrEmpty(absToFileDir) and not os.path.exists(absToFileDir):  # 检查文件夹是否存在,不存在则创建该目录
    os.mkdir(absToFileDir)
  absProtocExePath = os.path.abspath("lib/protoc.exe")
  cmd = "{} -I={} --csharp_out={} {}".format(absProtocExePath, absFromFileDir, absToFileDir, absFromFilePath)
  # print(cmd)
  os.system(cmd)

def main():
  filePathList = FileUtil.GetFilePathList("protos/")
  for filePath in filePathList:
    if "descriptor.proto" not in filePath:
      ToCS(filePath)
  print("finish")

main()
