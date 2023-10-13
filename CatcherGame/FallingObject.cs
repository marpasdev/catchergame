using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using MarpasEngine.Objects2D;

namespace CatcherGame
{
    public class FallingObject : Sprite
    {
        private Random random = new Random();
        private float speed = 100f;
        public int Bonus = 1;

        public FallingObject(Texture2D texture, GraphicsDeviceManager graphics) : base(texture)
        {
            ResetPosition(graphics);
        }

        public FallingObject(Texture2D texture, GraphicsDeviceManager graphics, int bonus) : base(texture)
        {
            ResetPosition(graphics);
            Bonus = bonus;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position.Y += speed * deltaTime;
        }

        public void ResetPosition(GraphicsDeviceManager graphics)
        {
            Position.X = random.Next(0, graphics.PreferredBackBufferWidth - Texture.Width + 1);
            Position.Y = -Texture.Height - 10f;
        }

        // TODO: move to Sprite class?
        public bool IsColliding(Sprite other)
        {
            if (Rectangle.Intersects(other.Rectangle))
            {
                return true;
            }
            return false;
        }
    }
}
