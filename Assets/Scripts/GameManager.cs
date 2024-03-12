using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private float spacing;
    public Vector3 squareVector;
    private Color[] generatedColors;
    [HideInInspector] public List<Vector3> squares = new List<Vector3>();
    Character[] chars;
    [SerializeField] int squareCount;
    private int emptyIndex = -1;
    List<Character> _sortedCharacters = new List<Character>();
    [SerializeField] List<GameObject> charParents = new List<GameObject>();
    [SerializeField] List<ColorPair> colorList = new List<ColorPair>();
    NPC[] NPCs;

    void Start()
    {
        SetCharColorAll();
        SpawnBoxes(squareCount, GameAssets.Instance.squarePrefab);
        FindNPCs();
    }
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

    }
   
    public void SetCharacterColor(GameObject gameObject, Color color1, Color color2)
    {
        chars = gameObject.GetComponentsInChildren<Character>();

        ColorHelper.GenerateColors(color1, color2, chars.Length, out generatedColors);

        for (int i = 0; i < chars.Length; i++)
        {
            chars[i].SetColor(generatedColors[i], i, charParents.IndexOf(gameObject));
            //gridManager.SetCharacterPosition(chars[i]);
            
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


        emptyIndex = (emptyIndex + 1) % squares.Count;
        if (_sortedCharacters.Count != squareCount)
        {
            character.transform.position = squares[emptyIndex];
            SoundManager.PlayBubbleSound();
            character.CreateFX();
            _sortedCharacters.Add(character);

            gridManager.ClearGrid(character);
        }
        character.GetComponent<Rigidbody>().useGravity = true;
        //character.GetComponent<Collider>().enabled = false;

        CheckLevelComplate();

    }

    void CheckLevelComplate()
    {
        if (squares.Count != _sortedCharacters.Count)
            return;

        bool isSuccess = true;
        int ParentIndexValue = _sortedCharacters[0].parentIndex;
        for (int i = 0; i < squares.Count; i++)
        {
            if (_sortedCharacters[i].index != i || _sortedCharacters[i].parentIndex != ParentIndexValue)
            {
                isSuccess = false;
            }
        }
        if (isSuccess)
        {
            Debug.Log("Success");

            //if (_sortedCharacters.Count != 0 && check == false)
            CharacterManager.touchCheck = false;
            Invoke(nameof(DestroySortedChar), 0.7f);
            Invoke(nameof(DestroyNPCs), 0.7f);
            Invoke(nameof(SetTouchCheck), 0.7f);
            
            //Next level
            Invoke(nameof(LoadNextPrivate), 1f);



        }
        else
        {
            Debug.Log("Failed");
            CharacterManager.touchCheck = false;

            Invoke(nameof(ReloadCharacters), 0.7f);
            Invoke(nameof(DestroySortedChar), 0.7f);

            Invoke(nameof(SetTouchCheck), 0.7f);
            //LevelManagerr.ReloadLevel();
        }
    }
    void LoadNextPrivate()
    {
        if (FindAnyObjectByType<Character>() == null)
        {
            levelManager.LoadNextLevel();
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
            SoundManager.PlayBubbleSound();
        }
        _sortedCharacters.Clear();
    }
    void DestroyNPCs()
    {
        for (int i = 0; i < NPCs.Length; i++)
        {
            Destroy(NPCs[i].gameObject);
            NPCs[i].CreateFX();
            SoundManager.PlayBubbleSound();
        }
        SoundManager.PlayCorrectSound();
    }
    void FindNPCs()
    {
        NPCs = new NPC[2];
        NPCs = FindObjectsOfType<NPC>();
    }
    void SetTouchCheck()
    {
        CharacterManager.touchCheck = true;
    }
    void ReloadCharacters()
    {
        for (int i = 0; i < squareCount; i++)
        {
            _sortedCharacters[i].GetComponent<Rigidbody>().useGravity = false;

            GameObject spawnChar = Instantiate(_sortedCharacters[i].gameObject);
            spawnChar.GetComponent<Character>().SpawnedAnimation();
            spawnChar.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
            gridManager.SetCharacterPosition(spawnChar.GetComponent<Character>());


            //spawnChar.GetComponent<Character>().CreateFX();
        }
    }
    void SpawnBoxes(int characterCount, GameObject prefab)
    {
        float totalBoxes = (characterCount - 1) * spacing; // Toplam kutu sayýsý
        int currentRow = 0;  // Mevcut satýr numarasý

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
            if (squares.Count != squareCount)
            {
                squares.Add(spawnPosition);
            }
        }
    }
    // 2.çözüm
    /*void SpawnBoxes(int characterCount, GameObject prefab)
    {
        float totalBoxes = (characterCount - 1) * spacing; // Toplam kutu sayýsý

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
