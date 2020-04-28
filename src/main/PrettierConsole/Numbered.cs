using System.Collections.Generic;
using System.Text;

namespace PrettierConsole {
    public class Numbered<T> {
        public Numbered(IEnumerable<T> record, bool bulleted) {
            Record = record;
            Bulleted = bulleted;
        }

        internal IEnumerable<T> Record { get; }
        internal bool Bulleted { get; }

        public override string ToString() {
            return MakeList().ToString();
        }

        StringBuilder MakeList() {
            var sb = new StringBuilder();
            var count = 1;
            foreach (var item in Record) {
                sb.Append(Bulleted ? $"* {item}" : $"{count}. {item}");
                sb.AppendLine();
                count++;
            }
            return sb;
        }
    }

}