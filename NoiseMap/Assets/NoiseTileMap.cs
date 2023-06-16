using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseTileMap : MonoBehaviour
{
    [SerializeField] private int mapSize;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile tile;

    private void Start()
    {
        Texture2D randomNoiseMap = Utils.GetRandomNoiseMap(mapSize, 5);
        Texture2D maskMap = Utils.GetCircularMaskMap(mapSize);
        Texture2D texture = Utils.GetMaskingMap(mapSize, randomNoiseMap, maskMap);
        for (int i = 0; i < mapSize; ++i)
        {
            for(int j = 0; j < mapSize; ++j)
            {
                Vector3Int pos = new Vector3Int(j - mapSize / 2, i - mapSize / 2, 0);
                tilemap.SetTile(pos, tile);
                tilemap.SetTileFlags(pos, TileFlags.None);
                float value = texture.GetPixel(j, i).grayscale;
                if(value > 0.5f)
                {
                    tilemap.SetColor(pos, Color.blue);
                }
                else
                {
                    tilemap.SetColor(pos, Color.green);
                }
            }
        }
    }
}