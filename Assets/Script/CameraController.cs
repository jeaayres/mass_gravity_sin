using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	//Variable player was public and can accessed by Unity interface
    public GameObject player;

    //Gets the distance between camera and the ball
    private Vector3 offset;

    public float theta = 0;

    // Start is called once GameMode starts
    void Start ()
    {
        //offset = transform.position - player.transform.position;
    }
    
    // LateUpdate is called once a frame, after all itens process Update
    void LateUpdate ()
    {
        float c = Mathf.Cos(-theta * Mathf.PI / 180.0f);
        float s = Mathf.Sin(-theta * Mathf.PI / 180.0f);
        var p = new Vector3(s * 3, 2, -c * 3);

        //transform.position = player.transform.position + offset;
        transform.position = player.transform.position + p;
    }

}
