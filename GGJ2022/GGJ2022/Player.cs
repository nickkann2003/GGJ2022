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
        Vector2[] previous;
        float[] yPrev;
        float previousUpdater;
        float[] jumpcd;
        bool asleep;

        Random rng;

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
            previous = new Vector2[5];
            yPrev = new float[52];
            for(int i = 0; i < yPrev.Length; i++)
            {
                yPrev[i] = -100;
            }
            for(int i = 0; i < previous.Length; i++)
            {
                previous[i] = new Vector2(-100, -100);
            }
            previousUpdater = 0;
            rng = new Random();
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
            else
            {
                velocity.Y = b.LinearVelocity.Y;
                if (key.IsKeyDown(Keys.A))
                {
                    velocity.X -= 100/(10+Math.Abs(velocity.X));
                }
                if (key.IsKeyDown(Keys.D))
                {
                    velocity.X += 100 /(10 + Math.Abs(velocity.X));
                }
                if (key.IsKeyDown(Keys.W))
                {
                    velocity.Y -= 15;
                }
                if (key.IsKeyDown(Keys.S))
                {
                    velocity.Y += 8;
                }
                b.LinearVelocity = new tainicom.Aether.Physics2D.Common.Vector2(0, velocity.Y);
                
            }
            previousUpdater -= deltaTime;

            previous[0].X = b.Position.X - (velocity.X * 0.1f);
            previous[0].Y = b.Position.Y;
            yPrev[0] = b.Position.Y;

            for (int i = previous.Length-1; i > 0; i--)
            {
                previous[i].X = previous[i - 1].X - (velocity.X*0.1f);
            }

                for(int i = yPrev.Length-1; i > 0;  i--)
                {
                    yPrev[i] = yPrev[i - 1];
                    if(i%10 == 0)
                    {
                        previous[(i / 10)-1].Y = yPrev[i];
                    }
                }
        }

        public void Draw()
        {
            if (asleep)
            {
                float alpha = Math.Abs(velocity.X) - 100;
                if (alpha < 0)
                {
                    alpha = 0;
                }
                else
                {
                    alpha = (alpha * 90) / (350 * 255);
                }
                    
                for(int i = 0; i < previous.Length; i++)
                {
                    batch.Draw(playerTexture, new Rectangle((int)previous[i].X, 900 - (int)((20f / 50f) * (size - i * 3))/2, size - (i * 3), (int)((20f / 50f) * (size - i * 3))), new Color(Color.Black, alpha));
                    batch.Draw(playerTexture, new Rectangle((int)previous[i].X, (int)previous[i].Y, size-(i*3), size-(i*3)), new Color(Color.White, alpha));
                }
                batch.Draw(playerTexture, new Rectangle((int)b.Position.X, 892, size, (int)((20f / 50f) * size)), new Color(Color.Black, alpha));
                
            }
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
