using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	//Variable player was public and can accessed by Unity interface
    public GameObject player;

    //Gets the distance between camera and the ball
    private Vector3 offset;

    // Start is called once GameMode starts
    void Start ()
    {
        offset = transform.position - player.transform.position;
    }
    
    // LateUpdate is called once a frame, after all itens process Update
    void LateUpdate ()
    {
        transform.position = player.transform.position + offset;
    }

}
