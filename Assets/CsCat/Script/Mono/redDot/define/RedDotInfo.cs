using System.Collections.Generic;

namespace CsCat
{
  public class RedDotInfo
  {
    public string tag;
    public MethodInvoker check_func;
    public List<string> listen_name_list = new List<string>();
    public List<string> child_tag_list = new List<string>();
    public Dictionary<string, MethodInvoker> child_tag_all_params_func_dict = new Dictionary<string, MethodInvoker>();

    public RedDotInfo(string tag, MethodInvoker check_func, List<string> listen_name_list,
      List<string> child_tag_list = null, Dictionary<string, MethodInvoker> child_tag_all_params_func_dict = null)
    {
      this.tag = tag;
      this.check_func = check_func;
      this.listen_name_list = listen_name_list;
      this.child_tag_list = child_tag_list;
      this.child_tag_all_params_func_dict = child_tag_all_params_func_dict;
    }
  }
}