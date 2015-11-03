using System;
using System.Text;

namespace QuickTest
{
    public static class TextGenerator
    {
        public static string LoremIpsum(int minWords, int maxWords,
            int minSentences, int maxSentences,
            int numParagraphs)
        {

            var words = new[]
            {
                "lorem", "ipsum", "dolor", "sit", "amet", "avec", "consectetuer", "bongo",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "giraffe",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat", "summa", "dogfood"
            };

            var rand = new Random();
            var numSentences = rand.Next(maxSentences - minSentences)
                               + minSentences + 1;
            var numWords = rand.Next(maxWords - minWords) + minWords + 1;

            var result = new StringBuilder();

            for (var p = 0; p < numParagraphs; p++)
            {
                for (var s = 0; s < numSentences; s++)
                {
                    for (var w = 0; w < numWords; w++)
                    {
                        if (w > 0)
                        {
                            result.Append(" ");
                        }
                        result.Append(words[rand.Next(words.Length)]);
                    }
                    result.Append(". ");
                }
            }
            return result.ToString();
        }
    }
}