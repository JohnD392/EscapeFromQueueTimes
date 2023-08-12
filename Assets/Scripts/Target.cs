using UnityEngine;

public class Target : MonoBehaviour {
    public GameObject hitMarkerPrefab;

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("HIT!");
        if (collision.gameObject.tag == "Bullet") {
            GameObject hitMarker = Instantiate(hitMarkerPrefab, collision.transform.position, Quaternion.LookRotation(-collision.contacts[0].normal, Vector3.up));
            Destroy(collision.gameObject);
            Destroy(hitMarker, 3f);
        }
    }
}
