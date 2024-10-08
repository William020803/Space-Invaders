﻿using Microsoft.Xna.Framework;
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
        private Vector2 velocity;
        private Texture2D tex;
        public Rectangle hitbox;
        public int lives = 3;
        private bool iFramesActive = false;        
        private int iFramesMax = 90;
        private int iFrames = 90;


        public Player(Vector2 position, Vector2 velocity, Texture2D tex, Rectangle hitbox)
        {
            this.position = position;
            this.velocity = velocity;
            this.tex = tex;
            this.hitbox = hitbox;
        }

        public void Update(Enemy[,] enemies)
        {
            if (iFramesActive)
            {
                iFrames--;

                if (iFrames <= 0)
                {
                    iFrames = iFramesMax;
                    iFramesActive = false;
                }
            }

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

            hitbox.Y = (int)position.Y;

            foreach (Enemy enemy in enemies)
            {
                if (hitbox.Intersects(enemy.hitbox) && !iFramesActive)
                {
                    lives--;
                    iFramesActive = true;
                }

                else if (enemy.hitbox.Y > Game1.screenDim.Y)
                {
                    lives--;
                } 
            }                        
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, position, Color.White);
        }
    }
}
