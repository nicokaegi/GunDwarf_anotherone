using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cave_gen : MonoBehaviour
{

    [SerializeField] int startX, startY, width, height;
    [SerializeField] float seed;
    [Range(0,1)]
    [SerializeField] float modifier;
    [SerializeField] GameObject wallTile;
    [SerializeField] GameObject groundTile;
    [SerializeField] GameObject Trigger;
    [SerializeField] bool north, south, east, west;

    // Start is called before the first frame update
    void Start()
    {
        //seed = Random.Range(-100000, 100000);
        PerlinCaveGen(startX, startY);

        if(north){

            Instantiate(Trigger, new Vector2(startX + 25, startY + 45), Quaternion.Euler(0, 0, 0));

        }
        if(south){

            Instantiate(Trigger, new Vector2(startX + 25, startY + 5), Quaternion.Euler(0, 0, 180));

        }
        if(east){

            Instantiate(Trigger, new Vector2(startX + 5, startY + 25), Quaternion.Euler(0, 0, 90));

        }
        if(west){

           Instantiate(Trigger, new Vector2(startX + 45, startY + 25), Quaternion.Euler(0, 0, 270));

        }

    }

    void PerlinCaveGen(int start_x, int start_y){


        for(int x = start_x; x < width; x++){

            for(int y = start_y; y < height; y++){

                int spawnpoint = 1 - Mathf.RoundToInt(Mathf.PerlinNoise((x * modifier) + seed, (y * modifier) + seed));
                if (spawnpoint == 1){

                    Instantiate(wallTile, new Vector2(x, y), Quaternion.identity);

                }
                else{

                    Instantiate(groundTile, new Vector2(x, y), Quaternion.identity);

                }
            }
        }
    }

    // Update is called once per frame
    /*
    void Update()
    {

    }
    */
}
