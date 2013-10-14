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
 public Vector3 dir;
	public Vector3 LastPos;
    //The calculated path
    public Path path;
    public Vector3 LastPath;
    //The AI's speed per second
    public float speed = 0.1f;
    
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;
 
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;
 
	void Awake()
	{
		//GlobalVars.player_transform = gameObject.transform;
		
	}
	
	//Gets mouse click location and builds a a path to that location
	void Update()
	{
		if(Input.GetMouseButton(0))
        {
		
			
			var ray = Camera.current.ScreenPointToRay (Input.mousePosition);
		    RaycastHit hit;
			
			if(Physics.Raycast(ray,out hit))
			{
				if((hit.point != LastPath)&&(hit.point.y==0))
				{
				
				targetPosition = hit.point;
				LastPath = targetPosition;
				targetPosition.y = 1.08f;
			    Start(); 
				}
				
			}

			
        }
	}
	
	
    public void Start () {
		LastPath.Set(0,0,0);
		LastPos = gameObject.transform.position;
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        seeker.StartPath (transform.position,targetPosition, OnPathComplete);
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        
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
        
		//This function makes the object move the final distance to the target postion
        if (currentWaypoint >= path.vectorPath.Count) {
            //Debug.Log ("End Of Path Reached");
			if((Vector3.Distance(LastPos,transform.position)>0.01)&&(Vector3.Distance(transform.position,targetPosition)>0.5))
			{
			dir = (targetPosition-transform.position).normalized;
        	dir *= speed * Time.fixedDeltaTime;
        	controller.SimpleMove (dir);
			}
            return;
        }
        
        //Direction to the next waypoint
        dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        controller.SimpleMove (dir);
       
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
            return;
        }
			LastPos = transform.position;
    }
}