using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DungeonGameTest
{
    public class Bbox
    {
        private Rectangle boundingBox;
        private Vector2 position;
        private int height;
        private int width;
        private Texture2D image;

        //bbox constructor, gets passed image and disired x and y coordiantes
        public Bbox(Texture2D img, float x, float y)
        {
            position = new Vector2(x, y);
            getSize(img);
            boundingBox = new Rectangle((int)x, (int)y, width, height);
            image = img;
        }

        //kinda don't need this anymore...
        public void createBbox(Texture2D image, float x, float y)
        {
            position.X = x;
            position.Y = y;
            getSize(image);
            boundingBox = new Rectangle((int)x, (int)y, width, height);
        }

        //gets size and width of image passed to bbox
        public void getSize(Texture2D image)
        {
            height = image.Height;
            width = image.Width;
        }

        //returns rectangle created by bbox from image
        public Rectangle getBboxRect()
        {
            return boundingBox;
        }

        //returns image passed to bbox object
        public Texture2D getImage()
        {
            return image;
        }
    }
}
