using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CatcherGame
{
    public class CatcherGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private SpriteFont fontSmall;
        private Platform player;
        private Rectangle playerRectangle;
        private FallingObject currentFallingObject;
        private FallingObject crate;

        private int score = 0;
        private int hiscore = 0;
        private bool miss = false;
        private bool isGameOver = false;

        public CatcherGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // TODO: fix this
            Window.Title = "Catcher";
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            fontSmall = Content.Load<SpriteFont>("File");

            player = new Platform(Content.Load<Texture2D>("Platform"), new PlayerInput(new List<Keys> { Keys.D, Keys.Right}, new List<Keys>() { Keys.A, Keys.Left }), graphics, 5);
            playerRectangle = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.Texture.Width, player.Texture.Height);

            crate = new FallingObject(Content.Load<Texture2D>("Crate"), graphics, 3);

            currentFallingObject = crate;

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (isGameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    player.ResetHealthPoints();
                    player.Position.X = 0f;
                    isGameOver = false;
                }
            }
            else
            {
                miss = false;

                player.Update(gameTime, graphics);
                currentFallingObject.Update(gameTime);

                if (currentFallingObject.IsColliding(player))
                {
                    currentFallingObject.ResetPosition(graphics);
                    score++;
                }

                if (currentFallingObject.Position.Y >= graphics.PreferredBackBufferHeight)
                {
                    miss = true;
                    currentFallingObject.ResetPosition(graphics);
                    player.HealthPoints--;
                }

                if (score > hiscore)
                {
                    hiscore = score;
                }

                if (player.HealthPoints <= 0)
                {
                    score = 0;
                    isGameOver = true;
                }
            }         



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (miss)
            {
                GraphicsDevice.Clear(Color.Red);
            }
            else
            {
                GraphicsDevice.Clear(Color.Gray);
            }

            spriteBatch.Begin();

            if (isGameOver)
            {
                spriteBatch.DrawString(fontSmall, "GAME OVER", new Vector2(graphics.PreferredBackBufferWidth / 2, 100f), Color.White);
                spriteBatch.DrawString(fontSmall, "Score: " + score, new Vector2(graphics.PreferredBackBufferWidth / 2, 130f), Color.White);
                spriteBatch.DrawString(fontSmall, "Hiscore: " + hiscore, new Vector2(graphics.PreferredBackBufferWidth / 2, 160f), Color.White);
            }
            else
            {
                player.Draw(spriteBatch);

                currentFallingObject.Draw(spriteBatch);

                spriteBatch.DrawString(fontSmall, "Score: " + score, new Vector2(10f, 50f), Color.White);
                spriteBatch.DrawString(fontSmall, "Hiscore: " + hiscore, new Vector2(10f, 80f), Color.White);
                spriteBatch.DrawString(fontSmall, "Lives: " + player.HealthPoints, new Vector2(10f, 110f), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}