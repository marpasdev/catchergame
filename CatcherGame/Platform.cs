using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarpasEngine.Objects2D;

namespace CatcherGame
{
    public class Platform : Sprite
    {
        private PlayerInput input;
        private float speed = 300f;
        private int maxHealthPoints;
        public int HealthPoints;

        public Platform(Texture2D texture, PlayerInput input, GraphicsDeviceManager graphics, int maxHealthPoints) : base(texture)
        {
            this.input = input;
            Position.Y = graphics.PreferredBackBufferHeight - (Texture.Height + 10f);
            this.maxHealthPoints = maxHealthPoints;
            HealthPoints = this.maxHealthPoints;
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {

            #region Movement
            KeyboardState kstate = Keyboard.GetState();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Keys key in input.Right)
            {
                if (kstate.IsKeyDown(key))
                {
                    Position.X += speed * deltaTime;
                    break;
                }
            }
            foreach (Keys key in input.Left)
            {
                if (kstate.IsKeyDown(key))
                {
                    Position.X -= speed * deltaTime;
                    break;
                }
            }
            #endregion

            // Boundary check
            Position.X = MathHelper.Clamp(Position.X, 0, graphics.PreferredBackBufferWidth - Texture.Width);
        }

        public void ResetHealthPoints()
        {
            HealthPoints = maxHealthPoints;
        }

    }
}
