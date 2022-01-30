/*
 * Nick Kannenberg
 * Class for words that will fade in
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GGJ2022
{
    class Word
    {
        //Fields
        SpriteBatch batch;
        SpriteFont font;
        String word;
        Vector2 pos;
        float startTime;
        float duration;
        float passed;

        public Word(SpriteBatch batch, SpriteFont font, String word, Vector2 position, float startTime, float duration)
        {
            this.batch = batch;
            this.font = font;
            this.word = word;
            this.pos = position;
            this.startTime = startTime;
            this.duration = duration + startTime;
            passed = 0;
        }

        public void Update(float deltaTime)
        {
            passed += deltaTime;
        }

        public void Draw()
        {
            if(passed > startTime)
            {
                if(passed > duration * 2)
                {

                }else if(passed > duration && passed < duration*2)
                {
                    int alpha = 255 - (int)((passed - duration) / (duration*2 - duration) * 255);
                    batch.DrawString(font, word, pos, new Color(alpha, alpha, alpha), 0, Vector2.Zero, 3f, SpriteEffects.None, 1);
                }
                else
                {
                    int alpha = (int)((passed - startTime) / (duration - startTime) * 255);
                    batch.DrawString(font, word, pos, new Color(alpha, alpha, alpha), 0, Vector2.Zero, 3f, SpriteEffects.None, 1);
                }
            }
        }
    }
}
