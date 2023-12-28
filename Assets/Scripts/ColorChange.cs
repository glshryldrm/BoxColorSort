using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Color baseColor = Color.blue; // Baþlangýç rengi
    public float variationCount = 1f; // Farklý tonlarda kaç renk gösterileceði
    public float colorChangeSpeed = 0.5f; // Renk deðiþim hýzý
    private Color ligthBaseColor;
    private Color darkBaseColor;

    void Start()
    {
        //// Nesnenin baþlangýç rengini ayarla
        //GetComponent<Renderer>().material.color = baseColor;
    }

    void Update()
    {
        // Her frame'de renk deðiþimini güncelle
        ChangeColorOverTime();
    }

    void ChangeColorOverTime()
    {



        float brightness = variationCount; // Parlaklýk deðeri (0 ile 1 arasýnda)
        ligthBaseColor = ChangeBrightness(baseColor, brightness);
        brightness = -variationCount; // Parlaklýk deðeri (0 ile 1 arasýnda)
        darkBaseColor = ChangeBrightness(baseColor, brightness);


        // Renk deðiþimini zamanla güncelle
        float lerpValue = Mathf.PingPong(Time.time * colorChangeSpeed, 1.0f); // 0 ile 1 arasýnda lineer bir deðer elde et
        Color lerpedColor = Color.Lerp(ligthBaseColor, darkBaseColor, lerpValue); // Renk deðiþimini uygula

        // Nesnenin rengini güncelle
        GetComponent<Renderer>().material.color = lerpedColor;
        

    }
    Color ChangeBrightness(Color color, float brightness)
    {
        // Renk parlaklýðýný deðiþtirir
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);
        return Color.HSVToRGB(h, s, brightness);
    }
}