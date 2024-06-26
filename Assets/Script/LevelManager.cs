using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Level texture")]
    [SerializeField] private Texture2D levelTexture;
    [Header("Tiles Prefabs")]
    [SerializeField] private GameObject prefabWallTile;
    [SerializeField] private GameObject prefabRoadTile;
    [HideInInspector] public List<RoadTile> roadTilesList = new List<RoadTile> ();
    [HideInInspector] public RoadTile defaultBallRoadTile;

    private Color colorWall = Color.white;
    private Color colorRoad = Color.black;
    private float unitPerPixel;
    void Start()
    {

    }
    private void Awake()
    {
        Generate();
        defaultBallRoadTile = roadTilesList[0];
    }
    private void Generate()
    {
        unitPerPixel = prefabWallTile.transform.lossyScale.x;
        float halfUnitPerPixel = unitPerPixel / 2f;
        float width = levelTexture.width;
        float height = levelTexture.height;
        Vector3 offset = (new Vector3(width / 2f, 0f, height / 2f) * unitPerPixel)
            - new Vector3(halfUnitPerPixel, 0f, halfUnitPerPixel); 
        for (int x = 0; x < width; x++){
          for (int y = 0; y < height; y++) {
                //Get pixel color :
                Color pixelColor = levelTexture.GetPixel(x, y);
                Vector3 spawnPos = ((new Vector3(x, 0f, y) * unitPerPixel) - offset);
                if (pixelColor == colorWall)
                    Spawn(prefabWallTile, spawnPos);
                else if (pixelColor == colorRoad)
                    Spawn(prefabRoadTile, spawnPos);
          }
        }
    }
    private void Spawn(GameObject prefabTile, Vector3 position)
    {
        //fix Y position:
        position.y = prefabTile.transform.position.y;
        GameObject obj = Instantiate(prefabTile, position, Quaternion.identity,transform);
        if (prefabTile == prefabRoadTile)
            roadTilesList.Add(obj.GetComponent<RoadTile>());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
