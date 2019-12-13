﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameRunningCube.Source.Helpers
{
    public class ObjectGenerator
    {
        public T GenerateRandomObject<T>(string path) where T : Objects2D, new()
        {
            var dim = new Vector2(30, 30);
            var pos = new Vector2((int)GlobalVariables.Random.Next(0, 600), (int)GlobalVariables.Random.Next(0, 200));

            var obj = new T
            {
                Dimention = dim, Position = pos, Texture2D = GlobalVariables.Content.Load<Texture2D>(path)
            };


            return obj;
        }

    }
}
