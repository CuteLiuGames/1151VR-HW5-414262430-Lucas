
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [Header("Sphere Settings")]
    [Range(16, 128)]
    public int segments = 64;

    [Header("Colors")]
    public Color topColor = new Color32(
        135, 206, 250, 255
    ); // Sky Blue #87CEFA

    public Color bottomColor = Color.white;

    void Start()
    {
        CreateBall();
    }

    Material MakeMaterial(Color color)
    {
        Shader shader = Shader.Find(
            "Universal Render Pipeline/Lit"
        );

        if (shader == null)
        {
            shader = Shader.Find("Standard");
        }

        Material mat = new Material(shader);
        mat.color = color;

        return mat;
    }

    void CreateBall()
    {
        Material skyBlue = MakeMaterial(topColor);
        Material white = MakeMaterial(bottomColor);

        CreateHemisphere(
            "Sky Blue Top",
            true,
            skyBlue
        );

        CreateHemisphere(
            "White Bottom",
            false,
            white
        );
    }

    void CreateHemisphere(
        string name,
        bool upper,
        Material material)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(transform, false);

        int lat = segments / 2;

        Vector3[] vertices =
            new Vector3[(lat + 1) * (segments + 1)];

        int[] triangles =
            new int[lat * segments * 6];

        for (int y = 0; y <= lat; y++)
        {
            float theta =
                (upper ? y : y + lat) *
                Mathf.PI / segments;

            for (int x = 0; x <= segments; x++)
            {
                float phi =
                    x * Mathf.PI * 2 / segments;

                int i = y * (segments + 1) + x;

                vertices[i] = new Vector3(
                    Mathf.Sin(theta) * Mathf.Cos(phi),
                    Mathf.Cos(theta),
                    Mathf.Sin(theta) * Mathf.Sin(phi)
                );
            }
        }

        int t = 0;

        for (int y = 0; y < lat; y++)
        {
            for (int x = 0; x < segments; x++)
            {
                int a = y * (segments + 1) + x;
                int b = a + segments + 1;

                // Outward-facing triangles
                triangles[t++] = a;
                triangles[t++] = b;
                triangles[t++] = a + 1;

                triangles[t++] = a + 1;
                triangles[t++] = b;
                triangles[t++] = b + 1;
            }
        }

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        obj.AddComponent<MeshFilter>().mesh = mesh;

        obj.AddComponent<MeshRenderer>().material =
            material;
    }
}
