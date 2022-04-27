using System;
using System.Linq;

namespace Farum.QA.TestAutomationEssentials.Support
{
    public class RandomGenerator
    {
        public static string GetRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPRSTUWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetGuid()
        {
            return $"{GetRandomString(8)}-{GetRandomString(4)}-{GetRandomString(4)}-{GetRandomString(4)}-{GetRandomString(12)}";
        }
    }
}
