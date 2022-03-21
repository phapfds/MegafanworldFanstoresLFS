using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ColliderFit : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private MeshCollider meshCollider;
    private Mesh mesh;
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        mesh = new Mesh();
    }

    void FixedUpdate()
    {
        skinnedMeshRenderer.BakeMesh(mesh);
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
    }
}
