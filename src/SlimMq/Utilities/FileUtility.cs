namespace SlimMq.Utilities
{
    internal static class FileUtility
    {
        internal static async Task DeleteWithRetry(string fullPath, int maxRetry = 100)
        {
            var attempts = 0;

			while (true)
			{
				try
				{
					File.Delete(fullPath);
					return;
				}
                catch (IOException ex)
                {
                    attempts++;

                    if (attempts < maxRetry)
                    {
                        await Task.Delay(10);
                    }
                    else
                    {
                        throw new ApplicationException("설정된 시도회수를 모두 초과하였습니다.", ex);
                    }
                }
            }
        }
    }
}
