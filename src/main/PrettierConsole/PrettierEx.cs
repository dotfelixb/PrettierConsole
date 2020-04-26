using System.Collections.Generic;

namespace PrettierConsole {
    public static class PrettierEx {

        public static Table<T> ToPrettierTable<T>(this IEnumerable<T> list, bool separator = false) => new Table<T>(list, separator);
            
    }
}
