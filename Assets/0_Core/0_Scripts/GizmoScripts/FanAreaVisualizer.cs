using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Egliss
{
    public class GizmoExtension
    {
        [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.InSelectionHierarchy)]
        private static void DrawGizmo(FanViewer view, GizmoType i_gizmoType)
        {
            //can't draw
            if (view.distance <= 0.0f)
                return;

            //Drawfanを実行
            DrawFan(view);
        }

        //DrawFanA内で実行
        private static void DrawFan(FanViewer view)
        {
            Gizmos.color = view.color;

            Vector3 pos = view.transform.position;
            Quaternion rot = view.transform.rotation;
            Vector3 scale = Vector3.one * view.distance;

            Mesh fanMesh = CreateFanMesh(view.fovX, view.quality);
            Gizmos.DrawMesh(fanMesh, pos, rot, scale);

            if (view.isIgnoreYFan)
                return;

            Mesh fanMesh2 = CreateFanMesh(view.fovY, view.quality);
            Gizmos.DrawMesh(fanMesh2, pos, rot * Quaternion.AngleAxis(90.0f, Vector3.forward), scale);
        }

        //DrawFan内で実行
        private static Mesh CreateFanMesh(float angle, int triangleCount)
        {
            var mesh = new Mesh();

            mesh.vertices = CreateFanVertexBuffer(angle, triangleCount);
            mesh.triangles = CreateFanIndexBuffer(triangleCount);
            mesh.RecalculateNormals();

            return mesh;
        }

        //CreateFanMesh内で実行
        private static int[] CreateFanIndexBuffer(int triangleCount)
        {
            int[] indexBuffer = new int[triangleCount * 6];
            for (int L10 = 0; L10 < triangleCount; L10++)
            {
                int index = L10 * 3;
                int index2 = triangleCount * 3 + index;

                //front
                indexBuffer[index + 1] = L10 + 1;
                indexBuffer[index + 2] = L10 + 2;
                //backface
                indexBuffer[index2 + 1] = L10 + 2;
                indexBuffer[index2 + 2] = L10 + 1;
            }
            return indexBuffer;
        }

        //CreateFanMesh内で実行
        private static Vector3[] CreateFanVertexBuffer(float angle, int triangleCount)
        {
            Vector3[] vertexBuffer = new Vector3[triangleCount + 2];

            // startpoint
            vertexBuffer[0] = Vector3.zero;

            float startRad = -angle / 2;
            float angleStep = angle / triangleCount;

            for (int L10 = 0; L10 < triangleCount + 1; L10++)
            {
                float nowRad = (startRad + angleStep * L10) * Mathf.Deg2Rad;
                vertexBuffer[L10 + 1] = new Vector3(Mathf.Sin(nowRad), 0.0f, Mathf.Cos(nowRad));
            }
            return vertexBuffer;
        }
    }
}
