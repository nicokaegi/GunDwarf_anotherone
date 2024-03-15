using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cave_gen : MonoBehaviour
{

    [SerializeField] int width, height;
    [SerializeField] float seed;
    [Range(0,1)]
    [SerializeField] float modifier;
    [SerializeField] GameObject tile;
    //[SerializeField] GameObject base_map;




    // Start is called before the first frame update
    void Start()
    {
        //seed = Random.Range(-100000, 100000);
        seed = -86300;
        perlinCave();

    }

    void perlinCave(){


        for(int x = 0; x < width; x++){

            for(int y = 0; y < height; y++){

                int spawnpoint = 1 - Mathf.RoundToInt(Mathf.PerlinNoise((x * modifier) + seed, (y * modifier) + seed));
                if (spawnpoint == 1){

                    Instantiate(tile, new Vector2(x, y), Quaternion.identity);

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
