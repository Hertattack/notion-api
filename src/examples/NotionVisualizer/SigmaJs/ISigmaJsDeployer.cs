namespace NotionVisualizer.SigmaJs
{
    public interface ISigmaJsDeployer
    {
        void Deploy(string outputFolder, string sigmaJsPackageFile);
    }
}