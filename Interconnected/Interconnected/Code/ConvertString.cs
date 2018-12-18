using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Interconnected.Code
{
    public class ConvertString
    {
        public static float ToSalary(double salary)
        {
            return (float)salary / 1000000;
        }
        public static string CutString(string str, int number)
        {
            if (str.Length > number)
            {
                str = str.Substring(0, number) + "...";
            }
            return str;
        }

        public static string ToUrlSlug(string text)
        {
            for (int i = 32; i < 48; i++)
            {

                text = text.Replace(((char)i).ToString(), " ");

            }
            text = text.Replace(".", "-");

            text = text.Replace(" ", "-");

            text = text.Replace(",", "-");

            text = text.Replace(";", "-");

            text = text.Replace(":", "-");

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower(); ;
        }
        public static string ToUrlSlug1(string text)
        {
            for (int i = 32; i < 48; i++)
            {

                text = text.Replace(((char)i).ToString(), " ");

            }
            text = text.Replace(".", "-");

            text = text.Replace(" ", "-");

            text = text.Replace(",", "-");

            text = text.Replace(";", "-");

            text = text.Replace(":", "-");

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower(); ;
        }

        public static string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();
        }
    }
}