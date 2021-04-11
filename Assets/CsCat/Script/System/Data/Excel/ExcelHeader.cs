using System;

namespace CsCat
{
  /// <summary>
  /// excel的头名 即列名
  /// </summary>
  [Serializable]
  public class ExcelHeader
  {
    #region field

    public string name;
    public ExcelDataType type;

    #endregion

    #region public method

    public override string ToString()
    {
      return string.Format("[DataSourceHeader name={0}, type={1}]", this.name, this.type);
    }

    #endregion

  }
}