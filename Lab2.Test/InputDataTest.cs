
namespace Lab2.Test;

public class InputDataTest : IDisposable
{
    private readonly string _temporaryInputFile;
    private readonly string _temporaryOutputFile;

    public InputDataTest()
    {
        _temporaryInputFile = Path.GetTempFileName();
        _temporaryOutputFile = Path.GetTempFileName();
    }

    [Fact]
    public void ProcessData_ValidInput_ReturnsCorrectResult()
    {
        string[] input =
        [
        "3",
        "8:19:16",
        "2:05:11",
        "12:50:07"
        ];
        string expectedResult = "2:05:11";

        File.WriteAllLines(_temporaryInputFile, input);
        string actualResult = Program.ProcessData(_temporaryInputFile);

        Assert.Equal(expectedResult, actualResult);
    }
    public void Dispose()
    {
        File.Delete(_temporaryInputFile);
        File.Delete(_temporaryOutputFile);
        GC.SuppressFinalize(this);
    }
}