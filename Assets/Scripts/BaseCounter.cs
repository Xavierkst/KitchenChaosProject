using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour {
    public virtual void interact(Player player) {
        Debug.LogError("BaseCounter interact called.");
    }
}
