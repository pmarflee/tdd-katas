using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Katas.Core.Minesweeper;
using Xunit;
using Xunit.Extensions;

namespace Katas.Tests.Minesweeper
{
    public class FieldPrinterTests
    {
        private readonly FieldPrinter _printer;

        public FieldPrinterTests()
        {
            _printer = new FieldPrinter();
        }

        [Theory, ClassData(typeof (FieldData))]
        public void PrintTests(IEnumerable<Field> fields, string expected)
        {
            Assert.Equal(expected, _printer.Print(fields));
        }

        private class FieldData : IEnumerable<object[]>
        {
            private static readonly Field Single4By4Field = new Field(new[]
            {
                "*...".ToCharArray(),
                "....".ToCharArray(),
                ".*..".ToCharArray(),
                "....".ToCharArray()
            });

            private static readonly Field Single3By5Field = new Field(new[]
            {
                "**...".ToCharArray(),
                ".....".ToCharArray(),
                ".*...".ToCharArray()
            });

            private readonly List<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    new[] {Single4By4Field},
                    new OutputBuilder()
                        .AddHeader()
                        .AddLine("*100")
                        .AddLine("2210")
                        .AddLine("1*10")
                        .AddLine("1110")
                        .Build()
                },
                new object[]
                {
                    new[] {Single3By5Field},
                    new OutputBuilder()
                        .AddHeader()
                        .AddLine("**100")
                        .AddLine("33200")
                        .AddLine("1*100")
                        .Build()
                },
                new object[]
                {
                    new[] {Single4By4Field, Single3By5Field},
                    new OutputBuilder()
                        .AddHeader()
                        .AddLine("*100")
                        .AddLine("2210")
                        .AddLine("1*10")
                        .AddLine("1110")
                        .AddSeparator()
                        .AddHeader()
                        .AddLine("**100")
                        .AddLine("33200")
                        .AddLine("1*100")
                        .Build()
                }
            };

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                return _data.GetEnumerator();
            }
        }

        private class OutputBuilder
        {
            private readonly StringBuilder _builder;
            private int _i;

            public OutputBuilder()
            {
                _builder = new StringBuilder();
            }

            public OutputBuilder AddHeader()
            {
                _builder.AppendLine(String.Concat("Field #", ++_i));

                return this;
            }

            public OutputBuilder AddLine(string line)
            {
                _builder.AppendLine(line);

                return this;
            }

            public OutputBuilder AddSeparator()
            {
                _builder.AppendLine();

                return this;
            }

            public string Build()
            {
                return _builder.ToString();
            }
        }
    }
}