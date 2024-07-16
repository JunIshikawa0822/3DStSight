using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Mesh_Test : MonoBehaviour
{
    public MeshFilter viewMeshFilterTest;
    Mesh viewMeshTest;

    // Start is called before the first frame update
    void Start()
    {
        viewMeshTest = new Mesh();
        viewMeshFilterTest.sharedMesh = viewMeshTest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        DrawFV(viewMeshTest);
    }

    void DrawFV(Mesh mesh)
    {
        Vector3[] vertices = new Vector3[] {
            transform.InverseTransformPoint(this.transform.position),
            transform.InverseTransformPoint(transform.position + new Vector3(-5, 0, 5)),
            transform.InverseTransformPoint(transform.position + new Vector3(5, 0, 5))
        };

        int[] triangles = new int[] {0, 1, 2};

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
