using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {
    // 
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    public int platesSpawnedAmountMax = 4;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateTaken;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0f;
            if (platesSpawnedAmount < platesSpawnedAmountMax) {
                ++platesSpawnedAmount;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject() && platesSpawnedAmount > 0) {
            --platesSpawnedAmount;
            // GetKitchenObject().SetKitchenObjectParent(player);
            KitchenObject.SpawnKitchenObject(player, plateKitchenObjectSO);
            OnPlateTaken?.Invoke(this, EventArgs.Empty);
        }
    }
}
