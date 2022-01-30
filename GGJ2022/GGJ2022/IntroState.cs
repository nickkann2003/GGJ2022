using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace GGJ2022
{
    class IntroState : State
    {
        //Fields
        private List<Word> text;
        private float timePassed;

        public IntroState(World world, SpriteBatch batch, Game1 game) : base(world, batch, game)
        {
            String[] words = "A second spent dreaming is a second well spent , but it cant be refunded".Split(" ");
            text = new List<Word>();
            timePassed = 0;
            float distance = 100;
            float time = 0;
            for(int i = 0; i < words.Length; i++)
            {
                if(words[i].Equals(",") || words[i].Equals(".")){
                    text.Add(new Word(batch, game.font, words[i], new Microsoft.Xna.Framework.Vector2(distance, 150 + 700 * (i / 10)), time, 3));
                    time += 1.5f;
                }
                else
                {
                    text.Add(new Word(batch, game.font, words[i], new Microsoft.Xna.Framework.Vector2(distance, 150 + 700 * (i / 10)), time, 3));
                    time += 0.35f;
                }
                distance += 30 * (words[i].Length + 1);
                if(i == 9)
                {
                    distance = 1050;
                }
            }
        }

        public override void Update(float deltaTime)
        {
            foreach(Word w in text)
            {
                w.Update(deltaTime);
            }
            timePassed += deltaTime;
            if(timePassed > 9)
            {
                timePassed = 0;
                GameStateManager.Transition(5, new AwakeState(world, batch, game));
            }
        }

        public override void Draw()
        {
            game.GraphicsDevice.Clear(Color.Black);
            foreach(Word w in text)
            {
                w.Draw();
            }
        }
    }
}
