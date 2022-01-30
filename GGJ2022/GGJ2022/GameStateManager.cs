using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace GGJ2022
{
    static class GameStateManager
    {
        //Fields
        static World world;
        static SpriteBatch batch;
        static Game1 game;

        //Transition variables
        static float transitionTimer;
        static float transitionMax;
        static State transitionState;

        static Texture2D blackSquare;

        static Stack<State> states = new Stack<State>();

        public static void GiveItems(World world, SpriteBatch batch, Game1 game)
        {
            GameStateManager.world = world;
            GameStateManager.batch = batch;
            GameStateManager.game = game;
            blackSquare = game.Content.Load<Texture2D>("blackSquare");
        }

        public static void Begin()
        {
            states.Push(new IntroState(world, batch, game));
        }

        public static void Update(float deltaTime)
        {
            foreach(State s in states)
            {
                s.Update(deltaTime);
            }
            transitionTimer -= deltaTime;
        }

        public static void Draw()
        {
            foreach (State s in states)
            {
                s.Draw();
            }
            if(transitionTimer > 0)
            {
                if (transitionTimer < transitionMax / 2)
                {
                    if (transitionState != null)
                    {
                        states.Pop().Dispose();
                        states.Push(transitionState);
                        transitionState = null;
                    }
                }
                int alpha = 255 - (int)(Math.Abs((transitionTimer-transitionMax/2)/(transitionMax/2)) * 255);
                batch.Draw(blackSquare, new Vector2(0, 0), new Color(0, 0, 0, alpha));
            }
        }

        public static void Transition(float time, State nextState)
        {
            transitionTimer = time;
            transitionMax = time;
            transitionState = nextState;
        }
    }
}
