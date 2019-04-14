using System;
using System.Net.Http.Headers;
using CommandLine;
using Newtonsoft.Json;

namespace Ywxt.Novel.Models
{
    [JsonObject(MemberSerialization.OptOut,ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Template
    {
        
        /// <summary>
        /// ID，保证不重复
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 网站
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// Url匹配表达式
        /// </summary>
        public string BookUrlMatcher { get; set; }
        /// <summary>
        /// 名称选择器
        /// </summary>
        public string NameMatcher { get; set; }
        /// <summary>
        /// 名称选择器属性
        /// </summary>
        public string NameHtmlAttribute { get; set; }
        /// <summary>
        /// 章节名称
        /// </summary>
        public string ChapterNamelMatcher { get; set; }
        /// <summary>
        /// 章节名属性
        /// </summary>
        public string ChapterNameAttribute { get; set; }
        /// <summary>
        /// 章节Url
        /// </summary>
        public string ChapterUrlMatcher { get; set; }
        /// <summary>
        /// 章节Url属性
        /// </summary>
        public string ChapterUrlAttribute { get; set; }
        /// <summary>
        /// 章节内容
        /// </summary>
        public string ChapterContentMatcher { get; set; }
        /// <summary>
        /// 章节属性
        /// </summary>
        public string ChapterContentAttribute { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string AuthorMatcher { get; set; }
        /// <summary>
        /// 作者Html属性
        /// </summary>
        public string AuthorAttribute { get; set; }
        
        public string UserAgent { get; set; }

        public override string ToString()
        {
            return $"{Id}\t\t\t{Website}";
        }
    }
}