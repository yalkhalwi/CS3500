﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// This class contains the default constants of the AgCubio world.
    /// </summary>
    public static class Constants
    {
        public const int Port = 11000;
        public const int Height = 1000, Width = Height;
        public const int HeartbeatsPerSecond = 25;
        public const int TopSpeed = 5;
        public const int LowSpeed = 1;
        public const int AttritionRate = 200;
        public const int FoodValue = 1;
        public const float PlayerStartMass = 1000;
        public const int MaxSplitDistance = 150;
        public const int MaxFood = 5000;
        public const float MinSplitMass = 100;
        public const double AbsorbConstant = 1.25;
        public const float MaxViewRange = 10000;
    }
}
