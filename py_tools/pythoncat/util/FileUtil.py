import os
import shutil
import chardet
from pythoncat.util.StringUtil import *
from chardet.universaldetector import UniversalDetector
detector = UniversalDetector()

class FileUtil(object):
  @staticmethod
  def GetFileEncoding(file_path):
    with open(file_path, 'rb') as file:
      encoding = chardet.detect(file.read())['encoding'] # 获取编码
    return encoding



  @staticmethod
  def ReadFile(file_path):
    file = open(file_path, 'r', encoding=FileUtil.GetFileEncoding(file_path), errors ='ignore')
    content = file.read()
    file.close()
    return content

  @staticmethod
  def ReadFileAsLineList(file_path):
    file = open(file_path, 'r', encoding=FileUtil.GetFileEncoding(file_path), errors ='ignore')
    line_list = file.readlines()
    file.close()
    return line_list

  @staticmethod
  def WriteFile(file_path, content,mode = 'w' , encoding ="utf-8" ): #mode w:替换 a:添加到末尾
    dir_path = os.path.dirname(file_path)
    if not StringUtil.IsNoneOrEmpty(dir_path) and not os.path.exists(dir_path): #检查文件夹是否存在,不存在则创建该目录
      os.makedirs(dir_path)
    # encoding = FileUtil.GetFileEncoding(file_path)
    file = open(file_path, mode, encoding=encoding, errors ='ignore')
    file.write(content)
    file.close()

  @staticmethod
  #获取root_dir_path下的文件路径，如何该该文件路径在filter_func中返回true，则过滤掉
  def GetFilePathList(root_dir_path, filter_func = None):
    result = []
    for dir_path, dir_names, file_names in os.walk(root_dir_path):
      for file_name in file_names:
        full_file_path = os.path.join(dir_path, file_name)
        if filter_func is not None and filter_func(full_file_path):
          continue
        result.append(full_file_path)

    return result

  @staticmethod
  def RemoveDir(dir_path):
    if os.path.exists(dir_path): #检查文件夹是否存在,不存在则创建该目录
      shutil.rmtree(dir_path)  # 删除文件夹

  @staticmethod
  def RemoveFile(file_path):
    if os.path.exists(file_path):
      os.remove(file_path)