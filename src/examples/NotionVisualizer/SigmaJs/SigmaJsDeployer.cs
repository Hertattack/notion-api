using System.IO;
using System.IO.Compression;

namespace NotionVisualizer.SigmaJs
{
    public class SigmaJsDeployer : ISigmaJsDeployer
    {
        public void Deploy(string outputFolder, string sigmaJsZipFile)
        {
            using var zipInputStream = new FileStream(sigmaJsZipFile, FileMode.Open);
            using var zip = new ZipArchive(zipInputStream);

            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            var sigmaJsLibraryFolder = Path.Join(outputFolder, "sigmajs");
            if (!Directory.Exists(sigmaJsLibraryFolder))
                Directory.CreateDirectory(sigmaJsLibraryFolder);

            zip.ExtractToDirectory(outputFolder);
        }
    }
}