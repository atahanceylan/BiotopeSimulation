
using System.Collections.Specialized;
using BiotopeSimulation.Services;
using BiotopeSimulation.Species;

namespace BiotopeSimulation.Tests
{
    public class FileServiceTests
    {
        private FileService fileService;
        private NameValueCollection filePathList;

        [SetUp]
        public void Setup()
        {
            fileService = new FileService();
            var filePath = AppDomain.CurrentDomain.BaseDirectory;
            filePathList = new NameValueCollection
            {
                {
                    "InputFilePath",
                    $"{filePath}{Path.DirectorySeparatorChar}SampleInputs{Path.DirectorySeparatorChar}sampleInput1.txt"
                },
                {
                    "InputFilePath2",
                    $"{filePath}{Path.DirectorySeparatorChar}SampleInputs{Path.DirectorySeparatorChar}sampleInput2.txt"
                },
                {
                    "InputFilePath3",
                    $"{filePath}{Path.DirectorySeparatorChar}SampleInputs{Path.DirectorySeparatorChar}sampleInput3.txt"
                }
            };
        }      

        [Test]        
        public void ShouldReturnBunkerBiotopeWhenFileParsedSuccessfully()
        {
            //Arrange
            string? inputFilePath = filePathList["InputFilePath"];

            //Act
            BunkerBiotope bunkerBiotope = fileService.ParseTextFileToObjects(inputFilePath!);

            //Assert
            Assert.That(bunkerBiotope is not null);
        }

        [Test]        
        public void ShouldThrowExceptionWhenInputFileIsNotValid()
        {
            //Arrange
            string? inputFilePath = filePathList["InputFilePath3"];

            //Act && Assert
            Assert.Throws<Exception>(() => fileService.ParseTextFileToObjects(inputFilePath!));
        }
    }
}