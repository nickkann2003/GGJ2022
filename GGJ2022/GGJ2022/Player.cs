using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        int size;
        Vector2 velocity;
        float[] jumpcd;
        bool asleep;

        Body b;

        public int Size { get => size; set => size = value; }
        public Vector2 Velocity { get => velocity; set => velocity = value; }

        public Player(World world, SpriteBatch batch, Texture2D playerSprite, Game1 game, bool asleep)
        {
            this.world = world;
            b = world.CreateBody(new tainicom.Aether.Physics2D.Common.Vector2(960, 540), 0, BodyType.Dynamic);
            List<tainicom.Aether.Physics2D.Common.Vector2> corners = new List<tainicom.Aether.Physics2D.Common.Vector2>();
            size = 50;
            corners.Add(new tainicom.Aether.Physics2D.Common.Vector2(0, 0));
            corners.Add(new tainicom.Aether.Physics2D.Common.Vector2(0, size));
            corners.Add(new tainicom.Aether.Physics2D.Common.Vector2(size, size));
            corners.Add(new tainicom.Aether.Physics2D.Common.Vector2(size, 0));
            Fixture f = b.CreatePolygon(new tainicom.Aether.Physics2D.Common.Vertices(corners), 20f);
            f.Friction = 0.1f;
            f.Restitution = 0;
            b.Mass = 1000;
            b.LinearDamping = 0;
            b.AngularDamping = 0;
            b.FixedRotation = true;
            this.batch = batch;
            playerTexture = playerSprite;
            velocity = new Vector2(0, 0);
            jumpcd = new float[4];
            this.asleep = asleep;
        }

        public void Update(float deltaTime)
        {
            KeyboardState key = Keyboard.GetState();
            if (!asleep)
            {
                velocity = new Vector2(b.LinearVelocity.X, b.LinearVelocity.Y);
                if (key.IsKeyDown(Keys.A) && jumpcd[0] <= 0)
                {
                    velocity.X -= 75;
                    jumpcd[0] = 0.07f;
                }
                if (key.IsKeyDown(Keys.D) && jumpcd[1] <= 0)
                {
                    velocity.X += 75;
                    jumpcd[1] = 0.07f;
                }
                if (key.IsKeyDown(Keys.W) && jumpcd[2] <= 0)
                {
                    velocity.Y -= 150;
                    jumpcd[2] = 0.5f;
                }
                jumpcd[0] -= deltaTime;
                jumpcd[1] -= deltaTime;
                jumpcd[2] -= deltaTime;
                b.LinearVelocity = new tainicom.Aether.Physics2D.Common.Vector2(velocity.X, velocity.Y);
                b.ApplyLinearImpulse(new tainicom.Aether.Physics2D.Common.Vector2(velocity.X, velocity.Y));
            }
            
        }

        public void Draw()
        {
            batch.Draw(playerTexture, new Rectangle((int)b.Position.X, (int)b.Position.Y, size, size), Color.White);
        }

        public void Dispose()
        {
            if(world.BodyList.Contains(b))
                world.Remove(b);
        }

        public Texture2D GetTexture()
        {
            return playerTexture;
        }

        public float GetX()
        {
            return b.Position.X;
        }
    }
}
