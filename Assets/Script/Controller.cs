using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controller : MonoBehaviour {

	//Variable speed was public and can accessed by Unity interface
    //public float speed;

    private int count;
    private int score;

    public Text countText;
    public Text winText;
    public Text loseText;

    //public ChuckSubInstance groundInstance;

    //Gets the attached GameObject Rigidbody property
    private Rigidbody rb;

    private bool onlyOne = true;

    // Start is called once GameMode starts
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        score = 0;
        SetCountText();
        winText.text = "";
        loseText.text = "";
    }

    // FixedUpdate is called once aplly forces
    void FixedUpdate ()
    {
        // float moveHorizontal = Input.GetAxis ("Horizontal");
        // float moveVertical = Input.GetAxis ("Vertical");

        // Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        // rb.AddForce (movement * speed);
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ("Pick up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            score = score + 1;
            SetCountText();
            GetComponent<ChuckSubInstance>().RunFile("PickUp.ck");
        }
        else if (other.gameObject.CompareTag ("Bad"))
        {
            other.gameObject.SetActive (false);
            score = score - 1;
            SetCountText();
            GetComponent<ChuckSubInstance>().RunFile("BadPickUp.ck");
        }
        else if (other.gameObject.CompareTag ("Death"))
        {
            loseText.text = "You died!";
            //groundInstance.RunFile("Death.ck");
            GetComponent<ChuckSubInstance>().RunFile("Death.ck");
        }

    }

    void SetCountText()
    {
        countText.text = "Points: " + score.ToString ();
        if (count >= 1 && onlyOne) //15
        {
            onlyOne = false;
            winText.text = "Game finished. Points: " + score.ToString ();
            //groundInstance.RunFile("Death.ck");
            GetComponent<ChuckSubInstance>().RunFile("Vic.ck");
        }
    }
}