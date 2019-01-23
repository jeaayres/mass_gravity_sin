using UnityEngine;
using System.Collections;

public class PickUpController : MonoBehaviour {

	// Update is called once a frame
    void Update () 
    {
        transform.Rotate (new Vector3 (0, 180, 0) * Time.deltaTime);
    }
}
