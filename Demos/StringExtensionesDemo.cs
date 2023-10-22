using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Gitusme.Net.Extensiones.Core;
using Com.Gitusme.Net.Extensiones.Demos.Model;

namespace Com.Gitusme.Net.Extensiones.Demos
{
    internal class StringExtensionesDemo : IExtensionesDemo
    {
        public override void Execute()
        {
            base.Execute();

            // Example 1: 判断string是否为null
            string str = null;
            if (str.IsNullOrEmpty())
            {
                System.Console.WriteLine("Example 1 输出结果:" + "null");
            }

            // Example 2: 判断string是否匹配正则
            string hello = "Hello, gitusme";
            var isMatch = hello.IsMatch(@"Hello, \w+");
            System.Console.WriteLine("Example 2 输出结果:" + isMatch);

            // Example 3: 将string转换为Json实体对象
            string json = "{\"Id\":0,\"Name\":\"Json Object\"}";
            var jsonObj = json.ToJsonObject<MyJsonObject>();
            System.Console.WriteLine("Example 3 输出结果:" + jsonObj.Name);

            // Example 4: 将string转换为Xml实体对象
            string xml = "﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<root xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" id=\"0\">" +
                "<name>Xml Object</name>" +
                "</root>";
            var xmlObj = xml.ToXmlObject<MyXmlObject>();
            System.Console.WriteLine("Example 4 输出结果:" + xmlObj.Name);

            // Example 5: 将string转换为DateTime
            string date = "2023/10/22 21:32:00";
            var dateTime = date.ToDateTime();
            System.Console.WriteLine("Example 5 输出结果:" + dateTime.ToString());

            // Example 6: 将string转换为字符数组
            string gitusme = "gitusme";
            var array = gitusme.ToCharArray();
            System.Console.WriteLine("Example 6 输出结果:" + array);

            // Example 7: 将string转换为int32
            string intStr = "100";
            var intValue = intStr.ToInt32() + 10;
            System.Console.WriteLine("Example 7 输出结果:" + intValue);

            // Example 8: 将string转换为decimal
            string decimalStr = "3.141592653589793238462643383279502884197";
            var decimalValue = decimalStr.ToDecimal();
            System.Console.WriteLine("Example 8 输出结果:" + decimalValue);

            // Example 9: 将string转换为uri
            string uriStr = "https://github.com/gitusme/Com.Gitusme.Net.Extensiones";
            var uriValue = uriStr.ToUri();
            System.Console.WriteLine("Example 9 输出结果:" + uriValue.Host);

            // Example 10: 将string转换为color
            string colorStr = "#aabbcc";
            var colorValue = colorStr.ToColor();
            System.Console.WriteLine("Example 10 输出结果:" + colorValue);
        }
    }
}
