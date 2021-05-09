using System;

namespace Model
{
    public class Vector
    {
        public int X;
        public int Y;

        public double Length => Math.Sqrt(X * X + Y * Y);
        public double Angle => Math.Acos(X/Length);
        
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector(double length, double angle)
        {
            X = (int)Math.Ceiling(length * Math.Cos(angle));
            Y = (int)Math.Ceiling(length * Math.Sin(angle));
        }
        
        public static Vector operator +(Vector v1, Vector v2) => new Vector(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector operator *(Vector v1, int num) => new Vector(v1.X * num, v1.Y * num);

        public static Vector Zero => new Vector(0, 0);

        public Vector GetDirectionVector()
        {
            return double.IsNaN(Angle) ? Zero : new Vector((int)(Math.Cos(Angle)), (int)(Math.Sin(Angle)));
        }
    }
}