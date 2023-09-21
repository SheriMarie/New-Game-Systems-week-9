using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCustom : MonoBehaviour
{
    public Texture2D getPixelTexture;
    public Texture2D setPixelTexture;
    public Color[] regionColours;
    public Color[] newPixelColours;

    void GetColours()
    {
        List<Color> colorsInImage = new List<Color>();
        for (int x = 0; x < getPixelTexture.width; x++)
        {
            for (int y = 0; y < getPixelTexture.height; y++)
            {
                if (getPixelTexture.GetPixel(x, y).a !=0)
                {
                    Color pixelColour = getPixelTexture.GetPixel(x, y);
                    if (!colorsInImage.Contains(pixelColour))
                    {
                        colorsInImage.Add(pixelColour);
                    }
                }
            }
        }
        regionColours = colorsInImage.ToArray();
        List<Color> c = new List<Color>();
        for (int i = 0; i < regionColours.Length; i++)
        {
            c.Add(regionColours[i]);
            newPixelColours = c.ToArray();
        }
    }

    void SetColours()
    {
        for (int x = 0; x < getPixelTexture.width; x++)
        {
            for (int y = 0; y < getPixelTexture.height; y++)
            {
                if (getPixelTexture.GetPixel(x, y).a != 0)
                {
                    for (int i = 0; i < regionColours.Length; i++)
                    {
                        if (getPixelTexture.GetPixel(x, y) == regionColours[i])
                        {
                            setPixelTexture.SetPixel(x, y, newPixelColours[i]);
                        }

                    }
                }
            }
        }
        setPixelTexture.Apply();
    }

    private void Awake()
    {
        GetColours();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetColours();
        }
    }
}
