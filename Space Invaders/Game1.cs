﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Space_Invaders
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public KeyboardState oldKeyState, keyState;
        public static Vector2 screenDim;
        public Vector2 playerPos;
        public Vector2 playerVel = new Vector2(3, 0);
        public Rectangle playerHitbox;
        public Vector2 enemyPos;
        public Vector2 enemyVel = new Vector2(0, 1);
        public Rectangle enemyHitbox;
        public Vector2 bulletPos;
        public Vector2 bulletVel = new Vector2(0, -2);
        public Rectangle bulletHitbox;
        public Texture2D startButtonTex;
        public Texture2D enemyTex;
        public Texture2D playerTex;
        public Texture2D bulletTex;         
        public Player player;
        public Enemy enemy;
        public Bullet bullet;
        public List<Enemy> enemyList = new List<Enemy>();
        public List<Enemy> enemyKillList = new List<Enemy>();
        public List<Bullet> bulletList = new List<Bullet>();
        public List<Bullet> bulletKillList = new List<Bullet>();
        public int score = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startButtonTex = Content.Load<Texture2D>("Startknapp");
            enemyTex = Content.Load<Texture2D>("enemy_A1");
            playerTex = Content.Load<Texture2D>("player");
            bulletTex = Content.Load<Texture2D>("bullet");
           
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Width;
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Height;
            graphics.ApplyChanges();
          
            screenDim = new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height);


            // Gives the players starting position and hitbox
            playerPos = new Vector2(screenDim.X / 2 - playerTex.Width, screenDim.Y - playerTex.Height);
            playerHitbox = new Rectangle((int)playerPos.X, (int)playerPos.Y, playerTex.Width, playerTex.Height);
            player = new Player(playerPos, playerVel, playerTex, playerHitbox);

            // Adds all the enemies to a list
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 16; j++)
                {
                    enemyPos = new Vector2(j * 30, i * 25);
                    enemyHitbox = new Rectangle((int)enemyPos.X, (int)enemyPos.Y, enemyTex.Width, enemyTex.Height);
                    enemy = new Enemy(enemyPos, enemyVel, enemyTex, enemyHitbox);
                    enemyList.Add(enemy);
                }
            }

            
           
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            oldKeyState = keyState;
            keyState = Keyboard.GetState();


            player.Update(this);


            if (keyState.IsKeyDown(Keys.Space) && oldKeyState.IsKeyUp(Keys.Space))
            {
                bulletPos = new Vector2(player.position.X + playerTex.Width / 2, player.position.Y);
                bulletHitbox = new Rectangle((int)bulletPos.X, (int)bulletPos.Y, bulletTex.Width, bulletTex.Height);
                bullet = new Bullet(bulletPos, bulletVel, bulletTex, bulletHitbox);
                bulletList.Add(bullet);
            }
            

            foreach (Bullet bullet in bulletList)
            {
                bullet.Update(bulletKillList);
            }



            foreach (Enemy enemy in enemyList)
            {

                enemy.Update();

                if (enemy.hitbox.Intersects(player.hitbox) || enemy.hitbox.Y > screenDim.Y)
                {                    
                    player.lives--;                    
                }                                               
            }

            foreach (Enemy enemy in enemyList)
            {
                foreach (Bullet bullet in bulletList)
                {
                    if (enemy.hitbox.Intersects(bullet.hitbox))
                    {
                        enemyKillList.Add(enemy);
                        bulletKillList.Add(bullet);
                    }
                }
            }

            foreach (Enemy killed in enemyKillList)
            {
                enemyList.Remove(killed);
            }

            foreach (Bullet gone in bulletKillList)
            {
                bulletList.Remove(gone);
            }


            Window.Title = "Space Invaders        Score: " + score;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();

            player.Draw(spriteBatch);

            foreach (Bullet bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }
            

            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(spriteBatch);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
