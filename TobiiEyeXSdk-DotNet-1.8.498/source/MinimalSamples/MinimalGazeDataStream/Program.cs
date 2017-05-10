﻿//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

namespace MinimalGazeDataStream
{
    using EyeXFramework;
    using System;
    using Tobii.EyeX.Framework;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    //using System.Windows.Input;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var vim = new Vim();
            vim.Foreground();

            var i = 0;
          

            using (var eyeXHost = new EyeXHost())
            {
                // Create a data stream: lightly filtered gaze point data.
                // Other choices of data streams include EyePositionDataStream and FixationDataStream.
                using (var lightlyFilteredGazeDataStream = eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered))
                {
                    // Start the EyeX host.
                    eyeXHost.Start();

                    // Write the data to the console.

                    lightlyFilteredGazeDataStream.Next += (s, e) =>
                    {
                        i++;
                        if (i%100 != 0)
                        {
                            return;
                        }

                      
                                          
                        //MouseOperations.SetCursorPosition((int)e.X, (int)e.Y);
                        //MouseButtons.Left;
                        vim.Go(e.X, e.Y);
                        /*if (e.X < 960)
                        {
                            vim.Go("h");
                        }
                        else
                        {
                            vim.Go("l");
                        }

                        if (e.Y < 540)
                        {
                            vim.Go("k");
                        }
                        else
                        {
                            vim.Go("j");
                        }*/

                        Console.WriteLine("Gaze point at ({0:0.0}, {1:0.0}) @{2:0}", e.X, e.Y, e.Timestamp);
                    };

                    // Let it run until a key is pressed.
                    Console.WriteLine("Listening for gaze data, press any key to exit...");
                    Console.In.Read();
                }
            }
        }
    }
}
