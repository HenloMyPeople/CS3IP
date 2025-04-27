using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Meta.XR;
using Meta.XR.MRUtilityKit;

public class RuntimeNavMeshBuilder : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;


    void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        MRUK.Instance.RegisterSceneLoadedCallback(BuildNavMesh);
    }

    public void BuildNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }

}

