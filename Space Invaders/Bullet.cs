
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
        //public KeyboardState oldKeyState, keyState;

        public Bullet(Vector2 position, Vector2 velocity, Texture2D tex, Rectangle hitbox)
        {
            this.position = position;
            this.velocity = velocity;
            this.tex = tex;
            this.hitbox = hitbox;
        }

        public void Update(List<Bullet> bulletsToRemove)
        {
            position.Y = position.Y + velocity.Y;
            hitbox.Y = (int)position.Y;
            
            // Adds bullets for removal if they're outside the screen
            if (position.Y < 0 )
            {
                bulletsToRemove.Add(this);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, position, Color.White);
        }

       /* public bool Spawn()
        {
            oldKeyState = keyState;
            keyState = Keyboard.GetState();
            return keyState.IsKeyDown(Keys.Space) || oldKeyState.IsKeyUp(Keys.Space);
        }*/
    }
}
