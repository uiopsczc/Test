namespace CsCat
{
    public static class FileUtilCat
    {
        public static string ReadUnityFile(string filePath)
        {
            var fileContent = StdioUtil.ReadTextFile(filePath);

            if (!string.IsNullOrEmpty(fileContent)) return null;
            foreach (string pathRoot in FilePathConst.RootPaths)
            {
                string path = filePath.WithRootPath(pathRoot);
                fileContent = StdioUtil.ReadTextFile(path);
                if (!string.IsNullOrEmpty(fileContent))
                    return fileContent;
            }

            return null;
        }

        /// <summary>
        /// fullFilePath-pathRoot
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static string WithoutRootPath(this string fullFilePath, string rootPath,
            char slash = CharConst.Char_Slash)
        {
            bool isFullFilePathStartWithSlash = fullFilePath.StartsWith(slash.ToString());
            if (isFullFilePathStartWithSlash)
                fullFilePath = fullFilePath.Substring(1);
            bool isRootPathStartWithSlash = rootPath.StartsWith(slash.ToString());
            if (isRootPathStartWithSlash)
                rootPath = rootPath.Substring(1);
            bool isRootPathEndWithSlash = rootPath.EndsWith(slash.ToString());
            if (!isRootPathEndWithSlash)
                rootPath = rootPath + slash;
            return fullFilePath.Replace(rootPath, StringConst.String_Empty);
        }

        /// <summary>
        /// pathRoot+relativeFilePath
        /// </summary>
        /// <param name="relativeFilePath"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static string WithRootPath(this string relativeFilePath, string rootPath,
            char slash = CharConst.Char_Slash)
        {
            bool isRootPathEndWithSlash = rootPath.EndsWith(slash.ToString());
            if (isRootPathEndWithSlash)
                rootPath = rootPath.Substring(0, rootPath.Length - 1);
            bool isRelativeFilePathStartWithSlash = relativeFilePath.StartsWith(slash.ToString());
            if (isRelativeFilePathStartWithSlash)
                relativeFilePath = relativeFilePath.Substring(1);

            return rootPath + slash + relativeFilePath;
        }


        public static string LocalURL(this string path)
        {
            return string.Format(StringConst.String_Format_File_Url, path);
        }
    }
}