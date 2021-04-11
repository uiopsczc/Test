

namespace CsCat
{
  public class NumberUnitInfo
  {
    public int index; // list 中的index
    public int zhi_shu; //指数
    public string number_unit; //数字单位
    public string id;

    public NumberUnitInfo(int index, int zhi_shu, string number_unit, string id)
    {
      this.index = index;
      this.zhi_shu = zhi_shu;
      this.number_unit = number_unit;
      this.id = id;
    }
  }
}
