using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMap : MonoBehaviour
{
    readonly int MAP_SIZE = 100;
    readonly float NOISE_SIZE = 5f;

    [SerializeField] private SpriteRenderer m_spriteRenderer;

    private void OnValidate()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Texture2D GetRandomNoiseMap(int mapSize, float noiseSize)
    {
        Texture2D texture = new Texture2D(mapSize, mapSize);
        texture = new Texture2D(mapSize, mapSize);
        Color[] colorMap = new Color[mapSize * mapSize];
        for (int i = 0; i < mapSize; ++i)
        {
            for (int j = 0; j < mapSize; ++j)
            {
                float perlinNoise = Mathf.PerlinNoise((float)j / mapSize * noiseSize, (float)i / mapSize * noiseSize);
                Color color = Color.white * perlinNoise;
                color.a = 1;
                colorMap[i * mapSize + j] = color;
            }
        }
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }
    public Texture2D GetCircularMaskMap(int size)
    {
        Texture2D texture = new Texture2D(size, size);
        texture = new Texture2D(size, size);
        Color[] colorMap = new Color[size * size];
        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; ++j)
            {
                float value = Mathf.InverseLerp(0, size/2, Vector2.Distance(new Vector2(i, j), new Vector2(size / 2, size / 2)));
                Color color = Color.white * value;
                color.a = 1;
                colorMap[i * size + j] = color;
            }
        }
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }
    public Texture2D GetMaskingMap(int size, Texture2D origMap, Texture2D maskMap)
    {
        Texture2D texture = new Texture2D(size, size);
        Color[] colorMap = new Color[size * size];
        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; ++j)
            {
                float gray = Mathf.InverseLerp(0, 2, origMap.GetPixel(j, i).grayscale + maskMap.GetPixel(j, i).grayscale);
                colorMap[i * size + j] = new Color(gray, gray, gray, 1);
            }
        }
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    private void Start()
    {
        Texture2D randomNoiseMap = GetRandomNoiseMap(MAP_SIZE, NOISE_SIZE);
        Texture2D maskMap = GetCircularMaskMap(MAP_SIZE);
        Sprite sprite = Sprite.Create(GetMaskingMap(MAP_SIZE, randomNoiseMap, maskMap), Rect.MinMaxRect(0,0, MAP_SIZE, MAP_SIZE), new Vector2(0.5f, 0.5f));
        m_spriteRenderer.sprite = sprite;
    }
}