using System.Collections.Generic;

namespace PrettierConsole {
    public static class PrettierEx {

        public static Table<T> ToPrettierTable<T>(this IEnumerable<T> list, bool separator = false) {
            // get properties of list
            var t = new Table<T>(list, separator);
            return t;
        }
    }
}