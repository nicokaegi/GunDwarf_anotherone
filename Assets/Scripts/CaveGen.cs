using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AstarPath;


public class CaveGen : MonoBehaviour
{
    public Vector2Int starting_coord;
    public Vector2Int ending_coord;
    public Vector2Int exclude_starting_coord;
    public Vector2Int exclude_ending_coord;
    public int generationThreshold = 1000000;


    public float seed;
    [Range(0,1)]
    public float modifier;
    public GameObject wallTile;
    public GameObject groundTile;
    public GameObject tressureTile;

    public GameObject enemy;

    public double caveUpdateTime;
    private double timeholder = 0.0f;

    private GameObject playerHolder;

    // Start is called before the first frame update
    void Start()
    {
        playerHolder = GameObject.Find("Carrot");
        seed = Random.Range(-10000, 10000);

        //bottom left
        Vector2Int exclude_start_point = new Vector2Int(-9, -9);
        //top right
        Vector2Int exclude_ending_coord = new Vector2Int(9, 9);

        PerlinCaveGen(starting_coord, ending_coord, exclude_start_point, exclude_ending_coord);


    }

    void Update(){

        // attempt at writing infinate cave generation but it doesn't perform very well
        /*
        float distanceToPlayer = Vector3.Distance(transform.position, playerHolder.transform.position);

        if(distanceToPlayer > generationThreshold){

            exclude_starting_coord[0] = starting_coord[0] + 1;
            exclude_starting_coord[1] = starting_coord[1] + 1;

            exclude_ending_coord[0] = ending_coord[0] + 1;
            exclude_ending_coord[1] = ending_coord[1] + 1;


            starting_coord[0] -= 1;
            starting_coord[1] -= 1;

            ending_coord[0] += 1;
            ending_coord[1] += 1;

            generationThreshold += 1;

            PerlinCaveGen(starting_coord, ending_coord, exclude_starting_coord, exclude_ending_coord);


        }
        */



    }

    public void PerlinCaveGen(Vector2Int starting_coord, Vector2Int ending_coord, Vector2Int exclude_starting_coord, Vector2Int exclude_ending_coord){

        for(int x = starting_coord.x; x < ending_coord.x; x++){

            for(int y = starting_coord.y; y < ending_coord.y; y++){

                // to make sure the ecluded area doesn't get filled in

                if(!((exclude_starting_coord.x < x && x < exclude_ending_coord.x) && (exclude_starting_coord.y < y && y < exclude_ending_coord.y))){

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

        AstarPath.active.Scan();

    }


}
