using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace GGJ2022
{
    class AwakeState : State
    {
        //Fields
        Body[] walls;

        public AwakeState(World world, SpriteBatch batch, Game1 game) : base(world, batch, game)
        {
            walls = new Body[4];
            walls[0] = world.CreateEdge(new tainicom.Aether.Physics2D.Common.Vector2(), new tainicom.Aether.Physics2D.Common.Vector2());
            walls[1] = world.CreateEdge(new tainicom.Aether.Physics2D.Common.Vector2(), new tainicom.Aether.Physics2D.Common.Vector2());
            walls[2] = world.CreateEdge(new tainicom.Aether.Physics2D.Common.Vector2(), new tainicom.Aether.Physics2D.Common.Vector2());
            walls[3] = world.CreateEdge(new tainicom.Aether.Physics2D.Common.Vector2(), new tainicom.Aether.Physics2D.Common.Vector2());
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
