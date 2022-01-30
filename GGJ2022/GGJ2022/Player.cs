using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Dynamics;

namespace GGJ2022
{
    class Player
    {
        World world;
        SpriteBatch batch;
        Texture2D playerTexture;


        Body b;
        public Player(World world, SpriteBatch batch, Texture2D playerSprite)
        {
            this.world = world;
            b = world.CreateBody(new tainicom.Aether.Physics2D.Common.Vector2(50, 50), 0, BodyType.Dynamic);
            Fixture f = b.CreateCircle(20f, 20f);
            f.Friction = 0f;
            f.Restitution = 0;
            b.Mass = 10;
            b.LinearDamping = 0;
            b.LinearVelocity = new tainicom.Aether.Physics2D.Common.Vector2(500, -200);
            this.batch = batch;
            playerTexture = playerSprite;
        }

        public void Update(float deltaTime)
        {

        }

        public void Draw(float deltaTime)
        {
            batch.Draw(playerTexture, new Vector2((int)b.Position.X, (int)b.Position.Y), Color.Black);
        }
    }
}
