﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fusee.Engine.Core;
using Fusee.Math.Core;
using Fusee.Serialization;

namespace Fusee.Tutorial.Core
{
    public static class SimpleMeshes 
    {
        public static MeshComponent CreateCuboid(float3 size)
        {
            return new MeshComponent
            {
                Vertices = new[]
                {
                    // left, bottom, front vertex
                    new float3(-0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 0  - belongs to left
                    new float3(-0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 1  - belongs to bottom
                    new float3(-0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 2  - belongs to front

                    // left, bottom, back vertex
                    new float3(-0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 3  - belongs to left
                    new float3(-0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 4  - belongs to bottom
                    new float3(-0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 5  - belongs to back

                    // left, up, front vertex
                    new float3(-0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 6  - belongs to left
                    new float3(-0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 7  - belongs to up
                    new float3(-0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 8  - belongs to front

                    // left, up, back vertex
                    new float3(-0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 9  - belongs to left
                    new float3(-0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 10 - belongs to up
                    new float3(-0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 11 - belongs to back

                    // right, bottom, front vertex
                    new float3( 0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 12 - belongs to right
                    new float3( 0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 13 - belongs to bottom
                    new float3( 0.5f*size.x, -0.5f*size.y, -0.5f*size.z), // 14 - belongs to front

                    // right, bottom, back vertex
                    new float3( 0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 15 - belongs to right
                    new float3( 0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 16 - belongs to bottom
                    new float3( 0.5f*size.x, -0.5f*size.y,  0.5f*size.z),  // 17 - belongs to back

                    // right, up, front vertex
                    new float3( 0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 18 - belongs to right
                    new float3( 0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 19 - belongs to up
                    new float3( 0.5f*size.x,  0.5f*size.y, -0.5f*size.z),  // 20 - belongs to front

                    // right, up, back vertex
                    new float3( 0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 21 - belongs to right
                    new float3( 0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 22 - belongs to up
                    new float3( 0.5f*size.x,  0.5f*size.y,  0.5f*size.z),  // 23 - belongs to back

                },
                Normals = new[]
                {
                    // left, bottom, front vertex
                    new float3(-1,  0,  0), // 0  - belongs to left
                    new float3( 0, -1,  0), // 1  - belongs to bottom
                    new float3( 0,  0, -1), // 2  - belongs to front

                    // left, bottom, back vertex
                    new float3(-1,  0,  0),  // 3  - belongs to left
                    new float3( 0, -1,  0),  // 4  - belongs to bottom
                    new float3( 0,  0,  1),  // 5  - belongs to back

                    // left, up, front vertex
                    new float3(-1,  0,  0),  // 6  - belongs to left
                    new float3( 0,  1,  0),  // 7  - belongs to up
                    new float3( 0,  0, -1),  // 8  - belongs to front

                    // left, up, back vertex
                    new float3(-1,  0,  0),  // 9  - belongs to left
                    new float3( 0,  1,  0),  // 10 - belongs to up
                    new float3( 0,  0,  1),  // 11 - belongs to back

                    // right, bottom, front vertex
                    new float3( 1,  0,  0), // 12 - belongs to right
                    new float3( 0, -1,  0), // 13 - belongs to bottom
                    new float3( 0,  0, -1), // 14 - belongs to front

                    // right, bottom, back vertex
                    new float3( 1,  0,  0),  // 15 - belongs to right
                    new float3( 0, -1,  0),  // 16 - belongs to bottom
                    new float3( 0,  0,  1),  // 17 - belongs to back

                    // right, up, front vertex
                    new float3( 1,  0,  0),  // 18 - belongs to right
                    new float3( 0,  1,  0),  // 19 - belongs to up
                    new float3( 0,  0, -1),  // 20 - belongs to front

                    // right, up, back vertex
                    new float3( 1,  0,  0),  // 21 - belongs to right
                    new float3( 0,  1,  0),  // 22 - belongs to up
                    new float3( 0,  0,  1),  // 23 - belongs to back
                },
                Triangles = new ushort[]
                {
                    0,  6,  3,     3,  6,  9,  // left
                    2, 14, 20,     2, 20,  8,  // front
                    12, 15, 18,    15, 21, 18, // right
                    5, 11, 17,    17, 11, 23,  // back
                    7, 22, 10,     7, 19, 22,  // top
                    1,  4, 16,     1, 16, 13,  // bottom 
                },
                UVs = new float2[]
                {
                    // left, bottom, front vertex
                    new float2( 1,  0), // 0  - belongs to left
                    new float2( 1,  0), // 1  - belongs to bottom
                    new float2( 0,  0), // 2  - belongs to front

                    // left, bottom, back vertex
                    new float2( 0,  0),  // 3  - belongs to left
                    new float2( 1,  1),  // 4  - belongs to bottom
                    new float2( 1,  0),  // 5  - belongs to back

                    // left, up, front vertex
                    new float2( 1,  1),  // 6  - belongs to left
                    new float2( 0,  0),  // 7  - belongs to up
                    new float2( 0,  1),  // 8  - belongs to front

                    // left, up, back vertex
                    new float2( 0,  1),  // 9  - belongs to left
                    new float2( 0,  1),  // 10 - belongs to up
                    new float2( 1,  1),  // 11 - belongs to back

                    // right, bottom, front vertex
                    new float2( 0,  0), // 12 - belongs to right
                    new float2( 0,  0), // 13 - belongs to bottom
                    new float2( 1,  0), // 14 - belongs to front

                    // right, bottom, back vertex
                    new float2( 1,  0),  // 15 - belongs to right
                    new float2( 0,  1),  // 16 - belongs to bottom
                    new float2( 0,  0),  // 17 - belongs to back

                    // right, up, front vertex
                    new float2( 0,  1),  // 18 - belongs to right
                    new float2( 1,  0),  // 19 - belongs to up
                    new float2( 1,  1),  // 20 - belongs to front

                    // right, up, back vertex
                    new float2( 1,  1),  // 21 - belongs to right
                    new float2( 1,  1),  // 22 - belongs to up
                    new float2( 0,  1),  // 23 - belongs to back                    
                },
                BoundingBox = new AABBf(-0.5f * size, 0.5f*size)
            };
        }

        public static MeshComponent CreateCylinder(float radius, float height, int segments)
        {
            float3[] verts = new float3[4 * segments + 2];
            float3[] norms = new float3[4 * segments + 2];
            ushort[] tris = new ushort[segments * 12];

            float delta = 2 * M.Pi / segments;

            verts[4 * segments] = new float3(0, 0.5f * height, 0);
            norms[4 * segments] = float3.UnitY;
            verts[4 * segments + 1] = new float3(0, -0.5f * height, 0);
            norms[4 * segments + 1] = -(float3.UnitY);

            verts[0] = new float3(radius, 0.5f * height, 0);
            norms[0] = float3.UnitY;
            verts[1] = new float3(radius, 0.5f * height, 0);
            norms[1] = new float3(M.Cos(0), 0, M.Sin(0));
            verts[2] = new float3(radius, -0.5f * height, 0);
            norms[2] = new float3(M.Cos(0), 0, M.Sin(0));
            verts[3] = new float3(radius, -0.5f * height, 0);
            norms[3] = -(float3.UnitY);


            for (int i = 1; i < segments; i++)
            {
                verts[4 * i]     = new float3(radius * M.Cos(i * delta), 0.5f * height, radius * M.Sin(i * delta));
                verts[4 * i + 1] = new float3(radius * M.Cos(i * delta), 0.5f * height, radius * M.Sin(i * delta));
                verts[4 * i + 2] = new float3(radius * M.Cos(i * delta), -0.5f * height, radius * M.Sin(i * delta));
                verts[4 * i + 3] = new float3(radius * M.Cos(i * delta), -0.5f * height, radius * M.Sin(i * delta));

                norms[4 * i] = float3.UnitY;
                norms[4 * i + 1] = new float3(M.Cos(i * delta), 0, M.Sin(i * delta));
                norms[4 * i + 2] = new float3(M.Cos(i * delta), 0, M.Sin(i * delta));
                norms[4 * i + 3] = -(float3.UnitY);
                //top triangle
                tris[12*(i-1) + 0] = (ushort) (4 * segments);       // top center point
                tris[12*(i-1) + 1] = (ushort) (4 * (i-1) + 0);      // previous top segment point
                tris[12*(i-1) + 2] = (ushort) (4 * i + 0);      // current top segment point
                //shell triangle 1
                tris[12*(i-1) + 3] = (ushort) (4 * (i-1) + 2);      // previous lower shell point
                tris[12*(i-1) + 4] = (ushort) (4 * i + 2);      // current lower shell point
                tris[12*(i-1) + 5] = (ushort) (4 * i + 1);      // current top shell point
                //shell triangle 2
                tris[12*(i-1) + 6] = (ushort) (4 * (i-1) + 2);      // previous lower shell point
                tris[12*(i-1) + 7] = (ushort) (4 * i + 1);     // current top shell point
                tris[12*(i-1) + 8] = (ushort) (4 * (i-1) + 1);      // previous top shell point
                //bottom triangle
                tris[12*(i-1) + 9]  = (ushort) (4 * segments + 1);    // bottom center point
                tris[12*(i-1) + 10] = (ushort) (4 * i + 3);     // current bottom segment point
                tris[12*(i-1) + 11] = (ushort) (4 * (i-1) + 3);    // previous bottom segment point
            }

                tris[12*segments - 12] = (ushort) 32; 
                tris[12*segments - 11] = (ushort) 28;     
                tris[12*segments - 10]  = (ushort) 0;    
                //shell triangle 1
                tris[12*segments - 9] = (ushort) 30;     
                tris[12*segments - 8] = (ushort) 2;     
                tris[12*segments - 7] = (ushort) 1;    
                //shell triangle 2
                tris[12*segments - 6] = (ushort) 30;   
                tris[12*segments - 5] = (ushort) 1;    
                tris[12*segments - 4] = (ushort) 29;    
                //bottom triangle
                tris[12*segments - 3]  = (ushort) 33;
                tris[12*segments - 2] = (ushort) 3;   
                tris[12*segments - 1] = (ushort) 31;

            return new MeshComponent
            {
                Vertices = verts,
                Normals = norms,
                Triangles = tris
            };
        }

        public static MeshComponent CreateCone(float radius, float height, int segments)
        {
            return CreateConeFrustum(radius, 0.0f, height, segments);
        }

        public static MeshComponent CreateConeFrustum(float radiuslower, float radiusupper, float height, int segments)
        {
            throw new NotImplementedException();
        }


        public static MeshComponent CreatePyramid(float baselen, float height)
        {
            throw new NotImplementedException();
        }
        public static MeshComponent CreateTetrahedron(float edgelen)
        {
            throw new NotImplementedException();
        }

        public static MeshComponent CreateTorus(float mainradius, float segradius, int segments, int slices)
        {
            throw new NotImplementedException();
        }

    }
}
