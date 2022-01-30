using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace GGJ2022
{
    class State
    {
        //Fields
        protected World world;
        protected SpriteBatch batch;
        protected Game1 game;

        public State(World world, SpriteBatch batch, Game1 game)
        {
            this.world = world;
            this.batch = batch;
            this.game = game;
        }

        public virtual void Update(float deltaTime)
        {

        }

        public virtual void Draw()
        {

        }

        public virtual void Dispose()
        {

        }
    }
}
