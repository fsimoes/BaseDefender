using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldController : MonoBehaviour {

    private static WorldController instance;

    public Sprite floorSprite;

    private World world;

    public static WorldController Instance
    {
        get
        {
            return instance;
        }

        protected set
        {
            instance = value;
        }
    }

    public World World
    {
        get
        {
            return world;
        }

       protected set
        {
            world = value;
        }
    }

    // Use this for initialization
    void Start () {
        if(Instance != null)
        {
            //error
        }
        Instance = this;
        //create empty world
        world = new World();
        //Create GO to each tiles
        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                Tile tile_data = world.GetTilesAt(x, y);

                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X,tile_data.Y,0);
                tile_go.transform.SetParent(this.transform,true);
                //create a empty sprite renderer
                tile_go.AddComponent<SpriteRenderer>();

                //lambda ()=> is equals to a empty function
                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tile_go); });
            }
        }

        world.randomizeTitles();
    }

   
	// Update is called once per frame
	void Update () {
      
	}

    void OnTileTypeChanged(Tile tile_data, GameObject tile_go)
    {
        if (tile_data.Type == Tile.TileType.Floor)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }else if(tile_data.Type == Tile.TileType.Empty)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = null;
        }else
        {
            Debug.LogError("OnTileTypeChanged - Unrecognized tile type.");
        }
    }
}
