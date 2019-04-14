using System;
using System.Text;
using Ywxt.Novel.Configuration;

namespace Ywxt.Novel.Command
{
    public class ListParser
    {
        private ListParser(){}

        private TemplateConfiguration _templateConfiguration =
            TemplateConfiguration.GetTemplateConfiguration();

        public string List()
        {
            var sb=new StringBuilder();
            sb.AppendLine("id\t\t\t\t网站");
            foreach (var template in _templateConfiguration.Templates)
            {
                sb.AppendLine(template.ToString());
            }

            return sb.ToString();
        }

        private static ListParser _parser;
        public static int Parse()
        {
            if (_parser==null)
            {
                _parser = new ListParser();
            }

            try
            {
                Console.WriteLine(_parser.List());
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("解析错误，错误信息:{0}",e.Message);
                return 1;
            }
        }
    }
}