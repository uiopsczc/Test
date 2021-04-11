using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class ColliderListInfo
  {
    public Vector3 center;

    public List<AABBBox> atk_box_list = new List<AABBBox>();

    public List<AABBBox> hit_box_list = new List<AABBBox>();
    

    public void DoSave(Hashtable dict)
    {
      dict["center"] = center.ToString();
      ArrayList atk_box_arrayList = atk_box_list.DoSaveList((atk_box, sub_dict) => atk_box.DoSave(sub_dict));
      ArrayList hit_box_arrayList = hit_box_list.DoSaveList((hit_box, sub_dict) => hit_box.DoSave(sub_dict));
      dict["atk_box_arrayList"] = atk_box_arrayList;
      dict["hit_box_arrayList"] = hit_box_arrayList;
    }

    public void DoRestore(Hashtable dict)
    {
      center = dict["center"].ToString().ToVector3();

      atk_box_list.DoRestoreList(dict["atk_box_arrayList"] as ArrayList, (sub_dict) =>
      {
        AABBBox atk_box = new AABBBox();
        atk_box.DoRestore(sub_dict);
        return atk_box;
      });

      hit_box_list.DoRestoreList(dict["hit_box_arrayList"] as ArrayList, (sub_dict) =>
      {
        AABBBox hit_box = new AABBBox();
        hit_box.DoRestore(sub_dict);
        return hit_box;
      });
    }
  }
}