using System.Collections.Generic;
using System.Linq;

namespace Courses.Domain.Constants
{
    public static class SupportedLanguages
    {
        public const string Russian = "Russian";
        public const string Belarusian = "Belarusian";
        public const string English = "English";
        public const string Deutsch = "Deutsch";
        public const string Spanish = "Spanish";
        public const string French = "French";

        public static IEnumerable<string> GetAll()
        {
            var type = typeof(SupportedLanguages);

            var fields = type
                .GetFields()
                .Concat(type.BaseType.GetFields())
                .Select(x => (string)x.GetValue(null));

            return new List<string>(fields);
        }
    }
}