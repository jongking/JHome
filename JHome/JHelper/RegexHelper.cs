using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JHelper
{
    /// <summary>
    /// 操作正则表达式的公共类
    /// </summary>    
    public class RegexHelper
    {
        public static string GetContextCoverS(string input, string left, string right)
        {
            left = FormatRegexToString(left);
            right = FormatRegexToString(right);
            return GetContextCover(input, left, right);
        }

        /// <summary>
        /// 获取用left和right包围的内容,返回不包括left和right
        /// </summary>
        public static string GetContextCover(string input, string left, string right)
        {
            var regex = new Regex(left + "(?<Result>.*)" + right, RegexOptions.Compiled);
            return regex.Match(input).Groups["Result"].Value;
        }

        public static string GetContextCoverByS(string input, string left, string right)
        {
            left = FormatRegexToString(left);
            right = FormatRegexToString(right);
            return GetContextCoverBy(input, left, right);
        }

        /// <summary>
        /// 获取用left和right包围的内容,返回包括left和right
        /// </summary>
        public static string GetContextCoverBy(string input, string left, string right)
        {
            var regex = new Regex(left + ".*" + right, RegexOptions.Compiled);
            var match = regex.Match(input);
            return match.Value;
        }
        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>        
        public static bool IsMatch(string input, string pattern)
        {
            return IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">筛选条件</param>
        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }

        public static bool IsContains(string input, params string[] args)
        {
            foreach (var str in args)
            {
                if (input.Contains(str))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 格式化正则表达式
        /// </summary>
        public static string FormatRegexToString(string str)
        {
            return str
                .Replace(@"\", @"\\")
                .Replace(".", @"\.")
                .Replace("*", @"\*")
                .Replace("+", @"\+")
                .Replace("?", @"\?")
                .Replace("^", @"\^")
                .Replace("<", @"\<")
                .Replace(">", @"\>")
                .Replace("$", @"\$");
        }
    }
}
