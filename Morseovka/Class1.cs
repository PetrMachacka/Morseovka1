using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Morseovka
{

    class MorseCodeConverter
    {
        private Dictionary<string, string> _morseCodeDictionary = new Dictionary<string, string>

    {
        {"A", ".-"}, {"B", "-..."}, {"C", "-.-."}, {"D", "-.."}, {"E", "."},
        {"F", "..-."}, {"G", "--."}, {"H", "...."}, {"I", ".."}, {"J", ".---"},
        {"K", "-.-"}, {"L", ".-.."}, {"M", "--"}, {"N", "-."}, {"O", "---"},
        {"P", ".--."}, {"Q", "--.-"}, {"R", ".-."}, {"S", "..."}, {"T", "-"},
        {"U", "..-"}, {"V", "...-"}, {"W", ".--"}, {"X", "-..-"}, {"Y", "-.--"},
        {"Z", "--.."},
        {"0", "-----"}, {"1", ".----"}, {"2", "..---"}, {"3", "...--"},
        {"4", "....-"}, {"5", "....."}, {"6", "-...."}, {"7", "--..."},
        {"8", "---.."}, {"9", "----."},
        {"@", ".--.-."}, {" ", ""}, {"CH", "----"}
    };

        public string normalize(string txt) 
        {
            string normalizedText = txt.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (var x in normalizedText)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(x) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(x);
                }
            }
        return sb.ToString();
    }

        public string Encode(string text)
        {
            text = text.Normalize();
            text = text.ToUpper();
            Console.WriteLine(text);
            var encodedText = new StringBuilder();

            foreach (var character in text)
            {
                
                    encodedText.Append(_morseCodeDictionary[character.ToString()] + "/");
            }

            return encodedText.ToString().TrimEnd();
        }

        public string Decode(string code)
        {
            var decodedText = new StringBuilder();
            var morseCodeSymbols = code.Split("/");

            var morseCodeDictionaryReversed = _morseCodeDictionary.ToDictionary(x => x.Value, x => x.Key);
            foreach (var symbol in morseCodeSymbols)
            {
                decodedText.Append(morseCodeDictionaryReversed[symbol]);
            }

            return decodedText.ToString();
        }
    }
}