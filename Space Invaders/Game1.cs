using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace Space_Invaders
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public Texture2D startButtonTex;
        public Vector2 pos;
        public Vector2 velocity;
        public Vector2 pos1;
        public Vector2 velocity1;
        public int windowWidth;
        public Player player;
        public List<Player> playerList = new List<Player>();

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
            windowWidth = Window.ClientBounds.Width;


            for (int i = 0; i < 6; i++)
            {
                Vector2 pos = new Vector2(0, i * 80);
                Vector2 vel = new Vector2(4, 0);
                player = new Player(startButtonTex, pos, vel, windowWidth);
                playerList.Add(player);
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();           

            foreach (Player player in playerList)
            {
                player.Update();
            }
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();

            
            foreach (Player player in playerList)
            {
                player.Draw(spriteBatch);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
