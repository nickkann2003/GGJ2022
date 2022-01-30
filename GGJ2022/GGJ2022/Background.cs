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
        }

        public void CreateFlotsam()
        {
            float width = rng.Next(150, 350);
            float height = width + rng.Next(-50, 50);
            flotsam.Add(new Flotsam(rng.Next(1000, 1800), rng.Next(0, 800), rng.Next(15, 85) / 100f, width, height, rng.Next(10), batch, game)); 
        }

        public void Update(float deltaTime, float velocity)
        {
            List<Flotsam> toRemove = new List<Flotsam>();
            foreach (Flotsam f in flotsam)
            {
                f.Update(deltaTime, velocity);
                if(f.X + f.Width < 0)
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
                CreateFlotsam();
                createTimer = rng.Next(35, 100) / 100f;
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
