using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PrettierConsole {
    public class Table<T> {
        /*   +-----------+-----------+-----------+
         **  | Col 1     | Col 2     | Col 3     |
         **  +-----------+-----------+-----------+
         **  | Rec 1     | Rec 2     | Rec       |
         **  +-----------+-----------+-----------+
         */

        // Feature 1: Head
        // Feature 2: Body
        // Feature 3: Length of each column

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
            // get length
            var length = this.ToTableLength() + 2;

            // make header
            var titles = this.ToHeaderTitle();
            var headers = MakeHeader(titles, length);
            sb.Append(headers);

            // make body
            var bodyList = this.ToBody();
            var body = MakeBody(bodyList, length);
            sb.Append(body);
            // make footer

            // return table
            return sb.ToString();
        }

        string MakeBody(List<string[]> bodyList, int length) {
            var sb = new StringBuilder();
            var separator = Separator(bodyList.First().Length, length);
            foreach (var bodyElm in bodyList) {
                sb.Append(PIPE);
                foreach (var body in bodyElm) {
                    sb.Append(MakeCell(body, length));
                    sb.Append(PIPE);
                }
                sb.Append("\n");
                sb.AppendLine(separator);
            }

            return sb.ToString();
        }

        string MakeHeader(string[] titles, int length) {
            var sb = new StringBuilder();
            var separator = Separator(titles.Length, length);
            sb.AppendLine(separator);
            sb.Append(PIPE);
            foreach (var title in titles) {
                sb.Append(MakeCell(title, length));
                sb.Append(PIPE);
            }
            sb.Append("\n");
            sb.AppendLine(separator);
            return sb.ToString();
        }

        string MakeCell(string title, int length) {
            return title.PadRight(length, ' ');
        }

        string Separator(int separatorCount, int padLength) {
            //  +-----------+-----------+-----------+
            var sb = new StringBuilder();

            do {
                sb.Append($"+{string.Empty.PadRight(padLength, DASH)}");
                separatorCount -= 1;
            } while (separatorCount > 0);
            sb.Append(CROSS);

            return sb.ToString();
        }
    }

    public static class TableEx {
        public static int ToTableLength<T>(this Table<T> table) {
            // get table record 
            var len = 0;
            foreach (var r in table.Record) {
                foreach (var p in r.GetType().GetProperties()) {
                    var propertyLength = p.GetValue(r).ToString().Length;

                    if (propertyLength > len) {
                        len = propertyLength;
                    }
                }
            }
            return len;
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
            var rl = new List<string>();
            var props = r.GetType().GetProperties();
            foreach (var p in props) {
                rl.Add(p.Name);
            }

            return rl.ToArray();
        }

        static IEnumerable<T> LengthIterator<T>(this IEnumerable<T> source, Action<T> predicate) {
            foreach (var elm in source) {
                yield return elm;
            }
        }
    }
}