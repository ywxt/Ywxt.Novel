using System;
using System.Threading.Tasks;
using Ywxt.Novel.Configuration;
using Ywxt.Novel.Models;

namespace Ywxt.Novel.Command
{
    public class InstallParser
    {
        private InstallParser()
        {
        }

        private TemplateConfiguration _templateConfiguration =
            TemplateConfiguration.GetTemplateConfiguration();

        public InstallCommand Options { get; set; }

        public async Task InstallTemplete()
        {
            Template template;
            if (Options.IsLocal)
            {
                template = await _templateConfiguration.ParseLocalTemplate(Options.Path);
            }
            else
            {
                template = await _templateConfiguration.ParseRemoteTemplate(new Uri(Options.Path));
            }

            await _templateConfiguration.InstallTemplate(template, Options.Override);
        }

        private static InstallParser _parser;

        public static async Task<int> Parse(InstallCommand option)
        {
            if (_parser == null)
            {
                _parser = new InstallParser();
            }

            _parser.Options = option;
            try
            {
                await _parser.InstallTemplete();
                Console.WriteLine("安装完成");
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
        }
    }
}