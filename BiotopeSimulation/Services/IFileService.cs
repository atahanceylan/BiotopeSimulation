using HeroWillSurviveOrNot.Species;

namespace HeroWillSurviveOrNot.Services
{
    public interface IFileService
    {
        BunkerBiotope Parse(string filePath);
    }
}
