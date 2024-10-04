using Lab3.ConstantsSets;


namespace Lab3.Services;

class LabyrinthService
{
    public static int FindPrincess(Labyrinth labyrinth)
    {
        (int, int, int) start = FindStart(labyrinth);
        (int, int, int)[] directions = [(0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1), (1, 0, 0)];
        Queue<((int, int, int), int steps)> queue = new();
        HashSet<(int, int, int)> visited = [];

        queue.Enqueue((start, 0));
        visited.Add(start);

        while (queue.Count > 0)
        {
            ((int x, int y, int z), int steps) = queue.Dequeue();

            if (labyrinth.Grid[x, y, z] == '2')
                return steps * 5;

            foreach ((int dx, int dy, int dz) in directions)
            {
                int nx = x + dx;
                int ny = y + dy;
                int nz = z + dz;

                if (IsValidMove(labyrinth, nx, ny, nz) && !visited.Contains((nx, ny, nz)))
                {

                    if (dx == 1 && x == labyrinth.H - 1)
                    {
                        continue;
                    }

                    queue.Enqueue(((nx, ny, nz), steps + 1));
                    visited.Add((nx, ny, nz));
                }
            }
        }

        return -1;
    }

    static (int, int, int) FindStart(Labyrinth labyrinth)
    {
        for (int i = 0; i < labyrinth.H; i++)
        {
            for (int j = 0; j < labyrinth.M; j++)
            {
                for (int k = 0; k < labyrinth.N; k++)
                {
                    if (labyrinth.Grid[i, j, k] == '1')
                    {
                        return (i, j, k);
                    }
                }
            }
        }

        Console.WriteLine("Start not found");
        Environment.Exit(EnviromentCode.SuccessfulExit);
        return (-1, -1, -1);
    }

    static bool IsValidMove(Labyrinth labyrinth, int x, int y, int z)
    {
        return x >= 0 && x < labyrinth.H && y >= 0 && y < labyrinth.M && z >= 0 && z < labyrinth.N && labyrinth.Grid[x, y, z] != 'o';
    }
}