using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInRow : MonoBehaviour {
    public float speed = 5.0f;
    public Text scoreText;
    public Text timeText;
    public GameObject paddle;

    private float scaleFactor;
    private float score;
    private float time;

    void Start()
    {
        // scale factor
        scaleFactor = 1.0f;

        // set score
        score = 0;
        SetScoreText();

        // set time
        time = 30.0f;
    }

    void Update()
    {
        PaddleMoving();

        if (time <= 0.0f)
        {
            // ended
            time = 0.0f;
        }
        else
        {
            time -= Time.deltaTime;

            if (Input.GetKey(KeyCode.Space))
                Move();

            SetTimeText();
        }
    }

    void PaddleMoving()
    {
        paddle.transform.rotation = Quaternion.Euler(SerialCommunication.RoadRotation);
    }

    void SetScoreText()
    {
        scoreText.text = Mathf.Round(score*10)/10 + "m";
    }

    void SetTimeText()
    {
        timeText.text = Mathf.Round(time * 10) / 10 + "s";
    }

    void Move()
    {
        this.transform.Translate(new Vector3(0, 0, -1) * speed * scaleFactor * Time.deltaTime);

        score += speed * scaleFactor * Time.deltaTime / 10;
        SetScoreText();
    }
}
