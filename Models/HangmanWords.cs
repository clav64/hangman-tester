using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Models
{
    internal class HangmanWords
    {
        public static List<String> EasyWords = new List<string> { "apple", "car", "float" };
        public static List<String> MediumWords = new List<string> { "bananas", "flopper", "streaker" };
        public static List<String> HardWords = new List<string> { "establishment", "frictionless", "distinguish" };
    }
}
