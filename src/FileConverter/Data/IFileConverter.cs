namespace FileConverter.Data
{
    public interface IFileConverter
    {
        void Convert(string inputPath, string outputPath);
    }
}
