using System.Collections;
using System.Collections.Generic;
using ReversoAPI.Web.DefinitionFeature.Domain.ValueObjects;

namespace ReversoAPI.Web.DefinitionFeature.Domain.Core.Entities
{
    public class DefinitionData
    {
        public string Text { get; set; }
        public Language Source { get; set; }

        public Language Target { get; set; }

        public IEnumerable<Defintion> Definitions { get; set; }
    }
}
