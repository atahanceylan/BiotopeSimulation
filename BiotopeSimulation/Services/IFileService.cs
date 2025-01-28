using BiotopeSimulation.Species;

namespace BiotopeSimulation.Services
{
    public interface IFileService
    {
        BunkerBiotope Parse(string filePath);
    }
}
