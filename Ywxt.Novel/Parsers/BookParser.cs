using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Ywxt.Novel.Command;
using Ywxt.Novel.Exceptions;
using Ywxt.Novel.Models;

namespace Ywxt.Novel.Parsers
{
    public class BookParser
    {
        private HttpClient _httpClient = new HttpClient();

        public Template Template { get; set; }

        public DownloadCommand Options { get; set; }

        public Book Book { get; } = new Book();


        public async Task<Book> Parse()
        {
            if (Template == null)
            {
                throw new UninitializedPropertyException(nameof(Template));
            }

            if (Options == null)
            {
                throw new UninitializedPropertyException(nameof(Options));
            }

            if (!Regex.IsMatch(Options.Book, Template.BookUrlMatcher))
            {
                throw new UnmatchedArgumentException(nameof(Options.Book));
            }

            _httpClient.DefaultRequestHeaders.UserAgent.Clear();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(Template.UserAgent);
            var html = await _httpClient.GetAsync(Options.Book);
            var document =
                await new HtmlParser().ParseDocumentAsync(await html.Content.ReadAsStreamAsync());
            return HtmlParse(document);
        }

        private Book HtmlParse(IHtmlDocument document)
        {
            Book.Url = document.BaseUrl;


            if (string.IsNullOrWhiteSpace(Template.NameMatcher))
            {
                Book.Name = string.Empty;
            }
            else
            {
                var name = document.QuerySelector(Template.NameMatcher);
                Book.Name = string.IsNullOrWhiteSpace(Template.NameHtmlAttribute)
                    ? name.TextContent
                    : name.GetAttribute(Template.NameHtmlAttribute);
            }

            if (string.IsNullOrWhiteSpace(Template.AuthorMatcher))
            {
                Book.Author = string.Empty;
            }
            else
            {
                var author = document.QuerySelector(Template.AuthorMatcher);
                Book.Author = string.IsNullOrWhiteSpace(Template.AuthorAttribute)
                    ? author.TextContent
                    : author.GetAttribute(Template.AuthorAttribute);
            }

            Book.Chapters = ParseChaptersInfo(document);


            return Book;
        }

        private IEnumerable<ChapterInfo> ParseChaptersInfo(IHtmlDocument document)
        {
            if (string.IsNullOrWhiteSpace(Template.ChapterUrlMatcher))
            {
                throw new ParseException("模板错误，未设置章节地址");
            }

            var chapterUrl = document.QuerySelectorAll(Template.ChapterUrlMatcher);
            var chapterTitle = document.QuerySelectorAll(Template.ChapterNamelMatcher);
            return chapterUrl.Zip(chapterTitle, (url, title) =>
            {
                var u = string.IsNullOrWhiteSpace(Template.ChapterUrlAttribute)
                    ? url.TextContent
                    : url.GetAttribute(Template.ChapterUrlAttribute);
                var t = string.IsNullOrWhiteSpace(Template.ChapterNameAttribute)
                    ? title.TextContent
                    : title.GetAttribute(Template.ChapterNameAttribute);
                return new ChapterInfo {Title = t, Url = new Uri(Book.Url, u)};
            });
        }

        public async Task<Chapter> ParseChapter(ChapterInfo info)
        {
            if (Template == null)
            {
                throw new UninitializedPropertyException(nameof(Template));
            }

            if (Options == null)
            {
                throw new UninitializedPropertyException(nameof(Options));
            }

            if (!Regex.IsMatch(Options.Book, Template.BookUrlMatcher))
            {
                throw new UnmatchedArgumentException(nameof(Options.Book));
            }

            _httpClient.DefaultRequestHeaders.UserAgent.Clear();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(Template.UserAgent);
            var chapter = await _httpClient.GetAsync(info.Url);
            var html =
                await new HtmlParser().ParseDocumentAsync(
                    await chapter.Content.ReadAsStreamAsync());
            var ch = new Chapter();

            if (string.IsNullOrWhiteSpace(Template.ChapterContentMatcher))
            {
                ch.Content = string.Empty;
            }
            else
            {
                var content = html.QuerySelector(Template.ChapterContentMatcher);
                ch.Content = string.IsNullOrWhiteSpace(Template.ChapterContentAttribute)
                    ? content.TextContent
                    : content.GetAttribute(Template.ChapterContentAttribute);
            }

            ch.Title = info.Title;
            return ch;
        }

       

        ~BookParser()
        {
            _httpClient.Dispose();
        }
    }
}