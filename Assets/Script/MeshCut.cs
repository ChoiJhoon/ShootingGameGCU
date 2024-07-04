using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCut : MonoBehaviour
{
    public Transform cuttingPlane;

    private void CutMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter is not attached to the GameObject.");
            return;
        }

        Mesh mesh = meshFilter.mesh;
        Plane plane = new Plane(cuttingPlane.up, cuttingPlane.position);

        List<Vector3> vertices = new List<Vector3>(mesh.vertices);
        List<int> triangles = new List<int>(mesh.triangles);
        List<Vector3> newVertices = new List<Vector3>();
        List<int> newTriangles = new List<int>();

        for (int i = 0; i < triangles.Count; i += 3)
        {
            Vector3 v1 = vertices[triangles[i]];
            Vector3 v2 = vertices[triangles[i + 1]];
            Vector3 v3 = vertices[triangles[i + 2]];

            bool side1 = plane.GetSide(v1);
            bool side2 = plane.GetSide(v2);
            bool side3 = plane.GetSide(v3);

            if (side1 == side2 && side2 == side3)
            {
                // All vertices are on the same side of the plane
                if (side1)
                {
                    newTriangles.Add(newVertices.Count);
                    newTriangles.Add(newVertices.Count + 1);
                    newTriangles.Add(newVertices.Count + 2);
                }

                newVertices.Add(v1);
                newVertices.Add(v2);
                newVertices.Add(v3);
            }
            else
            {
                // One or more vertices are on different sides of the plane
                // Split the triangle by the plane
                // Note: This is a simplification and won't work for all cases
                // Proper implementation requires more complex logic
            }
        }

        Mesh newMesh = new Mesh();
        newMesh.vertices = newVertices.ToArray();
        newMesh.triangles = newTriangles.ToArray();
        newMesh.RecalculateNormals();

        GameObject newObject = new GameObject("Cut Mesh");
        newObject.AddComponent<MeshFilter>().mesh = newMesh;
        newObject.AddComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CutMesh();
        }
    }
}
