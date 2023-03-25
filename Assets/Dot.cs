using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    /**
     * Stores info to be copied to Mesh arrays
     */
    public class Dot
    {
        public Vector3[] vertices { get; set; } = new Vector3[4];
        public Vector2[] uv { get; set; } = new Vector2[4];
        public int[] triangles { get; set; } = new int[6];

        
        public override String ToString()
        {
            return String.Format("v {0}-{1}-{2}-{3} u {4}-{5}-{6}-{7} t {8}-{9}-{10}-{11}-{12}-{13}",
                vertices[0], vertices[1], vertices[2], vertices[3], uv[0], uv[1], uv[2], uv[3], 
                triangles[0], triangles[1], triangles[2], triangles[3], triangles[4], triangles[5]
                );
        }
    }
}