using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuickTest.Runner
{
    class QuickTestRunnerConsole
    {
        static void Main()
        {
            FileExtensionTestRunner();
            Console.ReadKey();
        }

        private static void FileExtensionTestRunner()
        {
            var testCases = new List<string> {"floof", "test.png", "c:\\home\\code\\floof.floof", "c:\\home\\code\\floof.floof.pdf", "c:\\home\\code\\stuff\\asdasfd-sadfsadf-asfasfsd-asfda"};

            foreach (var testCase in testCases)
            {
                var sw = new Stopwatch();
                sw.Start();
                var regexResult = new InvalidLocalFileSpecification().IsSatisfiedBy(testCase);
                sw.Stop();
                Console.WriteLine("Test of [{0}] returned {1} in {2} ticks.", testCase, regexResult, sw.ElapsedTicks);
            }
        }

        private static void CompressionTestRunner()
        {
            var originalText = TextGenerator.LoremIpsum(1000000, 1000000, 1, 1, 1);
            var compressionTester = new CompressionTester();

            var compressionTimer = new Stopwatch();
            compressionTimer.Start();
            var compressedText = compressionTester.Compress(originalText);
            compressionTimer.Stop();

            var decompressionTimer = new Stopwatch();
            decompressionTimer.Start();

            var decompressedText = compressionTester.Decompress(compressedText);
            decompressionTimer.Stop();

            var originalTextLength = originalText.Length;
            var improvementInLength = originalTextLength - compressedText.Length;

            Console.WriteLine("Original text ({0} characters)", originalTextLength);
            Console.WriteLine();
            Console.WriteLine("Compressed text ({0} characters) [improvement by {1} characters / {2} % ]", compressedText.Length,
                improvementInLength,
                decimal.Round((100 * ((decimal)improvementInLength) / originalTextLength), 0, MidpointRounding.AwayFromZero));

            Console.WriteLine();
            Console.WriteLine("Decompressed text ({0} characters)", decompressedText.Length);

            Console.WriteLine();
            Console.WriteLine(originalText.Equals(decompressedText) ? "Orginal text EQUALS decompressed text" : "Fail");

            Console.WriteLine("Compression took {0} ms.", compressionTimer.ElapsedMilliseconds);
            Console.WriteLine("Decompression took {0} ms.", decompressionTimer.ElapsedMilliseconds);

            Console.ReadKey();
        }
    }
}

