using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NesneBolme : MonoBehaviour
{
    public Material yeniRenkMateryali;

    void Start()
    {
        BolgeyiRenklendir();
    }

    void BolgeyiRenklendir()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;

        Vector3[] vertices = mesh.vertices;
        Color[] renkler = new Color[vertices.Length];

        for (int i = 0; i < renkler.Length; i++)
        {
            renkler[i] = Random.ColorHSV(); // Rastgele renklerle doldurabilirsiniz
        }

        mesh.colors = renkler;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = yeniRenkMateryali;
    }
}
