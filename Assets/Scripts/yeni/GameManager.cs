using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int spawnCount= 3;
    [SerializeField] private GameObject charPrefab;
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private float spacing = 1.5f;
    private Color[] generatedColors;

    void Start()
    {
        SpawnCharacter(charPrefab);
        SpawnSquare(squarePrefab);
    }

    public void SpawnCharacter(GameObject prefab)
    {
        ColorHelper.GenerateColors(Color.red, Color.yellow, spawnCount, out generatedColors);
        
        for(int i = 0; i< spawnCount; i++)
        {
            GameObject newBall = Instantiate(prefab, transform.position, Quaternion.identity);
            newBall.transform.position = new Vector3(Random.Range(-4f, 4f), 1.5f, Random.Range(0f, 10f));
            newBall.GetComponent<Character>().SetColor(generatedColors[i], i);
            
        }
    }

    public void SpawnSquare(GameObject prefab)
    {
        float totalCubeWidth = (spawnCount - 1) * spacing;      // Toplam kutu geniþliði

        float startingX = -totalCubeWidth / 2f; // Baþlangýç X pozisyonu
        float startingY = 1.5f;                   // Y pozisyonu (sabit)
        float startingZ = -10f;                          // Z pozisyonu (sabit)

        for (int i = 0; i < spawnCount; i++)
        {
            float xPos = startingX + i * spacing;
            float yPos = startingY;
            float zPos = startingZ;
            Vector3 cubeVector = new Vector3(xPos, yPos, zPos);
            Instantiate(prefab, cubeVector, Quaternion.identity);
        }
    }
    public void OrganizeCharacter(Character character)
    {

    }
}
