using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using ReversoAPI.Web.DefinitionFeature.Domain.Core.Entities;
using ReversoAPI.Web.DefinitionFeature.Domain.ValueObjects;
using ReversoAPI.Web.Shared.Domain.Extensions;

namespace ReversoAPI.Web.DefinitionFeature.Domain.Supporting.Builders
{
    public class DefinitionParseBuilder
    {
        private readonly HtmlDocument _html;
        private readonly DefinitionData _response;

        public DefinitionParseBuilder(Stream htmlStream)
        {
            _html = new HtmlDocument();
            _html.Load(htmlStream);
            _response = new DefinitionData();
        }

        public DefinitionData Build() => _response;

        public DefinitionParseBuilder WithInputText()
        {
            try
            {
                _response.Text = _html.DocumentNode
                    .SelectSingleNode("//*[@id='search-definitions-input']")
                    .GetAttributeValue("value", string.Empty);

                return this;
            }
            catch
            {
                throw new ParsingException("Unable to parse input field.");
            }
        }

        public DefinitionParseBuilder WithLanguages()
        {
            try
            {
                _response.Language = _html.DocumentNode
                    .SelectSingleNode("//*[@id='src-selector']//span[@class='lang-name']")
                    .InnerHtml
                    .ToLanguage();

                if (_response.Language == Language.Unknown) throw new ParsingException();
            }
            catch
            {
                throw new ParsingException("Unable to parse source language.");
            }

            return this;
        }

        public DefinitionParseBuilder WithDefinitions()
        {
            var sourceLanguage = _response.Language;
            if (sourceLanguage == Language.Unknown) throw new ArgumentException($"'{_response.Language}' is not setted");

            try
            {
                //var sourceLayout = GetLayout(sourceLanguage);
                //var sourceSentences = _html.DocumentNode
                //    .SelectNodes($"//*[@id='examples-content']/div[@class='example']/div[@class='src {sourceLayout}']/span")
                //    .Select(n => n.InnerHtml.RemoveHtmlTags().ReplaceSpecSymbols());

                //var targetLayout = GetLayout(targetLanguage);
                //var targetSentences = _html.DocumentNode
                //    .SelectNodes($"//*[@id='examples-content']/div[@class='example']/div[@class='trg {targetLayout}']/span[@class='text'][1]")
                //    .Select(n => n.InnerHtml.RemoveHtmlTags().ReplaceSpecSymbols());

                //if (targetSentences.Count() != sourceSentences.Count())
                //    throw new ParsingException("Failed to parse an examples");
                var partOfSpeeches = _html.DocumentNode.SelectNodes("//*[@class='definition-pos-block']//h2")
                    .Select(x => x.InnerHtml.ToPartOfSpeech());

                var definitionSentenceGroups = _html.DocumentNode.SelectNodes("//*[@class='definition-pos-block']//div[contains(@class, 'definition-example')]");

                var defintions = new List<Defintion>();


                foreach (var partOfSpeech in partOfSpeeches.Select((v, i) => new { Index = i, Value = v }))
                {
                    var definitionSentences = _html.DocumentNode.SelectNodes($"//div[@class='definition-pos-block']");

                    foreach (var definitionSentence in definitionSentences)
                    {
                        //defintions.AddRange(new Defintion(sourceSentence, targetSentence));
                    }
                }

                _response.Definitions = defintions;

                return this;
            }
            catch
            {
                throw new ParsingException("Unable to parse contexts examples.");
            }
        }

        private string GetLayout(Language language)
        {
            return language == Language.Arabic ? $"rtl {Language.Arabic.ToString().ToLower()}" :
                   language == Language.Hebrew ? "rtl" :
                   "ltr";
        }
    }
}