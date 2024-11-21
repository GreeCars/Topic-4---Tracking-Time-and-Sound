using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Topic_4___Tracking_Time_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bombTexture;
        Rectangle bombRect;

        Texture2D explosionTexture;
        Rectangle explosionRect;

        Texture2D pliersTexture;
        Rectangle pliersRect;

        Rectangle greenwireRect;

        SpriteFont timeFont;

        SoundEffect explode;
        SoundEffectInstance explodeInstance;

        float seconds;

        bool isExploded;

        MouseState mouseState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            bombRect = new Rectangle(50, 50, 700, 400);
            explosionRect = new Rectangle(50, 50, 700, 400);
            pliersRect = new Rectangle(0, 0, 50, 50);
            greenwireRect = new Rectangle(546, 91, 586, 92);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            seconds = 0f;
            isExploded = false;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            explosionTexture = Content.Load<Texture2D>("explosion-image");
            pliersTexture = Content.Load<Texture2D>("pliers");
            timeFont = Content.Load<SpriteFont>("timeFont");
            explode = Content.Load<SoundEffect>("explosion");
            explodeInstance = explode.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            pliersRect.Location = mouseState.Position;
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
            if (!isExploded)
            {
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (pliersRect.Intersects(greenwireRect))
                    Exit();
            }
            else
            {
                if (explodeInstance.State == SoundState.Stopped)
                    Exit();

            }
            
            if (seconds > 15)
            {
                seconds = 0f;
                explodeInstance.Play();
                isExploded = true;
            }

           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (!isExploded)
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                _spriteBatch.DrawString(timeFont, (15 - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);
                _spriteBatch.Draw(pliersTexture, pliersRect, Color.White);
            }
            else
            {
                _spriteBatch.Draw(explosionTexture, explosionRect, Color.White);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
