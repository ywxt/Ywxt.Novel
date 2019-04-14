using System;
using System.Linq;
using System.Threading.Tasks;
using Ywxt.Novel.Configuration;
using Ywxt.Novel.Downloaders;
using Ywxt.Novel.Exceptions;
using Ywxt.Novel.Models;
using Ywxt.Novel.Parsers;

namespace Ywxt.Novel.Command
{
    public class DownloadParser
    {
        private BookDownloader _downloader = new BookDownloader
        {
            Parser = new BookParser()
        };

        private TemplateConfiguration _templateConfiguration =
            TemplateConfiguration.GetTemplateConfiguration();

        private DownloadCommand _options;

        private DownloadParser()
        {
            _downloader.OnDownloadBookBegin += OnDownloadBookBegin;
            _downloader.OnDownloadBookFail += OnDownloadBookFail;
            _downloader.OnDownloadBookComplete += OnDownloadBookComplete;
            _downloader.OnDownloadChapterBegin += OnDownloadChapterBegin;
            _downloader.OnDownloadChapterComplete += OnDownloadChapterComplete;
        }

        private static DownloadParser _parser;

        public static async Task<int> Parse(DownloadCommand command)
        {
            if (_parser == null)
            {
                _parser = new DownloadParser();
            }

            try
            {
                _parser.Options = command;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("模板参数有误，请检查后重试。错误信息：{0}", e.Message);
            }
            catch (ParseException e)
            {
                Console.WriteLine("模板参数有误，请检查后重试。错误信息：{0}", e.Message);
            }

            return await _parser.Parse();
        }

        public DownloadCommand Options
        {
            get => _options;
            set
            {
                _options = value;
                _downloader.Parser.Options = _options;
                _downloader.Parser.Template = _templateConfiguration.Templates
                    .Single(t => t.Id == _options.Temeplate);
            }
        }

        public async Task<int> Parse()
        {
            try
            {
                await _downloader.DownloadChapters();
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
        }

        private void OnDownloadBookBegin(Book book)
        {
            Console.WriteLine("开始下载:");
            Console.WriteLine("书名:{0}", book.Name);
            Console.WriteLine("作者:{0}", book.Author);
            Console.WriteLine("章节总数:{0}", book.Chapters.Count());
        }

        private void OnDownloadBookComplete(Book book)
        {
            Console.WriteLine("下载完成:{0}", book.Name);
        }

        private void OnDownloadBookFail(Book book, Exception e)
        {
            Console.WriteLine("下载失败:{0}", book.Name);
            Console.WriteLine(e.Message);
        }

        private void OnDownloadChapterBegin(int count, ChapterInfo info)
        {
            Console.WriteLine("{0}. 开始下载：{1}", count, info.Title);
        }

        private void OnDownloadChapterComplete(int count, Chapter chapter)
        {
            Console.WriteLine("{0}. 下载完成：{1}", count, chapter.Title);
        }
    }
}