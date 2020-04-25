using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static System.Console;

namespace PrettierConsole {
    public class Table<T> {

        StringBuilder sb;
        const char DASH = '-';
        const char PIPE = '|';
        const char CROSS = '+';
        public Table(IEnumerable<T> record) {
            Record = record;
            sb = new StringBuilder();
        }

        internal IEnumerable<T> Record { get; }

        public static Table<T> CreateTable(IEnumerable<T> record) {
            // create a table
            var table = new Table<T>(record);

            return table;
        }

        public override string ToString() {
            var sw = new Stopwatch();
            sw.Start();
            // get length
            var lengths = this.ToColumnLength();

            // make header
            var titles = this.ToHeaderTitle();
            var headers = MakeHeader(titles, lengths);
            sb.Append(headers);

            // make body
            var bodyList = this.ToBody();
            var body = MakeBody(bodyList, lengths);
            sb.Append(body);
            // make footer

            sb.AppendLine($"\n({Math.Round(sw.Elapsed.TotalSeconds, 2)} sec)");
            // return table
            return sb.ToString();
        }

        string MakeBody(List<string[]> bodyList, int[] lengths) {
            var sb = new StringBuilder();
            var separator = Separator(bodyList.First().Length, lengths);
            foreach (var bodyElm in bodyList) {
                sb.Append(PIPE);

                for (int i = 0; i < bodyElm.Length; i++) {
                    sb.Append(MakeCell(bodyElm[i], lengths[i]));
                    sb.Append(PIPE);
                }
                sb.Append("\n");
                sb.AppendLine(separator);
            }

            return sb.ToString();
        }

        string MakeHeader(string[] titles, int[] lengths) {
            var sb = new StringBuilder();
            var separator = Separator(titles.Length, lengths);
            sb.AppendLine(separator);
            sb.Append(PIPE);
            for (var i = 0; i < titles.Length; i++) {
                sb.Append(MakeCell(titles[i], lengths[i]));
                sb.Append(PIPE);
            }
            sb.Append("\n");
            sb.AppendLine(separator);
            return sb.ToString();
        }

        string MakeCell(string title, int length) => $" {title} ".PadRight(length, ' ');

        string Separator(int separatorCount, int[] padLengths) {
            //  +-----------+-----------+-----------+
            var sb = new StringBuilder();

            for (int i = 0; i < separatorCount; i++) {
                sb.Append($"+{string.Empty.PadRight(padLengths[i], DASH)}");
            }
            sb.Append(CROSS);

            return sb.ToString();
        }
    }

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
                    var nameLength = props[i].Name.Length;
                    var ps = props[i].GetValue(elm).ToString().Length;

                    arr[i] = nameLength > ps ? nameLength > arr[i] ? (nameLength + 2) : arr[i] : ps > arr[i] ? (ps + 2) : arr[i];
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