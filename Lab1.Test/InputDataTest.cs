
namespace Lab1.Test;

public class ProgramTest : IDisposable
{
    private readonly string _temporaryInputFile;
    private readonly string _temporaryOutputFile;

    public ProgramTest()
    {
        _temporaryInputFile = Path.GetTempFileName();
        _temporaryOutputFile = Path.GetTempFileName();
    }

    public static IEnumerable<object[]> ValidTestData()
    {
        yield return new object[] { new[] { "3 2", "3 2" }, 6 };
        yield return new object[] { new[] { "6 4", "1 3 2 5" }, 14 };
    }

    [Theory]
    [MemberData(nameof(ValidTestData))]
    public void ProcessData_ValidInput_ReturnsCorrectResult(string[] input, int expectedResult)
    {
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