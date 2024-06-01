using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class UDDController : MonoBehaviour
{
    public Transform targetPosition;

    private Seeker seeker;
    private CharacterController controller;

    public Path path;

    public float max_health = 15;
    public float curr_health;

    public float speed = 2;

    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;


    public GameObject playerTarget;
    public GameObject bulletObject;

    public bool reachedEndOfPath;

    private Vector3 initPosition;
    private float distanceDiff;

    private bool hunting = false;

    private float timeHolder = 0.0f;
    public float reloadTime = 3.0f;

    public void Start () {
        //so the UDdweller shots first the reloads
        timeHolder = reloadTime;
        curr_health = max_health;

        seeker = GetComponent<Seeker>();

        playerTarget = GameObject.Find("Carrot");
        Debug.Log(playerTarget);
        // Start a new path to the targetPosition, call the the OnPathComplete function
        // when the path has been calculated (which may take a few frames depending on the complexity)
    }


    public void basicShoot(GameObject playerTarget, int bulletForce){


        timeHolder += Time.deltaTime;

        if (timeHolder >= reloadTime) {
            timeHolder = 0;

            Vector3 diffVector = playerTarget.transform.position - transform.position;
            Vector3 DirectionToPlayer = Vector3.Normalize(diffVector);


            GameObject bulletInstance = Instantiate(bulletObject, (transform.position + 2*DirectionToPlayer),  Quaternion.Euler(DirectionToPlayer.x, DirectionToPlayer.y, DirectionToPlayer.z));
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(DirectionToPlayer * bulletForce);
        }

    }

    // Update is called once per frame
    public void Update () {

        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.transform.position);

        /*path logic */

        /* ai brain logic */


        if(curr_health <= max_health/2 ){

            Color color;
            ColorUtility.TryParseHtmlString("#7B0000", out color);

            GetComponent<SpriteRenderer>().color = color;

        }
        if(hunting){

            HuntPlayer();


            if(distanceToPlayer < 15){

                basicShoot(playerTarget, 1500);

            }

            if(distanceToPlayer > 40 ){

                hunting = false;

            }

        }else{

            if(distanceToPlayer < 20 ){

                hunting = true;
                seeker.StartPath(transform.position, playerTarget.transform.position, OnPathComplete);

            }
        }
    }

    public void OnPathComplete (Path p) {
        Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

        if (!p.error) {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){

            Debug.Log("hit by thingy");
            if (collision.gameObject.tag == "PlayerBullet"){
                Debug.Log("hit by player");
                reduceHealth();
            }

            if (collision.gameObject.tag == "UDDbullet") {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<BoxCollider2D>());
            }

    }


    private void reduceHealth(){

        curr_health -= 5;

        if(curr_health <= 0){
            Destroy(this.gameObject);
        }
    }

    private void HuntPlayer(){

        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame.


        /* copy pasted from https://arongranberg.com/astar/
        *
        * and modified by yours truly -- Nico Kaegi
        */

        if(path == null){

            return;
        }


        distanceDiff = Vector3.Distance(playerTarget.transform.position, initPosition);
        if(distanceDiff > 5 ){
            seeker.CancelCurrentPathRequest();
            seeker.StartPath(transform.position, playerTarget.transform.position, OnPathComplete);
            initPosition = playerTarget.transform.position;
        }


        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        float distanceToWaypoint;
        while (true) {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance) {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count) {
                    currentWaypoint++;
                } else {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            } else {
                break;
            }
        }

        // Slow down smoothly upon approaching the end of the path
        // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
        //var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint/nextWaypointDistance) : 1f;

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        // Multiply the direction by our desired speed to get a velocity

        transform.Translate(dir * speed * Time.deltaTime);

        // If you are writing a 2D game you should remove the CharacterController code above and instead move the transform directly by uncommenting the next line
        // transform.position += velocity * Time.deltaTime;


    }
}
