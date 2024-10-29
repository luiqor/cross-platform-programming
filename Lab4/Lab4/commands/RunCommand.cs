using McMaster.Extensions.CommandLineUtils;
using DotNetEnv;

namespace Lab4.Commands;
using Lab1 = LabLibrary.Lab1;
using Lab2 = LabLibrary.Lab2;
using Lab3 = LabLibrary.Lab3;

[Command(Name = "run", Description = "Run lab tasks")]
class RunCommand
{
    [Option(Description = "Specify input file", ShortName = "I", LongName = "input")]
    public required string InputFile { get; set; }

    [Option(Description = "Specify output file", ShortName = "o", LongName = "output")]
    public required string OutputFile { get; set; }

    [Argument(0, Description = "Lab name (lab1, lab2, lab3)")]
    public required string Lab { get; set; }

    private void OnExecute(CommandLineApplication app, IConsole console)
    {
        Env.Load(".env");

        string? envLabPath = Environment.GetEnvironmentVariable("LAB_PATH");
        Console.WriteLine($"Running {Lab} with input: {InputFile}, output: {OutputFile}, LAB_PATH: {envLabPath}");

        if (string.IsNullOrEmpty(Lab))
        {
            console.WriteLine("Please specify a lab assignment.");
            app.ShowHelp();
            return;
        }

        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        string inputPath = InputFile ?? Path.Combine(envLabPath ?? homeDirectory, "input.txt");
        string outputPath = OutputFile ?? Path.Combine(envLabPath ?? homeDirectory, "output.txt");


        Console.WriteLine($"Input path: {inputPath}");
        Console.WriteLine($"Output path: {outputPath}");

        if (!File.Exists(inputPath))
        {
            console.WriteLine($"Error: Input file not found at path '{inputPath}'");
            return;
        }

        switch (Lab.ToLower())
        {
            case "lab1":
                Lab1 lab1 = new();
                lab1.Run(inputPath, outputPath);
                break;
            case "lab2":
                Lab2 lab2 = new();
                lab2.Run(inputPath, outputPath);
                break;
            case "lab3":
                Lab3 lab3 = new();
                lab3.Run(inputPath, outputPath);
                break;
            default:
                console.WriteLine("Please specify a valid lab assignment.");
                break;
        }
    }
}