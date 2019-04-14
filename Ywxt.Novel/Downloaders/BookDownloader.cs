using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Ywxt.Novel.Models;
using Ywxt.Novel.Parsers;

namespace Ywxt.Novel.Downloaders
{
    public class BookDownloader
    {
        public event Action<int, Chapter> OnDownloadChapterComplete;
        public event Action<int, ChapterInfo> OnDownloadChapterBegin;
        public event Action<Book> OnDownloadBookBegin;
        public event Action<Book> OnDownloadBookComplete;
        public event Action<Book, Exception> OnDownloadBookFail;

        public BookParser Parser { get; set; }

        public async Task DownloadChapters()
        {
            try
            {
                var count = 0;
                if (Parser.Book.Chapters == null)
                {
                    await Parser.Parse();
                }

                OnDownloadBookBegin?.Invoke(Parser.Book);
                var file = File.Open(Path.Combine(Parser.Options.Path, $"{Parser.Book.Name}.txt"),
                    FileMode.Create);
                var writer = new StreamWriter(file);
                await writer.WriteLineAsync($"书名：{Parser.Book.Name}");
                await writer.WriteLineAsync($"作者：{Parser.Book.Author}");
                foreach (var info in Parser.Book.Chapters)
                {
                    OnDownloadChapterBegin?.Invoke(count,info);
                    var chapter = await Parser.ParseChapter(info);
                    await writer.WriteLineAsync(chapter.Title);
                    await writer.WriteLineAsync(chapter.Content);
                    OnDownloadChapterComplete?.Invoke(count,chapter);
                    count++;
                }

                await writer.FlushAsync();
                writer.Close();
                OnDownloadBookComplete?.Invoke(Parser.Book);
            }
            catch (Exception e)
            {
                OnDownloadBookFail?.Invoke(Parser.Book, e);
            }
        }
    }
}