using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public int edgeSize = 20;
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

    //Variables for objects
    public GameObject tree;
    public GameObject endLevelObject;
    public int treeSpawnHeight = 4;
    public float treeSpawnDepth = 1.5f;
    public float minTreeSpacing = 10;       //The minimum distance after which another tree can spawn

    private Tilemap map;
    private int maxTriesBeforeFailure = 50;
    private List<Vector3> mObjectLocations = new();


    // Start is called before the first frame update
    void Start()
    {
        SpawnTiles();
        Instantiate(endLevelObject, new Vector3(stageSize + edgeSize/2, 2), Quaternion.identity);
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < mObjectLocations.Count; i++)
        {
            Instantiate(tree, mObjectLocations[i], Quaternion.identity);
        }
    }

    private void SpawnTiles()
    {
        map = GetComponent<Tilemap>();

        //Draw invisible walls at edge
        for (int i = -gridDepth; i < invisibleWallHeight; i++)
        {
            map.SetTile(new Vector3Int(-edgeSize, i), invisibleWall);
        }

        //Draw ground from left to the spawn position
        //Draw from double left side so the ground doesn't actually finish at the wall
        for (int i = -edgeSize * 2; i < generationStartPoint; i++)
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
                map.SetTile(new Vector3Int(i - 1, currentHeight - 1), GetTile());
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
                                valid = false;
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
        //Flatten ground back to level 0
        int stageSizePostFlatten = stageSize;
        while (currentHeight != 0)
        {
            if (currentHeight < 0)
            {
                currentHeight++;
                //Add slope tile
                map.SetTile(new Vector3Int(stageSizePostFlatten - 1, currentHeight), groundSlopeUpTile);
                //Replace previous tile with dirt
                map.SetTile(new Vector3Int(stageSizePostFlatten - 1, currentHeight - 1), GetTile());

                map.SetTile(new Vector3Int(stageSizePostFlatten, currentHeight), soilTile);

                for (int j = currentHeight - 1; j > -gridDepth; j--)
                {
                    map.SetTile(new Vector3Int(stageSizePostFlatten, j), GetTile());
                }
            }
            else
            {
                //Add slope tile
                map.SetTile(new Vector3Int(stageSizePostFlatten, currentHeight), groundSlopeDownTile);
                currentHeight--;

                for (int j = currentHeight; j > -gridDepth; j--)
                {
                    map.SetTile(new Vector3Int(stageSizePostFlatten, j), GetTile());
                }
            }
            stageSizePostFlatten++;
        }

        //Draw invisible walls at edge
        for (int i = -gridDepth; i < invisibleWallHeight; i++)
        {
            map.SetTile(new Vector3Int(stageSizePostFlatten + edgeSize, i), invisibleWall);
        }

        //Draw ground from end of play area
        //Draw from double left side so the ground doesn't actually finish at the wall
        for (int i = stageSizePostFlatten; i < stageSize + edgeSize * 2; i++)
        {
            map.SetTile(new Vector3Int(i, 0), soilTile);
            for (int j = -1; j > -gridDepth; j--)
            {
                map.SetTile(new Vector3Int(i, j), GetTile());
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
        for (int i = 0; i < mObjectLocations.Count; i++)
        {
            //If there's another tree in the area, don't spawn a tree
            if (Mathf.Abs(mObjectLocations[i].x - x) < minTreeSpacing)
                return;
        }
        mObjectLocations.Add(new Vector3(x + platformsSize/2, treeSpawnHeight, treeSpawnDepth));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
