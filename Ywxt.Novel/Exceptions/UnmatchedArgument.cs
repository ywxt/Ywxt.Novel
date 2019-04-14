using System;

namespace Ywxt.Novel.Exceptions
{
    public class UnmatchedArgumentException:Exception
    {
        public UnmatchedArgumentException(string argument):base($"参数不匹配：{argument}"){}
    }
}