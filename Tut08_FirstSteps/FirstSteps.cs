using System;
using System.Collections.Generic;
using System.Linq;
using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static System.Math;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;

namespace Fusee.Tutorial.Core
{
    public class FirstSteps : RenderCanvas
    {   
        //Cube Generation Variables
        private int cubenumber = 0;
        private float cubeplacer = 0.0f;

        private TransformComponent _cubeTransform;
        private TransformComponent _cubeTransform2;
        private TransformComponent newcubetrans;
        private TransformComponent randcubetrans;
        private float _camAngle = 0;
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer(intensities in R, G, B, A).
            RC.ClearColor = new float4(0, 1, 0.5f, 1);

            _cubeTransform = new TransformComponent{
                Scale = new float3(1,1,1), 
                Translation = new float3(0,0,0),
                Rotation = new float3(45,45,0)
                
            };
            var cmat = new MaterialComponent{
                Diffuse = new MatChannelContainer{Color = new float3(0,0,1)},
                Specular = new SpecularChannelContainer{Color = float3.One, Shininess = 4}
            };
            var cmesh = SimpleMeshes.CreateCuboid(new float3(10,10,10));

            var cnode = new SceneNodeContainer();
            cnode.Components = new List<SceneComponentContainer>();
            cnode.Components.Add(_cubeTransform);
            cnode.Components.Add(cmat);
            cnode.Components.Add(cmesh);

            _scene = new SceneContainer();
            _scene.Children = new List<SceneNodeContainer>();
            _scene.Children.Add(cnode);

            _cubeTransform2 = new TransformComponent{
                Scale = new float3(1,1,1), 
                Translation = new float3(0,0,0),
                Rotation = new float3(45,45,0)
                
            };
            var cmat2 = new MaterialComponent{
                Diffuse = new MatChannelContainer{Color = new float3(1,0,1)},
                Specular = new SpecularChannelContainer{Color = float3.One, Shininess = 4}
            };
            var cmesh2 = SimpleMeshes.CreateCuboid(new float3(10,10,10));

            var cnode2 = new SceneNodeContainer();
            cnode2.Components = new List<SceneComponentContainer>();
            cnode2.Components.Add(_cubeTransform2);
            cnode2.Components.Add(cmat2);
            cnode2.Components.Add(cmesh);

            _scene.Children.Add(cnode2);

            //Cube Generation Block

            //Cube ring around the spinning subes
            while(cubenumber<8){
                newcubetrans = new TransformComponent{
                    Scale = new float3(1,1,1),
                    Translation = new float3(25*M.Sin(cubeplacer),25*M.Cos(cubeplacer),20*M.Sin(cubeplacer)),
                    Rotation = new float3(0,0,0)
                };
                var newmat = new MaterialComponent{
                    Diffuse = new MatChannelContainer{Color = new float3(1,0,0)},
                    Specular = new SpecularChannelContainer{Color = float3.One, Shininess = 4}
                };
                var newnode = new SceneNodeContainer();
                newnode.Components = new List<SceneComponentContainer>();
                newnode.Components.Add(newcubetrans);
                newnode.Components.Add(newmat);
                newnode.Components.Add(cmesh);

                _scene.Children.Add(newnode);

                cubenumber++;
                cubeplacer = cubeplacer + 0.8f;
            };

            //Random cube field around the central cube ring
            Random random = new Random();

            while(cubenumber<50){

                int xrnd = random.Next(-100, 100);
                if(xrnd<50 && xrnd>-50){
                    if(xrnd>=0){
                        xrnd = xrnd + 50;
                    }
                    xrnd = xrnd - 50;
                };
                int yrnd = random.Next(-100, 100);
                if(yrnd<50 && yrnd>-50){
                    if(yrnd>=0){
                        yrnd = yrnd + 50;
                    }
                    yrnd = yrnd - 50;
                };
                int zrnd = random.Next(-100, 100);
                if(zrnd<50 && zrnd>-50){
                    if(zrnd>=0){
                        zrnd = zrnd + 50;
                    }
                    zrnd = zrnd - 50;
                };

                randcubetrans = new TransformComponent{
                    Scale = new float3(1,1,1),
                    Translation = new float3(xrnd,yrnd,zrnd),
                    Rotation = new float3(25*M.Sin(cubeplacer),25*M.Cos(cubeplacer),20*M.Sin(cubeplacer))
                };
                var randmat = new MaterialComponent{
                    Diffuse = new MatChannelContainer{Color = new float3(1,1,0)},
                    Specular = new SpecularChannelContainer{Color = float3.One, Shininess = 4}
                };
                var randnode = new SceneNodeContainer();
                randnode.Components = new List<SceneComponentContainer>();
                randnode.Components.Add(randcubetrans);
                randnode.Components.Add(randmat);
                randnode.Components.Add(cmesh);

                _scene.Children.Add(randnode);

                cubenumber++;
                cubeplacer = cubeplacer + 0.7f;
            };
            

            _sceneRenderer = new SceneRenderer(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            _cubeTransform.Translation = new float3(8 * M.Cos(4 * TimeSinceStart),8 * M.Sin(4 * TimeSinceStart), 0);

            _cubeTransform.Rotation = new float3(10*M.Sin(TimeSinceStart), 0, 0);
            _cubeTransform2.Rotation = new float3(-10*M.Sin(TimeSinceStart), 0, 0);

            _cubeTransform2.Translation = new float3(-8 * M.Cos(4 * TimeSinceStart),-8 * M.Sin(4 * TimeSinceStart), 0);

            _camAngle = _camAngle + 90.0f * M.Pi/180.0f * DeltaTime;

    	    RC.View = float4x4.CreateTranslation(0,0,250) * float4x4.CreateRotationY(_camAngle);

            _sceneRenderer.Render(RC);


            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }


        // Is called when the window was resized
        public override void Resize()
        {
            // Set the new rendering area to the entire new windows size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}