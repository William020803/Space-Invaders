
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
    public class Bullet
    {
        public Vector2 position;
        public Vector2 velocity;
        public Texture2D tex;
        public Rectangle hitbox;

        public Bullet(Vector2 position, Vector2 velocity, Texture2D tex, Rectangle hitbox)
        {
            this.position = position;
            this.velocity = velocity;
            this.tex = tex;
            this.hitbox = hitbox;
        }

        public void Update()
        {
            position.Y = position.Y - velocity.Y;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {

            }
        }

        public void Draw()
        {

        }
    }
}
