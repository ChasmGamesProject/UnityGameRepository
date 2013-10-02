using UnityEngine;
using System.Collections;
//Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
//This line should always be present at the top of scripts which use pathfinding
using Pathfinding;
public class AstarAI : MonoBehaviour {
    //The point to move to
    public Vector3 targetPosition;
    
    private Seeker seeker;
    private CharacterController controller;
    public Vector3 Temp;
    //The calculated path
    public Path path;
    
    //The AI's speed per second
    public float speed = 1000;
    
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;
 
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;
	
	/*void Update ()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            gameObject.renderer.material.color = Color.red;
        }*/
	
	/* if (Input.GetButtonDown ("Fire1")) 
        {
        var mousePos = Input.mousePosition;
	*/
	
	/*var particle : GameObject;

function Update () {

if (Input.GetButtonDown ("Fire1")) {

// Construct a ray from the current mouse coordinates

var ray : Ray = Camera.main.ScreenPointToRay (Input.mousePosition);

var hit : RaycastHit;

if (Physics.Raycast (ray,hit)) {

// Create a particle if hit

Instantiate (particle, hit.point, transform.rotation);

}

}

}*/
	
	void Update()
	{
		if(Input.GetMouseButton(0))
        {
		
			
			var ray = Camera.current.ScreenPointToRay (Input.mousePosition);
		    RaycastHit hit;
			
			if(Physics.Raycast(ray,out hit))
			{
				targetPosition = hit.point;
			}

			targetPosition.y = 1.08f;
          Start(); // gameObject.renderer.material.color = Color.red;
        }
	}
 
    public void Start () {
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        seeker.StartPath (transform.position,targetPosition, OnPathComplete);
    }
    
    public void OnPathComplete (Path p) {
        Debug.Log ("Yey, we got a path back. Did it have an error? "+p.error);
        if (!p.error) {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }
 
    public void FixedUpdate () {
        if (path == null) {
            //We have no path to move after yet
            return;
        }
        
        if (currentWaypoint >= path.vectorPath.Count) {
            Debug.Log ("End Of Path Reached");
            return;
        }
        
        //Direction to the next waypoint
		
		Temp = path.vectorPath[currentWaypoint];
		//Temp.y +=2.08f;
        Vector3 dir = (Temp-transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        controller.SimpleMove (dir);
		//controller.
		//transform.position = path.vectorPath[currentWaypoint];
        
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
            return;
        }
    }
} 