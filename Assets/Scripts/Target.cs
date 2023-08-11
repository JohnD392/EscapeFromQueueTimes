using UnityEngine;

public class Target : MonoBehaviour {
    public GameObject hitMarkerPrefab;

    private void OnTriggerEnter(Collider other) {
        GameObject hitMarker = Instantiate(hitMarkerPrefab, other.transform.position, Quaternion.identity);
        Destroy(hitMarker, 3f);
    }
}
