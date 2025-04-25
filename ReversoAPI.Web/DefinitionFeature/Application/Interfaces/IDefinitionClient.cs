namespace ReversoAPI.Web.DefinitionFeature.Application.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using ReversoAPI.Web.DefinitionFeature.Domain.Core.Entities;

    public interface IDefinitionClient
    {
        Task<DefinitionData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}