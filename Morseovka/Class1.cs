using System;
using System.Collections.Generic;
using System.Globalization;
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
        {"@", ".--.-."}, {" ", ""},{"CH", "----"}
    };
        private char[] SplitText(string txt)
        {
            char[] Characters = txt.ToCharArray();
            return Characters;
        }
        private string[] SplitCoded(string txt)
        {
            string[] symbols = txt.Split("/");
            return symbols;
        }
        string normalize(string text)
        {
            string normalizedText = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (var x in normalizedText)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(x) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(x);
                }
            }
            string result = sb.ToString().Normalize(NormalizationForm.FormC);
            Encoding srcEnc = Encoding.UTF8;
            Encoding dstEnc = Encoding.ASCII;
            var srcBytes = srcEnc.GetBytes(result);
            var dstBytes = Encoding.Convert(srcEnc, dstEnc, srcBytes);
            string newText = dstEnc.GetString(dstBytes);
            return newText;
        }

        public string Encode(string txt)
        {
            var EncodedText = new StringBuilder();
            char[] characters = SplitText(normalize(txt.ToUpper()));
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i].ToString() ==  "C" && characters[i + 1].ToString() == "H")
                {
                    i += 1;
                    EncodedText.Append(_morseCodeDictionary["CH"]+ "/");
                }
                else
                {
                    EncodedText.Append(_morseCodeDictionary[characters[i].ToString()] + "/");
                }
            }
            return EncodedText.ToString();
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