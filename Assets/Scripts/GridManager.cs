using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    float gridX = 0.6f;
    float gridY = 0.6f;
    [SerializeField] int height;
    [SerializeField] int width;
    Vector3[,] gridMatrix;
    Grid[] grids;

    private void Start()
    {
        CalculateGrid();
        FindCharacterOnScene();

    }
    private void Update()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {

                Gizmos.DrawWireCube(gridMatrix[i, j], new Vector3(gridX, gridY, 0.6f));
            }
        }
    }
    void CalculateGrid()
    {
        gridMatrix = new Vector3[height, width];
        grids = new Grid[height * width];
        int i = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                gridMatrix[x, y] = new Vector3(transform.position.x + gridX * x, transform.position.y + gridY * y, -0.5f);
                grids[i] = new Grid(x, y, null);
                grids[i].vector = gridMatrix[x, y];
                i++;
            }
        }

    }
    public Vector3 GridCheck(Character character)
    {
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].character == null)
            {
                grids[i].character = character;
                return grids[i].vector;
            }
            else
            {
                Debug.Log("Empty grid is not found");
            }
        }
        return new Vector3(0f, 0f, 0f);
    }
    public void TurnNullCharacter(Character character)
    {
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].character == character)
            {
                grids[i].character = null;
            }
        }
    }
    public void DedectCharacterGrid(Character character,out float x,out float y)
    {
        x = character.transform.position.x / gridX;
        y = character.transform.position.y / gridY;
    }
    void FindCharacterOnScene()
    {
        Character[] chars = FindObjectsOfType<Character>();
        for (int i = 0; i < chars.Length; i++)
        {
            //if(grids[i].character == null)
            //{
            grids[i].character = chars[i];
            chars[i].transform.position = grids[i].vector;
            chars[i].SpawnedAnimation();
            //}
        }
    }
}
class Grid
{
    public int x;
    public int y;
    public Character character;
    public Vector3 vector;
    public Grid(int x, int y, Character character)
    {
        this.x = x;
        this.y = y;
        this.character = character;
    }
}
