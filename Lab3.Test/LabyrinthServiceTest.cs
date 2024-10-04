using Lab3.Services;

namespace Lab3.Test;
public class LabyrinthServiceTest : IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void IsValidMove_ValidMove_ReturnsTrue()
    {
        Labyrinth labyrinth = new(
        3,
        3,
        3,
        new char[3, 3, 3]
        {
        {
            { '.', '.', '.' },
            { '.', 'o', '.' },
            { '.', '.', '.' }
        },
        {
            { '.', '.', '.' },
            { '.', '.', '.' },
            { '.', '.', '.' }
        },
        {
            { '.', '.', '.' },
            { '.', '.', '.' },
            { '.', '.', '.' }
        }
        }
        );

        Assert.True(LabyrinthService.IsValidMove(labyrinth, 0, 0, 0));
    }

    [Fact]
    public void IsValidMove_InvalidMove_ReturnsFalse()
    {
        Labyrinth labyrinth = new(
        3,
        3,
        3,
        new char[3, 3, 3]
        {
        {
            { '.', '.', '.' },
            { '.','o', '.' },
            { '.', '.', '.' }
        },
        {
            { '.', '.', '.' },
            { '.', '.', '.' },
            { '.', '.', '.' }
        },
        {
            { '.', '.', '.' },
            { '.', '.', '.' },
            { '.', '.', '.' }
        }
        }
    );

        Assert.False(LabyrinthService.IsValidMove(labyrinth, 0, 1, 1));
    }


    public static IEnumerable<object[]> LabyrinthData()
    {
        yield return new object[]
        {
            new Labyrinth(
                3,
                3,
                3,
                new char[3, 3, 3]
                {
                    {
                        { '.', '.', '.' },
                        { '.', '1', '.' },
                        { '.', '.', '.' }
                    },
                    {
                        { '.', '.', '.' },
                        { '.', '.', '.' },
                        { '.', '.', '.' }
                    },
                    {
                        { '.', '.', '.' },
                        { '.', '.', '.' },
                        { '.', '.', '.' }
                    }
                }
            ),
            (0, 1, 1)
        };

        yield return new object[]
        {
            new Labyrinth(
                3,
                3,
                3,
                new char[3, 3, 3]
                {
                    {
                        { '.', '.', '.' },
                        { '.', '.', '.' },
                        { '.', '.', '.' }
                    },
                    {
                        { '.', '.', '.' },
                        { '.', '1', '.' },
                        { '.', '.', '.' }
                    },
                    {
                        { '.', '.', '.' },
                        { '.', '.', '.' },
                        { '.', '.', '.' }
                    }
                }
            ),
            (1, 1, 1)
        };

        yield return new object[]
        {
            new Labyrinth(
                3,
                3,
                3,
                new char[3, 3, 3]
                {
                    {
                        { '.', '.', '.' },
                        { '.', '.', '.' },
                        { '.', '.', '.' }
                    },
                    {
                        { '.', '.', '.' },
                        { '.', '.', '.' },
                        { '.', '.', '.' }
                    },
                    {
                        { '.', '.', '.' },
                        { '.', '1', '.' },
                        { '.', '.', '.' }
                    }
                }
            ),
            (2, 1, 1)
        };
    }

    [Theory]
    [MemberData(nameof(LabyrinthData))]
    public void FindStart_ValidData_ReturnsExpected(Labyrinth labyrinth, (int, int, int) expected)
    {
        var result = LabyrinthService.FindStart(labyrinth);
        Assert.Equal(expected, result);
    }
}
