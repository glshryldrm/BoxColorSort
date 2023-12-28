using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeOverTime : MonoBehaviour
{
    private float lerpValue = 0.0f; // Renk geçiþ deðeri
    public Color startColor; // Baþlangýç rengi
    public Color endColor; // Bitiþ rengi
    public float colorChangeSpeed = 0.2f; // Renk deðiþim hýzý

    void Start()
    {
        // Nesnenin baþlangýç rengini belirle
        GetComponent<Renderer>().material.color = startColor;
    }

    void Update()
    {
        // Her frame'de renk deðiþimini güncelle
        ChangeColorOverTime();
    }

    void ChangeColorOverTime()
    {
        // Renk deðiþimini zamanla güncelle
        lerpValue += Time.deltaTime * colorChangeSpeed;

        // Baþlangýçtan bitiþe doðru renk geçiþini gerçekleþtir
        Color lerpedColor = Color.Lerp(startColor, endColor, lerpValue);

        // Nesnenin rengini güncelle
        GetComponent<Renderer>().material.color = lerpedColor;

        // Geçiþ tamamlandýðýnda sýfýrla
        if (lerpValue >= 1.0f)
        {
            Destroy(gameObject); // Renk geçiþi tamamlandýðýnda nesneyi yok et
        }
    }
}