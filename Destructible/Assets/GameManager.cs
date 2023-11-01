using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst = null;

    [System.Serializable]
    public class elementPrefabData {
        public Particle.Element element;
        public GameObject prefab;
    }

    public Particle[][] grid;

    public elementPrefabData[] prefabData;

    List<GameObject> particleList = new List<GameObject>();

    public int particleIndex = 0;

    public float physicsUpdateDelta = 2f;
    
    public GameObject currParticle;

    void Awake() {
        if (inst == null) {
            inst = this;
        } else {
            Destroy(this);
        }

        foreach (elementPrefabData data in prefabData) {
            particleList.Add(data.prefab);
        }

        currParticle = particleList[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKey("mouse 0")) {
            SpawnParticle(currParticle, mousPos);
        }

        if (Input.GetKeyDown("r")) {
            ScrollParticle();
        }
    }

    public void SpawnParticle(GameObject particlePrefab, Vector3 pos) {
        Collider[] col = Physics.OverlapBox(TruncateVec(pos), new Vector3(0.45f, 0.45f, 0.45f), Quaternion.identity, 1 << LayerMask.NameToLayer("Particle"));
            if (col.Length > 0) {
                Destroy(col[0].gameObject);
            }
        GameObject.Instantiate(particlePrefab, TruncateVec(pos), Quaternion.identity);
    }

    public void SpawnParticle(Particle.Element element, Vector3 pos) {
        foreach(elementPrefabData data in prefabData) {
            if (data.element == element) {
                SpawnParticle(data.prefab, pos);
            }
        }
    }

    public void ScrollParticle() {
        if (particleIndex < particleList.Count - 1) {
            particleIndex++;
        } else {
            particleIndex = 0;
        }
        currParticle = particleList[particleIndex];
    }

    public bool CheckSpace(Vector2 point) {
        Collider[] col = Physics.OverlapBox(TruncateVec(point), new Vector3(0.45f, 0.45f, 0.45f), Quaternion.identity, 1 << LayerMask.NameToLayer("Particle"));
        if (col.Length > 0) {
            return true;
        } else {
            return false;
        }
    }

    public Vector3 TruncateVec(Vector2 point) {
        return new Vector3((int)point.x, (int)point.y, 0);
    }

    public Particle GetParticle(Vector3 point) {
        Collider[] col = Physics.OverlapBox(TruncateVec(point), new Vector3(0.45f, 0.45f, 0.45f), Quaternion.identity, 1 << LayerMask.NameToLayer("Particle"));
        if (col.Length > 0) {
            return col[0].gameObject.GetComponent<Particle>();
        } else {
            return null;
        }
    }
}
