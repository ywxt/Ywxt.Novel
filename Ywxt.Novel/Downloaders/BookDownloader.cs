using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Ywxt.Novel.Models;
using Ywxt.Novel.Parsers;
using Ywxt.Novel.Utils;

namespace Ywxt.Novel.Downloaders
{
    public class BookDownloader
    {
        public event Action<int, Chapter> OnDownloadChapterComplete;
        public event Action<int, ChapterInfo> OnDownloadChapterBegin;
        public event Action<Book> OnDownloadBookBegin;
        public event Action<Book> OnDownloadBookComplete;
        public event Action<Book, Exception> OnDownloadBookFail;
        private readonly ConcurrentBag<(int index, Chapter chapter)> _chapters = new ConcurrentBag<(int, Chapter)>();
        private volatile int _count = 0;
        public BookParser Parser { get; set; }

        public async Task DownloadChapters()
        {
            try
            {

                if (Parser.Book.Chapters == null)
                {
                    await Parser.Parse();
                }

                OnDownloadBookBegin?.Invoke(Parser.Book);

                var chapterParts = Parser.Book.Chapters.Split(50);
                var downloadTasks = chapterParts.Select(part => DownloadChapter(part));
                await Task.WhenAll(downloadTasks);
                await WriteChapters();

            }
            catch (Exception e)
            {
                OnDownloadBookFail?.Invoke(Parser.Book, e);
            }
        }

        private async Task WriteChapters()
        {
            var path = Directory.Exists(Parser.Options.Path) ? Path.Combine(Parser.Options.Path, $"{Parser.Book.Name}.txt") : Parser.Options.Path;

            var file = File.Open(path, FileMode.Create);
            var writer = new StreamWriter(file);
            await writer.WriteLineAsync($"书名：{Parser.Book.Name}");
            await writer.WriteLineAsync($"作者：{Parser.Book.Author}");

            var chapters = _chapters.OrderBy(e => e.index).Select(e => e.chapter);
            foreach (var chapter in chapters)
            {
                await writer.WriteLineAsync(chapter.Title);
                await writer.WriteLineAsync(chapter.Content);
            }
            await writer.FlushAsync();
            writer.Close();
            OnDownloadBookComplete?.Invoke(Parser.Book);
        }

        private async Task DownloadChapter(IEnumerable<(int index, ChapterInfo element)> part)
        {

            foreach (var info in part)
            {
                Interlocked.Increment(ref _count);
                OnDownloadChapterBegin?.Invoke(_count, info.element);
                var chapter = await Parser.ParseChapter(info.element);
                _chapters.Add((info.index, chapter));
                OnDownloadChapterComplete?.Invoke(_count, chapter);

            }

        }
    }
}