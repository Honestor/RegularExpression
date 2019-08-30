using System.Text.RegularExpressions;
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

        /// <summary>
        /// 匹配美元 不准确,后续的例子会完善
        /// </summary>
        [Fact]
        public void GetDollor()
        {
            var pattern = @"\$[0-9]+(\.[0-9][0-9])?";
            Assert.Matches(pattern, "$0.00000000000000000009");
            Assert.Matches(pattern, "$99999999999999999999.9");
            Assert.Matches(pattern, "$100000000000000000000");
            Assert.Matches(pattern, "$1,000,000,000");
            Assert.DoesNotMatch(pattern, "$.99");
        }

        /// <summary>
        /// 匹配时间 
        /// </summary>
        [Fact]
        public void GetTime()
        {
            //12小时制 存在问题
            var pattern12 = "(1[012]|[1-9]):[0-5][0-9].(am|pm)";
            Assert.Matches(pattern12, "1:59 am");
            Assert.Matches(pattern12, "1:59 pm");
            Assert.Matches(pattern12, "11:59 am");
            Assert.Matches(pattern12, "11:59 pm");
            Assert.Matches(pattern12, "12:59 am");
            Assert.Matches(pattern12, "12:59 pm");

            //24小时制
            var pattern24 = "^(0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";
            Assert.Matches(pattern24, "00:00");
            Assert.Matches(pattern24, "1:59");
            Assert.Matches(pattern24, "01:59");
            Assert.Matches(pattern24, "11:59");
            Assert.Matches(pattern24, "13:59");
            Assert.Matches(pattern24, "23:59");
            Assert.DoesNotMatch(pattern24, "25:59");
            Assert.DoesNotMatch(pattern24, "00:0001");

            //合并24小时制的多选分支如下
            var pattern24Better = "^([01]?[0-9]|2[0-3]):[0-5][0-9]$";
            Assert.Matches(pattern24Better, "00:00");
            Assert.Matches(pattern24Better, "1:59");
            Assert.Matches(pattern24Better, "01:59");
            Assert.Matches(pattern24Better, "11:59");
            Assert.Matches(pattern24Better, "13:59");
            Assert.Matches(pattern24Better, "23:59");
            Assert.Matches(pattern24Better, "9:59");
            Assert.Matches(pattern24Better, "0:59");
            Assert.DoesNotMatch(pattern24Better, "25:59");
            Assert.DoesNotMatch(pattern24Better, "00:0001");
        }

        /// <summary>
        /// 匹配一个容许输入数字和可能出现小数部分的的数字
        /// </summary>
        [Fact]
        public void GetNum()
        {
            var pattern = @"^[-+]?[0-9]+(\.[0-9]*)?$";
            Assert.Matches(pattern, "0.34");
            Assert.Matches(pattern, "+0.34");
            Assert.Matches(pattern, "-0.34");
            Assert.Matches(pattern, "999999999.999999999999");
            Assert.Matches(pattern, "+999999999.999999999999");
            Assert.Matches(pattern, "-999999999.999999999999");
            Assert.Matches(pattern, "99.");
            Assert.DoesNotMatch(pattern, ".99");
            Assert.DoesNotMatch(pattern, "x.99");
        }

        /// <summary>
        /// 匹配一个温度 可以出现数字
        /// </summary>
        [Fact]
        public void GetTemperature()
        {
            var pattern = @"^([-+]?[0-9]+(\.[0-9]*)?).([CF])$";
            Assert.Matches(pattern, "34C");
            Assert.Matches(pattern, "-34C");
            Assert.Matches(pattern, "+34C");
            Assert.Matches(pattern, "+0.34 C");
            Assert.Matches(pattern, "-0.34 C");
            Assert.Matches(pattern, "999999999.999999999999 C");
            Assert.Matches(pattern, "+999999999.999999999999 F");
            Assert.Matches(pattern, "-999999999.999999999999 F");
            Assert.Matches(pattern, "99. F");
            Assert.DoesNotMatch(pattern, ".99 F");
            Assert.DoesNotMatch(pattern, "x.99 F");
        }
    }
}
