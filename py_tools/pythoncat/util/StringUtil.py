import re


class StringUtil(object):
  # 判断是否为none或者""
  @staticmethod
  def IsNoneOrEmpty(value):
    if value is None or value.strip() == "":
      return True
    return False

  # 与str.split类似，但忽略双引号中的的split
  # ignore_left,ignore_right注意转移字符，需要加上\,例如忽略",则需要输入\\\"
  @staticmethod
  def SplitIgnore(content, split=",", ignore_left="\\\"", ignore_right=None):
    result = []
    if ignore_right is None:
      ignore_right = ignore_left
    pattern = "(%s)(?=([^%s]*%s[^%s]*%s)*[^%s]*$)" % (
    split, ignore_left, ignore_left, ignore_right, ignore_right, ignore_right)
    index = 0
    for match in re.finditer(pattern, content):
      result.append(content[index:match.start()])
      index = match.end()
    result.append(content[index:])
    return result

  # 转义字符
  @staticmethod
  def Escape(value):
    value = value.replace(r"\r\n", "\n") \
      .replace(r"\r", "\n")
    value = value.replace(r"\\", "\\") \
      .replace(r"\a", "\a") \
      .replace(r"\b", "\b") \
      .replace(r"\f", "\f") \
      .replace(r"\n", "\n") \
      .replace(r"\r", "\r") \
      .replace(r"\t", "\t") \
      .replace(r"\v", "\v") \
      .replace(r"\"", "\"") \
      .replace(r"\'", "\'")
    return value

  @staticmethod
  def UpperFirstLetter(value):
    result = (value[0:1]).upper()+ value[1:]
    return result

  @staticmethod
  def LowerFirstLetter(value):
    result = (value[0:1]).lower() + value[1:]
    return result

  def GetSpace(indent):
    space_count = 2
    if indent == 0:
      return ""
    return " " *(indent* space_count)

  @staticmethod
  def UpperFirstLetterOfArray(array):
    new_array = []
    for org_element in array:
      new_element = StringUtil.UpperFirstLetter(org_element)
      new_array.append(new_element)
    return new_array