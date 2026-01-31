using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimpleAnimationNamespace;

namespace Assignment_01;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _background;
    private SimpleAnimation _PlayerWalk;
    private Vector2 _PlayerPosition;
    private Vector2 _PlayerInput;

    private SpriteFont _Font;
    private string _message = "It is dangerous to go alone";

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }


    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 480;
        _graphics.ApplyChanges();

        _PlayerPosition = new Vector2(300,300);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _Font = Content.Load<SpriteFont>("SansFont");

        // TODO: use this.Content to load your game content here
        _background = Content.Load<Texture2D>("Zelda1Screen");

        _PlayerWalk = new SimpleAnimation(
            Content.Load<Texture2D>("Zelda1SpriteSheet"),
            frameWidth: 160,
            frameHeight: 160,
            frameCount: 4,
            framesPerSecond: 60
        );
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState kbCurrentState = Keyboard.GetState();
        _message = "";
    _PlayerInput = Vector2.Zero;
        #region arrow keys

        if(kbCurrentState.IsKeyDown(Keys.Down))
        {
            _PlayerInput += new Vector2(0,1);
            _message += "Down";
        }
        if (kbCurrentState.IsKeyDown(Keys.Up))
        {
            _PlayerInput += new Vector2(0,-1);

            _message += "Up ";
        }
        if (kbCurrentState.IsKeyDown(Keys.Left))
        {
            _PlayerInput += new Vector2(-1,0);

            _message += "Left ";
        }
        if (kbCurrentState.IsKeyDown(Keys.Right))
        {
            _PlayerInput += new Vector2(1,0);

            _message += "Right ";
        }
        #endregion

_PlayerPosition += _PlayerInput * 10;
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

Vector2 PlayerLocation = new Vector2(300, 140);
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        Rectangle BackgroundRect = new Rectangle(0,0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        _spriteBatch.Draw(_background, BackgroundRect, Color.White);
        _spriteBatch.DrawString(_Font, _message, Vector2.Zero, Color.Red);
        _PlayerWalk.Draw(_spriteBatch, _PlayerPosition, SpriteEffects.None);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
