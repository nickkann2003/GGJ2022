using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace GGJ2022
{
    class AsleepState : State
    {
        public AsleepState(World world, SpriteBatch batch, Game1 game) : base(world, batch, game)
        {

        }

        public override void Update(float deltaTime)
        {

        }

        public override void Draw()
        {
            game.GraphicsDevice.Clear(Color.AliceBlue);
        }
    }
}
