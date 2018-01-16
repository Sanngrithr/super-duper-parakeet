﻿using System;
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
using static Fusee.Engine.Common.KeyCodes;

namespace Fusee.Tutorial.Core
{
    public class HierarchyInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private float _camAngle = 0;
        private TransformComponent _baseTransform;

        private TransformComponent _bodyTransform;
        private TransformComponent _upperArmTransform;
        private TransformComponent _lowerArmTransform;
        private TransformComponent _firstClawTransform;
        private TransformComponent _secondClawTransform;

        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };

            _bodyTransform = new TransformComponent
            {
                Rotation = new float3(0, 0.3f, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 6, 0) 
            };

            _upperArmTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(2, 4, 0)
            };

            _lowerArmTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(-2, 4, 0)
            };

            _firstClawTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 4, -0.5f)
            };

            _secondClawTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 4, 0.5f)
            };

            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNodeContainer>
                {
                    new SceneNodeContainer
                    {
                        Components = new List<SceneComponentContainer>
                        {
                            // TRANSFROM COMPONENT
                            _baseTransform,

                            // MATERIAL COMPONENT
                            new MaterialComponent
                            {
                                Diffuse = new MatChannelContainer { Color = new float3(0.7f, 0.7f, 0.7f) },
                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                            },

                            // MESH COMPONENT
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        }
                    },

                    new SceneNodeContainer
                    {
                        Components = new List<SceneComponentContainer>
                        {
                            _bodyTransform,
                            new MaterialComponent
                            {
                                Diffuse = new MatChannelContainer{Color = new float3(1,0,0)},
                                Specular = new SpecularChannelContainer{Color = new float3(1,1,1), Shininess = 5}
                            },
                            SimpleMeshes.CreateCuboid(new float3(2,10,2))
                        },

                        Children = new List<SceneNodeContainer>
                        {
                            new SceneNodeContainer
                            {
                                Components = new List<SceneComponentContainer>
                                {
                                    _upperArmTransform,
                                },
                                Children = new List<SceneNodeContainer>
                                {
                                    new SceneNodeContainer
                                    {
                                        Components = new List<SceneComponentContainer>
                                        {
                                            new TransformComponent
                                            {
                                                Rotation = new float3(0,0,0),
                                                Scale = new float3(1,1,1),
                                                Translation = new float3(0,4,0)
                                            },
                                            new MaterialComponent
                                            {
                                                Diffuse = new MatChannelContainer{Color = new float3(0,1,0)},
                                                Specular = new SpecularChannelContainer{Color = new float3(1,1,1), Shininess = 5}
                                            },
                                            SimpleMeshes.CreateCuboid(new float3(2,10,2))
                                        },

                                        Children = new List<SceneNodeContainer>
                                        {
                                            new SceneNodeContainer
                                            {
                                                Components = new List<SceneComponentContainer>
                                                {
                                                    _lowerArmTransform,
                                                },
                                                Children = new List<SceneNodeContainer>
                                                {
                                                    new SceneNodeContainer
                                                    {
                                                        Components = new List<SceneComponentContainer>
                                                        {
                                                            new TransformComponent
                                                            {
                                                                Rotation = new float3(0,0,0),
                                                                Scale = new float3(1,1,1),
                                                                Translation = new float3(0,4,0)
                                                            },
                                                            new MaterialComponent
                                                            {
                                                                Diffuse = new MatChannelContainer{Color = new float3(0,0,1)},
                                                                Specular = new SpecularChannelContainer{Color = new float3(1,1,1), Shininess = 5}
                                                            },
                                                            SimpleMeshes.CreateCuboid(new float3(2,10,2))
                                                        },
                                                        Children = new List<SceneNodeContainer>
                                                        {
                                                            new SceneNodeContainer
                                                            {
                                                                Components = new List<SceneComponentContainer>
                                                                {
                                                                    _firstClawTransform,
                                                                },
                                                                Children = new List<SceneNodeContainer>
                                                                {
                                                                    new SceneNodeContainer
                                                                    {
                                                                        Components = new List<SceneComponentContainer>
                                                                        {
                                                                            new TransformComponent
                                                                            {
                                                                                Rotation = new float3(0,0,0),
                                                                                Scale = new float3(1,1,1),
                                                                                Translation = new float3(0,2,0)
                                                                            },

                                                                            new MaterialComponent
                                                                            {
                                                                                Diffuse = new MatChannelContainer{Color = new float3(0,1,1)},
                                                                                Specular = new SpecularChannelContainer{Color = new float3(1,1,1), Shininess = 5}
                                                                            },
                                                                            SimpleMeshes.CreateCuboid(new float3(2, 4, 1))
                                                                        }
                                                                    }
                                                                }
                                                            },

                                                            new SceneNodeContainer
                                                            {
                                                                Components = new List<SceneComponentContainer>
                                                                {
                                                                    _secondClawTransform,
                                                                },
                                                                Children = new List<SceneNodeContainer>
                                                                {
                                                                    new SceneNodeContainer
                                                                    {
                                                                        Components = new List<SceneComponentContainer>
                                                                        {
                                                                             new TransformComponent
                                                                            {
                                                                                Rotation = new float3(0,0,0),
                                                                                Scale = new float3(1,1,1),
                                                                                Translation = new float3(0,2,0)
                                                                            },

                                                                            new MaterialComponent
                                                                            {
                                                                                Diffuse = new MatChannelContainer{Color = new float3(1,1,0)},
                                                                                Specular = new SpecularChannelContainer{Color = new float3(1,1,1), Shininess = 5}
                                                                            },
                                                                            SimpleMeshes.CreateCuboid(new float3(2, 4, 1))
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }   
                                                }
                                            }                                                                                     
                                        }
                                    }        
                                }
                            }
                        }
                    }
                }
            };
        }

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);

            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            float bodyRot = _bodyTransform.Rotation.y;
            bodyRot += 0.1f* Keyboard.LeftRightAxis;
            _bodyTransform.Rotation = new float3(0,bodyRot,0);

            float greenRot = _upperArmTransform.Rotation.x;
            greenRot += 0.1f * Keyboard.UpDownAxis;
            _upperArmTransform.Rotation = new float3(greenRot,0,0);
            
            float blueRot = _lowerArmTransform.Rotation.x;
            blueRot += 0.1f * Keyboard.WSAxis;
            _lowerArmTransform.Rotation = new float3(blueRot,0,0);

            float fclawRot = _firstClawTransform.Rotation.x;
            fclawRot += 0.05f * Keyboard.ADAxis;
            if(fclawRot >= -1.3f && fclawRot <=0)
            {
                _firstClawTransform.Rotation = new float3(fclawRot, 0, 0);
            }

            float sclawRot = _secondClawTransform.Rotation.x;
            sclawRot += -0.05f * Keyboard.ADAxis;
            if(sclawRot <= 1.3f && sclawRot >= 0)
            {
                _secondClawTransform.Rotation = new float3(sclawRot, 0, 0);
            }

            while(Keyboard.GetKey(Q) == true)
            {
                if(fclawRot >= -1.3f && fclawRot <=0)
                {
                    fclawRot += (DeltaTime * Keyboard.ADAxis) / 100;
                    sclawRot += -(DeltaTime * Keyboard.ADAxis) / 100;

                    _firstClawTransform.Rotation = new float3(fclawRot, 0, 0);
                    _secondClawTransform.Rotation = new float3(sclawRot, 0, 0);
                }
            }

            if(Mouse.LeftButton == true)
            {
                _camAngle = _camAngle - (DeltaTime * Mouse.Velocity.x) / 100;
            };


            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, -10, 50) * float4x4.CreateRotationY(_camAngle);

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

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(Fusee.Math.Core.M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}