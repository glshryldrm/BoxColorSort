using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private LevelManager levelManager;
    private Color baseColor;// Baþlangýç rengi
    private float deviation = 0.2f;
    private float brightness = 1f;
    
    public void SpawnSphere(GameObject prefab)
    {
        int count = levelManager.currentLevel + 2; // Her seviyede küre sayýsýný artýr
        float spacing = 2f; // Küreler arasýndaki mesafe
        float totalSphereWidth = (count - 1) * spacing; // Toplam küre geniþliði
        

        float startingX = -totalSphereWidth / 2f; // Baþlangýç X pozisyonu
        float startingY = 1.5f;                   // Y pozisyonu (sabit)
        float startingZ = 0f;                          // Z pozisyonu (sabit)

        baseColor = Random.ColorHSV();

        for (int i = 0; i < count; i++)
        {

            Color closeRandomColor = GenerateCloseRandomColor(baseColor, deviation);

            float xPos = startingX + i * spacing;
            float yPos = startingY;
            float zPos = startingZ;

            GameObject newBall = Instantiate(prefab, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            newBall.GetComponent<Renderer>().material.color = closeRandomColor;
        }
    }
    public void SpawnCube(GameObject prefab)
    {
        int count = levelManager.currentLevel + 2;   // Her seviyede kutu sayýsýný artýr
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

            Instantiate(prefab, new Vector3(xPos, yPos, zPos), Quaternion.identity);
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

        float h, s, v;
        Color.RGBToHSV(closeRandomColor, out h, out s, out v);

        return Color.HSVToRGB(h, s, brightness);

    }
}
