using System.Collections;
using UnityEngine;

public class ADSState : IGunState {

    public Transform ADSTransform;
    float animationTimer = 0f;
    float ADSTime = .5f;

    public ADSState(Transform ADSTransform) {
        this.ADSTransform = ADSTransform;
    }

    public void OnEnterState(GameObject character) {
        animationTimer = 0f;
        GunStateMachine gsm = character.GetComponent<GunStateMachine>();
        gsm.StartCoroutine(GunTransfer(character, gsm.hipTransform, gsm.ADSTransform));
    }

    public void OnExitState(GameObject character) {
        GunStateMachine csm = character.GetComponent<GunStateMachine>();
        csm.StopCoroutine("GunTransfer");
        animationTimer = 0f;
        csm.StartCoroutine(GunTransfer(character, csm.gunTransform, csm.hipTransform));
    }

    public void Tick(GameObject character) { }

    IEnumerator GunTransfer(GameObject character, Transform startTransform, Transform targetTransform) {
        GunStateMachine gsm = character.GetComponent<GunStateMachine>();
        while (animationTimer < ADSTime) {
            animationTimer += Time.unscaledDeltaTime;
            gsm.gunTransform.position = Vector3.Lerp(
                startTransform.position,
                targetTransform.position,
                animationTimer / ADSTime
            );

            gsm.gunTransform.rotation = Quaternion.Lerp(
                startTransform.rotation,
                targetTransform.rotation,
                animationTimer / ADSTime
            );
            yield return new WaitForEndOfFrame();
        }
        gsm.gunTransform.position = targetTransform.position;
        gsm.gunTransform.rotation = targetTransform.rotation;
        yield return null;
    }
}

