using System.Collections.Generic;

namespace PrettierConsole {
    public static class PrettierEx {

        public static Table<T> ToPrettierTable<T>(this IEnumerable<T> record, bool separator = false) => new Table<T>(record, separator);

        public static Numbered<T> ToPrettierList<T>(this IEnumerable<T> record, bool bulleted = false) => new Numbered<T>(record, bulleted);
    }
}