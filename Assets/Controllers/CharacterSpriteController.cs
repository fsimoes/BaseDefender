﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteController : MonoBehaviour {
    Dictionary<Character, GameObject> characterGameObjectMap;

    Dictionary<string, Sprite> characterSprites;

    World world
    {
        get { return WorldController.Instance.world; }
    }

    // Use this for initialization

    void Start()
    {
        LoadSprites();

        // Instantiate our dictionary that tracks which GameObject is rendering which Tile data.
        characterGameObjectMap = new Dictionary<Character, GameObject>();

        // Register our callback so that our GameObject gets updated whenever
        // the tile's type changes.
        world.RegisterCharacterCreated(OnCharacterCreated);

        world.CreateCharacter(world.GetTileAt(world.Width/2, world.Height/2));
    }

    void LoadSprites()
    {
        characterSprites = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/Characters/");

        Debug.Log("LOADED RESOURCE:");
        foreach (Sprite s in sprites)
        {
            //Debug.Log(s);
            characterSprites[s.name] = s;
        }
    }

    public void OnCharacterCreated(Character character)
    {
        //Debug.Log("OnFurnitureCreated");
        // Create a visual GameObject linked to this data.

        // FIXME: Does not consider multi-tile objects nor rotated objects

        // This creates a new GameObject and adds it to our scene.
        GameObject char_go = new GameObject();

        // Add our tile/GO pair to the dictionary.
        characterGameObjectMap.Add(character, char_go);

        char_go.name = "Character";
        char_go.transform.position = new Vector3(character.currTile.X, character.currTile.Y, 0);
        char_go.transform.SetParent(this.transform, true);

        SpriteRenderer sr = char_go.AddComponent<SpriteRenderer>();
        sr.sprite = characterSprites["p1_front"];
        sr.sortingLayerName = "Character";
        // Register our callback so that our GameObject gets updated whenever
        // the object's into changes.
        //character.RegisterOnChangedCallback(OnFurnitureChanged);

    }

    //void OnFurnitureChanged(Furniture furn)
    //{
    //    //Debug.Log("OnFurnitureChanged");
    //    // Make sure the furniture's graphics are correct.

    //    if (characterGameObjectMap.ContainsKey(furn) == false)
    //    {
    //        Debug.LogError("OnFurnitureChanged -- trying to change visuals for furniture not in our map.");
    //        return;
    //    }

    //    GameObject furn_go = characterGameObjectMap[furn];
    //    //Debug.Log(furn_go);
    //    //Debug.Log(furn_go.GetComponent<SpriteRenderer>());

    //    furn_go.GetComponent<SpriteRenderer>().sprite = GetSpriteForFurniture(furn);
    //}



}
