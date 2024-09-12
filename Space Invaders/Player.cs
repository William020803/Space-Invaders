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
        public Vector2 position;
        public Vector2 velocity;
        public Texture2D tex;
        public Rectangle hitbox;
        public int lives = 3;


        public Player(Vector2 position, Vector2 velocity, Texture2D tex, Rectangle hitbox)
        {
            this.position = position;
            this.velocity = velocity;
            this.tex = tex;
            this.hitbox = hitbox;
        }

        public void Update(Game1 game)
        {
            hitbox.Y = (int)position.Y;

            if (Keyboard.GetState().IsKeyDown(Keys.Left) && position.X > 0)
            {        
                // left
                position.X = position.X - velocity.X;
            }

            else if(Keyboard.GetState().IsKeyDown(Keys.Right) && position.X < Game1.screenDim.X - tex.Width)
            {       
                // right
                position.X = position.X + velocity.X;
            }

            if (lives <= 0)
            {
                game.Exit();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, position, Color.White);
        }
    }
}
