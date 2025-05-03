using ReversoAPI.Web.DefinitionFeature.Application.Interfaces;
using System;

namespace ReversoAPI
{
    public interface IReversoClient : IDisposable
    {
        IContextClient Context { get; }
        ISynonymsClient Synonyms { get; }
        ISpellingClient Spelling { get; }
        ITranslationClient Translation { get; }
        IPronunciationClient Pronunciation { get; }
        IConjugationClient Conjugation { get; }
        IDefinitionClient Definition { get; }
    }
}