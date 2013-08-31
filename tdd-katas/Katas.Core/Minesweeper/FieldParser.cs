using System;
using System.Collections.Generic;
using System.Linq;

namespace Katas.Core.Minesweeper
{
    public class FieldParser
    {
        private const char Bomb = '*';
        private const char Space = '.';
        private static readonly Tuple<int, int> RowTerminator = Tuple.Create(0, 0);

        public IList<Field> Parse(string input)
        {
            string[] inputRows = input.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            int i = 0;
            var fields = new List<Field>();
            List<char[]> content = null;
            Tuple<int, int> header = null;
            var mode = ParseMode.None;

            do
            {
                string inputRow = inputRows[i];
                switch (mode)
                {
                    case ParseMode.None:
                        mode = ParseMode.Header;
                        break;
                    case ParseMode.Header:
                        if (content != null)
                        {
                            fields.Add(new Field(content.ToArray()));
                        }
                        content = new List<char[]>();
                        header = ParseHeader(inputRow);
                        if (header.Equals(RowTerminator))
                        {
                            mode = ParseMode.Finished;
                        }
                        else
                        {
                            mode = ParseMode.Body;
                            i++;
                        }
                        break;
                    case ParseMode.Body:
                        char[] body;
                        if (TryParseBody(inputRow, out body))
                        {
                            if (body.Length != header.Item2)
                            {
                                throw new ArgumentException();
                            }
                            content.Add(body);
                            i++;
                        }
                        else
                        {
                            mode = ParseMode.Header;
                        }
                        break;
                    case ParseMode.Finished:
                        break;
                }
            } while (mode != ParseMode.Finished);

            return fields.AsReadOnly();
        }

        private static Tuple<int, int> ParseHeader(string input)
        {
            string[] parsed = input.Split(' ');
            int rows, columns;
            return !Int32.TryParse(parsed[0], out rows) || !Int32.TryParse(parsed[1], out columns)
                ? null
                : Tuple.Create(rows, columns);
        }

        private static bool TryParseBody(string input, out char[] body)
        {
            char[] parsed = input.ToCharArray();
            if (!parsed.All(c => c == Bomb || c == Space))
            {
                body = null;
                return false;
            }
            body = parsed;
            return true;
        }

        private enum ParseMode
        {
            None,
            Header,
            Body,
            Finished
        }
    }
}