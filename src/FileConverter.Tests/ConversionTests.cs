using System;
using System.IO;
using FileConverter.Services;
using FileConverter.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeepEqual.Syntax;

namespace FileConverter.Tests
{
    [TestClass]
    public class ConversionTests
    {
        [TestMethod]
        public void HasXmlReader()
        {
            HasReader(".xml");   
        }

        [TestMethod]
        public void HasXmlWriter()
        {
            HasWriter(".xml");
        }

        [TestMethod]
        public void CanConvertXml()
        {
            CanConvert(".xml");
        }

        [TestMethod]
        public void HasJsonReader()
        {
            HasReader(".json");
        }

        [TestMethod]
        public void HasJsonWriter()
        {
            HasWriter(".json");
        }

        [TestMethod]
        public void CanConvertJson()
        {
            CanConvert(".json");
        }

        [TestMethod]
        public void HasBinaryReader()
        {
            HasReader(".bin");
        }

        [TestMethod]
        public void HasBinaryWriter()
        {
            HasWriter(".bin");
        }

        [TestMethod]
        public void CanConvertBinary()
        {
            CanConvert(".bin", ".xml");
        }

        [TestMethod]
        public void HasCsvReader()
        {
            HasReader(".csv");
        }

        [TestMethod]
        public void HasCsvWriter()
        {
            HasWriter(".csv");
        }

        [TestMethod]
        public void CanConvertCsv()
        {
            CanConvert(".csv");
        }

        private void CanConvert(string extension, string sourceExtension = null)
        {
            if (sourceExtension != null)
                HasReader(sourceExtension);

            HasReader(extension);
            HasWriter(extension);

            var path = GetTemplatePath(sourceExtension ?? extension);
            var sourceReader = GetReader(sourceExtension ?? extension);
            var reader = GetReader(extension);
            var writer = GetWriter(extension);

            object firstRead;
            object secondRead;

            using (var inputStream = File.OpenRead(path))
            {
                firstRead = sourceReader.Read(inputStream);

                Assert.IsNotNull(firstRead);

                using (var outputBuffer = new MemoryStream())
                {
                    writer.Write(outputBuffer, firstRead);
                    
                    var bytes = outputBuffer.ToArray();
                    var bytesLargerThanZero = bytes.Length > 0;

                    Assert.IsTrue(bytesLargerThanZero);

                    var inputBuffer = new MemoryStream(bytes);

                    secondRead = reader.Read(inputBuffer);
                }
            }

            firstRead.ShouldDeepEqual(secondRead);
        }

        private void HasWriter(string extension)
        {
            var writers = ConverterRegistrationScanner.SupportedWriters;
            Assert.IsTrue(writers.ContainsKey(extension));
        }

        private void HasReader(string extension)
        {
            var readers = ConverterRegistrationScanner.SupportedReaders;
            Assert.IsTrue(readers.ContainsKey(extension));
        }

        private string GetTemplatePath(string extension)
        {
            var executingFolder = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            return Path.Combine(executingFolder, "Template" + extension);
        }

        private IFileWriter GetWriter(string path)
        {
            var extension = Path.GetExtension(path);
            return ConverterRegistrationScanner.SupportedWriters[extension];
        }

        private IFileReader GetReader(string path)
        {
            var extension = Path.GetExtension(path);
            return ConverterRegistrationScanner.SupportedReaders[extension];
        }
    }
}
