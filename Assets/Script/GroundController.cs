using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {
    
	//Variable force was public and can accessed by Unity interface
    public float forceWeight;
    public int maxRotation;
    public string backgroundMusic;
    public string hitEffect;


    //Gets the attached GameObject Rigidbody property
    private Rigidbody rb;

    private float theta = 0.0f;


    void Start() 
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<ChuckMainInstance>().RunFile(backgroundMusic+".ck");
    }

    void OnCollisionEnter (Collision other)
    {
        Debug.Log(other.relativeVelocity.magnitude);

        GetComponent<ChuckMainInstance>().SetFloat("vel", other.relativeVelocity.magnitude);
        GetComponent<ChuckMainInstance>().RunFile(hitEffect+".ck");
    }

    void FixedUpdate() 
    {
    	// 0.1 degree per click
    	float horizontalForce = - Input.GetAxis ("Horizontal") * 0.1f;
        float verticalForce = Input.GetAxis ("Vertical") * 0.1f;

        theta += Input.GetAxis("Mouse X") * 10;
        float c = Mathf.Cos(theta * Mathf.PI / 180.0f);
        float s = Mathf.Sin(theta * Mathf.PI / 180.0f);
        Vector2 e1 = new Vector2(c, s);
        Vector2 e2 = new Vector2(-s, c);

        //var fc = GameObject.Find("Follow Camera");
        var fc = GetComponent<GameplayController>().followCamera;
        fc.transform.localRotation = Quaternion.Euler(10, theta, 0);
        fc.GetComponent<CameraController>().theta = theta;

        

        var cf = horizontalForce * e1 + verticalForce * e2;
        Vector3 force = new Vector3(cf.y, 0.0f, cf.x);


        //Vector3 force = new Vector3 (verticalForce, 0.0f, horizontalForce);

        Quaternion rotationAngle = Quaternion.Euler(force * forceWeight);

        Quaternion rotation = rb.rotation * rotationAngle;


	    if     ((rb.rotation.eulerAngles.z >= maxRotation)       &&  rb.rotation.eulerAngles.z < 180 && rotationAngle.z > 0){}
	   	else if((rb.rotation.eulerAngles.z <= 360 - maxRotation) &&  rb.rotation.eulerAngles.z > 180 && rotationAngle.z < 0){}
	   	else if((rb.rotation.eulerAngles.x >= maxRotation)       &&  rb.rotation.eulerAngles.x < 180 && rotationAngle.x > 0){}
	   	else if((rb.rotation.eulerAngles.x <= 360 - maxRotation) &&  rb.rotation.eulerAngles.x > 180 && rotationAngle.x < 0){}
	    else
	    {
	    	rb.MoveRotation(rotation);
	    }
    }

}
