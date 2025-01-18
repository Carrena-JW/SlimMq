namespace SlimMq.Utilities
{
    internal static class DirectoryUtility
    {
        internal static void CreateFolder(string path, bool isHidden = false)
        {
            if (Directory.Exists(path) is true)
            {
                return;
            }

            // 폴더 생성
            Directory.CreateDirectory(path);

            // 폴더 숨김 속성 설정
            if (isHidden is true)
            {
                var dirInfo = new DirectoryInfo(path);
                dirInfo.Attributes |= FileAttributes.Hidden;
            }
        }
    }
}
