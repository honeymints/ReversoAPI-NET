using System;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.DefinitionFeature.Application.Interfaces.Services;
using ReversoAPI.Web.DefinitionFeature.Domain.Core.Entities;
namespace ReversoAPI.Web.DefinitionFeature.Application.Services
{

    public class DefinitionService : IDefinitionService
    {
        private const string DefitinitionURL = "https://dictionary.reverso.net/";

        private readonly IAPIConnector _apiConnector;
        private readonly IParseService<DefinitionData> _parser;

        public DefinitionService(IAPIConnector apiConnector, IParseService<DefinitionData> parser)
        {
            _apiConnector = apiConnector;
            _parser = parser;
        }

        public async Task<DefinitionData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default)
        {
            var url = CombineUrl(text, source, target);

            using var response = await _apiConnector
                .GetAsync(url, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsHtml()) throw new FormatException("The server response contains data that is not in HTML format.");

            return _parser.Invoke(response.Content);
        }

        private Uri CombineUrl(string text, Language source, Language target)
        {
            var sourceLanguage = source.ToString().ToLower();
            var targetLanguage = target.ToString().ToLower();

            return new Uri(DefitinitionURL + $"{sourceLanguage}-definition/{text}#translation={targetLanguage}");
        }
    }
}