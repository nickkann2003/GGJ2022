using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GGJ2022
{
    class Timer
    {
        private SpriteBatch batch;
        private SpriteFont font;

        private float timePassed;
        bool countingDown;
        private bool active;

        public float TimePassed { get => timePassed; set => timePassed = value; }

        public Timer(float startTime, bool countingDown, SpriteBatch batch, SpriteFont font)
        {
            this.timePassed = startTime;
            this.countingDown = countingDown;
            this.batch = batch;
            this.font = font;
            active = true;
        }

        public void Update(float deltaTime)
        {
            if (active)
            {
                if (countingDown)
                {
                    timePassed -= deltaTime;
                }
                else
                {
                    timePassed += deltaTime;
                }
            }
        }

        public void Draw()
        {
            batch.DrawString(font, (Math.Truncate((timePassed - timePassed % 1) / 60)).ToString("00") + ":" + (Math.Truncate((timePassed - timePassed % 1) % 60)).ToString("00"), new Vector2(10, 10), Color.Black, 0, Vector2.Zero, 3, SpriteEffects.None, 1); ;
        }

        public void Stop()
        {
            active = false;
        }

        public void Start()
        {
            active = true;
        }
    }
}
