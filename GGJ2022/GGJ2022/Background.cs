using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GGJ2022
{
    class Background
    {
        private SpriteBatch batch;
        private Game1 game;
        private List<Flotsam> flotsam;
        Random rng;

        private Texture2D bg;
        private Texture2D bgOverlay;

        float createTimer;

        public Background(SpriteBatch batch, Game1 game)
        {
            this.batch = batch;
            this.game = game;
            flotsam = new List<Flotsam>();
            rng = new Random();
            createTimer = 1f;
            bg = game.Content.Load<Texture2D>("floatsomBackground");
            bgOverlay = game.Content.Load<Texture2D>("floatsomBackgroundOverlay");
            for(int i = 0; i < 5; i++)
            {
                CreateFlotsam(10);
            }
        }

        public void CreateFlotsam(float velocity)
        {
            float width = rng.Next(150, 350);
            float height = width + rng.Next(-50, 50);
            float mult = (rng.Next(90, 190) / 100f) * (rng.Next(70, 150) / 100f);
            if (velocity > 0)
            {
                flotsam.Add(new Flotsam(rng.Next(2400, 2900), rng.Next(-150, 800), mult, width, height, rng.Next(1, 11), batch, game));
            }
            else
            {
                flotsam.Add(new Flotsam(rng.Next(-900, -500), rng.Next(-150, 800), mult, width, height, rng.Next(1, 11), batch, game));
            }
            
        }

        public void Update(float deltaTime, float velocity)
        {
            List<Flotsam> toRemove = new List<Flotsam>();
            foreach (Flotsam f in flotsam)
            {
                f.Update(deltaTime, velocity);
                if(f.X + f.Width < -500 && velocity >= 0)
                {
                    toRemove.Add(f);
                }
                if(f.X + f.Width > 2400 && velocity < 0)
                {
                    toRemove.Add(f);
                }
            }
            foreach(Flotsam f in toRemove)
            {
                flotsam.Remove(f);
            }
            createTimer -= deltaTime;
            if(createTimer <= 0)
            {
                CreateFlotsam(velocity);
                if(Math.Abs(velocity) > 100)
                {
                    createTimer = rng.Next(1, 20) / 100f;
                }
                else
                {
                    createTimer = rng.Next(35, 80) / 100f;
                }
            }
        }

        public void Draw()
        {
            batch.Draw(bg, new Vector2(0, 0), Color.White);
            foreach(Flotsam f in flotsam)
            {
                f.Draw();
            }
            batch.Draw(bgOverlay, new Vector2(0, 0), Color.White);
        }

    }
}
