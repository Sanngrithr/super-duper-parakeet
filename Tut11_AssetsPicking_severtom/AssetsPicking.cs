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
    public class AssetsPicking : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;

        private TransformComponent _cabinTransform;
        private TransformComponent _baseTransform;
        private TransformComponent _lowerArmTransform;
        private TransformComponent _middleArmTransform;
        private TransformComponent _upperArmTransform;
        private TransformComponent _shovelTransform;

        private ScenePicker _scenePicker;
        private PickResult _currentPick;
        private float3 _oldColour;
        private TransformComponent _pickTransform;

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intentsity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);

            _scene = AssetStorage.Get<SceneContainer>("Excavator.fus");

            _scenePicker = new ScenePicker(_scene);

            _baseTransform = _scene.Children.FindNodes(node => node.Name == "ExcavatorBase")?.FirstOrDefault()?.GetTransform();
            _cabinTransform = _scene.Children.FindNodes(node => node.Name == "ExcavatorCabin")?.FirstOrDefault()?.GetTransform();
            _lowerArmTransform = _scene.Children.FindNodes(node => node.Name == "lowerArm")?.FirstOrDefault()?.GetTransform();
            _middleArmTransform = _scene.Children.FindNodes(node => node.Name == "middleArm")?.FirstOrDefault()?.GetTransform();
            _upperArmTransform = _scene.Children.FindNodes(node => node.Name == "upperArm")?.FirstOrDefault()?.GetTransform();
            _shovelTransform = _scene.Children.FindNodes(node => node.Name == "Shovel")?.FirstOrDefault()?.GetTransform();

            /*var scalehelper = new float3(0.5f,2,0.5f);
            _lowerArmTransform.Scale = scalehelper;
            _middleArmTransform.Scale = scalehelper;
            _upperArmTransform.Scale = scalehelper;*/

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, 0, 40) * float4x4.CreateRotationX(-(float) Atan(15.0 / 40.0));

            if (Mouse.LeftButton)
            {
                float2 pickPosClip = Mouse.Position * new float2(2.0f / Width, -2.0f / Height) + new float2(-1, 1);
                _scenePicker.View = RC.View;
                _scenePicker.Projection = RC.Projection;
                List<PickResult> pickResults = _scenePicker.Pick(pickPosClip).ToList();
                PickResult newPick = null;
                if (pickResults.Count > 0)
                {
                    pickResults.Sort((a, b) => Sign(a.ClipPos.z - b.ClipPos.z));
                    newPick = pickResults[0];
                }
                if (newPick?.Node != _currentPick?.Node)
                {
                    if (_currentPick != null)
                    {
                        _currentPick.Node.GetMaterial().Diffuse.Color = _oldColour;
                    }
                    if (newPick != null)
                    {
                        var mat = newPick.Node.GetMaterial();
                        _oldColour = mat.Diffuse.Color;
                        mat.Diffuse.Color = new float3(1, 0.4f, 0.4f);
                    }
                    _currentPick = newPick;
                    
                }
            }
                float cabinRot = _cabinTransform.Rotation.y;
                cabinRot += 0.1f * Keyboard.ADAxis;
                _cabinTransform.Rotation = new float3(0,cabinRot,0);

            if(_currentPick != null){
                _pickTransform = _currentPick.Node.GetTransform();
                float zRot = _pickTransform.Rotation.z;
                zRot += 0.1f * Keyboard.WSAxis;
                _pickTransform.Rotation = new float3(0,0,zRot);
            }
            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered farame) on the front buffer.
            Present();
        }


        // Is called when the window was resized
        public override void Resize()
        {
            // Set the new rendering area to the entire new windows size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45� Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}
