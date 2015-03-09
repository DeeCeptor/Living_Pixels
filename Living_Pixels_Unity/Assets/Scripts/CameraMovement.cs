using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () 
	{	
		float mousePosX = Input.mousePosition.x; 
		float mousePosY = Input.mousePosition.y; 
		int windowEdgeArea = 7; 
		float scrollSpeed = 10;
		
		if (mousePosX < windowEdgeArea || Input.GetKey(KeyCode.LeftArrow))
		{ 
			transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime); 
		} 
		
		if (mousePosX >= Screen.width - windowEdgeArea || Input.GetKey(KeyCode.RightArrow))
		{ 
			transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime); 
		}
		
		if (mousePosY < windowEdgeArea || Input.GetKey(KeyCode.DownArrow))
		{ 
			transform.Translate(transform.up * -scrollSpeed * Time.deltaTime); 
		} 
		
		if (mousePosY >= Screen.height - windowEdgeArea || Input.GetKey(KeyCode.UpArrow))
		{ 
			transform.Translate(transform.up * scrollSpeed * Time.deltaTime); 
		}

		// Scroll zooming
		float scrollInput = Input.GetAxis ("Mouse ScrollWheel");
		if (scrollInput != 0) 
		{
			camera.orthographicSize -= scrollInput * 3;
		}
	}
}
