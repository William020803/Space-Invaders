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
    public class Enemy
    {
        private Vector2 position;
        private Vector2 velocity;
        private Texture2D tex;
        public Rectangle hitbox;
        public bool active = true;
        private bool moveDown = false;
        private int direction = 1;
        

        public Enemy(Vector2 position, Vector2 velocity, Texture2D tex, Rectangle hitbox)
        {
            this.position = position;
            this.velocity = velocity;
            this.tex = tex;
            this.hitbox = hitbox;
        }

        public void Update(Enemy[,] enemies)
        {
            if (active)
            {
                if (!moveDown)
                {


                    position.X = position.X + velocity.X * direction;
                    hitbox.X = (int)position.X;
                    

                    if (position.X <= 0 || position.X >= Game1.screenDim.X - tex.Width)
                    {
                        moveDown = true;                        
                    }
                }

                if (moveDown)
                {
                    for (int i = 0; i < enemies.GetLength(0); i++)
                    {
                        for (int j = 0; j < enemies.GetLength(1); j++)
                        {
                            enemies[i, j].position.Y = enemies[i, j].position.Y + velocity.Y;
                            enemies[i, j].hitbox.Y = (int)enemies[i, j].position.Y;
                            enemies[i, j].direction = enemies[i, j].direction * -1;
                        }
                    }
                   
                    moveDown = false;
                    
                }   
                

                
                if (position.Y + tex.Height >= Game1.screenDim.Y)
                {
                    active = false;
                }
            }

            else
            {
                hitbox = Rectangle.Empty;
            }
            
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(tex, position, Color.White);
            }
            
        }
    }
}
