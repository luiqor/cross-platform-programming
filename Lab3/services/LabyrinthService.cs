
namespace Lab3.Services;

class LabyrinthService
{
    public static int FindPrincess(int h, int m, int n, char[,,] labyrinth)
    {
        (int, int, int) start = FindStart(h, m, n, labyrinth);
        (int, int, int)[] directions = [(0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1), (1, 0, 0)];
        Queue<((int, int, int) pos, int steps)> queue = new();
        HashSet<(int, int, int)> visited = [];

        queue.Enqueue((start, 0));
        visited.Add(start);

        while (queue.Count > 0)
        {
            ((int x, int y, int z), int steps) = queue.Dequeue();

            if (labyrinth[x, y, z] == '2')
                return steps * 5;

            foreach ((int dx, int dy, int dz) in directions)
            {
                int nx = x + dx, ny = y + dy, nz = z + dz;
                if (IsValidMove(h, m, n, labyrinth, nx, ny, nz) && !visited.Contains((nx, ny, nz)))
                {
                    if (dx == 1 && x == h - 1)
                        continue;
                    queue.Enqueue(((nx, ny, nz), steps + 1));
                    visited.Add((nx, ny, nz));
                }
            }
        }

        return -1;
    }

    static (int, int, int) FindStart(int h, int m, int n, char[,,] labyrinth)
    {
        for (int i = 0; i < h; i++)
            for (int j = 0; j < m; j++)
                for (int k = 0; k < n; k++)
                    if (labyrinth[i, j, k] == '1')
                        return (i, j, k);
        Console.WriteLine("Start not found");
        Environment.Exit(0);
        return (-1, -1, -1);
    }

    static bool IsValidMove(int h, int m, int n, char[,,] labyrinth, int x, int y, int z)
    {
        return x >= 0 && x < h && y >= 0 && y < m && z >= 0 && z < n && labyrinth[x, y, z] != 'o';
    }
}