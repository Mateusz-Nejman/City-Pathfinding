using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nejman.CPF;
using System;
using System.Collections.Generic;

namespace City_Pathfinding
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tex;
        
        private Texture2D _texture;
        private int mapX = 32, mapY = 32;
        private List<string> path;
        private string startPoint = "P_0_7";
        private string endPoint = "P_9_6";
        private SpriteFont font;
        //path = testMap.Find("P_0_7", "P_9_6");


        Map testMap;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            testMap = new Map();

            testMap.AddPoint(3, 0);
            testMap.AddPoint(2, 2);
            testMap.AddPoint(0, 4);
            testMap.AddPoint(0, 7);
            testMap.AddPoint(2, 4);
            testMap.AddPoint(3, 7);
            testMap.AddPoint(5, 6);
            testMap.AddPoint(6, 4);
            testMap.AddPoint(6, 6);
            testMap.AddPoint(6, 8);
            testMap.AddPoint(2, 10);
            testMap.AddPoint(5, 9);
            testMap.AddPoint(7, 9);
            testMap.AddPoint(8, 7);
            testMap.AddPoint(11, 6);
            testMap.AddPoint(9, 6);
            testMap.AddPoint(12, 3);
            testMap.AddPoint(9, 2);
            testMap.AddPoint(7, 3);
            testMap.AddPoint(5, 2);

            testMap.AddNeighbour("P_3_0", "P_2_2");
            testMap.AddNeighbour("P_2_2", "P_0_4");
            testMap.AddNeighbour("P_0_4", "P_0_7");

            testMap.AddNeighbour("P_0_7", "P_3_7");
            testMap.AddNeighbour("P_3_7", "P_5_6");
            testMap.AddNeighbour("P_5_6", "P_6_4");
            testMap.AddNeighbour("P_5_6", "P_6_6");
            testMap.AddNeighbour("P_5_6", "P_6_8");

            testMap.AddNeighbour("P_0_7", "P_2_4");

            testMap.AddNeighbour("P_0_7", "P_2_10");
            testMap.AddNeighbour("P_2_10", "P_5_9");
            testMap.AddNeighbour("P_5_9", "P_7_9");
            testMap.AddNeighbour("P_7_9", "P_8_7");
            testMap.AddNeighbour("P_8_7", "P_11_6");

            testMap.AddNeighbour("P_11_6", "P_9_6");

            testMap.AddNeighbour("P_11_6", "P_12_3");
            testMap.AddNeighbour("P_12_3", "P_9_2");
            testMap.AddNeighbour("P_9_2", "P_7_3");
            testMap.AddNeighbour("P_7_3", "P_5_2");

            path = testMap.Find(startPoint,endPoint);

            this.IsMouseVisible = true;
            this.Window.Title = "City Pathfinding Algorithm by Mateusz Nejman. Copyright 2019";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tex = Content.Load<Texture2D>("black");
            font = Content.Load<SpriteFont>("font");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (string key in testMap.Points.Keys)
            {
                int x = int.Parse(key.Split('_')[1]);
                int y = int.Parse(key.Split('_')[2]);

                Rectangle rect = new Rectangle(mapX + (x * 32) - 8, mapY + (y * 32) - 8, 16, 16);

                if(rect.Contains(Mouse.GetState().Position))
                {
                    if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        startPoint = key;
                        path = testMap.Find(startPoint, endPoint);
                    }
                    else if(Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        endPoint = key;
                        path = testMap.Find(startPoint, endPoint);
                    }
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //spriteBatch.Draw(tex, new Rectangle(0, 0, 16, 16), Color.White);

            foreach(string key in testMap.Points.Keys)
            {
                int x = int.Parse(key.Split('_')[1]);
                int y = int.Parse(key.Split('_')[2]);

                spriteBatch.Draw(tex, new Rectangle(mapX+(x * 32)-8, mapY+(y * 32)-8,16,16), Color.White);

                List<string> neighs = testMap.Points[key].Neighbours;

                for(int a = 0; a < neighs.Count; a++)
                {
                    int _x = int.Parse(neighs[a].Split('_')[1]);
                    int _y = int.Parse(neighs[a].Split('_')[2]);

                    DrawLine1(spriteBatch, new Vector2(mapX+(x*32), mapY+(y*32)), new Vector2(mapX+(_x*32), mapY+(_y*32)), Color.Black);
                }
            }

            for(int a = 0; a < path.Count; a++)
            {
                int x = int.Parse(path[a].Split('_')[1]);
                int y = int.Parse(path[a].Split('_')[2]);

                if (a < path.Count - 1)
                {
                    int _x = int.Parse(path[a+1].Split('_')[1]);
                    int _y = int.Parse(path[a + 1].Split('_')[2]);

                    DrawLine1(spriteBatch, new Vector2(mapX + (x * 32), mapY + (y * 32)), new Vector2(mapX + (_x * 32), mapY + (_y * 32)), Color.Gold);
                }

            }

            spriteBatch.DrawString(font, "Click LMB to change Start Point\nClick RMB to change End Point", new Vector2(400, 0), Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        private Texture2D GetTexture1(SpriteBatch spriteBatch)
        {
            if (_texture == null)
            {
                _texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                _texture.SetData(new[] { Color.White });
            }

            return _texture;
        }

        public void DrawLine1(SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness = 1f)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            DrawLine1(spriteBatch, point1, distance, angle, color, thickness);
        }

        public void DrawLine1(SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness = 1f)
        {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(GetTexture1(spriteBatch), point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }
    }
}
