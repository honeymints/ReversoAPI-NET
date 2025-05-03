namespace ReversoAPI.Web.DefinitionFeature.Domain.ValueObjects
{
    public class Defintion
    {
        public string Value { get; }

        public Language Language { get; }

        public PartOfSpeech PartOfSpeech { get; }

        public string Sentence { get; } 

        public string Context { get; }

        public Defintion(string value,
            Language language,
            PartOfSpeech partOfSpeech,
            string sentence,
            string context)
        {
            Value = value;
            Language = language;
            PartOfSpeech = partOfSpeech;
            Sentence = sentence;
            Context = context;
        }
    }

}