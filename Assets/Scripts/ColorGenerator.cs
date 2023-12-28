using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject ballPrefab; // Top prefab'ý
    public Color startColor = Color.red; // Baþlangýç rengi
    public Color endColor = Color.black; // Bitiþ rengi
    public int numberOfColors = 10; // Oluþturulacak renk sayýsý

    void Start()
    {
        GenerateColors();
    }

    void GenerateColors()
    {
        for (int i = 0; i < numberOfColors; i++)
        {
            // Açýktan koyuya doðru renk geçiþini gerçekleþtir
            float lerpValue = (float)i / (numberOfColors - 1);
            Color lerpedColor = Color.Lerp(startColor, endColor, lerpValue);

            // Oluþturulan rengi kullanarak bir nesne oluþtur ya da baþka bir iþlem yap
            SpawnBall(lerpedColor);

        }
    }

    
    void SpawnBall(Color color)
    {
        // Topu spawn et
        GameObject newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        newBall.transform.position = new Vector3(Random.Range(-5f, 5f), 1.5f, Random.Range(-5f, 5f));
        newBall.GetComponent<Renderer>().material.color = color;
    }
}