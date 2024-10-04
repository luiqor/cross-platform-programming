using Lab3.Services;
using Lab3.Validation;

class Program
{
    static void Main()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string inputFilePath = Path.Combine(baseDirectory, "assets", "lab3", "input.txt");
        string outputFilePath = Path.Combine(baseDirectory, "assets", "lab3", "output.txt");

        int bestTimeToPrincess = ProcessData(inputFilePath);
        File.WriteAllText(outputFilePath, bestTimeToPrincess.ToString());
    }

    public static int ProcessData(string inputFilePath)
    {
        DataValidator.Validate(() => DataValidator.ValidateInputFile(inputFilePath));

        string[] lines = File.ReadAllLines(inputFilePath);
        DataValidator.Validate(() => DataValidator.ValidateNumberOfLines(lines));

        int[] dimensions = lines[0].Split().Select(int.Parse).ToArray();
        DataValidator.Validate(() => DataValidator.ValidateLabyrinthDimensions(dimensions));

        int h = dimensions[0], m = dimensions[1], n = dimensions[2];
        char[,,] labyrinthGrid = new char[h, m, n];

        int lineIndex = 1;

        for (int i = 0; i < h; i++)
        {
            int nextLineIndex = lineIndex + m;
            DataValidator.Validate(() => DataValidator.ValidateLevelRowsCount(nextLineIndex, lines.Length, i));


            for (int j = 0; j < m; j++)
            {
                string line = lines[lineIndex++];
                DataValidator.Validate(() => DataValidator.ValidateLevelRowColumsCount(line.Length, n, i, j));

                for (int k = 0; k < n; k++)
                {
                    labyrinthGrid[i, j, k] = line[k];
                }
            }

            DataValidator.Validate(() => DataValidator.ValidateEmptyLineAfterLevel(i, h, lineIndex, lines));
            lineIndex++;
        }

        return LabyrinthService.FindPrincess(new Labyrinth(h, m, n, labyrinthGrid));
    }


}