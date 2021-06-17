class DictUtil(object):
  @staticmethod
  def GetOrAddDefault(dict, key, default_value):
    if not (key in dict.keys()):
      dict[key] = default_value
    return dict[key]

  @staticmethod
  def GetOrGetDefault(dict, key, default_value):
    if dict[key] is None:
      return default_value
    return dict[key]
