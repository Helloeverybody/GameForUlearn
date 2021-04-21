using System;

namespace My_game_for_Ulearn.Model
{
    public class Vector
    {
        public float X;
        public float Y;

        public double Length => Math.Sqrt(X * X + Y * Y);
        public double Angle => Math.Acos(X/Length);
        
        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator +(Vector v1, Vector v2) => new Vector(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector operator *(Vector v1, int num) => new Vector(v1.X * num, v1.Y * num);

        public static Vector Zero => new Vector(0, 0);

        // public Vector GetDirectionVector()
        // {
        //     return new Vector(Math.Cos(Angle), Math.Sin(Angle));
        // }
    }
}