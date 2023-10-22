using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Gitusme.Net.Extensiones.Core;
using Com.Gitusme.Net.Extensiones.Demos.Model;

namespace Com.Gitusme.Net.Extensiones.Demos
{
    internal class ObjectExensionesDemo : IExtensionesDemo
    {
        public override void Execute()
        {
            base.Execute();

            // Example 1: 判断对象是否为null
            MyObject obj = null;
            if (obj.IsNull())
            {
                System.Console.WriteLine("Example 1 输出结果:" + "null");
            }

            // Example 2: 对象为null的时候，返回传入的默认值
            var def = obj.OrDefault(new MyObject());
            System.Console.WriteLine("Example 2 输出结果:" + def);

            // Example 3: 如果对象不为null，则执行传入的Action行为
            def.IfPresent((it) =>
            {
                System.Console.WriteLine("Example 3 输出结果:" + it.Name);
            });

            // Example 4: 将Xml对象转换为Xml文本
            var xmlObj = new MyXmlObject();
            string xml = xmlObj.ToXml();
            System.Console.WriteLine("Example 4 输出结果:" + xml);

            // Example 5: 将Json对象转换为Json文本
            var jsonObj = new MyJsonObject();
            string json = jsonObj.ToJson();
            System.Console.WriteLine("Example 5 输出结果:" + json);

        }
    }
}

