using UnityEngine;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class SC_MapGenerator : MonoBehaviour
    {
        [Header("Stats")]
        public int width = 20;
        public int heigth = 20;
        public float noise_scale = 20;
        public float heigth_multiplier = 1;
        public float round;
        public float vehicle_step = 0.5f;

        private Mesh mesh;
        private MeshCollider meshCollider;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            meshCollider = GetComponent<MeshCollider>();

            GenerateTerrain();
        }

        private void GenerateTerrain()
        {
            Vector3[] vertices = new Vector3[(width + 1) * (heigth + 1)];
            int[] triangles = new int[width * heigth * 6];

            
            int octave = 4;
            float lacunarity = 2;

            // Create the vertices
            for (int z = 0; z <= heigth; z++)
            {
                for (int x = 0; x <= width; x++)
                {
                    float frequency = 1, amplitude = 1;

                    float y = 0;
                    for (int o = 0; o < octave; o++)
                    {
                        float posX = (x / noise_scale) * frequency;
                        float posY = (z / noise_scale) * frequency;

                        float perlin = Mathf.PerlinNoise(posX, posY) * 2 - 1;
                        y += perlin * amplitude;

                        amplitude *= 0.5f; // Persistence reduction per octave
                        frequency *= lacunarity;
                    }

                    y *= heigth_multiplier;
                    y = Mathf.Round(y / vehicle_step) * vehicle_step;

                    vertices[z * (width + 1) + x] = new Vector3(x, y, z);
                }
            }

            // Create the triangles
            int vert = 0;
            int tris = 0;

            for (int z = 0; z < heigth; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + width + 1;
                    triangles[tris + 2] = vert + 1;
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + width + 1;
                    triangles[tris + 5] = vert + width + 2;

                    vert++;
                    tris += 6;
                }
                vert++;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = mesh;
        }
    }
}