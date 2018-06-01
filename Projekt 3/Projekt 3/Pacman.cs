using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projekt_3
{
    class Pacman : Character
    {
        float rotation;

        public Pacman(Vector2 pos, Texture2D texture, Rectangle spritesheet, int scale) : base(pos, texture, spritesheet, scale)
        {

        }

        public override void Update(GameTime gameTime, Tile[,] tiles = null)
        {
            oldPos = pos;
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer <= 0)

            {
                frameTimer = frameInterval; frame++;

                rectangle.X = (frame % 3) * 16;                                                                                       //det är 3 bilder och dom är 16 * 16 stora.
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                characterFx = SpriteEffects.FlipHorizontally;
                pos.X -= 1f;
                frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                rotation = MathHelper.ToRadians(0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                characterFx = SpriteEffects.None;
                pos.X += 1f;
                frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                rotation = MathHelper.ToRadians(0);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                rotation = MathHelper.ToRadians(-90);
                characterFx = SpriteEffects.None;
                pos.Y -= 1f;
                frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                rotation = MathHelper.ToRadians(-90);
                characterFx = SpriteEffects.FlipHorizontally;
                pos.Y += 1f;
                frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }
                                                                                                                                                                                                    //så man räknar ifrån vänstra hörnet och inte ifrån mitten.
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, pos + new Vector2(25, 25), rectangle, Color.White, rotation, new Vector2(8, 8), scale, characterFx, 1);                                              //(25,25) för att få rätt på positionen, (8,8) för att göra så att pacman roteras i mitten.
                                                                                                                                                                                          //(8,8) bara för att scale inte gjorts än.
            base.Draw(spriteBatch);

        }

    }
}
