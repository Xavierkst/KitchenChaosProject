using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.Security.Cryptography;
// using System.Diagnostics;

public class Player : MonoBehaviour {

    public float moveSpeed = 7f;

    private void Update() {
        Vector2 inputVec = new Vector2(0, 0);

        if(Input.GetKey(KeyCode.W)) {
            inputVec.y = +1f;
        } 
        if (Input.GetKey(KeyCode.A)) {
            inputVec.x = -1f;
        } 
        if (Input.GetKey(KeyCode.S)) {
            inputVec.y = -1f;
        } 
        if (Input.GetKey(KeyCode.D)) {
            inputVec.x = +1f;
        }

        inputVec = inputVec.normalized;
        Vector3 moveDir = new Vector3(inputVec.x, 0f, inputVec.y);
        
        transform.position += moveDir * Time.deltaTime * moveSpeed;
        Debug.Log(transform.position);
    }
}
