//testing commit.
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

//background image
    private Texture2D _background;
//NPC sprites
    private Texture2D _OldMan;
    private SimpleAnimation _SimonWalkLeft;
    private SimpleAnimation _SimonWalkRight;
    private Vector2 _SimonPosition;
    private SimpleAnimation _CurrentSimonAnim;
    private float _SimonSpeed = 50f;
    private int _SimonDirection = -1;
//Player
    private SimpleAnimation _CurrentAnimation;
    private SimpleAnimation _PlayerWalkW;
    private SimpleAnimation _PlayerWalkA; 
    private SimpleAnimation _PlayerWalkS;
    private SimpleAnimation _PlayerWalkD;

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

        _PlayerPosition = new Vector2(300,200);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _Font = Content.Load<SpriteFont>("SansFont");

        // TODO: use this.Content to load your game content here
        _background = Content.Load<Texture2D>("Zelda1Screen");

        _OldMan = Content.Load<Texture2D>("Zelda1OldMan");

        _SimonWalkLeft = new SimpleAnimation(
            Content.Load<Texture2D>("SimonBelmontWalkLeftSpriteSheet"),
            324/3, 149, 3, 6);
        _SimonWalkRight = new SimpleAnimation(
            Content.Load<Texture2D>("SimonBelmontWalkRightSpriteSheet"),
            324/3, 149, 3, 6);
        _SimonPosition = new Vector2(400, 300);

//Player Animations WASD/Arrow keys
        _PlayerWalkW = new SimpleAnimation(
            Content.Load<Texture2D>("LinkUpSpriteSheet"),
            105/2, 51, 2, 8
        );
         _PlayerWalkA = new SimpleAnimation(
            Content.Load<Texture2D>("LinkLeftSpriteSheet"),
            105/2, 51, 2, 8
        );
         _PlayerWalkS = new SimpleAnimation(
            Content.Load<Texture2D>("LinkDownSpriteSheet"),
            105/2, 51, 2, 8
        );
         _PlayerWalkD = new SimpleAnimation(
            Content.Load<Texture2D>("LinkRightSpriteSheet"),
            105/2, 51, 2, 8
        );

        _CurrentAnimation = _PlayerWalkS;
        _CurrentSimonAnim = _SimonWalkLeft;
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState kbCurrentState = Keyboard.GetState();
        float DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _SimonPosition.X += _SimonDirection * _SimonSpeed * DeltaTime;
        _message = "";
    _PlayerInput = Vector2.Zero;

        if(_SimonPosition.X < 100)
        {
            _SimonDirection = 1;
            _CurrentSimonAnim = _SimonWalkRight;
        }
        else if(_SimonPosition.X > 500)
        {
            _SimonDirection = -1;
            _CurrentSimonAnim = _SimonWalkLeft;
        }

        #region arrow keys

        if(kbCurrentState.IsKeyDown(Keys.Up) || kbCurrentState.IsKeyDown(Keys.W))
        {
            _CurrentAnimation = _PlayerWalkW;
            _PlayerInput += new Vector2(0,-1);
            _message += "Up";
        }
        if (kbCurrentState.IsKeyDown(Keys.Left) || kbCurrentState.IsKeyDown(Keys.A))
        {
            _CurrentAnimation = _PlayerWalkA;
            _PlayerInput += new Vector2(-1,0);
            _message += "Left";
        }
        if (kbCurrentState.IsKeyDown(Keys.Down) || kbCurrentState.IsKeyDown(Keys.S))
        {
            _CurrentAnimation = _PlayerWalkS;
            _PlayerInput += new Vector2(0,1);
            _message += "Down";
        }
        if (kbCurrentState.IsKeyDown(Keys.Right) || kbCurrentState.IsKeyDown(Keys.D))
        {
            _CurrentAnimation = _PlayerWalkD;
            _PlayerInput += new Vector2(1,0);
            _message += "Right";
        }

        if(_CurrentAnimation != null)
        {
            _CurrentAnimation.Update(gameTime);
        }
        if(_CurrentSimonAnim != null)
        {
            _CurrentSimonAnim.Update(gameTime);
        }
        #endregion

_PlayerPosition += _PlayerInput * 5;
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

        Rectangle BackgroundRect = new Rectangle(0,0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        _spriteBatch.Draw(_background, BackgroundRect, Color.White);
        _spriteBatch.DrawString(_Font, _message, Vector2.Zero, Color.Red);
        _spriteBatch.Draw(_OldMan, new Rectangle(200,45,50,50), Color.White);

        _CurrentSimonAnim.Draw(_spriteBatch, _SimonPosition, SpriteEffects.None);
        //Player Animation
        _CurrentAnimation.Draw(_spriteBatch, _PlayerPosition, SpriteEffects.None);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
