using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace GGJ2022
{
    class Flotsam
    {
        float x;
        float y;
        float velMult;
        float width;
        float height;
        int idNum;
        float rotation;
        Color c;
        SpriteBatch batch;
        Texture2D sprite;

        Random rng;

        public float X { get => x; set => x = value; }
        public float Width { get => width; set => width = value; }

        public Flotsam(float startX, float startY, float velMultiplier, float width, float height, int idNum, SpriteBatch batch, Game1 game)
        {
            x = startX;
            y = startY;
            velMult = velMultiplier;
            this.width = width;
            this.height = height;
            this.idNum = idNum;
            this.batch = batch;
            sprite = game.Content.Load<Texture2D>("floatsom1");

            rng = new Random();
            c = new Color(rng.Next(100, 255), rng.Next(180, 230), rng.Next(100, 255), 180);
            rotation = rng.Next(-45, 45);
        }

        public void Update(float deltaTime, float velocity)
        {
            x -= velMult * velocity * deltaTime;
        }

        public void Draw()
        {
            batch.Draw(sprite, new Rectangle((int)x, (int)y, (int)width, (int)height), null, c, rotation, Vector2.Zero, SpriteEffects.None, 1);
            if(rng.NextDouble() > 0.9)
            {
                c = new Color(c, c.A + rng.Next(-1, 2));
            }
            
        }
    }
}
