using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject spherePrefab;
    public GameObject cubePrefab;

    private int currentLevel = 1;

    void Start()
    {
        SetupLevel(currentLevel);
    }

    void SetupLevel(int level)
    {
        int sphereCount = level + 2; // Her seviyede küre sayýsýný artýr
        int cubeCount = level + 2;   // Her seviyede kutu sayýsýný artýr

        float sphereSpacing = 2f; // Küreler arasýndaki mesafe
        float cubeSpacing = 2f;   // Kutular arasýndaki mesafe

        float totalSphereWidth = (sphereCount - 1) * sphereSpacing; // Toplam küre geniþliði
        float totalCubeWidth = (cubeCount - 1) * cubeSpacing;      // Toplam kutu geniþliði

        float startingX = -totalSphereWidth / 2f; // Baþlangýç X pozisyonu
        float startingY = 1.5f;                   // Y pozisyonu (sabit)
        float zPos = 0f;                          // Z pozisyonu (sabit)

        SpawnSphere(spherePrefab, sphereCount, sphereSpacing, startingX, startingY, zPos);
        SpawnCube(cubePrefab, cubeCount, cubeSpacing, startingX, startingY - 1.5f, zPos - 10f);
    }

    void SpawnSphere(GameObject prefab, int count, float spacing, float startX, float startY, float startZ)
    {
        for (int i = 0; i < count; i++)
        {
            float xPos = startX + i * spacing;
            float yPos = startY;
            float zPos = startZ;

            Instantiate(prefab, new Vector3(xPos, yPos, zPos), Quaternion.identity);
        }
    }

    void SpawnCube(GameObject prefab, int count, float spacing, float startX, float startY, float startZ)
    {
        for (int i = 0; i < count; i++)
        {
            float xPos = startX + i * spacing;
            float yPos = startY;
            float zPos = startZ;

            Instantiate(prefab, new Vector3(xPos, yPos, zPos), Quaternion.identity);
        }
    }

    public void NextLevel()
    {
        currentLevel++;
        SetupLevel(currentLevel);
    }
}
