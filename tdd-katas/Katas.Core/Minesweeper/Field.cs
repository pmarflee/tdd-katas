using System.Linq;

namespace Katas.Core.Minesweeper
{
    public class Field
    {
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
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Field) obj);
        }

        public override int GetHashCode()
        {
            return Content.GetHashCode();
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