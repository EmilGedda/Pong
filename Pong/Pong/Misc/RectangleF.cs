#region

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using C3.XNA;
using Microsoft.Xna.Framework;

#endregion

namespace Pong.Misc
{
    public struct RectangleF : IEquatable<RectangleF>
    {
        // ReSharper disable CompareOfFloatsByEqualityOperator
        // ReSharper disable UnusedMember.Global

        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float Height;
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float Width;
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float X;
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        public float Y;

        static RectangleF()
        {
            Empty = new RectangleF();
        }

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float Left
        {
            get { return X; }
        }

        public float Right
        {
            get { return X + Width; }
        }

        public float Top
        {
            get { return Y; }
        }

        public float Bottom
        {
            get { return Y + Height; }
        }

        public Point Location
        {
            get { return new Point((int)X, (int)Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Rectangle RoundedRectangle
        {
            get
            {
                return new Rectangle(
                    X.Round(),
                    Y.Round(),
                    Width.Round(),
                    Height.Round()
                    );
            }
        }
        public Point Center
        {
            get { return new Point((int)(X + Width / 2), (int)(Y + Height / 2)); }
        }

        public static RectangleF Empty { get; private set; }

        public bool IsEmpty
        {
            get
            {

                if (Width == 0 && Height == 0 && X == 0)
                    return Y == 0;
                return false;
            }
        }

        public bool Equals(RectangleF other)
        {
            if (X == other.X && Y == other.Y && Width == other.Width)
                return Height == other.Height;
            return false;
        }

        public static bool operator ==(RectangleF a, RectangleF b)
        {
            if (a.X == b.X && a.Y == b.Y && a.Width == b.Width)
                return a.Height == b.Height;
            return false;
        }

        public static bool operator !=(RectangleF a, RectangleF b)
        {
            if (a.X == b.X && a.Y == b.Y && a.Width == b.Width)
                return a.Height != b.Height;
            return true;
        }

        public void Offset(Point amount)
        {
            X += amount.X;
            Y += amount.Y;
        }

        public void Offset(int offsetX, int offsetY)
        {
            X += offsetX;
            Y += offsetY;
        }

        public void Inflate(int horizontalAmount, int verticalAmount)
        {
            X -= horizontalAmount;
            Y -= verticalAmount;
            Width += horizontalAmount * 2;
            Height += verticalAmount * 2;
        }

        public bool Contains(int x, int y)
        {
            if (X <= x && x < X + Width && Y <= y)
                return y < Y + Height;
            return false;
        }

        public bool Contains(Point value)
        {
            if (X <= value.X && value.X < X + Width && Y <= value.Y)
                return value.Y < Y + Height;
            return false;
        }

        public void Contains(ref Point value, out bool result)
        {
            result = X <= value.X && value.X < X + Width && Y <= value.Y && value.Y < Y + Height;
        }

        public bool Contains(RectangleF value)
        {
            if (X <= value.X && value.X + value.Width <= X + Width && Y <= value.Y)
                return value.Y + value.Height <= Y + Height;
            return false;
        }

        public void Contains(ref RectangleF value, out bool result)
        {
            result = X <= value.X && value.X + value.Width <= X + Width && Y <= value.Y &&
                     value.Y + value.Height <= Y + Height;
        }

        public bool Intersects(RectangleF value)
        {
            if (value.X < X + Width && X < value.X + value.Width && value.Y < Y + Height)
                return Y < value.Y + value.Height;
            return false;
        }

        public void Intersects(ref RectangleF value, out bool result)
        {
            result = value.X < X + Width && X < value.X + value.Width && value.Y < Y + Height &&
                     Y < value.Y + value.Height;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is RectangleF)
                flag = Equals((RectangleF)obj);
            return flag;
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format(currentCulture, "{{X:{0} Y:{1} Width:{2} Height:{3}}}",
                (object)X.ToString(currentCulture), (object)Y.ToString(currentCulture),
                (object)Width.ToString(currentCulture), (object)Height.ToString(currentCulture));
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Width.GetHashCode() + Height.GetHashCode();
        }
    }
}