using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureSpawner : MonoBehaviour
{
    public Transform reference;
    public List<GameObject> objPrefabs;
    List<GameObject> objs;

    public float maxDistance = 200f;
    public int numObjs = 100;
    public float maxScale = 4f;

    private void OnEnable() {
        reference = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        if (reference == null) return;
        if(objs !=null) foreach (GameObject tree in objs) Destroy(tree);
        objs = new List<GameObject>();
        for(int i=0; i<numObjs; i++) {
            Vector3 randomOffset = Random.insideUnitSphere * maxDistance;
            if (randomOffset.y < 0f) randomOffset = new Vector3(randomOffset.x, -randomOffset.y, randomOffset.z);
            Vector3 rayOrigin = randomOffset + reference.position;
            RaycastHit hit;
            bool didHit = Physics.Raycast(rayOrigin, Vector3.down, out hit, maxDistance, LayerMask.GetMask(new string[] { "Ground" }));
            if(didHit) {
                GameObject objPrefab = objPrefabs[Random.Range(0, objPrefabs.Count)];
                GameObject obj = Instantiate(objPrefab, hit.point, Quaternion.Euler(0f, Random.Range(0, 360), 0f), transform);
                obj.transform.localScale *= Random.Range(1f, maxScale);
                objs.Add(obj);
            } else {
                Debug.DrawLine(rayOrigin, rayOrigin + Vector3.down * 10000f, Color.red, 5f);
            }
        }
    }

    private void Update() {
        if (objs == null) return;
        foreach(GameObject tree in objs) {
            if (tree == null) continue;
            if(Vector3.Distance(tree.transform.position, reference.transform.position) > maxDistance) {
                //Destroy(tree);
            }
        }
    }
}
