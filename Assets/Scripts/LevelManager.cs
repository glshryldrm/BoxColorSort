using UnityEngine;
using System.Collections.Generic;


public class LevelManager : MonoBehaviour
{
    public GameObject spherePrefab;
    private GameObject selectedSphere;
    public GameObject cubePrefab;
    private Vector3 cubeVector;
    private int levelColor;
    private Color baseColor;// Baþlangýç rengi
    public GameObject[] spheres;
    public Color[] colors;
    public Vector3[] cubes;
    private int leftmostEmptySpotIndex = -1;
    private float hue;
    public bool isMoving = false;
    

    private ColorGenerator colorGenerator = new ColorGenerator();

    public int currentLevel = 3;
   

    void Start()
    {

        SetupLevel(currentLevel);
    }
    public void SetupLevel(int level)
    {
        //foreach (var sphere in spheres)
        //{
        //    Destroy(sphere);
        //}
        colors = new Color[currentLevel + 2]; // colors dizisini oluþtur
        cubes = new Vector3[currentLevel + 2]; // cubes dizisini oluþtur
        spheres = new GameObject[currentLevel + 2];

        SpawnSphere(spherePrefab);
        SpawnCube(cubePrefab);


    }
    public void SpawnSphere(GameObject prefab)
    {
        int count = currentLevel + 2; // Her seviyede küre sayýsýný artýr
        List<Color> colorList = colorGenerator.GenerateColorList();
        levelColor = (currentLevel % colorList.Count) - 1;
        baseColor = colorList[levelColor];
        

        for (int i = 0; i < count; i++)
        {
            hue = (i * 10);
            Color closeRandomColor = GenerateCloseRandomColor(baseColor, hue);


            GameObject newBall = Instantiate(prefab,transform.position, Quaternion.identity);
            newBall.transform.position = new Vector3(Random.Range(-4f, 4f), 1.5f, Random.Range(0f, 10f));
            newBall.GetComponent<Renderer>().material.color = closeRandomColor;
            newBall.AddComponent<ClickToMoveToArea>();

            spheres[i] = newBall;
            
        }
    
    }
    //public void OrderSpheresByBrightness()
    //{
    //    System.Array.Sort(spheres, (x, y) =>
    //    {
    //        float brightnessX = x.GetComponent<Renderer>().material.color.grayscale;
    //        float brightnessY = y.GetComponent<Renderer>().material.color.grayscale;
    //        return brightnessY.CompareTo(brightnessX);
    //    });
    //}

    public void SpawnCube(GameObject prefab)
    {
        int count = currentLevel + 2;   // Her seviyede kutu sayýsýný artýr
        float spacing = 0.1f;   // Kutular arasýndaki mesafe
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
            Instantiate(prefab, cubeVector, Quaternion.identity);
            cubeVector = new Vector3(xPos, yPos + 0.5f, zPos);
            cubes[i] = cubeVector;
        }
        
    }
    Color GenerateCloseRandomColor(Color baseColor, float hueChange)
    {
    
        Color.RGBToHSV(baseColor, out float h, out float s, out float v);
        h = (h + hueChange) % 360;
        return Color.HSVToRGB(h/360, s, v);


    }
    //public void NextLevel()
    //{
    //    currentLevel++;
    //    SetupLevel(currentLevel);
    //}


    public Vector3 FindLeftmostEmptySpot()
    {
        leftmostEmptySpotIndex = (leftmostEmptySpotIndex + 1) % cubes.Length;
        Vector3 leftmostEmptySpot = cubes[leftmostEmptySpotIndex];
        return leftmostEmptySpot;
    }

}


