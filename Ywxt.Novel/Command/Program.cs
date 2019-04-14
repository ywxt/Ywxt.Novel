using CommandLine;

namespace Ywxt.Novel.Command
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Parser.Default.ParseArguments<DownloadCommand, SearchCommand>(args)
                .MapResult
                (
                    (DownloadCommand downloadCommand) => 0,
                    (SearchCommand searchCommand) => 0,
                    erro => 1
                );
        }

      
    }
}