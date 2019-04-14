using System;

namespace Ywxt.Novel.Utils
{
    public static class Prompt
    {
        public static string Answer(string queston, bool nullable = false, bool newLine = true)
        {
            if (newLine)
            {
                Console.WriteLine(queston);
            }
            else
            {
                Console.Write(queston);
            }

            string answer;
            do
            {
                answer = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(answer) && !nullable);

            return answer;
        }
    }
}