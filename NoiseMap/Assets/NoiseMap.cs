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

    private void Start()
    {
        Texture2D randomNoiseMap = Utils.GetRandomNoiseMap(Random.Range(0, 10000), Random.Range(0, 10000), MAP_SIZE, NOISE_SIZE);
        Texture2D maskMap = Utils.GetCircularMaskMap(MAP_SIZE);
        Sprite sprite = Sprite.Create(Utils.GetMaskingMap(MAP_SIZE, randomNoiseMap, maskMap), Rect.MinMaxRect(0,0, MAP_SIZE, MAP_SIZE), new Vector2(0.5f, 0.5f));
        m_spriteRenderer.sprite = sprite;
    }
}