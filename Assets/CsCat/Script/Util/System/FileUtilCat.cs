namespace CsCat
{
  public static class FileUtilCat
  {
    public static string ReadUnityFile(string file_path)
    {
      string file_content;
      file_content = StdioUtil.ReadTextFile(file_path);

      if (string.IsNullOrEmpty(file_content))
      {
        foreach (string path_root in FilePathConst.RootPaths)
        {
          string path = file_path.WithRootPath(path_root);
          file_content = StdioUtil.ReadTextFile(file_path);
          if (!string.IsNullOrEmpty(file_content))
            return file_content;
        }
      }

      return null;
    }

    /// <summary>
    /// fullFilePath-pathRoot
    /// </summary>
    /// <param name="full_file_path"></param>
    /// <param name="root_path"></param>
    /// <returns></returns>
    public static string WithoutRootPath(this string full_file_path, string root_path, char slash = '/')
    {
      bool is_full_file_path_start_with_slash = full_file_path.StartsWith(slash.ToString());
      if (is_full_file_path_start_with_slash)
        full_file_path = full_file_path.Substring(1);
      bool is_root_path_start_with_slash = root_path.StartsWith(slash.ToString());
      if (is_root_path_start_with_slash)
        root_path = root_path.Substring(1);
      bool is_root_path_end_with_slash = root_path.EndsWith(slash.ToString());
      if (!is_root_path_end_with_slash)
        root_path = string.Format("{0}{1}", root_path, slash);



      return full_file_path.Replace(root_path, "");
    }

    /// <summary>
    /// pathRoot+relativeFilePath
    /// </summary>
    /// <param name="relative_file_path"></param>
    /// <param name="root_path"></param>
    /// <returns></returns>
    public static string WithRootPath(this string relative_file_path, string root_path, char slash = '/')
    {
      bool is_root_path_end_with_slash = root_path.EndsWith(slash.ToString());
      if (is_root_path_end_with_slash)
        root_path = root_path.Substring(0, root_path.Length - 1);
      bool is_relative_file_path_start_with_slash = relative_file_path.StartsWith(slash.ToString());
      if (is_relative_file_path_start_with_slash)
        relative_file_path = relative_file_path.Substring(1);

      return string.Format("{0}{1}{2}", root_path, slash, relative_file_path);
    }


    public static string localURL(this string path)
    {
      return string.Format("file://{0}", path);
    }


  }
}