using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHelper
{

    public static void GenerateColors(Color startColor, Color endColor, int colorCount, out Color[] colors)
    {
        colors = new Color[colorCount];

        for(int i = 0;i <colorCount; i++)
        {
            Color lerpedColor = Color.Lerp(startColor, endColor, (float)i / (float)colorCount);
            colors[i] = lerpedColor;
        }
    }
    
}
[System.Serializable]
public class ColorPair
{
    public Color color1;
    public Color color2;
}

