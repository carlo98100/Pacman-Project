using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_3
{
    class MainClass //ärvs av Character och Wall.
    {
        protected Vector2 pos;
        protected Texture2D texture;
        protected Rectangle rectangle;
        protected Rectangle boundingBox;


        public MainClass(Vector2 pos, Texture2D texture, Rectangle rectangle)
        {
            
            this.pos = pos;
            this.texture = texture;
            this.rectangle = rectangle;

            boundingBox = new Rectangle((int)pos.X, (int)pos.Y, rectangle.Width, rectangle.Height);
        }

        public Rectangle GetBoundingBox()
        {
            return boundingBox;
        }


    }
}
