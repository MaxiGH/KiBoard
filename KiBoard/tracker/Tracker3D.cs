﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.Numerics;

namespace KiBoard.tracker
{
    class Tracker3D : Tracker
    {
        private KinectSensor sensor;
        private MultiSourceFrameReader multiReader;
        private Body[] bodyData;

        public Tracker3D(KinectSensor sensor, MultiSourceFrameReader multiReader)
        {
            this.sensor = sensor;
            this.multiReader = multiReader;
            Console.WriteLine("tracker created");
        }
        
        // Returns the coordinates of the right hand in an float array in Kinect-Space
        public HandCollection getHandCollection()
        {
            Hand l = new Hand();
            Hand r = new Hand();
            if (multiReader != null)
            {
                // Getting the latest Frameset
                var frame = multiReader.AcquireLatestFrame();
                if (frame != null)
                {
                    // Getting the latest bodyFrame
                    var bodyFrame = frame.BodyFrameReference.AcquireFrame();
                    if (bodyFrame != null)
                    {
                        if (bodyData == null)
                        {
                            // Creates the the array with the number of tracked Bodyies
                            bodyData = new Body[bodyFrame.BodyFrameSource.BodyCount];
                        }
                        // Save the Body-Jointpositions to bodyData
                        bodyFrame.GetAndRefreshBodyData(bodyData);
                        bodyFrame.Dispose();
                        bodyFrame = null;
                    }
                }
                frame = null;
                int index = -1;
                for (int i = 0; i < sensor.BodyFrameSource.BodyCount; i++)
                {
                    if (bodyData == null || bodyData[i] == null)
                    {
                        continue;
                    }
                    if (bodyData[i].IsTracked)
                    {
                        index = i;
                    }
                }
                if (index > -1)
                {
                    // We use the right Hand
                    float xPos = bodyData[index].Joints[JointType.HandRight].Position.X;
                    float yPos = bodyData[index].Joints[JointType.HandRight].Position.Y;
                    float zPos = bodyData[index].Joints[JointType.HandRight].Position.Z;
                    //Console.WriteLine("Rechte Hand {0}, {1}, {2}", xPos, yPos, zPos);

                    r.jointCoordinate = new Vector3(xPos, yPos, zPos);
                }
                else
                {
                    System.Console.WriteLine("no body found");
                }
            }

            return new HandCollection(l, r);
        }
    }
}
