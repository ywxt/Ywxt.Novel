using System;
using System.Collections.Generic;

namespace Ywxt.Novel.Models
{
    public class Book
    {
        public string Name { get; set; }

        public string Author { get; set; }
        
        public Uri Url { get; set; }

        public IEnumerable<ChapterInfo> Chapters { get; set; }
    }
}