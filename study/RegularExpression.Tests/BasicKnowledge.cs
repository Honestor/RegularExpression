using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Xunit;

namespace RegularExpression.Tests
{
    /// <summary>
    /// 基础知识
    /// </summary>
    public class BasicKnowledge
    {
        /// <summary>
        /// ^符号测试
        /// </summary>
        [Fact]
        public void Head()
        {
            string pattern = "^hel";
            string n = "hello world";
            //第一个是h,接着是e接着l,所以hello world符合规则.
            Assert.Matches(pattern, n);

            string n1 = "sello world";
            //第一个字符不是h,不匹配
            Assert.DoesNotMatch(pattern,n1);

            string n2 = "Hel world";
            //不忽略大小写,第一个字符串不是小写h,所以不匹配
            Assert.DoesNotMatch(new Regex(pattern), n2);

            string n3 = "Hel world";
            //忽略大小写,第一个字符串是h接着e接着l
            Assert.Matches(new Regex(pattern, RegexOptions.IgnoreCase), n3);
        }

        /// <summary>
        /// $测试
        /// </summary>
        [Fact]
        public void Tail()
        {
            string pattern = "ld$";
            string n = "hello world";
            //最后一个字符是d,且前一个是l,匹配
            Assert.Matches(pattern, n);

            string n1 = "hello wordl";
            //最后一个字符是l,不匹配
            Assert.DoesNotMatch(pattern, n1);

            string n2 = "Hel worlD";
            //不忽略大小写,最后一个字符不是小写d,所以不匹配
            Assert.DoesNotMatch(new Regex(pattern), n2);

            string n3 = "Hel worLD";
            //忽略大小写,第一个字符串是D接着L,匹配
            Assert.Matches(new Regex(pattern, RegexOptions.IgnoreCase), n3);
        }

        /// <summary>
        /// ^和$符号联测
        /// </summary>
        [Fact]
        public void HeadAndTail()
        { 
            string pattern = "^hello world$";
            string n = "hello world";
            //第一个是h,接着是e接着l,所以hello world符合规则.
            Assert.Matches(pattern, n);
        }

        /// <summary>
        /// 字符组 []
        /// </summary>
        [Fact]
        public void CharGroup()
        {
            //匹配第一个字符为g,第二个为r,第三个字符为e或者a第四个为y的字符串
            var p1 = "gr[ea]y";
            string n1 = "gray";
            Assert.Matches(p1, n1);
            string n2 = "grey";
            Assert.Matches(p1, n2);

            //匹配第一个字符为g,第二个为r或者w,第三个字符为e或者a第四个为y的字符串
            var p2 = "g[rw][ea]y";
            string n3 = "gray";
            Assert.Matches(p2, n3);
            string n4 = "grey";
            Assert.Matches(p2, n4);
            string n5 = "gway";
            Assert.Matches(p2, n3);
            string n6 = "gwey";
            Assert.Matches(p2, n4);

            //匹配第一个字符为h,第二个字符为1到5的字符串
            var p3 = "h[12345]";
            Assert.Matches(p3, "h1");
            Assert.Matches(p3, "h2");
            Assert.Matches(p3, "h3");
            Assert.Matches(p3, "h4");
            Assert.Matches(p3, "h5");

            //匹配第一个字符为h,第二个字符为1到5的字符串
            var p4 = "h[1-5]";
            Assert.Matches(p4, "h1");
            Assert.Matches(p4, "h2");
            Assert.Matches(p4, "h3");
            Assert.Matches(p4, "h4");
            Assert.Matches(p4, "h5");

            //匹配字符串是否以数字开头,第二个字母是小写字母
            var p5 = "^[0-9][a-z]";
            for (int i = 0; i <= 9; i++)
            {
                for (var j = 'a'; j < 'z'; j++)
                {
                    Assert.Matches(p5, $"{i.ToString()}{j}");
                }
            }

            //匹配第一个字符是否是数字或者小写字母
            var p6 = "^[0-9a-z]";
            for (var j = 'a'; j < 'z'; j++)
            {
                Assert.Matches(p6, $"{j.ToString()}1");
            }

            for (int i = 0; i <= 9; i++)
            {
                Assert.Matches(p6, $"{i.ToString()}a");
            }

            Assert.DoesNotMatch(p6,"A1");

            //匹配第一个字符是否或者是大小写字母
            var p7 = "^[0-9a-zA-Z]";
            for (var x = 'a'; x < 'z'; x++)
            {
                Assert.Matches(p7, $"{x.ToString()}1");
            }

            for (var y = 'A'; y < 'Z'; y++)
            {
                Assert.Matches(p7, $"{y.ToString()}1");
            }

            for (int z = 0; z <= 9; z++)
            {
                Assert.Matches(p7, $"{z.ToString()}1");
            }

            //匹配一个字母是否以字母数字下划线开头
            var p8 = "^[0-9a-zA-Z_]";
            for (var x = 'a'; x < 'z'; x++)
            {
                Assert.Matches(p8, $"{x.ToString()}1");
            }

            for (var y = 'A'; y < 'Z'; y++)
            {
                Assert.Matches(p8, $"{y.ToString()}1");
            }

            for (int z = 0; z <= 9; z++)
            {
                Assert.Matches(p8, $"{z.ToString()}a");
            }

            string[] singles = {"`","~","!","@","#","%","$","^","&","*","(",")","-","+",",",".","'","\"/",":",";","[","]","{","}","\\","|","=","/","*","?","<",">"};
            foreach (var t in singles)
            {
                Assert.DoesNotMatch(p8, $"{t}1");
                Assert.DoesNotMatch(p8, $"{t}z");
            }


        }

        /// <summary>
        /// 排除行字符 ^
        /// 注:排除行字符是匹配一个是不1到3范围的数字,h后面没有数字并正则表达式引擎并没有找到不是1到3以外的字符
        /// </summary>
        [Fact]
        public void ExceptChar()
        {
            //匹配除第一个字符是h,第二个字符不是1到3的字符
            var p1 = "h[^1-3]";
            Assert.DoesNotMatch(p1, "h1");
            Assert.DoesNotMatch(p1, "h2");
            Assert.DoesNotMatch(p1, "h3");
            Assert.Matches(p1, "h4");
            Assert.Matches(p1, "h5");
            //注:排除行字符是匹配一个是不1到3范围的数字,h后面没有数字并正则表达式引擎并没有找到不是1到3以外的字符
            Assert.DoesNotMatch(p1, "h");
        }

        /// <summary>
        /// 用.(点号)匹配任意字符
        /// </summary>
        [Fact]
        public void PointChar()
        {
            //匹配一个具体的日期08-19-66或08.19.66或08/19/66
            //注:在方括号里面的元字符并不具有元字符的功能,他们只代表简单的字符
            //这里的.相当于一个字符
            var p1 = "08[-./]19[-./]66";
            Assert.Matches(p1, "08-19-66");
            Assert.Matches(p1, "08.19.66");
            Assert.Matches(p1, "08/19/66");

            //匹配一个字符串中包含连续的三个子字符串08、19、66中间包含任务两个字符
            //这里.(点号)相当于占位符,这里属于正则表达式的元字符
            var p2 = "08.19.66";
            Assert.Matches(p2, "08-19-66");
            Assert.Matches(p2, "08.19.66");
            Assert.Matches(p2, "08/19/66");
            Assert.Matches(p2, "08x19y66");
        }

        /// <summary>
        /// 匹配任意子表达式 | (或表达式,多选分支)
        /// </summary>
        [Fact]
        public void SuitAnyPattern()
        {
            var p1 = "hel[ol]o";
            Assert.Matches(p1, "hello");
            Assert.Matches(p1, "heloo");
            var p2 = "hello|heloo";
            Assert.Matches(p2, "hello");
            Assert.Matches(p2, "heloo");
            var p3 = "hel(l|o)o";
            Assert.Matches(p3, "hello");
            Assert.Matches(p3, "heloo");
        }

        /// <summary>
        /// 单词分界符 \b\b
        /// </summary>
        [Fact]
        public void WordSplitSymbol()
        {
            var content= "hello regular expression";
            //匹配字符串中是否包含regular
            var pattern = @"\bregular\b";
            Assert.Matches(pattern, content);
            var result=Regex.Replace(content, pattern, "world");
            Assert.Equal("hello world expression", result);
        }

        /// <summary>
        /// 可选项元素
        /// ? 问号前面紧邻的元素可以不出现也可以只出现一次
        /// </summary>
        [Fact]
        public void OptionalElements() 
        {
            var color = "color";
            var colour = "colour";
            var pattern = "colou?r";
            Assert.Matches(pattern, color);
            Assert.Matches(pattern, colour);

            //匹配7月6号
            var patternTwo = "July?[.]6(th)?";
            var c1 = "July.6";
            var c2 = "July.6th";
            var c3 = "Jul.6th";
            var c4 = "Jul.6";
            Assert.Matches(patternTwo, c1);
            Assert.Matches(patternTwo, c2);
            Assert.Matches(patternTwo, c3);
            Assert.Matches(patternTwo, c4);
        }

        /// <summary>
        /// 其他量词,重复出现的词
        /// .+  加号前面紧邻的元素出现至少一次或多次
        /// .*   星号前面紧邻的元素出现多次或者不出现
        /// </summary>
        [Fact]
        public void RepeatShow()
        {
            var pattern = "<HR.+SIZE.*=.*66.*>";
            var c1 = "<HR SIZE = 66 >";
            var c2 = "<HR    SIZE   =     66     >";
            var c3 = "<HR                 SIZE    =              66                                          >";
            var c4 = "<HRSIZE           =   66  >";
            Assert.Matches(pattern, c1);
            Assert.Matches(pattern, c2);
            Assert.Matches(pattern, c3);
            Assert.DoesNotMatch(pattern, c4);

            var pattern1 = "<HR.+SIZE.*=.*[0-9]+.*";
            var c5 = "<HR SIZE = 6 >";
            var c6 = "<HR    SIZE   =     666     >";
            var c7 = "<HR                 SIZE    =              6666                                          >";
            var c8 = "<HR SIZE           =   06  >";
            Assert.Matches(pattern1, c5);
            Assert.Matches(pattern1, c6);
            Assert.Matches(pattern1, c7);
            Assert.Matches(pattern1, c8);
        }

        /// <summary>
        /// 反向引用
        /// </summary>
        [Fact]
        public void BackReference()
        {
            var pattern = @"([0-9])([a-z])\1\2";
            var c1 = "0a0a";
            Assert.Matches(pattern,c1);
            var c2 = "9a9a";
            Assert.Matches(pattern, c2);

            //检索字符串中连续两次相同的字符串，例如:12356085608456,该字符串中连续出现了5608
            var pattern1 = @"(.+)\1";
            var c3 = "12356085608456";
            Assert.Matches(pattern1, c3);
            var c4 = "58924564561785";
            Assert.Matches(pattern1, c4);
            var c5= "112112asdasd";
            Assert.Matches(pattern1, c5);
            var pattern2 = @"([0-9]+).*\1";
            var c6= "123ffghfhgfh123";
            Assert.Matches(pattern2, c6);
            var c7 = "123ffghfhgfh456";
            Assert.DoesNotMatch(pattern2, c7);
        }

        /// <summary>
        /// 转义符
        /// 让元字符失去作用
        /// </summary>
        [Fact]
        public void EscapeCharacter()
        {
            string pattern = @"www\.baidu\.com";
            Assert.Matches(pattern, "www.baidu.com.cn");
            Assert.DoesNotMatch(pattern, "wwwxbaiduycom.cn");
            Assert.DoesNotMatch(pattern, "www baidu com.cn");
        }

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




