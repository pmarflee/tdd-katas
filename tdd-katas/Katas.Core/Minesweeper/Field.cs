using System.Linq;

namespace Katas.Core.Minesweeper
{
    public class Field
    {
        private static readonly int[,] Offsets =
        {
            {-1, -1},
            {0, -1},
            {1, -1},
            {1, 0},
            {1, 1},
            {0, 1},
            {-1, 1},
            {-1, 0}
        };

        protected bool Equals(Field other)
        {
            if (Content.Length != other.Content.Length)
            {
                return false;
            }

            return Content
                .Zip(other.Content, (mine, theirs) => new {mine, theirs})
                .All(pair => pair.mine.SequenceEqual(pair.theirs));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Field) obj);
        }

        public override int GetHashCode()
        {
            return Content.GetHashCode();
        }

        public int CountNumberOfAdjacentBombs(int x, int y)
        {
            var count = 0;

            for (var i = 0; i < Offsets.GetLength(0); i++)
            {
                var x1 = x + Offsets[i, 0];
                var y1 = y + Offsets[i, 1];
                if (x1 >= 0 && x1 < Columns && y1 >= 0 && y1 < Rows && Content[y1][x1] == '*')
                {
                    count++;
                }
            }

            return count;
        }

        public int Rows { get { return Content.Length; }}
        public int Columns { get { return Content[0].Length; }}

        public readonly char[][] Content;

        internal Field(char[][] content)
        {
            Content = content;
        }
    }
}