//using System;
//using System.Collections.Generic;
//
//namespace CsCat
//{
//  public class RedDotLogic : GameEntity
//  {
//    private Dictionary<string, RedDotInfo>
//      red_dot_info_dict = new Dictionary<string, RedDotInfo>(); //  dict<tag = red_dot_info>
//
//    private Dictionary<string, Dictionary<string, bool>> listen_trigger_name_dict =
//      new Dictionary<string, Dictionary<string, bool>>(); //dict<listen_name = dict<trigger_name(self_or_parent_tag) = true>>
//
//    private Dictionary<string, Dictionary<string, bool>> tag_trigger_name_dict =
//      new Dictionary<string, Dictionary<string, bool>>(); // dict<tag = dict<trigger_name(self_or_parent_tag) = true>>
//
//    private Dictionary<string, EventListenerInfo>
//      listener_dict =
//        new Dictionary<string, EventListenerInfo>(); //dict<listen_name = listener> listener触发listen_trigger_name_dict中对应的trigger_name
//
//    public override void Init()
//    {
//      base.Init();
//      foreach (var red_dot_info in RedDotConst.Red_Dot_Info_List)
//      {
//        this.AddRedDotInfo(red_dot_info.tag, red_dot_info.check_func, red_dot_info.listen_name_list,
//          red_dot_info.child_tag_list, red_dot_info.child_tag_all_params_func_dict);
//      }
//
//      ListenAllEvent();
//    }
//
//    public void AddRedDotInfo(string tag, MethodInvoker check_func, List<string> listen_name_list,
//      List<string> child_tag_list, Dictionary<string, MethodInvoker> child_tag_all_params_func_dict)
//    {
//      if (this.red_dot_info_dict.ContainsKey(tag))
//        throw new Exception(string.Format("重复的tag {0}", tag));
//      if (child_tag_list != null)
//        this.red_dot_info_dict[tag] = this.CreateRedDotInfoContiansChildTagList(tag, check_func, listen_name_list,
//          child_tag_list, child_tag_all_params_func_dict);
//      else
//        this.red_dot_info_dict[tag] =
//          new RedDotInfo(tag, check_func, listen_name_list, child_tag_list, child_tag_all_params_func_dict);
//    }
//
//    private RedDotInfo CreateRedDotInfoContiansChildTagList(string tag, MethodInvoker check_func,
//      List<string> listen_name_list,
//      List<string> child_tag_list, Dictionary<string, MethodInvoker> child_tag_all_params_func_dict)
//    {
//      Func<object[], bool> warp_check_func = (args) =>
//      {
//        foreach (var child_tag in child_tag_list)
//        {
//          var child_red_dot_info = this.red_dot_info_dict[child_tag];
//          if (child_tag_all_params_func_dict != null && child_tag_all_params_func_dict.ContainsKey(child_tag))
//          {
//            var child_tag_all_params = child_tag_all_params_func_dict[child_tag].Invoke<object[]>();
//            foreach (var child_tag_params in child_tag_all_params)
//            {
//              if (child_red_dot_info.check_func.Invoke<bool>(child_tag_params as object[])) // 一般是这里调用
//                return true;
//            }
//          }
//          else
//          {
//            if (child_red_dot_info.check_func.Invoke<bool>()) //一般是这里调用
//              return true;
//          }
//        }
//
//        if (check_func != null)
//          return check_func.Invoke<bool>();
//        return false;
//      };
//      return new RedDotInfo(tag, new MethodInvoker(warp_check_func), listen_name_list, child_tag_list);
//    }
//
//    private void ListenAllEvent()
//    {
//      foreach (var tag in this.red_dot_info_dict.Keys)
//      {
//        var red_dot_info = this.red_dot_info_dict[tag];
//        var listen_name_list = red_dot_info.listen_name_list ?? new List<string>();
//        // child_tag触发的时候连带触发其父tag,构建child_tag对应的父tag引用
//        this.RecordAllTrigger(null, tag, red_dot_info); // 因为可能没有child_red_dot_info.listen_name_list
//        foreach (var listen_name in listen_name_list)
//        {
//          this.RecordAllTrigger(listen_name, tag, red_dot_info);
//          if (!this.listener_dict.ContainsKey(listen_name))
//          {
//            this.listener_dict[listen_name] = this.AddListener(listen_name, () =>
//            {
//              // 这里再转触发给red_dot_mgr
//              var dict = this.listen_trigger_name_dict[listen_name];
//              foreach (var trigger_name in dict.Keys)
//              {
//                this.Broadcast(trigger_name);
//              }
//            });
//          }
//        }
//      }
//    }
//
//    private void RecordAllTrigger(string listen_name, string trigger_name, RedDotInfo red_dot_info)
//    {
//      var tag = red_dot_info.tag;
//      this.RecordTagTrigger(tag, trigger_name);
//      this.RecordListenTrigger(listen_name, trigger_name);
//      if (red_dot_info.child_tag_list != null)
//      {
//        foreach (var child_tag in red_dot_info.child_tag_list)
//        {
//          var child_red_dot_info = this.red_dot_info_dict[child_tag];
//          //child_tag触发的时候连带触发其父tag,构建child_tag对应的父tag引用
//          this.RecordAllTrigger(null, trigger_name, child_red_dot_info); // 因为可能没有child_red_dot_info.listen_name_list
//          foreach (var _listen_name in child_red_dot_info.listen_name_list)
//            this.RecordAllTrigger(_listen_name, trigger_name, child_red_dot_info);
//        }
//      }
//    }
//
//    private void RecordTagTrigger(string tag, string trigger_name)
//    {
//      if (tag.IsNullOrWhiteSpace())
//        return;
//      var dict = this.tag_trigger_name_dict.GetOrAddDefault(tag, () => new Dictionary<string, bool>());
//      dict[tag] = true;
//    }
//
//    private void RecordListenTrigger(string listen_name, string trigger_name)
//    {
//      if (listen_name.IsNullOrWhiteSpace())
//        return;
//      var dict = this.listen_trigger_name_dict.GetOrAddDefault(listen_name, () => new Dictionary<string, bool>());
//      dict[trigger_name] = true;
//    }
//
//
//    ///////////////////////////////////////Util////////////////////////////////////////////////////
//    public RedDotInfo GetRedDotInfoByTag(string tag)
//    {
//      if (!red_dot_info_dict.ContainsKey(tag))
//        throw new Exception(string.Format("不存在的tag {0}", tag));
//      var red_dot_info = this.red_dot_info_dict[tag];
//      return red_dot_info;
//    }
//
//    public void TriggerTag(string tag)
//    {
//      if (!tag_trigger_name_dict.ContainsKey(tag))
//        throw new Exception(string.Format("TriggerTag不存在的 tag  {0}", tag));
//      var dict = this.tag_trigger_name_dict[tag];
//      foreach (var trigger_name in dict.Keys)
//      {
//        this.Broadcast(trigger_name);
//      }
//    }
//  }
//}