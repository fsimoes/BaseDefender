using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character  {
    
    public float X
    {
        get
        {
            return Mathf.Lerp(currTile.X, destTile.X,movementPercentage);
        }
    }
    public float Y
    {
        get
        {
            return Mathf.Lerp(currTile.Y, destTile.Y, movementPercentage);
        }
    }

   public Tile currTile { get; protected set; }
    Tile destTile;

    float movementPercentage;
    float speed = 2f;

    public Character(Tile tile)
    {
        currTile = destTile = tile;
    }

    public void Update(float deltaTime)
    {
        if(currTile == destTile)
        {
            return;
        }


        float distToTravel = Mathf.Sqrt(Mathf.Pow(currTile.X - destTile.X,2) + Mathf.Pow(currTile.Y - destTile.Y, 2));

        float distThisFrame = speed * deltaTime;

        float percThisFrame = distThisFrame / distThisFrame;

        movementPercentage += percThisFrame;

        if(movementPercentage >= 1)
        {
            currTile = destTile;
            movementPercentage = 0;
        }
    }

    public void SetDestination(Tile tile)
    {
        if(currTile.IsNeighbour(tile,true) == false)
        {
            Debug.Log("SetDestination");
        }
        destTile = tile;
    }

}
