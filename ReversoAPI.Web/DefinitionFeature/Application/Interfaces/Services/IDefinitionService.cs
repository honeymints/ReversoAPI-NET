using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.DefinitionFeature.Domain.Core.Entities;

namespace ReversoAPI.Web.DefinitionFeature.Application.Interfaces.Services
{
    public interface IDefinitionService
    {
        Task<DefinitionData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}