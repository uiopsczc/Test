import os
import shutil
import chardet
from pythoncat.util.StringUtil import *
from chardet.universaldetector import UniversalDetector
detector = UniversalDetector()

class FileUtil(object):
  @staticmethod
  def GetFileEncoding(filePath):
    with open(filePath, 'rb') as file:
      encoding = chardet.detect(file.read())['encoding']# 获取编码
    return encoding



  @staticmethod
  def ReadFile(filePath):
    file = open(filePath, 'r', encoding=FileUtil.GetFileEncoding(filePath), errors='ignore')
    content = file.read()
    file.close()
    return content

  @staticmethod
  def ReadFileAsLineList(filePath):
    file = open(filePath, 'r', encoding=FileUtil.GetFileEncoding(filePath), errors='ignore')
    lineList = file.readlines()
    file.close()
    return lineList

  @staticmethod
  def WriteFile(filePath, content, mode='w', encoding="utf-8"): #mode w:替换 a:添加到末尾
    dirPath = os.path.dirname(filePath)
    if not StringUtil.IsNoneOrEmpty(dirPath) and not os.path.exists(dirPath): #检查文件夹是否存在,不存在则创建该目录
      os.makedirs(dirPath)
    # encoding = FileUtil.GetFileEncoding(file_path)
    file = open(filePath, mode, encoding=encoding, errors='ignore')
    file.write(content)
    file.close()

  @staticmethod
  #获取rootDirPath下的文件路径，如何该该文件路径在filter_func中返回true，则过滤掉
  def GetFilePathList(rootDirPath, filterFunc=None):
    result = []
    for dirPath, dirNames, fileNames in os.walk(rootDirPath):
      for fileName in fileNames:
        fullFilePath = os.path.join(dirPath, fileName)
        if filterFunc is not None and filterFunc(fullFilePath):
          continue
        result.append(fullFilePath)

    return result

  @staticmethod
  def RemoveDir(dirPath):
    if os.path.exists(dirPath): #检查文件夹是否存在,不存在则创建该目录
      shutil.rmtree(dirPath)  # 删除文件夹

  @staticmethod
  def RemoveFile(filePath):
    if os.path.exists(filePath):
      os.remove(filePath)