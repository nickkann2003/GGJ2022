/*
 * Nick Kannenberg
 * 1/28/2022
 * Camera Handler Class:
 * I dont know how this stuff works, so this is gonna simplify it 
 * with methods and make it easier to use throughout the program
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GGJ2022
{
    class CameraHandler
    {
        //Cam
        private OrthographicCamera camera;

        //Takes necessary stuff for cam
        public CameraHandler(GameWindow Window, GraphicsDevice GraphicsDevice)
        {
            //Makes cam
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            camera = new OrthographicCamera(viewportAdapter);
        }

        /// <summary>
        /// View matrix for sprite batch, transformMatrix: cam.GetCameraViewMatrix()
        /// </summary>
        /// <returns>View matrix of camera</returns>
        public Matrix GetCameraViewMatrix()
        {
            return camera.GetViewMatrix();
        }
    }
}
