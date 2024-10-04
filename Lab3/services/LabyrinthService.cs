using Lab3.ConstantsSets;

using Lab3.Services.ConstantsSets;

namespace Lab3.Services;

class LabyrinthService
{
    private const int LastIndexOffset = 1;
    private const int NotFound = -1;
    public static int FindPrincess(Labyrinth labyrinth)
    {
        (int, int, int) start = FindStart(labyrinth);
        (int, int, int)[] directions = [(0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1), (1, 0, 0)];
        Queue<((int, int, int), int steps)> queue = new();
        HashSet<(int, int, int)> visited = [];

        queue.Enqueue((start, LabyrinthStep.InitialCount));
        visited.Add(start);

        while (queue.Count > 0)
        {
            ((int x, int y, int z), int steps) = queue.Dequeue();

            if (labyrinth.Grid[x, y, z] == LabyrinthCode.Princess)
            {
                return steps * LabyrinthStep.SecondsFor;
            }

            foreach ((int dx, int dy, int dz) in directions)
            {
                int nx = x + dx;
                int ny = y + dy;
                int nz = z + dz;

                if (IsValidMove(labyrinth, nx, ny, nz) && !visited.Contains((nx, ny, nz)))
                {

                    if (dx == LabyrinthStep.Single && x == labyrinth.H - LastIndexOffset)
                    {
                        continue;
                    }

                    queue.Enqueue(((nx, ny, nz), steps + LabyrinthStep.Single));
                    visited.Add((nx, ny, nz));
                }
            }
        }

        return NotFound;
    }

    static (int, int, int) FindStart(Labyrinth labyrinth)
    {
        for (int i = 0; i < labyrinth.H; i++)
        {
            for (int j = 0; j < labyrinth.M; j++)
            {
                for (int k = 0; k < labyrinth.N; k++)
                {
                    if (labyrinth.Grid[i, j, k] == LabyrinthCode.Start)
                    {
                        return (i, j, k);
                    }
                }
            }
        }

        Console.WriteLine("Start not found");
        Environment.Exit(EnviromentCode.SuccessfulExit);
        return (NotFound, NotFound, NotFound);
    }

    static bool IsValidMove(Labyrinth labyrinth, int x, int y, int z)
    {
        return x >= 0 && x < labyrinth.H && y >= 0 && y < labyrinth.M && z >= 0 && z < labyrinth.N && labyrinth.Grid[x, y, z] != LabyrinthCode.Wall;
    }
}