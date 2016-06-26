using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MouseManager : MonoBehaviour {

    public GameObject cursorSelectorPrefab;
    public World worldController;
    public Text infoText;

    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    Vector3 dragStartPos;
    List<GameObject> dragPreviewObjects = new List<GameObject>();

    public Tile.TileType buildingType;
    public bool isBuilding = false;

    void Update()
    {
        DisplayInfo();

        if (isBuilding)
        {
            currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currFramePosition.z = 0;

            //UpdateCursor();
            UpdateDragging();

            lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lastFramePosition.z = 0;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isBuilding = false;
            }
        }
    }

    //void UpdateCursor()
    //{
    //    Tile tileUnderMouse = GetTileAtWorlCoord(currFramePosition);
    //    if (tileUnderMouse != null)
    //    {
    //        cursorSelectorPrefab.SetActive(true);
    //        Vector3 cursorPos = new Vector3(tileUnderMouse.x, tileUnderMouse.y, 0);
    //        cursorSelectorPrefab.transform.position = cursorPos;
    //    }
    //    else
    //    {
    //        cursorSelector.SetActive(false);
    //    }
    //}

    void UpdateDragging()
    {
        //if we are over ui element get the hell out
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //Start drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = currFramePosition;
        }

        int startX = Mathf.FloorToInt(dragStartPos.x);
        int endX = Mathf.FloorToInt(currFramePosition.x);
        int startY = Mathf.FloorToInt(dragStartPos.y);
        int endY = Mathf.FloorToInt(currFramePosition.y);

        //If we drag things in another direction flip things around
        if (endX < startX)
        {
            int tmp = endX;
            endX = startX;
            startX = tmp;
        }
        if (endY < startY)
        {
            int tmp = endY;
            endY = startY;
            startY = tmp;
        }

        //Clean up old drag previews
        while (dragPreviewObjects.Count > 0)
        {
            GameObject go = dragPreviewObjects[0];
            dragPreviewObjects.RemoveAt(0);
            SimplePool.Despawn(go);
        }

        //Display preview of what we are going to build
        if (Input.GetMouseButton(0))
        {
            //Loop through all the tiles selected
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    Tile t = worldController.GetTileAt(x, y);
                    if (t != null)
                    {
                        //Display preview ontop of the tile
                        GameObject go = SimplePool.Spawn(cursorSelectorPrefab, new Vector3(x, y, 0), Quaternion.identity);
                        go.transform.SetParent(this.transform, true);
                        dragPreviewObjects.Add(go);
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Loop through all the tiles selected
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    Tile t = worldController.GetTileAt(x, y);
                    if (t != null && t.buildable)
                    {
                        //Build the tile that is selected
                        switch (buildingType)
                        {
                            case Tile.TileType.Empty:
                                t.type = Tile.TileType.Empty;
                                break;
                            case Tile.TileType.Floor:
                                t.type = Tile.TileType.Floor;
                                break;
                            case Tile.TileType.Wall:
                                t.type = Tile.TileType.Wall;
                                break;
                        }
                    }
                }
            }
        }
    }

    void DisplayInfo()
    {
        Tile tileUnderMouse = GetTileAtWorlCoord(currFramePosition);
        if (tileUnderMouse != null)
        {
            infoText.text = "Tile: " + tileUnderMouse.type + ", More info here";
        }
    }

    public void StartBuild(Tile.TileType type)
    {
        isBuilding = true;
        buildingType = type;
    }

    Tile GetTileAtWorlCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return worldController.GetTileAt(x, y);
    }
}
