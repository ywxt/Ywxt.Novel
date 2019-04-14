using CommandLine;
using Ywxt.Novel.Configuration;

namespace Ywxt.Novel.Command
{
    [Verb("download", HelpText = "下载")]
    public class DownloadCommand
    {
        [Option('t', "template", Required = true, HelpText = "模板")]
        public string Temeplate { get; set; }

        [Option('o', "output",Required = true, HelpText = "保存路径", Default = TemplateConfiguration.BookPath)]
        public string Path { get; set; }

        [Value(0, HelpText = "书", MetaName = "book", Required = true)]
        public string Book { get; set; }
    }
}