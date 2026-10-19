using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake() {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start() {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateTaken += PlatesCounter_OnPlateTaken;
    }

    private void PlatesCounter_OnPlateTaken(object sender, System.EventArgs e) {
        int plateIdx = plateVisualGameObjectList.Count - 1;
        Destroy(plateVisualGameObjectList[plateIdx]);
        plateVisualGameObjectList.Remove(plateVisualGameObjectList[plateIdx]);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e) {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = 0.1f; 
        plateVisualTransform.localPosition = new Vector3(0, plateVisualGameObjectList.Count * plateOffsetY, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
