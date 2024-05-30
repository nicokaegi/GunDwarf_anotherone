using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AstarPath;


public class CaveGen : MonoBehaviour
{
    public int startX, startY, width, height;
    public float seed;
    [Range(0,1)]
    public float modifier;
    public GameObject wallTile;
    public GameObject groundTile;
    public GameObject tressureTile;
    public GameObject trigger;

    public GameObject enemy;

    public bool north, south, east, west;
    public double caveUpdateTime;
    private double timeholder= 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        seed = Random.Range(-10000, 10000);
        Vector2Int starting_coord = new Vector2Int(startX, startY);
        Vector2Int exclude_top_right = new Vector2Int(9, 9);
        Vector2Int exclude_bottom_left = new Vector2Int(-9, -9);

        PerlinCaveGen(starting_coord, width, height, exclude_bottom_left, exclude_top_right);


    }

    public void PerlinCaveGen(Vector2Int starting_coord, int width, int height, Vector2Int exclude_bottom_left, Vector2Int exclude_top_right){

        for(int x = starting_coord.x; x < starting_coord.x + width; x++){

            for(int y = starting_coord.y; y < starting_coord.y + height; y++){

                // to make sure the ecluded area doesn't get filled in
                if(!((exclude_bottom_left.x < x && x < exclude_top_right.x) && (exclude_bottom_left.y < y && y < exclude_top_right.y))){

                    float tileProb = 1 - Mathf.PerlinNoise((x * modifier) + seed, (y * modifier) + seed);
                    // if bigger
                    if (tileProb > .95 ){

                        Instantiate(tressureTile, new Vector2(x, y), Quaternion.identity);

                    }
                    // if the noise is greater than fifty spawn a wall
                    else if( tileProb > .50 ){

                        Instantiate(wallTile, new Vector2(x, y), Quaternion.identity);

                    }else{

                        Instantiate(groundTile, new Vector2(x, y), Quaternion.identity);


                        // spawn enemys on some p ercentage of the cave floor
                        if ( 0.10 > Random.Range(0.0f,99.0f)){

                            Instantiate(enemy, new Vector2(x, y), Quaternion.identity);

                        }



                    }

                }else{

                    Instantiate(groundTile, new Vector2(x, y), Quaternion.identity);

                }
            }
        }
    }

    void Update(){

        timeholder += Time.deltaTime;
        if(timeholder >= caveUpdateTime){
            AstarPath.active.Scan();
            timeholder = 0.0;
        }

    }

}
