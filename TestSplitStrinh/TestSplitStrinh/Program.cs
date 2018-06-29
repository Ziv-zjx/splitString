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
            string str = @"SZ_CreditCardRecords  记录里有重复的刷卡记录，记录的是每一刷卡的记录；
SZ_AttendanceRecords  记录的是符合考勤模板（进出校园）的一条记录（一个考勤时段只记录一次正常刷卡）
SZ_CreditCardRecordsSS 记录宿舍进出表的刷卡记录 （一个考勤时段只记录一次正常刷卡）

SZ_CreditCardRecords有个usertype字段，这个字段是人员类型，0是老师，2是学生，3是家长

SZ_CreditCardRecords有个usertype字段，这个字段是人员类型，0是老师，2是学生，3是家长，我们现在只统计学生就行了

慧校园的修改下
查总量
select name,card_num,mobile,school_id,intime,systime,attendance_id ,
	 DATEDIFF( Minute, CONVERT(varchar(16),intime,120), CONVERT(varchar(16),systime,120)) AS CC
 from SZ_CreditCardRecords where convert(varchar(10),intime,120)='2018-06-27' 
 and usertype=2 

查延时量
select name,card_num,mobile,school_id,intime,systime,attendance_id ,
	 DATEDIFF( Minute, CONVERT(varchar(16),intime,120), CONVERT(varchar(16),systime,120)) AS CC
 from SZ_CreditCardRecords where convert(varchar(10),intime,120)='2018-06-27' 
 and usertype=2 
and convert(varchar(10),intime,120)=convert(varchar(10),systime,120)
 and DATEDIFF( Minute, CONVERT(varchar(16),intime,120), CONVERT(varchar(16),systime,120)) between 6 and 999999999999999  
   ORDER BY CC DESC
";// Console.ReadLine();
            Console.WriteLine(GetByteForGB(str));

            Console.Read();
        }


        static string GetByteForGB(string str)
        {
            Regex regex = new Regex("[\r|\n|\t\f]");//\r是回车符
            str = regex.Replace(str, "");
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
