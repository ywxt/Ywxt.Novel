using CommandLine;
using Ywxt.Novel.Models;

namespace Ywxt.Novel.Command
{
    [Verb("install", HelpText = "安装模板")]
    public class InstallCommand
    {
        [Value(0, HelpText = "模板路径", MetaName = "path")]
        public string Path { get; set; }

        [Option('l', "local", HelpText = "是否本地路径", Default = false)]
        public bool IsLocal { get; set; }

        [Option('o', "override", Default = true, HelpText = "是否覆盖存在文件")]
        public bool Override { get; set; }
    }
}