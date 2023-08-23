using UnityEngine;
using System;

[RequireComponent(typeof(MeshFilter))]
public class ChunkGenerator {
    public int numQuadsX;
    public int numQuadsZ;

    private int heightMapX;
    private int heightMapZ;

    public int chunkX;
    public int chunkZ;

    public float frequency1;
    public float frequency2;
    public float frequency3;

    public float verticality1;
    public float verticality2;
    public float verticality3;

    private float scale;

    Vector3[] vertices;
    Vector2[] uv;

    public delegate float NoiseFunction(float worldX, float worldZ);

    public ChunkGenerator(int x, int z, float scale) {
        this.chunkX = x;
        this.chunkZ = z;
        this.scale = scale;
        this.numQuadsX = 50;
        this.numQuadsZ = 50;
    }

    public Mesh GenerateMesh(NoiseFunction noiseFunc) {
        heightMapX = numQuadsX + 1;
        heightMapZ = numQuadsZ + 1;
        float[,] heightMap = GenerateHeightMap(noiseFunc);
        heightMap = GenerateSpires(heightMap);

        return GenerateMeshFromHeightMap(heightMap);
    }


    private float[,] GenerateSpires(float[,] heightMap) {
        float offset = 0f;
        float scale = 100f;
        float threshold = .61f;
        float heightIncrease = 8f;
        float frequency = .03f;
        for(int i=0; i<heightMap.GetLength(0); i++) {
            for(int j=0; j<heightMap.GetLength(1); j++) {
                float p = Mathf.PerlinNoise(frequency * (chunkX * numQuadsX + i), frequency * (chunkZ * numQuadsZ + j));
                if(p > threshold) {
                    Debug.Log("Setting spire!");
                    heightMap[i, j] += heightIncrease;
                }
            }
        }
        return heightMap;
    }

    static (int row, int col)[] GetNeighborPositions(float[,] matrix, int row, int col) {
        int numRows = matrix.GetLength(0);
        int numCols = matrix.GetLength(1);

        (int, int)[] neighbors = new (int, int)[8];
        int count = 0;

        for (int r = -1; r <= 1; r++) {
            for (int c = -1; c <= 1; c++) {
                if (r == 0 && c == 0)
                    continue; // Skip the center element (the original element)

                int newRow = row + r;
                int newCol = col + c;

                // Check if the new position is within bounds of the matrix
                if (newRow >= 0 && newRow < numRows && newCol >= 0 && newCol < numCols) {
                    neighbors[count] = (newRow, newCol);
                    count++;
                }
            }
        }
        // Resize the array to remove any unused slots
        Array.Resize(ref neighbors, count);
        return neighbors;
    }

    public float[,] GenerateHeightMap(NoiseFunction noiseFunc) {
        float[,] heightMap = new float[heightMapX, heightMapZ]; // +1 because width/depth are measured by quads, not points
        for (int x = 0; x < heightMapX; x++) {
            for (int z = 0; z < heightMapZ; z++) {
                heightMap[x, z] = noiseFunc(chunkX * numQuadsX + x, chunkZ * numQuadsZ + z);
            }
        }
        return heightMap;
    }

    public Mesh GenerateMeshFromHeightMap(float[,] heightMap) {
        vertices = new Vector3[heightMapX * heightMapZ];
        uv = new Vector2[vertices.Length];
        int[] triangles = new int[(numQuadsX) * (numQuadsZ) * 6];

        int vertexIndex = 0;
        int triangleIndex = 0;

        for (int z = 0; z < heightMapZ; z++) {
            for (int x = 0; x < heightMapX; x++) {
                vertices[vertexIndex] = new Vector3(scale * (chunkX * (numQuadsX) + x), scale * heightMap[x, z], scale * (chunkZ * (numQuadsZ) + z));
                uv[vertexIndex] = new Vector2((float)x / (float)(numQuadsX), (float)z / (float)(numQuadsZ));

                if (x < heightMapX - 1 && z < heightMapZ - 1) {
                    int topLeft = vertexIndex;
                    int topRight = vertexIndex + 1;
                    int bottomLeft = vertexIndex + heightMapX;
                    int bottomRight = vertexIndex + heightMapX + 1;

                    triangles[triangleIndex] = topLeft;
                    triangles[triangleIndex + 1] = bottomLeft;
                    triangles[triangleIndex + 2] = topRight;
                    triangles[triangleIndex + 3] = topRight;
                    triangles[triangleIndex + 4] = bottomLeft;
                    triangles[triangleIndex + 5] = bottomRight;

                    triangleIndex += 6;
                }

                vertexIndex++;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        return mesh;
    }

    //void OnDrawGizmos() {
    //    if(vertices != null && uv != null) {
    //        foreach(Vector3 vertex in vertices) {
    //            Gizmos.DrawCube(vertex, Vector3.one * .4f);
    //        }
    //    }
    //}
}
