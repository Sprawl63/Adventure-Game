using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DungeonGameTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D levelOneBg, item1Tex, item2Tex;
        Level firstLevel;
        Bbox item1, item2;

        Vector2 backPos = Vector2.Zero;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            base.Initialize();
            LoadContent(true);

        }

        protected void LoadContent(bool loadAllContent)
        {
            //loading images
            spriteBatch = new SpriteBatch(GraphicsDevice);
            levelOneBg = Content.Load<Texture2D>("level1");
            item1Tex = Content.Load<Texture2D>("item1");
            item2Tex = Content.Load<Texture2D>("item2");

            //creating items and adding to bbox lists
            firstLevel = new Level(levelOneBg);
            item1 = new Bbox(item1Tex, 100, 100);
            item2 = new Bbox(item2Tex, 150, 150);
            firstLevel.addBbox(item1);
            firstLevel.addBbox(item2);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Draw(firstLevel.getBackground(), backPos, Color.White);

            //display images that are in bbox list
            foreach(Bbox item in firstLevel.getBboxs())
            {
                spriteBatch.Draw(item.getImage(), item.getBboxRect(), Color.White);
            }

            base.Draw(gameTime);
            spriteBatch.End();

        }

    }
}
