using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    GameManager gm;

    public Image currParticleDisplay;
    public TMP_Text currParticleText;

    public GameObject particleDisplayContainer;
    public GameObject uiParticle;
    public Vector3 uiParticleInitialScale;
    public GameObject currUIParticle = null;
    List<GameObject> uiParticleList = new List<GameObject>();

    public float uiHeight;
    public float uiWidth;
    public float uiScale = 25;

    void Awake() {
        uiHeight = Camera.main.orthographicSize * 2;
        uiWidth = uiHeight * Camera.main.aspect;
        uiParticleInitialScale = uiParticle.transform.localScale;
    }

    void Start()
    {
        gm = GameManager.inst;

        for (int i = 0; i < gm.particleList.Count; i++) {
            GameObject newUIParticle = GameObject.Instantiate(uiParticle, new Vector3(i * (uiScale * 1.2f) + uiWidth / 2, (uiHeight * 1.8f), 0), Quaternion.identity, particleDisplayContainer.transform);
            newUIParticle.transform.Find("Image").GetComponent<Image>().sprite = gm.particleList[i].GetComponent<SpriteRenderer>().sprite;
            newUIParticle.transform.Find("Text").GetComponent<TMP_Text>().text = gm.particleList[i].GetComponent<Particle>().ToString();
            uiParticleList.Add(newUIParticle);
        }

        UpdateCurrParticle();
    }

    void Update()
    {
        if (Input.GetKeyDown("q")) {
            gm.ScrollParticle(-1);
        }
        if (Input.GetKeyDown("e")) {
            gm.ScrollParticle(1);
        }
    }

    public void UpdateCurrParticle() {
        if (currUIParticle) {
            currUIParticle.transform.localScale = uiParticleInitialScale;
        }
        uiParticleList[gm.particleIndex].transform.localScale = uiParticleInitialScale * 1.5f;
        currUIParticle = uiParticleList[gm.particleIndex];
    }
}