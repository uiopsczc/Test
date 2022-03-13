class DictUtil(object):
  @staticmethod
  def GetOrAddDefault(dict, key, defaultValue):
    if not (key in dict.keys()):
      dict[key] = defaultValue
    return dict[key]

  @staticmethod
  def GetOrGetDefault(dict, key, defaultValue):
    if dict[key] is None:
      return defaultValue
    return dict[key]
