using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    float characterX = 0.6f;
    float characterY = 0.6f;
    float charPosX;
    float charPosY;
    Vector3[] charPosV2;
    Vector3[] deadGrids = {new Vector3(-1.83f, 1.7f, -0.5f),
                new Vector3(-1.83f, 2.3f, -0.5f),
                new Vector3(-1.17f, 1.7f, -0.5f),
                new Vector3(-1.17f, 2.3f, -0.5f)};
    GridType[] gridTypes;
    [SerializeField] LayerMask charLayer;
    public List<Vector3> EmptyGrids = new List<Vector3>();

    public enum GridType
    {
        full,
        empty,
        dead
    };
    private void Start()
    {
        CalculateLoc(out charPosV2);
        FindEmptyGrid();
        FindCharAddToGrid();

    }
    private void Update()
    {

    }
    private void OnDrawGizmos()
    {
        Vector3 vector1 = new Vector3(0f, 0f, 0f);
        Vector3 vector2 = new Vector3(characterX, characterY, 0.6f);
        Vector3[] charPos = new Vector3[49];
        Gizmos.color = Color.blue;
        int y = 0;
        for (float i = 0; i < 3.6; i += 0.6f)
        {
            for (float j = 0; j < 3.6; j += 0.6f)
            {

                vector1.x = j;
                vector1.y = i;
                vector1.z = -0.5f;

                charPosX = (gameObject.transform.position.x + vector1.x);
                charPosY = (gameObject.transform.position.y + vector1.y) + 0.3f;

                charPos[y] = new Vector3(charPosX, charPosY, -0.5f);
                y++;
            }
        }
        for (int i = 0; i < 49; i++)
        {
            Vector3 vector = new Vector3(charPos[i].x, charPos[i].y, -0.5f);
            Gizmos.DrawWireCube(vector, vector2);

        }
    }
    void CalculateLoc(out Vector3[] charPos)
    {
        Vector3 vector1 = new Vector3(0f, 0f, 0f);
        charPos = new Vector3[49];
        gridTypes = new GridType[49];
        int y = 0;

        for (float i = 0; i < 3.6; i += 0.6f)
        {
            for (float j = 0; j < 3.6; j += 0.6f)
            {

                vector1.x = j;
                vector1.y = i;
                vector1.z = -0.5f;

                charPosX = (gameObject.transform.position.x + vector1.x);
                charPosY = (gameObject.transform.position.y + vector1.y);

                charPos[y] = new Vector3(charPosX, charPosY, -0.5f);


                if (y == 14 || y == 20 || y == 21 || y == 27)
                {
                    gridTypes[y] = GridType.dead;
                }
                else
                {
                    gridTypes[y] = GridType.empty;
                }

                //Debug.Log(charPos[y]);
                y++;
            }
        }

    }
    public void FindEmptyGrid()
    {
        EmptyGrids.Clear();
        for (int i = 0; i < charPosV2.Length; i++)
        {
            if (gridTypes[i] == GridType.empty)
            {
                EmptyGrids.Add(charPosV2[i]);
            }
        }
    }

    void CheckGrids(Character character)
    {
        Vector3 characterPos = character.gameObject.transform.position;
        for (int i = 0; i < charPosV2.Length; i++)
        {

            if (characterPos == charPosV2[i] && gridTypes[i] == GridType.empty)
            {
                gridTypes[i] = GridType.full;
                return;
            }
        }
    }
    void FindCharAddToGrid()
    {
        Character[] charGO = FindObjectsOfType<Character>();
        for (int i = 0; i < charGO.Length; i++)
        {
            charGO[i].transform.position = EmptyGrids[i];
            charGO[i].SpawnedAnimation();
            CheckGrids(charGO[i]);
        }
    }
    public void TurnToFullGrid(Vector3 vector)
    {
        for (int i = 0; i < charPosV2.Length; i++)
        {
            if (vector == charPosV2[i])
            {
                gridTypes[i] = GridType.full;
                break;
            }
        }
    }
    public void TurnToEmptyGrid(Vector3 vector)
    {
        for (int i = 0; i < charPosV2.Length; i++)
        {
            if (vector == charPosV2[i])
            {
                gridTypes[i] = GridType.empty;
                break;
            }
        }
        FindEmptyGrid();
    }
}
