using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

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

        string[] particleFile = File.ReadAllLines("Assets/Particle.cs");

        int startIndex = -1;
        int endIndex = -1;
        for(int i = 0; i < particleFile.Length; i++) {
            if (particleFile[i] =="    static Dictionary<System.Type, ParticleData> particleData = new Dictionary<System.Type, ParticleData>() {") {
                startIndex = i;
            }
            if (startIndex != -1 && endIndex == -1 && particleFile[i] == "    };") {
                endIndex = i;
            }
        }

        string insertText = "";
        List<string> insertTextList = new List<string>();

        foreach(string assetName in assetNames) {
            string particlePath = AssetDatabase.GUIDToAssetPath(assetName);
            gm.prefabData.Add(AssetDatabase.LoadAssetAtPath<GameObject>(particlePath));
            string particleName = particlePath.Substring(particlePath.LastIndexOf('/') + 1, particlePath.Length - particlePath.LastIndexOf('/') - 8);

            insertTextList.Add("{typeof(" + particleName + "), new ParticleData(" + '\n' + "newMass : 100," + '\n' + "newMatterState : MatterState.Solid"+ '\n' + ")}," + '\n');

            GameObject newPrefab = gm.prefabData[0];
            newPrefab.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Particles/" + particleName + ".png");
            DestroyImmediate(newPrefab.GetComponent<Particle>(), true);
            newPrefab.AddComponent(AssetDatabase.LoadAssetAtPath<Particle>("Assets/Particles/" + particleName + ".cs").GetType());
        }

        string[] newParticleFile = new string[particleFile.Length + insertTextList.Count];
        for (int i = 0; i <= startIndex; i++) {
            newParticleFile[i] = particleFile[i];
        }
        for (int i = 0; i < insertTextList.Count; i++) {
            newParticleFile[startIndex + i + 1] = insertTextList[i];
        }
        for (int i = 0; i < particleFile.Length - endIndex; i++) {
            newParticleFile[endIndex + i + 1] = particleFile[endIndex + i];
        }
        File.WriteAllLines("Assets/Particle.cs", newParticleFile);
    }
}
