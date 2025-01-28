
using System.Collections.Specialized;
using BiotopeSimulation.Services;
using BiotopeSimulation.Species;

namespace BiotopeSimulation.Tests
{
    public class WalkServiceTests
    {
        private FileService fileService;
        private WalkService walkService;
        private NameValueCollection filePathList;

        [SetUp]
        public void Setup()
        {
            fileService = new FileService();
            walkService = new WalkService();
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
                }
            };
        }

        [Test]        
        public void ShouldHeroSurviveFromSampleInputOne()
        {
            //Arrange
            string? inputFilePath = filePathList["InputFilePath"];

            BunkerBiotope bunkerBiotope = fileService.ParseTextFileToObjects(inputFilePath!);

            //Act
            walkService.Walk(bunkerBiotope);

            //Assert
            Assert.That(bunkerBiotope.Hero.IsHeroAlive);
        }

        [Test]        
        public void ShouldHeroNotSurviveFromSampleInputSecond()
        {
            //Arrange            
            string? inputFilePath = filePathList["InputFilePath2"];
            BunkerBiotope bunkerBiotope = fileService.ParseTextFileToObjects(inputFilePath!);

            //Act
            walkService.Walk(bunkerBiotope);

            //Assert
            Assert.That(!bunkerBiotope.Hero.IsHeroAlive);
        }
    }
}
