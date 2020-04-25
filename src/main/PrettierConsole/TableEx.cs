using System;
using System.Collections.Generic;
using System.Linq;

namespace PrettierConsole {
    public static class TableEx {
        public static int[] ToColumnLength<T>(this Table<T> table) {
            // get table record 

            var r = table.Record.First();
            var p = r.GetType().GetProperties();
            var len = p.Length;
            var arr = new int[len];

            foreach (var elm in table.Record) {
                var props = elm.GetType().GetProperties();
                // get header len
                for (int i = 0; i < props.Length; i++) {
                    var nameLength = props[i].Name.Length + 2;
                    var ps = props[i].GetValue(elm).ToString().Length + 2;

                    arr[i] = nameLength > ps ? nameLength > arr[i] ? nameLength : arr[i] : ps > arr[i] ? ps : arr[i];
                }
            }

            return arr;
        }

        public static List<string[]> ToBody<T>(this Table<T> table) {
            var outer = new List<string[]>();
            var inner = new List<string>();

            foreach (var r in table.Record) {
                var props = r.GetType().GetProperties();
                foreach (var p in props) {
                    inner.Add(p.GetValue(r).ToString());
                }
                outer.Add(inner.ToArray());
                inner.Clear();
            }
            return outer;
        }

        public static string[] ToHeaderTitle<T>(this Table<T> table) {
            var r = table.Record.First();
            var titlesList = new List<string>();
            var props = r.GetType().GetProperties();
            foreach (var p in props) {
                titlesList.Add(p.Name);
            }
            return titlesList.ToArray();
        }

        static string ToCapitalizeFirstLetter(this string value) {
            if (string.IsNullOrEmpty(value)) {
                throw new ArgumentException(nameof(value));
            }

            var toArray = value.ToArray();
            var first = toArray.First().ToString().ToUpper();
            toArray[0] = char.Parse(first);

            return string.Join("", toArray);
        }

        static IEnumerable<T> LengthIterator<T>(this IEnumerable<T> source, Action<T> predicate) {
            foreach (var elm in source) {
                yield return elm;
            }
        }
    }
}