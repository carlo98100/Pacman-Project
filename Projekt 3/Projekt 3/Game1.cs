using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace Projekt_3
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 pacmanPos;
        Vector2 foodPos;
        Vector2 gtBluePos;
        Vector2 gtOrangePos;
        Vector2 scorePos;
        Vector2 livesPos;

        Rectangle pacmanSpritesheet;
        Rectangle gtBlueSpritesheet;
        Rectangle gtOrangeSpritesheet;

        Tile[,] tiles;

        Pacman pm;
        Food fd;
        Ghosts gtBlue;
        Ghosts gtOrange;

        Texture2D wallTexture;
        Texture2D spriteSheet;

        List<string> WallString = new List<string>();
        List<Character> characters = new List<Character>();
        List<Food> foodList = new List<Food>();

        private SpriteFont scoreFont;

        int score;
        int lives;
        int tileSize;
        int scale;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            tileSize = 50;
            scale = 2;

             score = 0;
             scorePos = new Vector2(0, 0);

            lives = 3;
            livesPos = new Vector2(400, 0);

        }

        protected override void Initialize()
        {

            graphics.PreferredBackBufferWidth = 550;
            graphics.PreferredBackBufferHeight = 550;
            IsMouseVisible = true;
            graphics.ApplyChanges();

            base.Initialize();
        }

        private void AddFood(int j, int i)
        {

            foodPos = new Vector2(j * tileSize, i * tileSize);

            fd = new Food(foodPos, spriteSheet, pacmanSpritesheet);
            foodList.Add(fd);

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            wallTexture = Content.Load<Texture2D>("Wall");

            scoreFont = Content.Load<SpriteFont>("Score");

            spriteSheet = Content.Load<Texture2D>("SpriteSheet");
            pacmanSpritesheet = new Rectangle(0, 0, 16, 16);
            gtBlueSpritesheet = new Rectangle(0, 48, 16, 16);
            gtOrangeSpritesheet = new Rectangle(0, 64, 16, 16);

            StreamReader sr = new StreamReader(@"Wall.txt");

            while (!sr.EndOfStream)
            {
                WallString.Add(sr.ReadLine());
            }
            sr.Close();

            tiles = new Tile[WallString[0].Length, WallString.Count];

            for (int i = 0; i < WallString.Count; i++)
            {
                for (int j = 0; j < WallString[0].Length; j++)
                {
                    if (WallString[i][j] == 'W')
                    {
                        Rectangle rect = new Rectangle(j * 50, i * 50, 50, 50);
                        Vector2 wallPos = new Vector2(j * 50, i * 50);

                        tiles[j, i] = new Wall(wallPos, wallTexture, rect);
                    }

                    if (WallString[i][j] == 'P')
                    {
                        pacmanPos = new Vector2(j * tileSize, i * tileSize);

                        pm = new Pacman(pacmanPos, spriteSheet, pacmanSpritesheet, scale);
                        characters.Add(pm);
                    }

                    if (WallString[i][j] == 'F')
                    {
                        AddFood(j, i);
                    }

                    if (WallString[i][j] == 'A')
                    {

                        AddFood(j, i);

                        gtBluePos = new Vector2(j * tileSize, i * tileSize);

                        gtBlue = new Ghosts(gtBluePos, spriteSheet, gtBlueSpritesheet, scale);
                        characters.Add(gtBlue);
                    }

                    if (WallString[i][j] == 'B')
                    {

                        AddFood(j, i);

                        gtOrangePos = new Vector2(j * tileSize, i * tileSize);

                        gtOrange = new Ghosts(gtOrangePos, spriteSheet, gtOrangeSpritesheet, scale);
                        characters.Add(gtOrange);

                    }
                }
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            bool wallCollisionFound = false;
            for (int i = 0; i < tiles.GetLength(0) && !wallCollisionFound; i++)                     //Om det inte skett en kollision så kan den gå vidare.
            {
                for (int j = 0; j < tiles.GetLength(1) && !wallCollisionFound; j++)                 //om Det inte skett en kollision så kan den gå vidare.
                {

                    if(tiles[i,j] != null)
                    {
                        if (pm.GetBoundingBox().Intersects(tiles[i,j].GetBoundingBox()))            //Kollar om det sker en kollision.
                        {
                            wallCollisionFound = true;                                              //Det har hänt en kollison.
                            pm.SetToOldPos();                                                       //sätter gamla positionen.
                            
                        }                      
                    }
                }
            }

            for (int i = 0; i < foodList.Count; i++)
            {
                if (pm.GetBoundingBox().Intersects(foodList[i].GetBoundingBox()))                    //kollar om det sker en kollision mellan Pacman och maten.
                {
                    foodList.RemoveAt(i);                                                            //Om det har det så ska maten på den platsen försvinna.
                    score++;                                                                         //Då får man ett poäng.
                    break;
                                                                                    
                }
            }

            if (pm.GetBoundingBox().Intersects(gtBlue.GetBoundingBox()))                            //om pacman kommer emot ett blå spöke så förlorar han ett liv.
            {
                characters.Remove(pm);

                pm = new Pacman(pacmanPos, spriteSheet, pacmanSpritesheet, scale);
                characters.Add(pm);

                lives--;
            }

            if (pm.GetBoundingBox().Intersects(gtOrange.GetBoundingBox()))                          //om pacman kommer emot ett orange spöke så förlorar han ett liv.
            {
                characters.Remove(pm);

                pm = new Pacman(pacmanPos, spriteSheet, pacmanSpritesheet, scale);
                characters.Add(pm);

                lives--;
            }

            gtBlue.Update(gameTime, tiles);                                                                  //Uppdatering av Blått spöke.
            gtOrange.Update(gameTime, tiles);                                                                //Uppdatering av Blått spöke.

            pm.Update(gameTime);                                                                             //Uppdatering av Pacman.

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            foreach (Food fd in foodList)                                                              //Nedan ritas hela food listan ut.
            {
                fd.Draw(spriteBatch);
            }

            for (int i = 0; i < characters.Count; i++)                                                 //Nedan ritas hela character listan ut.
            {
                characters[i].Draw(spriteBatch);
            }

            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j] != null)
                    {
                        tiles[i, j].Draw(spriteBatch);
                    }
                }
            }

            if (lives == 0)
            {
                spriteBatch.DrawString(scoreFont, "You lost", new Vector2(225, 0), Color.Black);     
            }

            if (foodList.Count == 0)
            {
                spriteBatch.DrawString(scoreFont, "You won", new Vector2(225, 0), Color.Black);
            }

            spriteBatch.DrawString(scoreFont, "Score: " + score, scorePos, Color.Black);
            spriteBatch.DrawString(scoreFont, "Lives left: " + lives, livesPos, Color.Black);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
