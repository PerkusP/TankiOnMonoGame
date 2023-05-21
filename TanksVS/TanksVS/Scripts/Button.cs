using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanksVS.Scripts
{
    public class Button
    {
        private MouseState _currentMouseState;
        private bool _isHovering;
        private MouseState _previousMouseState;
        private Texture2D _texture;
        

        public EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColor { get; set; }
        public Vector2 Position { get; private set; }
        public Rectangle Rectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height); } }
        public string Text { get; set; }

        public Button(Texture2D texture, Vector2 position) 
        {
            _texture = texture;
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var color = Color.White;
            if (_isHovering)
            {
                color = Color.Gray;
            }
            spriteBatch.Draw(_texture, Rectangle, color);
        }
        
        public void Update(GameTime gameTime)
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            var mouse = new Rectangle(_currentMouseState.X, _currentMouseState.Y, 1,1);
            _isHovering = false;

            if (Rectangle.Intersects(mouse))
            {
                _isHovering = true;
                if (_currentMouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

    }
}
