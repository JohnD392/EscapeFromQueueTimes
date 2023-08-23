using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {
    List<ChunkGenerator> wgl;

    public int numChunksX;
    public int numChunksZ;

    public GameObject chunkPrefab;

    public float frequency1;
    public float frequency2;
    public float frequency3;

    public float verticality1;
    public float verticality2;
    public float verticality3;

    List<GameObject> chunks;

    public bool trigger;

    public float scale = 100f;

    // Start is called before the first frame update
    void Start() {
        chunks = new List<GameObject>();
        Generate();
        this.transform.position = new Vector3(0f, -((verticality1/2 + verticality2/2 + verticality3/2) * scale), 0f);
    }

    private void Generate() {
        wgl = new List<ChunkGenerator>();
        for (int i = 0; i < numChunksX; i++) {
            for (int j = 0; j < numChunksZ; j++) {
                wgl.Add(new ChunkGenerator(i, j, scale));
            }
        }
        foreach (ChunkGenerator cg in wgl) {
            GameObject chunk = Instantiate(chunkPrefab, this.transform);
            chunk.transform.localPosition = Vector3.zero;
            chunk.GetComponent<MeshFilter>().mesh = cg.GenerateMesh(GetHeight);
            chunk.GetComponent<MeshCollider>().sharedMesh = chunk.GetComponent<MeshFilter>().sharedMesh;
            chunks.Add(chunk);
        }
    }

    public float GetHeight(float worldX, float worldZ) {
        float perlinLayer1 = Mathf.PerlinNoise(worldX * frequency1, worldZ * frequency1) * verticality1;
        float perlinLayer2 = Mathf.PerlinNoise(worldX * frequency2, worldZ * frequency2) * verticality2;
        float perlinLayer3 = Mathf.PerlinNoise(worldX * frequency3, worldZ * frequency3) * verticality3;
        return perlinLayer1 + perlinLayer2 + perlinLayer3;
    }

    private void Update() {
        if(trigger) {
            trigger = false;
            foreach(GameObject go in chunks) Destroy(go);
            chunks = new List<GameObject>();
            Generate();
        }
    }
}
