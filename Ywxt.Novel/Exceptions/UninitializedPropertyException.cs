using System;

namespace Ywxt.Novel.Exceptions
{
    public class UninitializedPropertyException : Exception
    {
        public UninitializedPropertyException(string property) : base($"未初始化的属性：{property}")
        {
        }
    }
}