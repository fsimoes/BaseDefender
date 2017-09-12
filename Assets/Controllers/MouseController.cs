using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public GameObject circleCursor;

    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    Vector3 dragStartPostion;
    List<GameObject> dragPreviewGameObjects;
	// Use this for initialization
	void Start () {
        dragPreviewGameObjects = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {

        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;
        //update circle position
        Tile tileUnderMouse = GetTileAtWorldCoord(currFramePosition);
        if(tileUnderMouse != null)
        {
            circleCursor.SetActive(true);
            Vector3 cursorPosition = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
            circleCursor.transform.position = cursorPosition;
        }else
        {
            circleCursor.SetActive(false);
        }

        //Start Drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPostion = currFramePosition;
        }
        int start_x = Mathf.FloorToInt(dragStartPostion.x);
        int end_x = Mathf.FloorToInt(currFramePosition.x);
        if (end_x < start_x)
        {
            int temp = end_x;
            end_x = start_x;
            start_x = temp;

        }
        int start_y = Mathf.FloorToInt(dragStartPostion.y);
        int end_y = Mathf.FloorToInt(currFramePosition.y);
        if (end_y < start_y)
        {
            int temp = end_y;
            end_y = start_y;
            start_y = temp;

        }
        //Clean  old drag

        while (dragPreviewGameObjects.Count > 0)
        {
            GameObject go = dragPreviewGameObjects[0];
            dragPreviewGameObjects.RemoveAt(0);
            SimplePool.Despawn(go);
        }

        //Display Drag Area
        if (Input.GetMouseButton(0))
        {
            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldController.Instance.World.GetTilesAt(x, y);
                    if (t != null)
                    {
                        GameObject go = SimplePool.Spawn(circleCursor, new Vector3(x,y,0),Quaternion.identity);
                        go.transform.SetParent(this.transform, true);
                        dragPreviewGameObjects.Add(go);
                    }

                }
            }

        }

        //Stop Drag
        if (Input.GetMouseButtonUp(0))
        {         
          for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldController.Instance.World.GetTilesAt(x, y);
                    if(t != null)
                    {
                        t.Type = Tile.TileType.Floor;
                    }

                }
            }
        }


        //handle screen dragging
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2)) //hold right or middle button
        {
            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);
        }

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize,3f,25f);

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }

    Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        

        return WorldController.Instance.World.GetTilesAt(x, y);
    }
}
