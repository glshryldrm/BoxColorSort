using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManagerr LevelManagerr;
    [SerializeField] private GameObject charPrefab;
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private float spacing = 1.5f;
    public Vector3 squareVector;
    private Color[] generatedColors;
    [HideInInspector] public Vector3[] squares;
    private int emptyIndex = -1;
    [SerializeField] Color color1;
    [SerializeField] Color color2;


    List<Character> _sortedCharacters = new List<Character>();
    [SerializeField]  List<Character> characters = new List<Character>();
    void Start()
    {
        SetCharacterColor();
        SpawnSquare(squarePrefab);
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
        float totalCubeWidth = (characters.Count - 1) * spacing;      // Toplam kutu geniþliði
        float startingX = -totalCubeWidth / 2f; // Baþlangýç X pozisyonu
        float startingY = 1.5f;                   // Y pozisyonu (sabit)
        float startingZ = -10f;                          // Z pozisyonu (sabit)
        squares = new Vector3[characters.Count];

        for (int i = 0; i < characters.Count; i++)
        {
            float xPos = startingX + i * spacing;
            float yPos = startingY;
            float zPos = startingZ;
            squareVector = new Vector3(xPos, yPos, zPos);
            Instantiate(prefab, squareVector, Quaternion.identity);
            squareVector = new Vector3(xPos, yPos + 0.5f, zPos);
            squares[i] = squareVector;
        }
    }
    public void OrganizeCharacter(Character character)
    {
        if (_sortedCharacters.Contains(character))
            return;

        emptyIndex = (emptyIndex + 1) % squares.Length;
        character.transform.position = squares[emptyIndex];
        character.GetComponent<Rigidbody>().isKinematic = true;
        character.GetComponent<Collider>().enabled = false;
        character.GetComponent<ParticleSystem>().Play();

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
            LevelManagerr.check = true;
            LevelManagerr.ReloadLevel();
        }
    }
    
}
