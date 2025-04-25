namespace ReversoAPI.Web.DefinitionFeature.Application
{
    using System.Threading;
    using System.Threading.Tasks;
    using ReversoAPI.Web.DefinitionFeature.Application.Interfaces;
    using ReversoAPI.Web.DefinitionFeature.Application.Interfaces.Services;
    using ReversoAPI.Web.DefinitionFeature.Application.Validators;
    using ReversoAPI.Web.DefinitionFeature.Domain.Core.Entities;

    public class DefinitionClient : IDefinitionClient
    {
        private readonly IDefinitionService _definitionService;

        public DefinitionClient(IDefinitionService definitionService)
        {
            _definitionService = definitionService;
        }
        public async Task<DefinitionData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken)
        {
            var validationResult = new DefinitionRequestValidator(text, source, target).Validate();

            if (!validationResult.IsValid)
            {
                throw validationResult.Exception;
            }
           return await _definitionService.GetAsync(text, source, target, cancellationToken).ConfigureAwait(false);

        }
    }
}