using System.Drawing;

namespace My_game_for_Ulearn.Model
{
    public class Player
    {
        public float X;
        public float Y;
        public Image playerSprite;
        private const float maxSpeed = 10;
        public Vector move = Vector.Zero;
        public double speed;

        public bool WalkForward = false;
        public bool WalkBackward = false;
        public bool WalkRight = false;
        public bool WalkLeft = false;
        
        public Player(float x, float y)
        {
            X = x;
            Y = y;
            playerSprite = Image.FromFile(@"C:\Users\Vsevolod\RiderProjects\MygameforUlearn\My game for Ulearn\Assets\Sprite-0001.png");
        }
        
        public void Translate()
        {
            if (WalkForward)
                move += new Vector(0, -1);
            if (WalkBackward)
                move += new Vector(0, 1);
            if (WalkRight)
                move += new Vector(1, 0);
            if (WalkLeft)
                move += new Vector(-1, 0);

            X = maxSpeed * move.X;
            Y = maxSpeed * move.Y;
        }
    }
}