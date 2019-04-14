using System;
using System.Threading.Tasks;
using Ywxt.Novel.Configuration;
using Ywxt.Novel.Models;
using Ywxt.Novel.Utils;

namespace Ywxt.Novel.Command
{
    public class NewParser
    {
        private TemplateConfiguration _templateConfiguration =
            TemplateConfiguration.GetTemplateConfiguration();

        private NewParser()
        {
        }

        public Template CreateTemplate()
        {
            var id = Prompt.Answer("模板ID(不能重复):");
            var website = Prompt.Answer("网站(e.g. biquge.com):");
            var bookUrlMatcher = Prompt.Answer("书籍地址正则表达式");
            var ua = Prompt.Answer("浏览器标识符(UA，可空):", true);
            var nameMatcher = Prompt.Answer("书籍名称Css选择器");
            var nameAttribute =
                Prompt.Answer("书籍名称Html属性(如果书籍名称包含在所需的Html标签的属性中，请填写属性名称，否则请留空)", true);
            var authorMatcher = Prompt.Answer("作者名称Css选择器");
            var authorAttribute =
                Prompt.Answer("作者名称Html属性(如果作者名称包含在所需的Html标签的属性中，请填写属性名称，否则请留空)", true);
            var chapterNameMatcher = Prompt.Answer("章节标题Css选择器(章节列表所在页的章节标题，非章节内容所在页)");
            var chapterNameAttribute =
                Prompt.Answer("章节名称Html属性(如果章节名称包含在所需的Html标签的属性中，请填写属性名称，否则请留空)", true);
            var chapterUrlMatcher = Prompt.Answer("章节地址Css选择器");
            var chapterUrlAttribute =
                Prompt.Answer("章节地址Html属性(如果章节地址包含在所需的Html标签的属性中，请填写属性名称，否则请留空)", true);
            var chapterContentMatcher = Prompt.Answer("章节内容Css选择器");
            var chapterContentAttribute =
                Prompt.Answer("章节内容Html属性(如果章节内容包含在所需的Html标签的属性中，请填写属性名称，否则请留空)", true);
            return new Template
            {
                Id = id,
                Website = website,
                BookUrlMatcher = bookUrlMatcher,
                UserAgent = ua,
                NameMatcher = nameMatcher,
                NameHtmlAttribute = nameAttribute,
                AuthorMatcher = authorMatcher,
                AuthorAttribute = authorAttribute,
                ChapterNamelMatcher = chapterNameMatcher,
                ChapterNameAttribute = chapterNameAttribute,
                ChapterUrlMatcher = chapterUrlMatcher,
                ChapterUrlAttribute = chapterUrlAttribute,
                ChapterContentMatcher = chapterContentMatcher,
                ChapterContentAttribute = chapterContentAttribute
            };
        }

        public async Task New()
        {
            var template = CreateTemplate();
            await _templateConfiguration.InstallTemplate(template, true);
        }

        private static NewParser _parser;

        public static async Task<int> Parser()
        {
            if (_parser == null)
            {
                _parser = new NewParser();
            }

            try
            {
                await _parser.New();
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("创建失败，错误信息:{0}",e.Message);
                return 1;
            }
        }
    }
}