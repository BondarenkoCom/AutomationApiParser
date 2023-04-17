using AutoParser.Interfaces;

namespace AutoParser.Factory
{
    public class FactoryRealize
    {
        private readonly IResponseSorterFactory _responseSorterFactory;

        public FactoryRealize(IResponseSorterFactory responseSorterFactory)
        {
            _responseSorterFactory = responseSorterFactory;
        }
    }
}
