using SlimMq.Models;

namespace SlimMq.Utilities
{
    internal static class AlternateDataStreamUtility
    {
        private const string ADS_STREAM_TAG = "SlimMq";

        internal static void WriteFileIdToADS(string filePath, QueueFileMeta metaData)
        {
            var pathWithStreamTag = GenerateFilePathWithStreamTag(filePath);

            using (FileStream fs = new FileStream(pathWithStreamTag, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(metaData);
                }
            }
        }

        internal static QueueFileMeta? ReadFileIdFromADS(string filePath)
        {
            var pathWithStreamTag = GenerateFilePathWithStreamTag(filePath);

            using (FileStream fs = new FileStream(pathWithStreamTag, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs))
            {
                var data =  reader.ReadToEndAsync();
            }

            return new QueueFileMeta();
        }

        internal static bool IsNTFS(string path)
        {
            var rootPath = Path.GetPathRoot(path)!;

            var driveInfo = new DriveInfo(rootPath);

            return driveInfo.DriveFormat.Equals("ntfs", StringComparison.OrdinalIgnoreCase);
        }


        private static string GenerateFilePathWithStreamTag(string path)
        {
            return $"{path}:{ADS_STREAM_TAG}";
        }
    }
}
 