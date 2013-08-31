using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Katas.Core.Minesweeper
{
    public class FieldPrinter
    {
        public string Print(IEnumerable<Field> fields)
        {
            var builder = new FieldPrintOutputBuilder();
            var fieldList = fields as List<Field> ?? new List<Field>(fields);
            var i = 0;

            foreach (var field in fieldList)
            {
                builder.AddHeader();
                i++;
                AddFieldToPrintOutput(field, builder);
                if (i < fieldList.Count)
                {
                    builder.AddLineTerminator();
                }
            }

            return builder.Build();
        }

        private static void AddFieldToPrintOutput(Field field, FieldPrintOutputBuilder builder)
        {
            for (var i = 0; i < field.Rows; i++)
            {
                for (var j = 0; j < field.Columns; j++)
                {
                    if (field.Content[i][j] == '*')
                    {
                        builder.AddCharacter('*');
                    }
                    else
                    {
                        var number = field.CountNumberOfAdjacentBombs(j, i);
                        builder.AddCharacter(number.ToString(CultureInfo.InvariantCulture)[0]);
                    }
                }
                builder.AddLineTerminator();
            }
        }

        private class FieldPrintOutputBuilder
        {
            private readonly StringBuilder _builder = new StringBuilder();
            private int _i;

            public void AddHeader()
            {
                _builder.AppendLine(String.Concat("Field #", ++_i));
            }

            public void AddCharacter(char character)
            {
                _builder.Append(character);
            }

            public void AddLineTerminator()
            {
                _builder.AppendLine();
            }

            public string Build()
            {
                return _builder.ToString();
            }
        }
    }
}