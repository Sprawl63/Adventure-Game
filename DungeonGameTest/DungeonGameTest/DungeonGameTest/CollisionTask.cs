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
    public class CollisionTask : Task
    {
        //collision task from task interface
        //gets two bbox's and returns true of they are colliding and the the element is as desired
        bool Task.isCompleted(Bbox bbox1, Bbox bbox2, string el)
        {
            Rectangle box1 = bbox1.getBboxRect();
            if (bbox1.getBboxRect().Intersects(bbox2.getBboxRect()) && el == GameObject.getElement())
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
