using System;
using System.Collections.Generic;

namespace CsCat
{
  public class BuildUtil
  {
    public static bool CheckResVersionIsNew(string client_res_version, string server_res_version)
    {
      if (client_res_version == null)
        return true;
      var client_ver_list = client_res_version.Split('.');
      var server_ver_list = server_res_version.Split('.');

      if (client_res_version.Length >= 3 && server_ver_list.Length >= 3)
      {
        var client_v0 = int.Parse(client_ver_list[0]);
        var client_v1 = int.Parse(client_ver_list[1]);
        var client_v2 = int.Parse(client_ver_list[2]);
        var server_v0 = int.Parse(server_ver_list[0]);
        var server_v1 = int.Parse(server_ver_list[1]);
        var server_v2 = int.Parse(server_ver_list[2]);

        if (client_v0 < server_v0)
          return true;
        if (client_v0 > server_v0)
          return false;

        if (client_v1 < server_v1)
          return true;
        if (client_v1 > server_v1)
          return false;

        if (client_v2 < server_v2)
          return true;
        if (client_v2 >= server_v2)
          return false;
      }

      return false;
    }


    public static List<string> GetManifestDiffAssetBundleList(Manifest client_manifest, Manifest server_manifest)
    {
      var different_assetBundle_list = new List<string>();

      if (client_manifest.assetBundleManifest == null)
      {
        different_assetBundle_list.AddRange(server_manifest.GetAllAssetBundlePaths());
        return different_assetBundle_list;
      }

      var server_assetBundle_names = server_manifest.GetAllAssetBundlePaths();
      var client_assetBundle_names = client_manifest.GetAllAssetBundlePaths();
      foreach (var server_assetBundle_name in server_assetBundle_names)
      {
        var index = Array.FindIndex(client_assetBundle_names, element => element.Equals(server_assetBundle_name));
        if (index == -1)
          different_assetBundle_list.Add(server_assetBundle_name);
        else if (!client_manifest.GetAssetBundleHash(client_assetBundle_names[index])
          .Equals(server_manifest.GetAssetBundleHash(server_assetBundle_name)))
          different_assetBundle_list.Add(server_assetBundle_name);
      }

      return different_assetBundle_list;
    }
  }
}