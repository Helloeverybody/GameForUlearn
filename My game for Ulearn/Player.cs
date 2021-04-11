using System.Drawing;

namespace My_game_for_Ulearn
{
    public class Player
    {
        public Player(float x, float y)
        {
            X = x;
            Y = y;
            playerSprite = Image.FromFile(@"C:\Users\Vsevolod\RiderProjects\MygameforUlearn\My game for Ulearn\Assets\Sprite-0001.png");
        }
        public float X = 0;
        public float Y = 0;
        public Image playerSprite;
    }
}