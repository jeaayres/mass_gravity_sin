using UnityEngine;
using System.Collections;

public class BadPickUpController : MonoBehaviour {

	// Update is called once a frame
    void Update () 
    {
        transform.Rotate (new Vector3 (30, 45, 60) * Time.deltaTime);
    }
}
