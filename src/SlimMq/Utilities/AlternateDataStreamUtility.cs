﻿namespace SlimMq.Utilities
{
    internal static class AlternateDataStreamUtility
    {
        private const string ADS_STREAM_TAG = "SlimMq";

        internal static void WriteFileIdToADS(string filePath, QueueFileMeta metaData)
        {
            var pathWithStreamTag = GenerateFilePathWithStreamTag(filePath);

            using (var fs = new FileStream(pathWithStreamTag, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    var json = JsonConvert.SerializeObject(metaData);
                    writer.Write(json);
                }
            }
        }

        internal static async Task<QueueFileMeta?> ReadFileIdFromADSAsync(string filePath)
        {
            var pathWithStreamTag = GenerateFilePathWithStreamTag(filePath);

            using (var fs = new FileStream(pathWithStreamTag, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {

                    var data =   await reader.ReadToEndAsync();
                    var metaData = JsonConvert.DeserializeObject<QueueFileMeta>(data);
                    return metaData;
                }
            }
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
 