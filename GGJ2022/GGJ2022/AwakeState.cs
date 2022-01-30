using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
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

        Player p;

        Timer t;

        public AwakeState(World world, SpriteBatch batch, Game1 game) : base(world, batch, game)
        {
            walls = new Body[4];
            walls[0] = world.CreateEdge(new tainicom.Aether.Physics2D.Common.Vector2(1210, 790), new tainicom.Aether.Physics2D.Common.Vector2(710, 790));
            walls[1] = world.CreateEdge(new tainicom.Aether.Physics2D.Common.Vector2(710, 290), new tainicom.Aether.Physics2D.Common.Vector2(710, 790));
            walls[2] = world.CreateEdge(new tainicom.Aether.Physics2D.Common.Vector2(1210, 290), new tainicom.Aether.Physics2D.Common.Vector2(710, 290));
            walls[3] = world.CreateEdge(new tainicom.Aether.Physics2D.Common.Vector2(1210, 290), new tainicom.Aether.Physics2D.Common.Vector2(1210, 790));
            p = new Player(world, batch, game.Content.Load<Texture2D>("playerCharacter"), game, false);
            t = new Timer(0, false, batch, game.font);
        }

        public override void Update(float deltaTime)
        {
            p.Update(deltaTime);
            t.Update(deltaTime*2);
            if(t.TimePassed >= 45.8f && t.TimePassed < 46)
            {
                t.TimePassed = 46;
                GameStateManager.Transition(5, new AsleepState(world, batch, game, 48f));
            }
        }

        public override void Draw()
        {
            game.GraphicsDevice.Clear(Color.LightGray);
            Primitives2D.DrawRectangle(batch, new Rectangle(710, 290, 500, 500), Color.AntiqueWhite);
            p.Draw();
            Primitives2D.DrawLine(batch, new Vector2(1210, 790), new Vector2(710, 790), Color.Black);
            Primitives2D.DrawLine(batch, new Vector2(710, 290), new Vector2(710, 790), Color.Black);
            Primitives2D.DrawLine(batch, new Vector2(1210, 290), new Vector2(710, 290), Color.Black);
            Primitives2D.DrawLine(batch, new Vector2(1210, 290), new Vector2(1210, 790), Color.Black);
            t.Draw();
        }

        public override void Dispose()
        {
            base.Dispose();
            for(int i = 0; i < walls.Length; i++)
            {
                if(world.BodyList.Contains(walls[i]))
                {
                    world.Remove(walls[i]);
                }
            }
            p.Dispose();
        }


    }
}
