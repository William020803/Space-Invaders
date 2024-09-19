using Microsoft.Xna.Framework;
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
        private KeyboardState oldKeyState, keyState;
        public static Vector2 screenDim;        
        private Vector2 playerVel = new Vector2(3, 0);       
        private Vector2 enemyVel = new Vector2(1, 5);                
        private Vector2 bulletVel = new Vector2(0, -2);        
        private Texture2D startButtonTex;
        private Texture2D enemyTex;
        private Texture2D playerTex;
        private Texture2D bulletTex; 
        private SpriteFont pointText;
        public Player player;
        public Enemy enemy;
        public Bullet bullet;
        public ScoreManager scoreManager;
        Enemy[,] enemies = new Enemy[5, 12];                
        public List<Bullet> bulletList = new List<Bullet>();        
        private int bulletCooldownMax = 20;
        private int bulletCooldown;
        private bool bulletCooldownActive = false;

        enum GameState
        {
            Start,
            Play,
            End
        }

        GameState currentGameState;
        

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
            pointText = Content.Load<SpriteFont>("points");
           
            // Changes the size of the game window
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Width;
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Height;
            graphics.ApplyChanges();
          
            screenDim = new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height);


            // Gives the players starting position and hitbox
            Vector2 playerPos = new Vector2(screenDim.X / 2 - playerTex.Width, screenDim.Y - playerTex.Height);
            Rectangle playerHitbox = new Rectangle((int)playerPos.X, (int)playerPos.Y, playerTex.Width, playerTex.Height);
            player = new Player(playerPos, playerVel, playerTex, playerHitbox);

            // Adds all the enemies to a list
            for(int i = 0; i < enemies.GetLength(0); i++)
            {
                for(int j = 0; j < enemies.GetLength(1); j++)
                {
                    Vector2 enemyPos = new Vector2(20 + j * 30, i * 25);
                    Rectangle enemyHitbox = new Rectangle((int)enemyPos.X, (int)enemyPos.Y, enemyTex.Width, enemyTex.Height);
                    enemy = new Enemy(enemyPos, enemyVel, enemyTex, enemyHitbox);
                    enemies[i, j] = enemy;
                }
            }

            scoreManager = new ScoreManager();

            bulletCooldown = bulletCooldownMax;

            currentGameState = GameState.Start;
           
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                //Exit();

            switch (currentGameState)
            {
                case GameState.Start:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        currentGameState = GameState.Play;
                    }
                    break;

                case GameState.Play:

                    oldKeyState = keyState;
                    keyState = Keyboard.GetState();

                    if (bulletCooldownActive)
                    {
                        bulletCooldown--;

                        if (bulletCooldown <= 0)
                        {
                            bulletCooldown = bulletCooldownMax;
                            bulletCooldownActive = false;
                        }
                    }


                    // Spawns bullets if the conditions are met
                    if (keyState.IsKeyDown(Keys.Space) && oldKeyState.IsKeyUp(Keys.Space) && !bulletCooldownActive)
                    {
                        Vector2 bulletPos = new Vector2(player.position.X + playerTex.Width / 2, player.position.Y);
                        Rectangle bulletHitbox = new Rectangle((int)bulletPos.X, (int)bulletPos.Y, bulletTex.Width, bulletTex.Height);
                        bullet = new Bullet(bulletPos, bulletVel, bulletTex, bulletHitbox);
                        bulletList.Add(bullet);

                        bulletCooldownActive = true;
                    }


                    foreach (Bullet bullet in bulletList)
                    {
                        bullet.Update();
                    }



                    foreach (Enemy enemy in enemies)
                    {
                        enemy.Update(enemies);
                    }

                    player.Update(enemies);

                    /*if (player.lives <= 0)
                    {
                        currentGameState = GameState.End;
                    }*/

                    // Checks for collisions between bullets and enemies
                    foreach (Enemy enemy in enemies)
                    {
                        foreach (Bullet bullet in bulletList)
                        {
                            if (enemy.hitbox.Intersects(bullet.hitbox))
                            {
                                scoreManager.Update();
                                enemy.active = false;
                                bullet.active = false;
                            }
                        }
                    }


                    // Displays game title and current score
                    Window.Title = "Space Invaders                   lives: " + player.lives;

                    break;

                case GameState.End:

                break;
            }           
           
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (currentGameState)
            {
                case GameState.Start:

                    spriteBatch.Draw(startButtonTex, new Vector2((screenDim.X - startButtonTex.Width) / 2, screenDim.Y / 2), Color.White);
                    break;

                case GameState.Play:

                    player.Draw(spriteBatch);

                    foreach (Bullet bullet in bulletList)
                    {
                        bullet.Draw(spriteBatch);
                    }


                    foreach (Enemy enemy in enemies)
                    {
                        enemy.Draw(spriteBatch);
                    }

                    spriteBatch.DrawString(pointText, "Points: " + scoreManager.score, new Vector2(100, 100), Color.White);

                    break;




                case GameState.End:
                    break;
            }
                       
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
