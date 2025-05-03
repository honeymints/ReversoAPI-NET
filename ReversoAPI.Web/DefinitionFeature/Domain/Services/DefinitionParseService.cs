using System.IO;
using ReversoAPI.Web.DefinitionFeature.Domain.Core.Entities;
using ReversoAPI.Web.DefinitionFeature.Domain.Supporting.Builders;
using ReversoAPI.Web.Shared.Domain.Services;

namespace ReversoAPI.Web.DefinitionFeature.Domain.Services
{
    public class DefinitionParseService : BaseParser<DefinitionData>
    {
        private readonly ILogger _log;
        public DefinitionParseService(ILogger log)
        {
            _log = log;
        }
        protected override DefinitionData Parse(Stream htmlStream)
        {
            try
            {
                return new DefinitionParseBuilder(htmlStream)
                    .WithInputText()
                    .WithDefinitions()
                    .Build();
            }
            catch (ParsingException ex)
            {
                _log?.Error(ex.Message);
                return null;
            }
        }
    }
}