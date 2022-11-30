using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TerrainGeneration : MonoBehaviour
{
    public Tile[] groundTiles;
    public Tile soilTile;
    public Tile groundSlopeUpTile;
    public Tile groundSlopeDownTile;
    public Tile invisibleWall;
    public Tile platformTile;
    public int stageSize = 200;
    public int leftSize = 20;
    public int invisibleWallHeight = 20;
    public int gridDepth = 10;
    public int generationStartPoint = 5;
    public int groundMinHeight = -3;
    public int groundMaxHeight = 3;
    public int platformMaxHeight = 10;
    public int heightChangeChance = 4;
    public int platformsToSpawn = 15;
    public int platformsSize = 3;
    public int platformSpacing = 3;         //The minimum space under each platform

    private Tilemap map;
    private int maxTriesBeforeFailure = 50;


    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Tilemap>();

        //Draw invisible walls at edges
        for (int i = -gridDepth; i < invisibleWallHeight; i++)
        {
            map.SetTile(new Vector3Int(-leftSize, i), invisibleWall);
            map.SetTile(new Vector3Int(stageSize, i), invisibleWall);
        }

        //Draw ground from left to the spawn position
        for (int i = -leftSize; i < generationStartPoint; i++)
        {
            map.SetTile(new Vector3Int(i, 0), soilTile);
            for (int j = -1; j > -gridDepth; j--)
            {
                map.SetTile(new Vector3Int(i, j), GetTile());
            }
        }
        int currentHeight = 0;
        //Flags to stop quick changes in direction
        bool wentDown = false;
        bool wentUp = false;
        //Draw ground, moving up and down randomly
        for (int i = generationStartPoint; i < stageSize; i++)
        {
            int heightChange = Random.Range(0, 1 + heightChangeChance);
            if (heightChange == 0 && currentHeight > groundMinHeight && !wentUp)
            {
                //Add slope tile
                map.SetTile(new Vector3Int(i, currentHeight), groundSlopeDownTile);
                currentHeight--;
                wentDown = true;
                wentUp = false;
            }
            else if (heightChange == heightChangeChance && currentHeight < groundMaxHeight && !wentDown)
            { 
                currentHeight++;
                wentUp = true;
                wentDown = false;
                //Add slope tile
                map.SetTile(new Vector3Int(i - 1, currentHeight), groundSlopeUpTile);
                //Replace previous tile with dirt
                map.SetTile(new Vector3Int(i - 1, currentHeight- 1), GetTile());
            }
            else
            {
                wentDown = false;
                wentUp = false;
            }
            //Fill in ground, all the way to the bottom
            Tile topTile;
            if (!wentDown)
                topTile = soilTile;
            else
                topTile = GetTile();
            map.SetTile(new Vector3Int(i, currentHeight), topTile);

            for (int j = currentHeight - 1; j > -gridDepth; j--)
            {
                map.SetTile(new Vector3Int(i, j), GetTile());
            }
        }

        //Add upper platforms for platforming
        for (int i = 0; i < platformsToSpawn; i++) 
        {
            bool valid = false;
            int triesBeforeFailure = maxTriesBeforeFailure;
            while (valid == false)
            {
                //Platform size +2 because you have to consider the sides to tell if it's a valid platform
                int xcord = Random.Range(generationStartPoint, stageSize - (platformsSize + 2));
                int spawnLevel = 0;
                while (spawnLevel < platformMaxHeight)
                {
                    valid = true;
                    for (int j = 0; j < platformsSize + 2; j++)
                    {
                        for (int k = spawnLevel - (platformSpacing); k < spawnLevel + (platformSpacing + 1); k++)
                        {
                            if (map.HasTile(new Vector3Int(xcord + j, k)))
                            {
                                valid= false;
                                break;
                            }
                        }
                        if (valid == false)
                        {
                            break;
                        }
                    }
                    if (valid == true)
                    {
                        //There is an empty space under and above this area, so we can spawn a platform
                        SpawnPlatform(xcord + 1, spawnLevel);
                        break;
                    }
                    else
                    {
                        spawnLevel++;
                        triesBeforeFailure--;
                        if (triesBeforeFailure < 0)
                        {
                            Debug.LogError("Spawning platforms failed, no valid platform spot found after " + maxTriesBeforeFailure + " tries");
                            return;
                        }
                    }
                }
            }
        }
    }

    private Tile GetTile()
    {
        int i = Random.Range(0, groundTiles.Length);
        return groundTiles[i];
    }

    void SpawnPlatform(int x, int y)
    {
        for (int i = 0; i < platformsSize; i++)
        {
            map.SetTile(new Vector3Int(x + i, y), platformTile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
