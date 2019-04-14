using System;

namespace Ywxt.Novel.Exceptions
{
    public class ParseException:Exception
    {
        public ParseException(string message):base(message){}
    }
}