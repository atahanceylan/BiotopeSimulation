using HeroWillSurviveOrNot.Services;
using HeroWillSurviveOrNot.Species;
using System.Collections.Specialized;

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
            filePathList = new NameValueCollection
            {
                {
                    "InputFilePath",
                    @"C:\\repos\\BiotopeSimulation\\BiotopeSimulation\\SampleInputs\\sampleInput1.txt"
                },
                {
                    "InputFilePath2",
                    @"C:\\repos\\BiotopeSimulation\\BiotopeSimulation\\SampleInputs\\sampleInput2.txt"
                },
                {
                    "InputFilePath3",
                    @"C:\\repos\\BiotopeSimulation\\BiotopeSimulation\\SampleInputs\\sampleInput3.txt"
                }
            };

        }      

        [Test]        
        public void ShouldReturnBunkerBiotopeWhenFileParsedSuccessfully()
        {
            //Arrange
            string? inputFilePath = filePathList["InputFilePath"];

            //Act
            BunkerBiotope bunkerBiotope = fileService.Parse(inputFilePath!);

            //Assert
            Assert.IsTrue(bunkerBiotope is not null);
        }

        [Test]        
        public void ShouldThrowExceptionWhenInputFileIsNotValid()
        {
            //Arrange
            string? inputFilePath = filePathList["InputFilePath3"];

            //Act && Assert
            Assert.Throws<IndexOutOfRangeException>(() => fileService.Parse(inputFilePath!));
        }
    }
}