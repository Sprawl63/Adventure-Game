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
using Microsoft.Kinect;

namespace DungeonGameTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        int levelOneTrue;
        int levelTwoTrue;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        bool detected = false;
        //vector for hand location
        Vector2 righthandPosition = new Vector2();
        //font for writing to screen if need be
        SpriteFont font;
        //kinect sensor
        KinectSensor kinectSensor;
        //string to output if not connected
        string connectedStatus = "Not connected";
        //levels
        Level levelOne, levelTwo;
        //items bboxs
        Bbox torchBbox1, torchBbox2, fountainBbox;
        //item textures
        Texture2D torch, fountain;
        //level textures
        Texture2D level1, level2;
        //tasks
        CollisionTask levelOneT1, levelOneT2;
        CollisionTask levelTwoT1;
        //test hand
        Bbox rightHand;
        Texture2D handImage;
        //current level
        int currentLevel = 1;

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

            //hand = Content.Load<Texture2D>("crosshair");
            handImage = Content.Load<Texture2D>("hand");
            font = Content.Load<SpriteFont>("SpriteFont1"); //have to create a font size 8 named spritefont1
            DiscoverKinectSensor();

            base.Initialize();
            LoadContent(true);

        }

        protected void LoadContent(bool loadAllContent)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            levelOneTrue = 0;
            levelTwoTrue = 0;
            //load bbox images
            torch = Content.Load<Texture2D>("torch");
            fountain = Content.Load<Texture2D>("fountain");

            //load level images
            level1 = Content.Load<Texture2D>("level1");
            level2 = Content.Load<Texture2D>("level2");

            //load item bboxs
            torchBbox1 = new Bbox(torch, 600, 150, "fire");
            torchBbox2 = new Bbox(torch, 150, 150, "fire");
            fountainBbox = new Bbox(fountain, 250, 220, "water");

            //load levels
            levelOne = new Level(level1);
            levelTwo = new Level(level2);

            //right hand
            rightHand = new Bbox(handImage, righthandPosition.X, righthandPosition.Y, "normal");

            //fill level bbox lists
            levelOne.addBbox(torchBbox1);
            levelOne.addBbox(torchBbox2);
            levelTwo.addBbox(fountainBbox);

            //create tasks
            levelOneT1 = new CollisionTask(rightHand, torchBbox1, "fire");
            levelOneT2 = new CollisionTask(rightHand, torchBbox2, "fire");
            levelTwoT1 = new CollisionTask(rightHand, fountainBbox, "fire");

            //level one tasks
            levelOne.addTask(levelOneT1);
            levelOne.addTask(levelOneT2);

            //level two tasks
            levelTwo.addTask(levelTwoT1);
        }

        protected override void UnloadContent()
        {
            kinectSensor.Stop();
            kinectSensor.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            //processKeyboard(gameTime);

            rightHand.setBboxRect((int)righthandPosition.X, (int)righthandPosition.Y);

            if (currentLevel == 1)
            {
                foreach (Task task in levelOne.getTasks())
                {
                    if (task.isCompleted() == true)
                    {
                        levelOneTrue += task.getValue();
                    }
                }

                if (levelOneTrue == levelOne.getTasks().Count)
                {
                    currentLevel = 2;
                }
            }
            
            if (currentLevel == 2)
            {
                foreach (Task task in levelTwo.getTasks())
                {
                    if (task.isCompleted() == true)
                    {
                        levelTwoTrue += task.getValue();
                    }
                }

                if (levelTwoTrue == levelTwo.getTasks().Count)
                {
                    currentLevel = 1;
                }
            }
            
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            

            //display images that are in bbox list
            if (currentLevel == 1)
            {
                spriteBatch.Draw(levelOne.getBackground(), backPos, Color.White);
                foreach (Bbox item in levelOne.getBboxs())
                {
                    spriteBatch.Draw(item.getImage(), item.getBboxRect(), Color.White);
                }
            }
                /*
            else if (currentLevel == 2)
            {
                spriteBatch.Draw(levelTwo.getBackground(), backPos, Color.White);
                foreach (Bbox item in levelTwo.getBboxs())
                {
                    spriteBatch.Draw(item.getImage(), item.getBboxRect(), Color.White);
                }
            }
                 */
            else
            {
                spriteBatch.Draw(levelTwo.getBackground(), backPos, Color.White);
                foreach (Bbox item in levelTwo.getBboxs())
                {
                    spriteBatch.Draw(item.getImage(), item.getBboxRect(), Color.White);
                }
            }

            spriteBatch.Draw(handImage, righthandPosition, Color.White);

            if (detected == true)
            {
                spriteBatch.DrawString(font, "Skeleton Detected", new Vector2(0, 0), Color.GreenYellow);
            }
            else
            {
                spriteBatch.DrawString(font, "No Skeleton Detected: Move around a bit", new Vector2(0, 0), Color.GreenYellow);
            }

            spriteBatch.DrawString(font, connectedStatus, new Vector2(0, 10), Color.GreenYellow);
            spriteBatch.DrawString(font, levelOneTrue.ToString(), new Vector2(0, 20), Color.GreenYellow);
            spriteBatch.DrawString(font, currentLevel.ToString(), new Vector2(0, 30), Color.GreenYellow);
            spriteBatch.DrawString(font, rightHand.getElement(), new Vector2(0, 40), Color.GreenYellow);

            base.Draw(gameTime);
            spriteBatch.End();

        }

        public Vector2 lefthandPosition { get; set; }

        /*
        void processKeyboard(GameTime gameTime)
        {
            KeyboardState s = Keyboard.GetState();

            if (s.IsKeyDown(Keys.Left))
            {
                currentLevel = 1;
            }
            if (s.IsKeyDown(Keys.Right))
            {
                currentLevel = 2;
            }
        }
         */

        private void DiscoverKinectSensor()
        {
            foreach (KinectSensor sensor in KinectSensor.KinectSensors)
            {
                if (sensor.Status == KinectStatus.Connected)
                {
                    // Found one, set our sensor to this
                    kinectSensor = sensor;
                    break;
                }
            }

            if (this.kinectSensor == null)
            {
                connectedStatus = "Found none Kinect Sensors connected to USB";
                return;
            }

            // You can use the kinectSensor.Status to check for status
            // and give the user some kind of feedback
            switch (kinectSensor.Status)
            {
                case KinectStatus.Connected:
                    {
                        connectedStatus = "Status: Connected";
                        break;
                    }
                case KinectStatus.Disconnected:
                    {
                        connectedStatus = "Status: Disconnected";
                        break;
                    }
                case KinectStatus.NotPowered:
                    {
                        connectedStatus = "Status: Connect the power";
                        break;
                    }
                default:
                    {
                        connectedStatus = "Status: Error";
                        break;
                    }
            }

            // Init the found and connected device
            if (kinectSensor.Status == KinectStatus.Connected)
            {
                InitializeKinect();
            }
        }

        private bool InitializeKinect()
        {
            // Skeleton Stream
            kinectSensor.SkeletonStream.Enable(new TransformSmoothParameters()
            {
                Smoothing = 0.5f,
                Correction = 0.5f,
                Prediction = 0.5f,
                JitterRadius = 0.05f,
                MaxDeviationRadius = 0.04f
            });
            kinectSensor.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinectSensor_SkeletonFrameReady);

            try
            {
                kinectSensor.Start();
            }
            catch
            {
                connectedStatus = "Unable to start the Kinect Sensor";
                return false;
            }
            return true;
        }

        void kinectSensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    //int skeletonSlot = 0;
                    Skeleton[] skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletonData);
                    Skeleton playerSkeleton = (from s in skeletonData where s.TrackingState == SkeletonTrackingState.Tracked select s).FirstOrDefault();
                    if (playerSkeleton != null)
                    {
                        detected = true;
                        Joint rightHand = playerSkeleton.Joints[JointType.HandRight];
                        righthandPosition = new Vector2((((0.5f * rightHand.Position.X) + 0.5f) * (640)), (((-0.5f * rightHand.Position.Y) + 0.5f) * (480)));
                    }
                }
            }
        }
    }
}