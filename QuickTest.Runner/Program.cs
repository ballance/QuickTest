using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace QuickTest.Runner
{
    class QuickTestRunnerConsole
    {
        static void Main()
        {
            CRCTestRunner();
            Console.ReadKey();
        }

        private static void CRCTestRunner()
        {
            var bytesToCrc = new byte[]         { 56, 54, 55, 53, 51, 48, 57, 47, 56, 54, 55, 53, 51, 48, 57, 47, 56, 54, 55, 53, 51, 48, 57, 47, 56, 54, 55, 53, 51, 48, 57, 47, 56, 54, 55, 53, 51, 48, 57, 47 };
            var bytesToCrcOffByOne = new byte[] { 56, 54, 55, 53, 51, 48, 57, 47, 56, 54, 55, 53, 51, 48, 57, 47, 56, 54, 55, 53, 51, 48, 57, 47, 56, 54, 55, 53, 51, 48, 57, 47, 56, 54, 55, 53, 51, 48, 57, 47 };

            var crc_guy = new CRC.Crc32();

            byte[] madeSomeCrc;
            using (var bytesToCrCMemStream = new MemoryStream(bytesToCrc))
            {
                  madeSomeCrc = crc_guy.ComputeHash(bytesToCrCMemStream);
            }

            byte[] madeSomeCrcOffByOne;
            using (var bytesToCrCOffByOneMemStream = new MemoryStream(bytesToCrcOffByOne))
            {
                madeSomeCrcOffByOne = crc_guy.ComputeHash(bytesToCrCOffByOneMemStream);
            }

            Console.WriteLine("before not off by one: {0}", Encoding.ASCII.GetString(bytesToCrc));
            Console.WriteLine("before is off by one:  {0}", Encoding.ASCII.GetString(bytesToCrcOffByOne));

            Console.WriteLine("not off one: {0}", Encoding.ASCII.GetString(madeSomeCrc));
            Console.WriteLine("Off by one:  {0}", Encoding.ASCII.GetString(madeSomeCrcOffByOne));
            Console.WriteLine();
            Console.WriteLine("Are equal? {0}", madeSomeCrc.SequenceEqual(madeSomeCrcOffByOne));
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
        }
    }
}

