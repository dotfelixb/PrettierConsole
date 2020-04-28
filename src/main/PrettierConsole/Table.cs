using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PrettierConsole {
    public class Table<T> {

        StringBuilder sb;
        const char DASH = '-';
        const char PIPE = '¦';
        const char CROSS = '+';
        public Table(IEnumerable<T> record, bool separator) {
            Record = record ??
                throw new ArgumentNullException(nameof(record));
            sb = new StringBuilder();
            Separator = separator;
        }

        internal IEnumerable<T> Record { get; }
        public bool Separator { get; set; }

        public static Table<T> CreateTable(IEnumerable<T> record) {
            // create a table
            var table = new Table<T>(record, false);

            return table;
        }

        StringBuilder MakeBody(IEnumerable<IEnumerable<string>> bodyList, int[] lengths, bool separator) {
            var sb = new StringBuilder(capacity: 1024);
            var sep = MakeSeparator(bodyList.First().Count(), lengths);
            foreach (var bodyElm in bodyList) {
                sb.Append(PIPE);
                var bodyCount = bodyElm.Count();

                for (int i = 0; i < bodyCount; i++) {
                    sb.Append(MakeCell(bodyElm.ElementAt(i), lengths[i]));
                    sb.Append(PIPE);
                }
                sb.AppendLine();
                if (separator) { sb.Append(sep); }
            }
            if (!separator) { sb.Append(sep); }
            return sb;
        }

        StringBuilder MakeHeader(IEnumerable<string> titles, int[] lengths) {
            var sb = new StringBuilder(capacity: 256);
            var titlesCount = titles.Count();
            var sep = MakeSeparator(titlesCount, lengths);
            sb.Append(sep);
            sb.Append(PIPE);
            for (var i = 0; i < titlesCount; i++) {
                sb.Append(MakeCell(titles.ElementAt(i), lengths[i]));
                sb.Append(PIPE);
            }
            sb.AppendLine();
            sb.Append(sep);

            return sb;
        }

        string MakeCell(string title, int length) => $" {title} ".PadRight(length, ' ');

        StringBuilder MakeSeparator(int separatorCount, int[] padLengths) {
            //  +-----------+-----------+-----------+
            var sb = new StringBuilder(capacity: 128);

            for (int i = 0; i < separatorCount; i++) {
                sb.Append($"{CROSS}{string.Empty.PadRight(padLengths[i], DASH)}");
            }
            sb.Append(CROSS);
            sb.AppendLine();

            return sb;
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
            var body = MakeBody(bodyList, lengths, Separator);
            sb.Append(body);
            // make footer

            sb.AppendLine($"\n({Math.Round(sw.Elapsed.TotalSeconds, 4)} sec)");
            // return table
            return sb.ToString();
        }
    }

}