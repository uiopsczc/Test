namespace CsCat
{
    public class NumberUnitInfo
    {
        public int index; // list 中的index
        public int zhiShu; //指数
        public string numberUnit; //数字单位
        public string id;

        public NumberUnitInfo(int index, int zhiShu, string numberUnit, string id)
        {
            this.index = index;
            this.zhiShu = zhiShu;
            this.numberUnit = numberUnit;
            this.id = id;
        }
    }
}