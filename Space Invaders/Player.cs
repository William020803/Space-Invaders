using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    public class Player
    {
        public Texture2D tex;
        public Vector2 pos;
        public Vector2 velocity;
        public int windowWidth;

        public Player(Texture2D tex, Vector2 pos, Vector2 velocity, int windowWidth) 
        {
            this.tex = tex;
            this.pos = pos;
            this.velocity = velocity;
            this.windowWidth = windowWidth;
       
        }

        public void Update()
        {
            if (pos.X < 0 || pos.X > windowWidth - tex.Width)
            {
                velocity = velocity * -1;
            }

            pos = pos + velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}
