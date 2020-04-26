using System;
using System.Collections.Generic;
using System.Linq;

namespace PrettierConsole {
    public static class TableEx {
        public static int[] ToColumnLength<T>(this Table<T> table) {
            // for array size just check the first rows for column
            var arr = new int[typeof(T).GetProperties().Length];
            var props = typeof(T).GetProperties();

            foreach (var elm in table.Record) {
                for (int i = 0; i < props.Length; i++) {
                    // for the property itself ie column header
                    var headerLen = props[i].Name.Length + 2;

                    // for property value ie cell
                    var cellLen = props[i].GetValue(elm).ToString().Length + 2;

                    arr[i] = headerLen > cellLen ?
                        (headerLen > arr[i] ? headerLen : arr[i]) :
                        (cellLen > arr[i] ? cellLen : arr[i]);
                }
            }

            return arr;
        }

        public static IEnumerable<IEnumerable<string>> ToBody<T>(this Table<T> table) {
            var inner = new List<string>();

            foreach (var r in table.Record) {
                var props = r.GetType().GetProperties();
                foreach (var p in props) {
                    inner.Add(p.GetValue(r).ToString());
                }

                yield return inner;
                inner.Clear();
            }
        }

        public static IEnumerable<string> ToHeaderTitle<T>(this Table<T> table) {
            var r = table.Record.First();
            var props = r.GetType().GetProperties();
            foreach (var p in props) {
                yield return p.Name; // .ToCapitalizeFirstLetter();
            }
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
    }
}