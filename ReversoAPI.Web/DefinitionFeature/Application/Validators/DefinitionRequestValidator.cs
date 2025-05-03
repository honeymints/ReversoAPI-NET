using System;
using System.Collections.Generic;
using System.Linq;
using ReversoAPI.Web.Shared.Application.Interfaces;
using ReversoAPI.Web.Shared.Application.Validators;

namespace ReversoAPI.Web.DefinitionFeature.Application.Validators
{
    public class DefinitionRequestValidator : AbstractValidator
    {
        private readonly static Language[] _supportedLanguages = {
            Language.Arabic, Language.German, Language.Spanish,
            Language.French, Language.Hebrew, Language.Italian,
            Language.Japanese, Language.Korean, Language.Dutch,
            Language.Polish, Language.Portuguese, Language.Romanian,
            Language.Russian, Language.Swedish, Language.Turkish,
            Language.Ukrainian, Language.Chinese, Language.English
            };


        public string Text { get; }
        public Language Language { get; }

        public DefinitionRequestValidator(string text, Language language)
        {
            Text = text;
            Language = language;
        }
        protected override IEnumerable<Func<IValidationResult>> GetValidators()
        {
            Func<IValidationResult>[] validators = {
                () => ValidateText(Text),
                () => ValidateLanguage(Language),
                //() => ValidateLanguageCompatibility(Language, Target),
             };
            return validators;
        }


        private IValidationResult ValidateText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                var message = "Text cannot be null or empty.";
                return new ValidationResult(false, message, new ArgumentException(message, nameof(text)));
            }

            if (text.Length > 6000)
            {
                var message = "The text provided exceeds the limit of 6000 symbols.";
                return new ValidationResult(false, message, new ArgumentException(message, nameof(text)));
            }

            return null;
        }

        private IValidationResult ValidateLanguage(Language language)
        {
            if (!_supportedLanguages.Contains(language))
            {
                var message = $"'{language}' is not supported.";
                return new ValidationResult(false, message, new NotSupportedException(message));
            }

            return null;
        }

        private IValidationResult ValidateLanguageCompatibility(Language source, Language target)
        {
            if (source == target)
            {
                var message = $"Source and target languages have the same value.";
                return new ValidationResult(false, message, new ArgumentException(message));
            }

            return null;
        }

    }
}