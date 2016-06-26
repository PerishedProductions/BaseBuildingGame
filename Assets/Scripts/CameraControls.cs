using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {
    
    //All of them variables
    private float horizontal;
    private float vertical;
    private float mouseWheel;

    private Camera cam;
    
    public float speed;
	
    //Init
    void Start()
    {
        cam = GetComponent<Camera>();
    }

	//Called every frame
	void Update ()
    {
        //Storing input
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        //Manging scrolling in and out
        if (mouseWheel < 0 && cam.orthographicSize <= 24)
        {
            cam.orthographicSize += 2;
        }
        else if (mouseWheel > 0 && cam.orthographicSize >= 7)
        {
            cam.orthographicSize -= 2;
        }

        //Managing camera movement 
        this.transform.Translate(new Vector2(horizontal * speed, vertical * speed));
	}
}
