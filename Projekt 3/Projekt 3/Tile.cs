using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_3
{
    class Tile : MainClass //Ärvs av Wall.
    {
        Rectangle rect;

        public Tile(Vector2 pos, Texture2D texture, Rectangle rectangle) : base(pos, texture, rectangle)
        {
            this.pos = pos;
            this.texture = texture;

            rect = rectangle;          

        }

        public virtual void Draw(SpriteBatch sb)
        {

          sb.Draw(texture, rect, Color.White);

        }

    }
}
