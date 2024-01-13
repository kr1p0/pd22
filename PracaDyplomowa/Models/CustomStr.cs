using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class CustomStr
    {
        public CustomStr() { }

        public static string Adjust(string input, int length=14)
        {
            if (string.IsNullOrEmpty(input)) return "┅";
            var result = input.Length > length ? input.Substring(0, length) + "..." : input;
            return result;
        }

        public static string GenerateRandomString(int stringLength)
        {
            StringBuilder sb = new StringBuilder();
            char[] alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
            char[] alphabet2 = Enumerable.Range('a', 26).Select(x => (char)x).ToArray();
            alphabet = alphabet.Concat(alphabet2).ToArray();
            Random rnd = new Random();
            for(int i =0; i< stringLength;i++)
            {
                int rndIndex = rnd.Next(0, alphabet.Length - 1);
                sb.Append(alphabet[rndIndex]);
            }
            return sb.ToString();
        }
    }
}
