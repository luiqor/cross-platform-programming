using McMaster.Extensions.CommandLineUtils;
namespace Lab4;

[Command(Name = "labtool", Description = "Tool for running lab assignments")]
class Program
{
    public static int Main(string[] args)
    {
        return CommandLineApplication.Execute<Program>(args);
    }

    private void OnExecute(CommandLineApplication app)
    {
        app.ShowHelp();
    }
}