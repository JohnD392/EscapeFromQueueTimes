using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looting : MonoBehaviour
{
    public LayerMask itemLayerMask;
    public float maxDistance = 1.3f;

    private void Update() {
        Debug.DrawLine(transform.position, (transform.position + transform.forward) * maxDistance, Color.red, .2f);
        RaycastHit hit;
        bool didHit = Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, itemLayerMask);
        if(didHit) {
            Debug.Log("Player can loot " + hit.transform.name);
        }
    }
}
