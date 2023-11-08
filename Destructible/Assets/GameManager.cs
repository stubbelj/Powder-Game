using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst = null;
    public UIManager uiManager;

    public System.Random r = new System.Random();

    /*[System.Serializable]
    public class elementPrefabData {
        public GameObject prefab;
    }*/

    public Particle[][] grid;

    public List<GameObject> prefabData = new List<GameObject>();

    public List<GameObject> particleList = new List<GameObject>();

    public int particleIndex = 0;

    public float physicsUpdateDelta = 2f;
    
    public GameObject currParticle;

    void Awake() {
        if (inst == null) {
            inst = this;
        } else {
            Destroy(this);
        }

        foreach (GameObject prefab in prefabData) {
            particleList.Add(prefab);
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

    }

    public void SpawnParticle(GameObject particlePrefab, Vector3 pos) {
        Collider[] col = Physics.OverlapBox(TruncateVec(pos), new Vector3(0.45f, 0.45f, 0.45f), Quaternion.identity, 1 << LayerMask.NameToLayer("Particle"));
            if (col.Length > 0) {
                Destroy(col[0].gameObject);
            }
        GameObject.Instantiate(particlePrefab, TruncateVec(pos), Quaternion.identity);
    }

    public void SpawnParticle(System.Type type, Vector3 pos) {
        foreach(GameObject prefab in prefabData) {
            if (prefab.GetComponent<Particle>().GetType() == type) {
                SpawnParticle(prefab, pos);
            }
        }
    }

    public void ScrollParticle(int index) {
        if (index > 0) {
            if (particleIndex + index < particleList.Count) {
                particleIndex += index;
            } else {
                particleIndex = 0;
            }
        } else {
            if (0 <= particleIndex + index) {
                particleIndex += index;
            } else {
                particleIndex = particleList.Count - 1;
            }
        }

        currParticle = particleList[particleIndex];

        uiManager.UpdateCurrParticle();
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
