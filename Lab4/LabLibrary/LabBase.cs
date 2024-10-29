namespace LabLibrary;

public abstract class LabBase
{
    protected abstract string LabName { get; }
    public abstract void Run(string inputFile, string outputFile);
}
