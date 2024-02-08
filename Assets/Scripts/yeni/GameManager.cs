using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManagerr LevelManagerr;
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private float spacing = 1.5f;
    public Vector3 squareVector;
    private Color[] generatedColors;
    [HideInInspector] public Vector3[] squares;
    private int emptyIndex = -1;
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    int maxBoxesPerRow = 5; // Bir sýradaki maksimum kutu sayýsý


    List<Character> _sortedCharacters = new List<Character>();
    [SerializeField]  List<Character> characters = new List<Character>();
    void Start()
    {
        SetCharacterColor();
        //SpawnSquare(squarePrefab);
        SpawnBoxes(characters.Count, squarePrefab);
    }

    public void SetCharacterColor()
    {
        ColorHelper.GenerateColors(color1, color2, characters.Count, out generatedColors);

        for (int i = 0; i < characters.Count; i++)
        {
           
           characters[i].SetColor(generatedColors[i], i);

        }
    }

    public void SpawnSquare(GameObject prefab)
    {
        

        if (characters.Count<=5)
        {
            CalculateLoc(characters.Count, prefab);
        }
        else if(characters.Count>5 & characters.Count<=10)
        {
            CalculateLoc(characters.Count, prefab);
        }        
    }
    public void OrganizeCharacter(Character character)
    {
        if (_sortedCharacters.Contains(character))
            return;
        character.CreateFX();
        emptyIndex = (emptyIndex + 1) % squares.Length;
        character.transform.position = squares[emptyIndex];
        //character.GetComponent<Rigidbody>().isKinematic = true;
        //character.GetComponent<Collider>().enabled = false;
        _sortedCharacters.Add(character);

        CheckLevelComplate();
    }

    void CheckLevelComplate()
    {
        if (squares.Length != _sortedCharacters.Count)
            return;

        bool isSuccess = true;

        for (int i = 0; i < squares.Length; i++)
        {
            if (_sortedCharacters[i].index != i)
            {
                isSuccess = false;
            }
        }

        if(isSuccess)
        {
            Debug.Log("Success");
            //Next level
            LevelManagerr.LoadNextLevel();
        }
        else
        {
            Debug.Log("Failed");
            LevelManagerr.ReloadLevel();
        }
    }
    void CalculateLoc(int characterCount, GameObject prefab)
    {
        
        float totalCubeWidth = (characterCount -1) * spacing;      // Toplam kutu geniþliði
        float startingX = -totalCubeWidth / 2f; // Baþlangýç X pozisyonu
        float startingY = 1.5f;                   // Y pozisyonu (sabit)
        float startingZ = -10f;                          // Z pozisyonu (sabit)
        squares = new Vector3[characterCount];


        for (int i = 0; i < characterCount; i++)
        {
            if (i < 5)
            {
                float xPos = startingX + i * spacing;
                float yPos = startingY;
                float zPos = startingZ;
                squareVector = new Vector3(xPos, yPos, zPos);
                Instantiate(prefab, squareVector, Quaternion.identity);
                squareVector = new Vector3(xPos, yPos + 1f, zPos);
                squares[i] = squareVector;
            }
            else if (i >= 5)
            {
                float xPos = startingX - (characterCount/2) + i * spacing ;
                float yPos = startingY;
                float zPos = startingZ - 2f;
                squareVector = new Vector3(xPos, yPos, zPos);
                Instantiate(prefab, squareVector, Quaternion.identity);
                squareVector = new Vector3(xPos, yPos + 1f, zPos);
                squares[i] = squareVector;
            }
            
        }
    }
    /*void SpawnBoxes(int characterCount, GameObject prefab)
    {
        int totalBoxes = characterCount; // Toplam kutu sayýsý
        int currentRow = 0;  // Mevcut satýr numarasý
        squares = new Vector3[characterCount];

        for (int i = 0; i < totalBoxes; i++)
        {
            // Sýradaki kutunun pozisyonunu belirle
            //float xPos = (i % maxBoxesPerRow * spacing);
            float xPos = -(totalBoxes - 1) + i % maxBoxesPerRow * spacing;
            float zPos = currentRow * spacing -3f;

            // Spawn edilecek kutunun pozisyonunu belirle
            Vector3 spawnPosition = new Vector3(xPos, 0f, zPos);

            // Kutuyu spawn et
            GameObject newBox = Instantiate(prefab, spawnPosition, Quaternion.identity);
            spawnPosition = new Vector3(xPos, 0f, zPos);
            squares[i] = spawnPosition;
            // Bir sýradaki maksimum kutu sayýsýna ulaþýldýysa bir üst satýra geç
            if ((i + 1) % maxBoxesPerRow == 0)
            {
                currentRow--;
            }
        }
    }*/

    //1. çözüm
    /*void SpawnBoxes(int characterCount, GameObject prefab)
    {
        int totalBoxes = characterCount; // Toplam kutu sayýsý
        int currentRow = 2;  // Mevcut satýr numarasý
        squares = new Vector3[characterCount];

        float startingX = -2.2f; // X pozisyonu baþlangýcý
        float startingZ = -4f; // Z pozisyonu baþlangýcý

        for (int i = 0; i < totalBoxes; i++)
        {
            // Sýradaki kutunun pozisyonunu belirle
            float xPos = startingX + (i % maxBoxesPerRow * spacing);
            float zPos = startingZ + (currentRow * spacing);

            // Spawn edilecek kutunun pozisyonunu belirle
            Vector3 spawnPosition = new Vector3(xPos, 0f, zPos);

            // Kutuyu spawn et
            GameObject newBox = Instantiate(prefab, spawnPosition, Quaternion.identity);
            squares[i] = spawnPosition;

            // Bir sýradaki maksimum kutu sayýsýna ulaþýldýysa bir üst satýra geç
            if ((i + 1) % maxBoxesPerRow == 0)
            {
                currentRow--;
            }
        }
    }*/

    // 2.çözüm
    /*void SpawnBoxes(int characterCount, GameObject prefab)
    {
        int totalBoxes = characterCount; // Toplam kutu sayýsý
        squares = new Vector3[characterCount];

        float startingX = -2.2f; // X pozisyonu baþlangýcý
        float startingZ = -2f; // Z pozisyonu baþlangýcý

        float currentX = startingX;
        float currentZ = startingZ;
        int direction = 1; // Yön: 1 saða, 2 aþaðý, 3 sola, 4 yukarý
        int stepsUntilChange = 1; // Bir sonraki yöne geçmek için kaç adým gerektiðini belirler
        int stepCount = 0; // Adým sayýsý

        for (int i = 0; i < totalBoxes; i++)
        {
            // Sýradaki kutunun pozisyonunu belirle
            Vector3 spawnPosition = new Vector3(currentX, 0f, currentZ);

            // Kutuyu spawn et
            GameObject newBox = Instantiate(prefab, spawnPosition, Quaternion.identity);
            squares[i] = spawnPosition;

            // Yönü deðiþtirmek için adým sayýsýný kontrol et
            if (stepCount == stepsUntilChange)
            {
                stepCount = 0;
                if (direction == 1 || direction == 3)
                {
                    stepsUntilChange++;
                }
                direction = (direction % 4) + 1;
            }

            // Bir sonraki adýmýn pozisyonunu belirle
            switch (direction)
            {
                case 1: // Saða
                    currentX += spacing;
                    break;
                case 2: // Aþaðý
                    currentZ -= spacing;
                    break;
                case 3: // Sola
                    currentX -= spacing;
                    break;
                case 4: // Yukarý
                    currentZ += spacing;
                    break;
            }

            stepCount++;
        }
    }*/


}
