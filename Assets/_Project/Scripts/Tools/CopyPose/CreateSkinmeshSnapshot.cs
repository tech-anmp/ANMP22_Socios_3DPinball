using NaughtyAttributes;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class CreateSkinmeshSnapshot : MonoBehaviour
{
    [Button]
    private void CreateSnapshot()
    {
        SkinnedMeshRenderer mr = GetComponent<SkinnedMeshRenderer>();
        Mesh sm = new Mesh();
        mr.BakeMesh(sm);

#if UNITY_EDITOR
        AssetDatabase.CreateAsset(sm, "Assets//"+mr.name+".mesh");
#endif
    }
}
