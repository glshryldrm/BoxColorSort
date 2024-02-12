using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManagerr LevelManagerr;
    [SerializeField] private float spacing = 1.5f;
    public Vector3 squareVector;
    private Color[] generatedColors;
    [HideInInspector] public Vector3[] squares;
    int maxBoxesPerRow = 5; 
    bool check = false;
    Character[] chars;
    [SerializeField] int squareCount;
    private int emptyIndex = -1;

    List<Character> _sortedCharacters = new List<Character>();
    [SerializeField] List<GameObject> charParents = new List<GameObject>();
    [SerializeField] List<ColorPair> colorList = new List<ColorPair>();
    

    void Start()
    {
        SetCharColorAll();
        SpawnBoxes(squareCount, GameAssets.Instance.squarePrefab);
    }

    public void SetCharacterColor(GameObject gameObject, Color color1, Color color2)
    {
        chars = new Character[gameObject.GetComponentsInChildren<Character>().Length];
        chars = gameObject.GetComponentsInChildren<Character>();
        ColorHelper.GenerateColors(color1, color2, chars.Length, out generatedColors);  
        for (int i = 0; i < chars.Length; i++)
        {
            
            chars[i].SetColor(generatedColors[i], i);

        }
    }
    void SetCharColorAll()
    {
        if (charParents.Count == colorList.Count)
        {
            for (int i = 0; i < charParents.Count; i++)
            {
                if (charParents[i].GetComponentsInChildren<Character>().Length == squareCount)
                {
                    SetCharacterColor(charParents[i], colorList[i].color1, colorList[i].color2);
                    Character[] chars = charParents[i].GetComponentsInChildren<Character>();
                    for (int j = 0; j < chars.Length; j++)
                    {
                        chars[j].parentIndex = i;
                    }
                }
                else
                    Debug.Log("CharParents child counts is not equal");
            }
        }
        else
            Debug.Log("CharParents and ColorList counts is not equal");
        
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
        int ParentIndexValue = _sortedCharacters[0].parentIndex;
        for (int i = 0; i < squares.Length; i++)
        {
            if (_sortedCharacters[i].index != i || _sortedCharacters[i].parentIndex != ParentIndexValue)
            {
                isSuccess = false;
            }
        }

        if(isSuccess)
        {
            Debug.Log("Success");

            //if (_sortedCharacters.Count != 0 && check == false)
            Invoke(nameof(DestroySortedChar), 0.7f);

            //Next level
            Invoke(nameof(LoadNextPrivate), 1f);
            
            
           
        }
        else
        {
            Debug.Log("Failed");
            LevelManagerr.ReloadLevel();
        }
    }
    void LoadNextPrivate()
    {
        if (FindAnyObjectByType<Character>() == null)
        {
            LevelManagerr.LoadNextLevel();
        }
        else
        {
            Debug.Log("Character hala varr");
        }
    }
    void DestroySortedChar()
    {
        for (int i = 0; i < _sortedCharacters.Count; i++)
        {
            Destroy(_sortedCharacters[i].gameObject);
            _sortedCharacters[i].CreateFX();
            SoundManager.PlaySound();
        }
        _sortedCharacters.Clear();
        check = true;
    }
    
    void CalculateLoc(int characterCount, GameObject prefab)
    {
        
        float totalCubeWidth = (characterCount -1) * spacing;      
        float startingX = -totalCubeWidth / 2f; 
        float startingY = 1.5f;                   
        float startingZ = -10f;                          
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
    void SpawnBoxes(int characterCount, GameObject prefab)
    {
        float totalBoxes = (characterCount-1) * spacing; // Toplam kutu sayýsý
        int currentRow = 0;  // Mevcut satýr numarasý
        squares = new Vector3[characterCount];

        for (int i = 0; i < characterCount; i++)
        {
            // Sýradaki kutunun pozisyonunu belirle
            //float xPos = (i % maxBoxesPerRow * spacing);
            float xPos = -(totalBoxes) / 2f + i * spacing;
            float zPos = currentRow * spacing - 3f;

            // Spawn edilecek kutunun pozisyonunu belirle
            Vector3 spawnPosition = new Vector3(xPos, -0.3f, zPos);

            // Kutuyu spawn et
            Instantiate(prefab, spawnPosition, Quaternion.identity);
            spawnPosition = new Vector3(xPos, 0.3f, zPos);
            squares[i] = spawnPosition;
            if(squares.Length == characterCount)
            {
                
            }
            // Bir sýradaki maksimum kutu sayýsýna ulaþýldýysa bir üst satýra geç
            if ((i + 1) % maxBoxesPerRow == 0)
            {
                currentRow--;
            }
        }
    }

    ////1. çözüm
    //void SpawnBoxes(int characterCount, GameObject prefab)
    //{
    //    int totalBoxes = characterCount; // Toplam kutu sayýsý
    //    int currentRow = 2;  // Mevcut satýr numarasý
    //    squares = new Vector3[characterCount];

    //    float startingX = -2.2f; // X pozisyonu baþlangýcý
    //    float startingZ = -4f; // Z pozisyonu baþlangýcý

    //    for (int i = 0; i < totalBoxes; i++)
    //    {
    //        // Sýradaki kutunun pozisyonunu belirle
    //        float xPos = startingX + (i % maxBoxesPerRow * spacing);
    //        float zPos = startingZ + (currentRow * spacing);

    //        // Spawn edilecek kutunun pozisyonunu belirle
    //        Vector3 spawnPosition = new Vector3(xPos, -0.3f, zPos);

    //        // Kutuyu spawn et
    //        GameObject newBox = Instantiate(prefab, spawnPosition, Quaternion.identity);
    //        spawnPosition = new Vector3(xPos, 0.3f, zPos);
    //        squares[i] = spawnPosition;

    //        // Bir sýradaki maksimum kutu sayýsýna ulaþýldýysa bir üst satýra geç
    //        if ((i + 1) % maxBoxesPerRow == 0)
    //        {
    //            currentRow--;
    //        }
    //    }
    //}

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
    }
    */
   
}
