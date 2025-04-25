using System.Collections;
using System.Collections.Generic;

namespace ReversoAPI.Web.DefinitionFeature.Domain.Core.Entities
{
    public class DefinitionData
    {
        public string Text { get; set; }
        public string Definition { get; set; }
        public IList<Example> Examples { get; set; }
        public Language Source { get; set; }

        public Language Target { get; set; }
    }
}
