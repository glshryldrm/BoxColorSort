using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class GridManager : MonoBehaviour
{
    float gridX = 0.6f;
    float gridY = 0.6f;
    [SerializeField] int height;
    [SerializeField] int width;

    Grid[,] gridMatrix;

    [SerializeField] List<Vector2> invalidGridIndices = new List<Vector2>();

    private void Awake()
    {
        CalculateGrid();
        FindChars();
    }
    private void OnDrawGizmos()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 vector = new Vector3(transform.position.x + gridX * x, transform.position.y + gridY * y, -0.5f);
                Gizmos.DrawWireCube(vector, new Vector3(gridX, gridY, 0.6f));
                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
                style.normal.textColor = Color.black;
                //UnityEditor.Handles.Label(vector, x.ToString() + " - " + y.ToString(), style);
                //burada kodu yorum satirina aldim cunku diger turlu apk alamýyordum
            }
        }
    }
    void CalculateGrid()
    {
        gridMatrix = new Grid[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Grid g = new Grid(x, y, null);
                g.vector = new Vector3(transform.position.x + gridX * x, transform.position.y + gridY * y, -0.5f);
                gridMatrix[x, y] = g;

                if (invalidGridIndices.Contains(new Vector2(x, y)))
                    g.IsValid = false;
            }
        }
    }

    public void SetCharacterPosition(Character c)
    {
        Vector2 index = new Vector2(-1, -1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (!gridMatrix[x, y].IsValid || gridMatrix[x, y].character != null)
                    continue;

                gridMatrix[x, y].character = c;
                c.transform.position = gridMatrix[x, y].vector;

                return;
            }
        }
    }

    public void ClearGrid(Character c)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (gridMatrix[x, y].character == c)
                {
                    gridMatrix[x, y].character = null;

                    return;
                }
            }
        }
    }
    public void FindChars()
    {
        Character[] characters = new Character[height*width];
        characters = FindObjectsOfType<Character>();
        for (int i = 0; i < characters.Length; i++)
        {
            SetCharacterPosition(characters[i]);
        }
    }
}
class Grid
{
    public int x;
    public int y;
    public Character character;
    public Vector3 vector;
    public bool IsValid = true;
    public Grid(int x, int y, Character character)
    {
        this.x = x;
        this.y = y;
        this.character = character;
    }
}
