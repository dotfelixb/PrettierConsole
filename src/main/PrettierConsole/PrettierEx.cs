using System;
using System.Collections;
using System.Collections.Generic;

namespace PrettierConsole {
    public static class PrettierEx {

        public static Table<T> ToPrettierTable<T>(this IEnumerable<T> list) {
            // get properties of list
            var t = new Table<T>(list);
            return t;
        }
    }
}