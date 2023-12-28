using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject spherePrefab;
    public GameObject cubePrefab;
    private Color baseColor;// Baþlangýç rengi
    public float deviation = 0.1f;
    private float brightness = 1f;

    private Color[] sphereColors;

    private int currentLevel = 3;

    void Start()
    {
        SetupLevel(currentLevel);
    }

    public void SetupLevel(int level)
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
        SpawnCube(cubePrefab, cubeCount, cubeSpacing, startingX, startingY, zPos - 10f);

        sphereColors = new Color[sphereCount];
        for (int i = 0; i < sphereCount; i++)
        {
            sphereColors[i] = Random.ColorHSV();
        }
    }

    void SpawnSphere(GameObject prefab, int count, float spacing, float startX, float startY, float startZ)
    {
        baseColor = Random.ColorHSV();
        for (int i = 0; i < count; i++)
        {
            
            Color closeRandomColor = GenerateCloseRandomColor(baseColor, deviation);

            float xPos = startX + i * spacing;
            float yPos = startY;
            float zPos = startZ;

            GameObject newBall = Instantiate(prefab, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            newBall.GetComponent<Renderer>().material.color = closeRandomColor;

            // Týklama iþleyicisi ekleyerek küreye týklanýnca bir cube'ye taþýma fonksiyonunu çaðýr
            newBall.AddComponent<ClickToMoveToCube>().SetCubePrefab(cubePrefab);
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
    Color GenerateCloseRandomColor(Color baseColor, float deviation)
    {
        // Her bir renk bileþeni için küçük random sapma deðeri üret
        float rDeviation = Random.Range(-deviation, deviation);
        float gDeviation = Random.Range(-deviation, deviation);
        float bDeviation = Random.Range(-deviation, deviation);

        // Random sapma deðerlerini kullanarak birbirine yakýn renk oluþtur
        Color closeRandomColor = new Color(
            Mathf.Clamp01(baseColor.r + rDeviation),
            Mathf.Clamp01(baseColor.g + gDeviation),
            Mathf.Clamp01(baseColor.b + bDeviation)
        );
        float h, s, v;
        Color.RGBToHSV(closeRandomColor, out h, out s, out v);
        return Color.HSVToRGB(h, s, brightness);

        //return closeRandomColor;
    }
}
