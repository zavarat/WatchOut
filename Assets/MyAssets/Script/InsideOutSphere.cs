using UnityEngine;
using System.Collections;
using System.Linq;

public class InsideOutSphere : MonoBehaviour
{

    [SerializeField]
    private MeshFilter meshFilter;
    private Mesh domeMesh;

	void Start ()
    {
        domeMesh = meshFilter.mesh;
        domeMesh.triangles = domeMesh.triangles.Reverse().ToArray();
    }
}
