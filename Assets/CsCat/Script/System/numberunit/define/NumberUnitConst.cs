using System.Collections.Generic;

namespace CsCat
{
  public static class NumberUnitConst
  {
    public static Dictionary<string, NumberUnitInfo> To_Number_Unit_Dict(List<NumberUnitInfo> number_unit_list)
    {
      if (number_unit_list == null)
        return null;
      var number_unit_dict = new Dictionary<string, NumberUnitInfo>();
      foreach (var number_unit_info in number_unit_list)
        number_unit_dict[number_unit_info.id] = number_unit_info;
      return number_unit_dict;
    }

    public static List<NumberUnitInfo> To_Number_Unit_List(Dictionary<string, NumberUnitInfo> number_unit_dict)
    {
      if (number_unit_dict == null)
        return null;
      var number_unit_list = new List<NumberUnitInfo>();
      foreach (var number_unit_info in number_unit_dict.Values)
        number_unit_list.Add(number_unit_info);
      number_unit_list.QuickSort((a, b) => { return a.index <= b.index; });
      return number_unit_list;
    }

    public static List<NumberUnitInfo> Number_Unit_List = new List<NumberUnitInfo>()
    {
      new NumberUnitInfo(0, 0, "", "n"),
      new NumberUnitInfo(1, 3, "K", "K"),
      new NumberUnitInfo(2, 6, "M", "M"),
      new NumberUnitInfo(3, 9, "G", "G"),
      new NumberUnitInfo(4, 12, "T", "T"),
      new NumberUnitInfo(5, 15, "q", "q"),
      new NumberUnitInfo(6, 18, "Q", "Q"),
      new NumberUnitInfo(7, 21, "s", "s"),
      new NumberUnitInfo(8, 24, "S", "S"),
      new NumberUnitInfo(9, 27, "O", "O"),
      new NumberUnitInfo(10, 30, "N", "N"),
      new NumberUnitInfo(11, 33, "D", "D"),
      new NumberUnitInfo(12, 36, "Ud", "Ud"),
      new NumberUnitInfo(13, 39, "Dd", "Dd"),
      new NumberUnitInfo(14, 42, "Td", "Td"),
      new NumberUnitInfo(15, 45, "Qt", "Qt"),
      new NumberUnitInfo(16, 48, "Qd", "Qd"),
      new NumberUnitInfo(17, 51, "Sd", "Sd"),
      new NumberUnitInfo(18, 54, "St", "St"),
      new NumberUnitInfo(19, 57, "Od", "Od"),
      new NumberUnitInfo(20, 60, "Nd", "Nd"),
      new NumberUnitInfo(21, 63, "V", "V"),
    };

    private static Dictionary<string, NumberUnitInfo> _Number_Unit_Dict = new Dictionary<string, NumberUnitInfo>();

    public static Dictionary<string, NumberUnitInfo> Number_Unit_Dict
    {
      get
      {
        if (_Number_Unit_Dict.Count == 0)
          _Number_Unit_Dict = To_Number_Unit_Dict(Number_Unit_List);
        return _Number_Unit_Dict;
      }
    }


    public static List<NumberUnitInfo> Number_Unit_List2 = new List<NumberUnitInfo>()
    {
      new NumberUnitInfo(0, 0, "", "n"),
      new NumberUnitInfo(1, 3, "K", "K"),
      new NumberUnitInfo(2, 6, "M", "M"),
      new NumberUnitInfo(3, 9, "B", "B"), //此处与Number_Unit_List1不同
      new NumberUnitInfo(4, 12, "T", "T"),
      new NumberUnitInfo(5, 15, "q", "q"),
      new NumberUnitInfo(6, 18, "Q", "Q"),
      new NumberUnitInfo(7, 21, "s", "s"),
      new NumberUnitInfo(8, 24, "S", "S"),
      new NumberUnitInfo(9, 27, "O", "O"),
      new NumberUnitInfo(10, 30, "N", "N"),
      new NumberUnitInfo(11, 33, "D", "D"),
      new NumberUnitInfo(12, 36, "Ud", "Ud"),
      new NumberUnitInfo(13, 39, "Dd", "Dd"),
      new NumberUnitInfo(14, 42, "Td", "Td"),
      new NumberUnitInfo(15, 45, "Qt", "Qt"),
      new NumberUnitInfo(16, 48, "Qd", "Qd"),
      new NumberUnitInfo(17, 51, "Sd", "Sd"),
      new NumberUnitInfo(18, 54, "St", "St"),
      new NumberUnitInfo(19, 57, "Od", "Od"),
      new NumberUnitInfo(20, 60, "Nd", "Nd"),
      new NumberUnitInfo(21, 63, "V", "V"),
    };

    private static Dictionary<string, NumberUnitInfo> _Number_Unit_Dict2 = new Dictionary<string, NumberUnitInfo>();

    public static Dictionary<string, NumberUnitInfo> Number_Unit_Dict2
    {
      get
      {
        if (_Number_Unit_Dict2.Count == 0)
          _Number_Unit_Dict2 = To_Number_Unit_Dict(Number_Unit_List2);
        return _Number_Unit_Dict2;
      }
    }
  }
}
