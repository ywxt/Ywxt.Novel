using System.Threading.Tasks;
using CommandLine;

namespace Ywxt.Novel.Command
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var result = Parser.Default
                .ParseArguments<DownloadCommand, InstallCommand, ListCommand, NewCommand>(args);
            await result.MapResult
            (
                async (DownloadCommand downloadCommand) =>
                    await DownloadParser.Parse(downloadCommand),
                async (InstallCommand installCommand) =>
                    await InstallParser.Parse(installCommand),
                async (ListCommand listCommand) => await Task.Run(() => ListParser.Parse()),
                async (NewCommand newCommand) => await NewParser.Parser(),
                async error => await Task.FromResult(1)
            );
        }
    }
}