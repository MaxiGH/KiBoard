﻿using System;
using System.Collections.Generic;
using System.Numerics;

namespace KiBoard.calibration
{
    class KeyCalibrator : Calibrator
    {
        private List<CalibrationPoint> points;
        private List<Vector3> wallPoints;
        private tracker.Tracker tracker;

        public KeyCalibrator(tracker.Tracker t)
        {
            points = new List<CalibrationPoint>();
            wallPoints = new List<Vector3>();
            tracker = t;
            initiateWallPoints();
        }

        private void initiateWallPoints()
        {
            wallPoints.Add(new Vector3(0, 0, 0));
            wallPoints.Add(new Vector3(0, 1, 0));
            wallPoints.Add(new Vector3(1, 0, 0));
        }

        public List<CalibrationPoint> getCalibrationPoints()
        {
            if (hasCalibrationPoints())
            {
                return points;
            }
            throw new MethodAccessException("can't get CalibrationPoints; points.Count=" + points.Count);
        }

        public int getNumberOfPoints()
        {
            return wallPoints.Count;
        }

        private bool keyPressed()
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey();
                return true;
            }
            return false;
        }

        private void addPoint()
        {
            points.Add(new CalibrationPoint(tracker.getHandCollection().right.jointCoordinate, wallPoints[points.Count]));
            System.Console.WriteLine("CalibrationPoint " + points.Count + " set");
        }

        public void tick()
        {
            if (!hasCalibrationPoints() && keyPressed())
            {
                addPoint();
            }
        }

        public bool hasCalibrationPoints()
        {
            return (points.Count == getNumberOfPoints());
        }
    }
}