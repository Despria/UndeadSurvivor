using System;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Map Setting")]
    /// TileMap Assign Order
    /// 0 1 2 
    /// 3 4 5
    /// 6 7 8
    [SerializeField] Transform[] tiles;
    BoxCollider2D centerCollider; 
    int centerToBorderOfOneTileMapSide = 11;
    int sizeOfOneTileMapSide = 22;
    int tileRow = 3;

    [Header("Map Border")]
    [SerializeField] Vector2Variable mapBorderUpLeft;
    [SerializeField] Vector2Variable mapBorderDownRight;

    [Header("Player Position")]
    [SerializeField] Vector3Variable playerPosition;

    void Awake()
    {
        centerCollider = GetComponent<BoxCollider2D>();

        mapBorderUpLeft.runtimeValue = new Vector2(-sizeOfOneTileMapSide * 1.5f, sizeOfOneTileMapSide * 1.5f);
        mapBorderDownRight.runtimeValue = new Vector2(sizeOfOneTileMapSide * 1.5f, -sizeOfOneTileMapSide * 1.5f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CheckPlayerPosition();
        }
    }

    void CheckPlayerPosition()
    {
        /// Right: 0
        if (playerPosition.runtimeValue.x > centerCollider.offset.x + centerToBorderOfOneTileMapSide) 
        {
            MoveTiles(0);

            mapBorderUpLeft.runtimeValue += Vector2.right * sizeOfOneTileMapSide;
            mapBorderDownRight.runtimeValue += Vector2.right * sizeOfOneTileMapSide;
            centerCollider.offset += Vector2.right * sizeOfOneTileMapSide;
        }
        /// Left: 1                                                                           
        else if (playerPosition.runtimeValue.x < centerCollider.offset.x - centerToBorderOfOneTileMapSide) 
        {
            MoveTiles(1);

            mapBorderUpLeft.runtimeValue += Vector2.left * sizeOfOneTileMapSide;
            mapBorderDownRight.runtimeValue += Vector2.left * sizeOfOneTileMapSide;
            centerCollider.offset += Vector2.left * sizeOfOneTileMapSide;
        }                                                                                    
        /// Up: 2                                                                            
        else if (playerPosition.runtimeValue.y > centerCollider.offset.y + centerToBorderOfOneTileMapSide)
        {
            MoveTiles(2);

            mapBorderUpLeft.runtimeValue += Vector2.up * sizeOfOneTileMapSide;
            mapBorderDownRight.runtimeValue += Vector2.up * sizeOfOneTileMapSide;
            centerCollider.offset += Vector2.up * sizeOfOneTileMapSide;
        }
        /// Down: 3                                                                          
        else if (playerPosition.runtimeValue.y < centerCollider.offset.y - centerToBorderOfOneTileMapSide)
        {
            MoveTiles(3);

            mapBorderUpLeft.runtimeValue += Vector2.down * sizeOfOneTileMapSide;
            mapBorderDownRight.runtimeValue += Vector2.down * sizeOfOneTileMapSide;
            centerCollider.offset += Vector2.down * sizeOfOneTileMapSide;
        }
    }

    void MoveTiles(int direction)
    {
        Transform[] oldTiles = new Transform[tiles.Length];
        Array.Copy(tiles, oldTiles, tiles.Length);

        switch (direction)
        {
            case 0: // Right
                for (int i = 0; i < tiles.Length; i++)
                {
                    int reOrder = i % 3;

                    if (reOrder == 0)
                    {
                        // 기존 타일 배열의 인덱스 변경
                        // (오브젝트 위치만 바뀌고 인덱스는 그대로 하기 위함)
                        tiles[i + 2] = oldTiles[i];

                        // 타일 이동
                        oldTiles[i].position += sizeOfOneTileMapSide * tileRow * Vector3.right;
                    }
                    else
                    {
                        tiles[i - 1] = oldTiles[i];
                    }
                }
                break;

            case 1: // Left
                for (int i = 0; i < tiles.Length; i++)
                {
                    int reOrder = i % 3;

                    if (reOrder == 2)
                    {
                        tiles[i - 2] = oldTiles[i];
                        oldTiles[i].position += sizeOfOneTileMapSide * tileRow * Vector3.left;
                    }
                    else
                    {
                        tiles[i + 1] = oldTiles[i];
                    }
                }
                break;

            case 2: // Up
                for (int i = 0; i < tiles.Length; i++)
                {
                    int reOrder = i + 3;

                    if (reOrder >= 9)
                    {
                        tiles[i - 6] = oldTiles[i];
                        oldTiles[i].position += sizeOfOneTileMapSide * tileRow * Vector3.up;
                    }
                    else
                    {
                        tiles[i + 3] = oldTiles[i];
                    }
                }
                break;

            case 3: // Down
                for (int i = 0; i < tiles.Length; i++)
                {
                    int reOrder = i - 3;

                    if (reOrder < 0)
                    {
                        tiles[i + 6] = oldTiles[i];
                        oldTiles[i].position += sizeOfOneTileMapSide * tileRow * Vector3.down;
                    }
                    else
                    {
                        tiles[i - 3] = oldTiles[i];
                    }
                }
                break;
        }
    }
}