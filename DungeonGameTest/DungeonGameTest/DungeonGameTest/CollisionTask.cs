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
        private Bbox bbox1, bbox2;
        string element;
        private int num = 1;

        public CollisionTask(Bbox b1, Bbox b2, string el)
        {
            bbox1 = b1;
            bbox2 = b2;
            element = el;
        }

        //gets two bbox's and returns true of they are colliding and the the element is as desired
        //bool Task.isCompleted(Bbox bbox1, Bbox bbox2, string element)
        bool Task.isCompleted()
        {
            Rectangle box1 = bbox1.getBboxRect();
            if (bbox1.getBboxRect().Intersects(bbox2.getBboxRect()) && bbox1.getElement().Equals(bbox2.getElement()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        int Task.getValue()
        {
            int k = num;
            
            if (num == 1)
            {
                num--;
                return k;
            }
            else
            {
                return 0;
            }
        }

    }
}
