using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {
    
	//Variable force was public and can accessed by Unity interface
    public float forceWeight;
    public int maxRotation;
    public string backgroundMusic;
    public string hitEffect;
    private int hitCount;

    //Gets the attached GameObject Rigidbody property
    private Rigidbody rb;


    void Start() 
    {
        rb = GetComponent<Rigidbody>();
        hitCount = 0;
        GetComponent<ChuckSubInstance>().RunFile(backgroundMusic+".ck");
    }

    void OnCollisionEnter (Collision other)
    {
        Debug.Log(other.relativeVelocity.magnitude);

        GetComponent<ChuckSubInstance>().SetFloat("vel", other.relativeVelocity.magnitude);
        GetComponent<ChuckSubInstance>().RunFile(hitEffect+".ck");
    }

    void FixedUpdate() 
    {
    	// 0.1 degree per click
    	float horizontalForce = - Input.GetAxis ("Horizontal") * 0.1f;
        float verticalForce = Input.GetAxis ("Vertical") * 0.1f;
        //Debug.Log("forca");
        //Debug.Log(horizontalForce);

        Vector3 force = new Vector3 (verticalForce, 0.0f, horizontalForce);

        Quaternion rotationAngle = Quaternion.Euler(force * forceWeight);

        Quaternion rotation = rb.rotation * rotationAngle;


	    // Clamp the X value
	    //rotation.x = Mathf.Clamp(rotation.eulerAngles.x, 0, 90);
	    // rotation.y = Mathf.Clamp(rotation.y, -90, 90);
	    // rotation.z = Mathf.Clamp(rotation.z, -90, 90);

        //Debug.Log(rb.rotation.eulerAngles.z);
        //Debug.Log(rb.rotation.z);
        //rb.MoveRotation(rotation);
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
