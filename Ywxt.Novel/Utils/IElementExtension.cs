using System;
using System.Collections.Generic;
using System.Text;
using AngleSharp.Dom;

namespace Ywxt.Novel.Utils
{
    public static class ElementExtension
    {
        public static string Content(this INodeList elements)
        {
            var sb = new StringBuilder();
            foreach (var element in elements)
            {
                if (element.NodeName.Equals("#text", StringComparison.CurrentCultureIgnoreCase))
                {
                    sb.AppendLine(element.TextContent);
                }
            }
            return sb.ToString();
        }
    }
}