namespace Lab3;

public class Labyrinth(int h, int m, int n, char[,,] grid)
{
    public int H { get; } = h;
    public int M { get; } = m;
    public int N { get; } = n;
    public char[,,] Grid { get; } = grid;
}