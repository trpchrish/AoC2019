using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2019.Solutions
{
    public class Day4
    {
        public static int Day4_1()
        {
            var passwords = GeneratePasswordRange();
            var validPasswords = new List<int>();

            foreach (var password in passwords)
            {
                if (HasSameAdjacentDigits(password.ToString()) && HasAscendingDigits(password.ToString()))
                    validPasswords.Add(password);
            }

            return validPasswords.Count;
        }

        public static int Day4_2()
        {
            var passwords = GeneratePasswordRange();
            var validPasswords = new List<int>();

            foreach (var password in passwords)
            {
                if (HasSameAdjacentDigits(password.ToString()) && HasAscendingDigits(password.ToString()) && HasUniqueDigitPairs(password.ToString()))
                    validPasswords.Add(password);
            }

            return validPasswords.Count;
        }

        private static bool HasUniqueDigitPairs(string password)
        {  
            return password.GroupBy(x => x).Any(g => g.Count() == 2);           
            
        }

        private static bool HasSameAdjacentDigits(string password)
        {              
            return password.GroupBy(x => x).Any(g => g.Count() >= 2);
        }

        private static bool HasAscendingDigits(string password)
        {
            return password.SequenceEqual(password.OrderBy(c => c)); ;
        }

        private static List<int> GeneratePasswordRange()
        {
            var passwords = new List<int>();

            for (var i = 152085; i <= 670283; i++)
                passwords.Add(i);

            return passwords;
        }
    }
}
