using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnObjectPickup += Player_OnObjectPickup;
        BaseCounter.OnObjectPutdown += BaseCounter_OnObjectPutdown;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
        PlayerSound.OnPlayerMove += PlayerSound_OnPlayerMove;
    }

    private void PlayerSound_OnPlayerMove(object sender, System.EventArgs e) {
        PlaySound(audioClipRefSO.footstep, Player.Instance.transform.position);
    }

    private void TrashCounter_OnObjectTrashed(object sender, System.EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnObjectPutdown(object sender, System.EventArgs e) {
        BaseCounter counter = sender as BaseCounter;
        PlaySound(audioClipRefSO.objectPutdown, counter.transform.position);
    }

    private void Player_OnObjectPickup(object sender, System.EventArgs e) {
        PlaySound(audioClipRefSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {
        PlaySound(audioClipRefSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        PlaySound(audioClipRefSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
