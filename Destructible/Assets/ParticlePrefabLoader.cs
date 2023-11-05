using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ParticlePrefabLoader : MonoBehaviour
{
    public GameManager gm;

    public bool reloadParticlePrefabs = false;

    void Update()
    {
        if (reloadParticlePrefabs) {
            ReloadParticlePrefabs();
            reloadParticlePrefabs = false;
        }
    }

    void ReloadParticlePrefabs() {
        string[] assetNames = AssetDatabase.FindAssets("t:prefab", new string[] {"Assets/Particles"});
        gm.prefabData = new List<GameObject>();
        foreach(string assetName in assetNames) {
            gm.prefabData.Add(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(assetName)));
        }
    }
}
