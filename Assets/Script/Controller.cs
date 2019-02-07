using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controller : MonoBehaviour {

    private int count;
    private int score;

    public Text countText;
    public Text winText;
    public Text loseText;

    public GameObject canvasPrefab;

    //Gets the attached GameObject Rigidbody property
    private Rigidbody rb;

    private bool onlyOne = true;
    public ChuckMainInstance inst;

    // Start is called once GameMode starts
    void Start ()
    {
        rb = GetComponent<Rigidbody>();

        var c = Instantiate(canvasPrefab);
        countText = c.transform.Find("Count text").gameObject.GetComponent<Text>();
        winText = c.transform.Find("Win text").gameObject.GetComponent<Text>();
        loseText = c.transform.Find("Lose text").gameObject.GetComponent<Text>();

        count = 0;
        score = 0;
        SetCountText();
        winText.text = "";
        loseText.text = "";
    }

    // FixedUpdate is called once aplly forces
    void FixedUpdate ()
    {
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ("Pick up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            score = score + 1;
            SetCountText();
            inst.RunFile("PickUp.ck");
        }
        else if (other.gameObject.CompareTag ("Bad"))
        {
            other.gameObject.SetActive (false);
            score = score - 1;
            SetCountText();
            inst.RunFile("BadPickUp.ck");
        }
        else if (other.gameObject.CompareTag ("Death"))
        {
            loseText.text = "You died!";
            inst.RunFile("Death.ck");
        }

    }

    void SetCountText()
    {
        countText.text = "Points: " + score.ToString ();
        if (count >= 15 && onlyOne) //15
        {
            onlyOne = false;
            winText.text = "Game finished. Points: " + score.ToString ();
            inst.RunFile("Victory.ck");
        }
    }
}