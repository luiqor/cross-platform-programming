
namespace Lab3.Test;

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
        "3 3 3",
        "1..",
        "oo.",
        "...",
        "",
        "ooo",
        "..o",
        ".oo",
        "",
        "ooo",
        "o..",
        "o.2"
        ];
        int expectedResult = 60;

        File.WriteAllLines(_temporaryInputFile, input);
        int actualResult = Program.ProcessData(_temporaryInputFile);

        Assert.Equal(expectedResult, actualResult);
    }

    public void Dispose()
    {
        File.Delete(_temporaryInputFile);
        File.Delete(_temporaryOutputFile);
        GC.SuppressFinalize(this);
    }
}