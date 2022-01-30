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
        //Fields
        Timer t;
        Texture2D background;
        Body floor;
        Player p;

        //Constructors
        public AsleepState(World world, SpriteBatch batch, Game1 game, float time) : base(world, batch, game)
        {
            t = new Timer(time, true, batch, game.font);
            background = game.Content.Load<Texture2D>("floatsomBackground");
            floor = world.CreateEdge(new tainicom.Aether.Physics2D.Common.Vector2(0,900), new tainicom.Aether.Physics2D.Common.Vector2(1920,900));
            p = new Player(world, batch, game.Content.Load<Texture2D>("playerCharacter"), game, true);
        }

        //Methods

        public override void Update(float deltaTime)
        {
            t.Update(deltaTime);
            p.Update(deltaTime);
            if(t.TimePassed < 0)
            {
                t.TimePassed = 0;
                t.Stop();
                GameStateManager.Transition(5, new AwakeState(world, batch, game));
            }
        }

        public override void Draw()
        {
            batch.Draw(background, new Vector2(0, 0), Color.White);
            t.Draw();
            batch.Draw(p.GetTexture(), new Rectangle((int)p.GetX(), 892, p.Size, (int)((20f / 50f) * p.Size)), Color.DarkGray);
            p.Draw();
        }

        public override void Dispose()
        {
            base.Dispose();
            p.Dispose();
        }
    }
}
