using Lab3.Services;

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

    public static int ProcessData(string filename)
    {

        string[] lines = File.ReadAllLines(filename);

        var dimensions = lines[0].Split().Select(int.Parse).ToArray();

        int h = dimensions[0], m = dimensions[1], n = dimensions[2];

        char[,,] labyrinth = new char[h, m, n];

        return LabyrinthService.FindPrincess(h, m, n, labyrinth);
    }


}