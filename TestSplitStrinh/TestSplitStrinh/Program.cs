using System;
using System.Text;
using System.Text.RegularExpressions;

namespace TestSplitStrinh
{
    class Program
    {
        //定义字符串分割的长度
        const int Length = 20;
        static void Main(string[] args)
        {
            Console.WriteLine("请输入你要输入的字符串！");
            string str = Console.ReadLine();
            Console.WriteLine(GetByteForGB(str));

            Console.Read();
        }


        static string GetByteForGB(string str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int count = 0;
            int stringCount =0;
            while(true)
            {
                string s =  str.Substring(stringCount, 1);
               
                if (Regex.IsMatch(s, "[\u0000-\u007f]"))
                {
                    stringBuilder.Append(s);
                    count++;
                }
                else
                {
                    stringBuilder.Append(s);
                    count += 2;
                }
                if (count >= Length)
                {
                    stringBuilder.Append("\n");
                    count = 0;
                }
                stringCount++;
                if (str.Length == stringCount)
                {
                    break;
                }
      
            }

            return stringBuilder.ToString();
        }
    }
}
