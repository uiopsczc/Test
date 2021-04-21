using System;
using System.Collections.Generic;

namespace CsCat
{
  public static class CompareUtilTest
  {
    class TmpStruct
    {
      public int code;
      public string name;
      public int age;
      public string tmp;

      public TmpStruct(int code, string name, int age, string tmp)
      {
        this.code = code;
        this.name = name;
        this.age = age;
        this.tmp = tmp;
      }
      public override string ToString()
      {
        return "[" + code + "," + name + "," + age + "," + tmp + "]";
      }
    }
    public static void Test()
    {
      List<TmpStruct> data_list = new List<TmpStruct>();

      data_list.Add(new TmpStruct(3, "a", 5, "f1"));
      data_list.Add(new TmpStruct(2, "b", 1, "f2"));
      data_list.Add(new TmpStruct(4, "c", 6, "f6"));
      data_list.Add(new TmpStruct(4, "c", 6, "f3"));
      data_list.Add(new TmpStruct(2, "d", 2, "f4"));
      data_list.Add(new TmpStruct(3, "a", 4, "f5"));


      data_list.SortWithCompareRules(CompareRule1, CompareRule2, CompareRule3);
      LogCat.warn(data_list);
    }

    static int CompareRule1(TmpStruct a, TmpStruct b)
    {
      return a.code - b.code;
    }

    static int CompareRule2(TmpStruct a, TmpStruct b)
    {
      return a.name.CompareTo(b.name);
    }

    static int CompareRule3(TmpStruct a, TmpStruct b)
    {
      return a.age - b.age;
    }

  }
}