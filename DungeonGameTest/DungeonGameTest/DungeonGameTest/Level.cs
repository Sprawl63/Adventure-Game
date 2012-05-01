using System;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace DungeonGameTest
{
    class Level
    {
        //level class containing lists for level bbox's and tasks
        private Texture2D backgroundImage;
        private List<Task> tasks;
        private List<Bbox> bboxes;

        public Level(Texture2D bg)
        {
            backgroundImage = bg;
            tasks = new List<Task>();
            bboxes = new List<Bbox>();
        }

        //add task to task list
        public void addTask(Task t)
        {
            tasks.Add(t);
        }

        //remove task from task list
        public void removeTask(Task t)
        {
            tasks.Remove(t);
        }

        //add bbox to bbox list
        public void addBbox(Bbox b)
        {
            bboxes.Add(b);
        }

        //remove bbox from bbox list
        public void removeBbox(Bbox b)
        {
            bboxes.Remove(b);
        }

        //return level background image
        public Texture2D getBackground()
        {
            return backgroundImage;
        }

        //return bbox list
        public List<Bbox> getBboxs()
        {
            return bboxes;
        }

        //return task list
        public List<Task> getTasks()
        {
            return tasks;
        }
    }
}
