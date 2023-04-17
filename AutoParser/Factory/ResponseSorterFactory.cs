using AutoParser.Interfaces;
using AutoParser.ParsingDictionary;

namespace AutoParser.Factory
{
    public class ResponseSorterFactory : IResponseSorterFactory
    {
        public IResponseSorter CreateResponseSorter(string projectName)
        {
            switch (projectName)
            {
                case "DoctorProject":
                    return new DoctorResponseSorter();
                case "PharmacyProject":
                    return new PharmacyResponseSorter();
                default:
                    throw new ArgumentException($"Invalid projectName: {projectName}");
            }
        }
    }
}