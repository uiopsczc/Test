using System;
using System.Collections;

namespace CsCat
{
  public class Scenes
  {
    private Doer parent_doer;
    private string sub_doer_key;

    public Scenes(Doer parent_doer, string sub_doer_key)
    {
      this.parent_doer = parent_doer;
      this.sub_doer_key = sub_doer_key;
    }

    ////////////////////DoXXX/////////////////////////////////
    //卸载
    public void DoRelease()
    {
      SubDoerUtil3.DoReleaseSubDoer<Scene>(this.parent_doer, this.sub_doer_key);
    }

    //保存
    public void DoSave(Hashtable dict, Hashtable dict_tmp, string save_key = null)
    {
      save_key = save_key ?? "scenes";
      var scenes = this.GetScenes();
      var dict_scenes = new Hashtable();
      var dict_scenes_tmp = new Hashtable();
      foreach (var scene in scenes)
      {
        var dict_scene = new Hashtable();
        var dict_scene_tmp = new Hashtable();
        scene.PrepareSave(dict_scene, dict_scene_tmp);
        string rid = scene.GetRid();
        dict_scenes[rid] = dict_scene;
        if (!dict_scene_tmp.IsNullOrEmpty())
          dict_scenes_tmp[rid] = dict_scene_tmp;
      }

      if (!dict_scenes.IsNullOrEmpty())
        dict[save_key] = dict_scenes;
      if (!dict_scenes_tmp.IsNullOrEmpty())
        dict_tmp[save_key] = dict_scenes_tmp;
    }

    //还原
    public void DoRestore(Hashtable dict, Hashtable dict_tmp, string restore_key = null)
    {
      restore_key = restore_key ?? "scenes";
      this.ClearScenes();
      var dict_scenes = dict.Remove2<Hashtable>(restore_key);
      var dict_scenes_tmp = dict_tmp?.Remove2<Hashtable>(restore_key);
      if (!dict_scenes.IsNullOrEmpty())
      {
        foreach (string rid in dict_scenes.Keys)
        {
          var scene_dict = this.GetSceneDict_ToEdit();
          Hashtable dict_scene = dict_scenes[rid] as Hashtable;
          Scene scene = Client.instance.sceneFactory.NewDoer(rid) as Scene;
          scene.SetEnv(this.parent_doer);
          Hashtable dict_scene_tmp = null;
          if (dict_scenes_tmp != null && dict_scenes_tmp.ContainsKey(rid))
            dict_scene_tmp = dict_scenes_tmp[rid] as Hashtable;
          scene.FinishRestore(dict_scene, dict_scene_tmp);
          scene_dict[rid] = scene;
        }
      }
    }
    ///////////////////////////////OnXXX/////////////////////////////////////////////


    ////////////////////////////////////////////////////////////////////////////
    public Scene[] GetScenes(string id = null, Func<Scene, bool> filter_func = null)
    {
      return SubDoerUtil3.GetSubDoers(parent_doer, sub_doer_key, id, filter_func);
    }

    public Hashtable GetSceneDict_ToEdit() //可以直接插入删除
    {
      return SubDoerUtil3.GetSubDoerDict_ToEdit(parent_doer, sub_doer_key);
    }


    public Scene GetScene(string id_or_rid)
    {
      return SubDoerUtil3.GetSubDoer<Scene>(parent_doer, sub_doer_key, id_or_rid);
    }

    public void ClearScenes()
    {
      SubDoerUtil3.ClearSubDoers<Scene>(this.parent_doer, this.sub_doer_key, scene => { });
    }
  }
}