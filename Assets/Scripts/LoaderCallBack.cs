using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour {
    private bool isFirstLoad = true;

    private void Update() {
        if (isFirstLoad) {
            isFirstLoad = false;
            Loader.LoaderCallBack();
        }
    }
}
