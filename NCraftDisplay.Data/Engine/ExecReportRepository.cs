using Newtonsoft.Json;
using System.IO;

namespace NCraftDisplay.Data.Engine
{
    public class ExecReportRepository
    {
        public ExecReportRepository(string workingDir)
        {
            _workingDir = workingDir;
        }

        public void Save(RunResult result, string playerDir)
        {
            var filePath = Path.Combine(
                _workingDir,
                playerDir,
                string.Format("run_{0:yyyy-MM-dd_HH-mm-ss}.json", result.RunDate));

            using (var fs = new FileStream(filePath, FileMode.CreateNew))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, result);
            }
        }

        private readonly string _workingDir;
    }
}