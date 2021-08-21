using System.IO;
using System.IO.Compression;

namespace NotionVisualizer.SigmaJs
{
    public class SigmaJsDeployer : ISigmaJsDeployer
    {
        public void Deploy(string folder, string outputFolder)
        {
            using var zipInputStream = new FileStream(outputFolder, FileMode.Open);
            using var zip = new ZipArchive(zipInputStream);

            zip.ExtractToDirectory(folder);
        }
    }
}