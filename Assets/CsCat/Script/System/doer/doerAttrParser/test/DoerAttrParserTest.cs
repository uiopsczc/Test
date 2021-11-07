namespace CsCat
{
    public static class DoerAttrParserTest
    {
        public static void Test()
        {
            DoerAttrParser doerAttrParser = new DoerAttrParser(Client.instance.user);
            var result = "";
            //    result = doerAttrParser.ParseString("{#u.pos2}");
            //    result = doerAttrParser.Parse("{eval((5+4)*6)}");//eval()
            //    result = doerAttrParser.Parse("{#cfgData.CfgItemData.1.icon_path}");//
            //    result = doerAttrParser.Parse("{@hasSubString(abcdef,de)}");//
            //   for (int i = 1; i < 100; i++)
            //   {
            //     result = doerAttrParser.Parse("{random(4,8)}"); //random()
            //     LogCat.log(result);
            //   }
            LogCat.log(result);
        }

        public static void Test2()
        {
            Client.instance.user.SetPos2(5, 6);
        }
    }
}