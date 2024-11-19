using System.Formats.Asn1;
using System.Runtime.InteropServices;

using McMaster.Extensions.CommandLineUtils;

using Lab4.Utils;
using Lab1 = LabLibrary.Lab1;
using Lab2 = LabLibrary.Lab2;
using Lab3 = LabLibrary.Lab3;

namespace Lab4.Commands;
[Command(Name = "run", Description = "Run lab tasks")]
public class RunCommand
{
    [Option(Description = "Specify input file", ShortName = "I", LongName = "input")]
    public required string InputFile { get; set; }

    [Option(Description = "Specify output file", ShortName = "o", LongName = "output")]
    public required string OutputFile { get; set; }

    [Argument(0, Description = "Lab name (lab1, lab2, lab3)")]
    public required string Lab { get; set; }

    private void OnExecute(CommandLineApplication app, IConsole console)
    {
        string? envLabPath = (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                                ? UnixEnvironmentUtil.GetEnvironmentVariableUnix("LAB_PATH")
                                : Environment.GetEnvironmentVariable("LAB_PATH", EnvironmentVariableTarget.Machine);

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

        var answer = string.Empty;

        switch (Lab.ToLower())
        {
            case "lab1":
                Lab1 lab1 = new();
                answer = lab1.Run(inputPath);
                break;
            case "lab2":
                Lab2 lab2 = new();
                answer = lab2.Run(inputPath);
                break;
            case "lab3":
                Lab3 lab3 = new();
                answer = lab3.Run(inputPath);
                break;
            default:
                console.WriteLine("Please specify a valid lab assignment.");
                break;
        }

        if (string.IsNullOrEmpty(answer))
        {
            console.WriteLine("No answer was returned from the lab assignment.");
            return;
        }

        File.WriteAllText(outputPath, answer);

        console.WriteLine("Done! Check the output file for the results.");
    }
}