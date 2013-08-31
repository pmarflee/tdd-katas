using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Katas.Core.Minesweeper;
using Xunit;
using Xunit.Extensions;

namespace Katas.Tests.Minesweeper
{
    public class FieldParserTests
    {
        private readonly FieldParser _parser;

        public FieldParserTests()
        {
            _parser = new FieldParser();
        }

        [Theory, ClassData(typeof (FieldData))]
        public void ValidInputTests(string input, IList<Field> expected)
        {
            Assert.Equal(expected, _parser.Parse(input));
        }

        private class FieldData : IEnumerable<object[]>
        {
            private static readonly string[] Single4By4Input =
            {
                "*...",
                "....",
                ".*..",
                "...."
            };

            private static readonly string[] Single3By5Input =
            {
                "**...",
                ".....",
                ".*..."
            };

            private readonly List<object[]> _data = new List<object[]>
            {
                new object[]
                {
                    new InputBuilder().Add(Single4By4Input).Build(),
                    new ReadOnlyCollection<Field>(new[] {Single4By4Input.ToField()})
                },
                new object[]
                {
                    new InputBuilder().Add(Single3By5Input).Build(),
                    new ReadOnlyCollection<Field>(new[] {Single3By5Input.ToField()})
                },
                new object[]
                {
                    new InputBuilder().Add(Single3By5Input).Add(Single4By4Input).Build(),
                    new ReadOnlyCollection<Field>(new[] {Single3By5Input.ToField(), Single4By4Input.ToField()})
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
    }

    public static class TestHelper
    {
        public static string CreateInput(this string[] rows)
        {
            return String.Join(Environment.NewLine, rows);
        }

        public static Field ToField(this string[] input)
        {
            return new Field(input.Select(line => line.ToCharArray()).ToArray());
        }
    }

    internal class InputBuilder
    {
        private readonly StringBuilder _builder;

        public InputBuilder()
        {
            _builder = new StringBuilder();
        }

        public InputBuilder Add(string[] input)
        {
            _builder.AppendLine(String.Concat(input.Length, " ", input[0].Length));

            foreach (string line in input)
            {
                _builder.AppendLine(line);
            }

            return this;
        }

        public string Build()
        {
            _builder.Append("0 0");

            return _builder.ToString();
        }
    }
}