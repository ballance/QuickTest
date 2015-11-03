using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest
{
    public class CompressionTester
    {
        public static int bufferSize = 32768;
        public string Compress(string uncompressedString)
        {
            var inputData = Encoding.UTF8.GetBytes(uncompressedString);
            using (var output = new MemoryStream())
            {
                using (var compression = new GZipStream(output, CompressionMode.Compress, false))
                {
                    compression.Write(inputData, 0, inputData.Length);
                }
                return Convert.ToBase64String(output.ToArray());
            }
        }
        
        public string Decompress(string compressedString)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedString);
            using (var inStream = new MemoryStream(gzBuffer))
            {
                using (var outStream = new MemoryStream())
                {
                    using (var gzipStream = new GZipStream(inStream, CompressionMode.Decompress))
                    {
                        var buffer = new byte[bufferSize];
                        int bytesRead;
                        while ((bytesRead = gzipStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            outStream.Write(buffer, 0, bytesRead);
                        }
                    }
                    return Encoding.UTF8.GetString(outStream.ToArray());
                }
            }
        }
    }
}
