using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public GameObject followCameraPrefab;
    public GameObject[] balls;


    public GameObject followCamera;
    GameObject currentBall;

    int id = 0;

    float lastTime = 0;


    void CreateBall()
    {
        currentBall = Instantiate(balls[id % balls.Length]);
        currentBall.GetComponent<Controller>().inst = GetComponent<ChuckMainInstance>();
        followCamera.GetComponent<CameraController>().player = currentBall;
    }


    void Awake()
    {
        followCamera = Instantiate(followCameraPrefab);
        CreateBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("joystick button 0"))
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 3);
        }

        if (Input.GetButton("joystick button 2"))
        {
            if (Time.fixedTime - lastTime > 0.3)
            {
                Destroy(currentBall);
                ++id;
                CreateBall();
                lastTime = Time.fixedTime;
            }
        }

    }
}
