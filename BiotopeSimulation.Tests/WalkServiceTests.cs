using HeroWillSurviveOrNot.Services;
using HeroWillSurviveOrNot.Species;
using System.Collections.Specialized;

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
            filePathList = new NameValueCollection
            {
                {
                    "InputFilePath",
                    @"C:\\repos\\BiotopeSimulation\\BiotopeSimulation\\SampleInputs\\sampleInput1.txt"
                },
                {
                    "InputFilePath2",
                    @"C:\\repos\\BiotopeSimulation\\BiotopeSimulation\\SampleInputs\\sampleInput2.txt"
                }
            };
        }

        [Test]        
        public void ShouldHeroSurviveFromSampleInputOne()
        {
            //Arrange
            string? inputFilePath = filePathList["InputFilePath"];

            BunkerBiotope bunkerBiotope = fileService.Parse(inputFilePath!);

            //Act
            walkService.Walk(bunkerBiotope);

            //Assert
            Assert.IsTrue(bunkerBiotope.Hero.IsHeroAlive);
        }

        [Test]        
        public void ShouldHeroNotSurviveFromSampleInputSecond()
        {
            //Arrange            
            string? inputFilePath = filePathList["InputFilePath2"];
            BunkerBiotope bunkerBiotope = fileService.Parse(inputFilePath!);

            //Act
            walkService.Walk(bunkerBiotope);

            //Assert
            Assert.IsFalse(bunkerBiotope.Hero.IsHeroAlive);
        }
    }
}
