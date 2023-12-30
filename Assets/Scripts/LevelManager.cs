using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject spherePrefab;
    public GameObject cubePrefab;
    private Vector3 cubeVector;
    private GameObject newCube;
    private Color baseColor;// Baþlangýç rengi
    public GameObject[] spheres;
    public Color[] colors;
    public Vector3[] cubes;
    private float deviation = 0.1f;

    public int currentLevel = 3;
   

    void Start()
    {
        SetupLevel(currentLevel);
    }
    public void SetupLevel(int level)
    {
        foreach (var sphere in spheres)
        {
            Destroy(sphere);
        }
        colors = new Color[currentLevel + 2]; // colors dizisini oluþtur
        cubes = new Vector3[currentLevel + 2]; // cubes dizisini oluþtur
        spheres = new GameObject[currentLevel + 2];

        SpawnSphere(spherePrefab);
        OrderSpheresByBrightness();
        SpawnCube(cubePrefab);


        }
    public void SpawnSphere(GameObject prefab)
    {
        int count = currentLevel + 2; // Her seviyede küre sayýsýný artýr
        float spacing = 2f; // Küreler arasýndaki mesafe
        float totalSphereWidth = (count - 1) * spacing; // Toplam küre geniþliði


        baseColor = Random.ColorHSV();

        for (int i = 0; i < count; i++)
        {
            
            Color closeRandomColor = GenerateCloseRandomColor(baseColor, deviation);


            GameObject newBall = Instantiate(prefab,transform.position, Quaternion.identity);
            newBall.transform.position = new Vector3(Random.Range(-4f, 4f), 1.5f, Random.Range(0f, 10f));
            newBall.GetComponent<Renderer>().material.color = closeRandomColor;
            newBall.AddComponent<ClickToMoveToArea>();

            spheres[i] = newBall; 
        }
    }
    public void OrderSpheresByBrightness()
    {
        System.Array.Sort(spheres, (x, y) =>
        {
            float brightnessX = x.GetComponent<Renderer>().material.color.grayscale;
            float brightnessY = y.GetComponent<Renderer>().material.color.grayscale;
            return brightnessY.CompareTo(brightnessX);
        });
    }

    public void SpawnCube(GameObject prefab)
    {
        int count = currentLevel + 2;   // Her seviyede kutu sayýsýný artýr
        float spacing = 2f;   // Kutular arasýndaki mesafe
        float totalCubeWidth = (count - 1) * spacing;      // Toplam kutu geniþliði

        float startingX = -totalCubeWidth / 2f; // Baþlangýç X pozisyonu
        float startingY = 1.5f;                   // Y pozisyonu (sabit)
        float startingZ = -10f;                          // Z pozisyonu (sabit)

        for (int i = 0; i < count; i++)
        {
            float xPos = startingX + i * spacing;
            float yPos = startingY;
            float zPos = startingZ;
            cubeVector = new Vector3(xPos, yPos, zPos);
            newCube = Instantiate(prefab, cubeVector, Quaternion.identity);
            cubes[i] = cubeVector;
        }
        
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

        //float h, s, v;
        //Color.RGBToHSV(closeRandomColor, out h, out s, out v);

        //return Color.HSVToRGB(h, s, brightness);
        return closeRandomColor;

    }
    public void NextLevel()
    {
        currentLevel++;
        SetupLevel(currentLevel);
    }
    public Vector3 GetCubePosition(int index)
    {
        // index sýnýrlarý kontrol et
        index = Mathf.Clamp(index, 0, cubes.Length - 1);

        return cubes[index];
    }

    public int GetSphereIndex(GameObject sphere)
    {
        return System.Array.IndexOf(spheres, sphere);
    }

}

