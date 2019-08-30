using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RegularExpression.Tests
{
    /// <summary>
    /// 正则表达式的应用
    /// </summary>
    public class Application
    {
        /// <summary>
        /// 获取引号内得字符串 注意下,比较难理解
        /// </summary>
        [Fact]
        public void GetQuotationMark()
        {
            //C#字符串中引号需要转义 两个引号之间不是引号的且中间可以出现多个字符的字符串
            var pattern = "\"[^\"]*\"";
            var content = "<script src=\"1.html\"></script>";
            var matchs = Regex.Match(content, pattern);
            Assert.Equal("1.html", matchs.Value.Replace("\"", ""));
        }
    }
}
