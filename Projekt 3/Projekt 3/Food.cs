using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projekt_3
{
    class Food : Tile
    {
        public Food(Vector2 pos, Texture2D texture, Rectangle rectangle) : base(pos, texture, rectangle)
        {
            
            boundingBox = new Rectangle((int)pos.X + 16, (int)pos.Y + 16, 16, 16);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, pos + new Vector2(25, 25), rectangle, Color.White, 0, new Vector2(8, 8), 1, 0, 1);

            base.Draw(spriteBatch);

        }

    }

}
