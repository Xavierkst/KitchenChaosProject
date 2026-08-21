using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] float rotateSpeed = 10f;
    private bool isWalking;
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

        isWalking = moveDir != Vector3.zero;
        transform.position += moveDir * Time.deltaTime * moveSpeed;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }
    public bool IsWalking() {
        return isWalking;
    }
}
