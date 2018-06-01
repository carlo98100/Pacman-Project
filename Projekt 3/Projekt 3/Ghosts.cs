using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projekt_3
{
    class Ghosts : Character
    {

        static Random random = new Random();

        int ghostJ;
        int ghostI;

        Vector2 currentDir;
        int step;

        List<Vector2> possibleDirr = new List<Vector2>();

        public Ghosts(Vector2 pos, Texture2D texture, Rectangle spritesheet, int scale) : base(pos, texture, spritesheet, scale)
        {
            currentDir = Vector2.Zero;
            step = 0;
        }

        public override void Update(GameTime gameTime, Tile[,] tiles)
        {

            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer <= 0)
            {
                frameTimer = frameInterval; frame++;

                rectangle.X = (frame % 8) * 16;                                                                                       //det är 8 bilder och dom är 16 * 16 stora.
            }
            if (step == 50)
            {
                step = 0;
                ghostJ = (int)pos.X / 50;
                ghostI = (int)pos.Y / 50;

                if (!(tiles[ghostJ + 1, ghostI] is Wall))
                {

                    possibleDirr.Add(new Vector2(1, 0));

                }

                if (!(tiles[ghostJ - 1, ghostI] is Wall))
                {

                    possibleDirr.Add(new Vector2(-1, 0));

                }

                if (!(tiles[ghostJ, ghostI + 1] is Wall))
                {

                    possibleDirr.Add(new Vector2(0, 1));

                }

                if (!(tiles[ghostJ, ghostI - 1] is Wall))
                {

                    possibleDirr.Add(new Vector2(0, -1));

                }

                int dirr = random.Next(0, possibleDirr.Count);
                if (possibleDirr.Count > 0)
                {
                    currentDir = possibleDirr[dirr];
                }
            }
            step++;
            //pos += possibleDirr[1];
            pos = pos + currentDir;

            possibleDirr.Clear();

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, pos + new Vector2(25, 25), rectangle, Color.White, 0, new Vector2(8, 8), scale, characterFx, 1);                                              //(25,25) för att få rätt på positionen, (8,8) för att göra så att pacman roteras i mitten.

            base.Draw(spriteBatch);

        }
    }
}
