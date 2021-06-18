import unicodedata  # 处理ASCii码的包


class NumberUtil(object):
  @staticmethod
  def IsNumber(s):
    try:  # 如果能运行float(s)语句，返回True（字符串s是浮点数）
      float(s)
      return True
    except ValueError:  # ValueError为Python的一种标准异常，表示"传入无效的参数"
      pass  # 如果引发了ValueError这种异常，不做任何事情（pass：不做任何事情，一般用做占位语句）
    try:
      unicodedata.numeric(s)  # 把一个表示数字的字符串转换为浮点数返回的函数
      return True
    except (TypeError, ValueError):
      pass
    return False
