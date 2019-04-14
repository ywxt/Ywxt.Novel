using CommandLine;

namespace Ywxt.Novel.Command
{
    [Verb("search", HelpText = "搜索")]
    public class SearchCommand
    {
        [Option('s', "site", Required = true, HelpText = "网站")]
        public string WebSite { get; set; }

        public string Path { get; set; }

        [Value(0, HelpText = "关键词", MetaName = "key", Required = true)]
        public string Key { get; set; }
    }
}