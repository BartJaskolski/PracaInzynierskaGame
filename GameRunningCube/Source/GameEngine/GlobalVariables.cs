using System;
using GameRunningCube.Source.GameEngine.Input;
using GameRunningCube.Source.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameRunningCube
{
    class GlobalVariables
    {
        public static GraphicsDeviceManager Graphics;
        public static ContentManager Content;
        public static SpriteBatch SpriteBatch;

        public static KeyboardController KeyboardController;

        public static ObjectGenerator ObjectGenerator;

        public static Random Random;
        public static SpriteFont SpriteFont;
    }
}
