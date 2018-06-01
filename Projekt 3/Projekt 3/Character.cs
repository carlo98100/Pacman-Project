using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_3
{
    class Character : MainClass // ärvs av Pacman, Ghosts och Food.
    {

        public Texture2D debugTexture;
        protected int scale;
        protected int frame;

        protected double frameTimer;
        protected double frameInterval;

        protected SpriteEffects characterFx;

        protected Vector2 oldPos;

        public Character( Vector2 pos,Texture2D texture, Rectangle rectangle, int scale) : base(pos, texture, rectangle)
        {
            debugTexture = null;
            this.scale = scale;

            oldPos = pos;           
            frame = 0;
            frameTimer = 100;
            frameInterval = 140;
            characterFx = SpriteEffects.None;
        }

       

        public virtual void Update(GameTime gameTime, Tile[,] tiles = null)
        {
            boundingBox = new Rectangle((int)pos.X + 25/2 - 1, (int)pos.Y + 25/2 - 1, (rectangle.Width * scale), (rectangle.Height * scale));    //spritesheet.width / 2 för att få den ska delas på rätt sätt 

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //if (debugTexture != null)
            //    spriteBatch.Draw(debugTexture, boundingBox, Color.Red);

        }

        public void SetToOldPos()
        {
            pos = oldPos;
        }

    }
}
