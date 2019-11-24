using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public   GameObject  background1;
    public   GameObject  background2;
    public   GameObject  gameOverImage;
    public   AudioClip gameOverSound;
    public   Text        scoreText;
    public   float       speed = 5.0f;

    private  Vector3     rotation;

    private float       score;
    private  float       scoreFactor;
    private  float       time;
    private  float       speedFactor;
    private  float       minX;
    private  bool        isWalk;
    private  float       suddenAngle;


    void Start ()
    {
        // 최소 X 좌표 설정
        minX = -450f;

        // speed 계수
        speedFactor = 1.0f;

        // score
        score = 0.0f;
        scoreFactor = 5.0f;
        SetScoreText();

        // time
        time = 0.0f;

        // 걷기 상태 설정
        isWalk = true;
        GetComponent<Animator>().SetBool("isWalk", isWalk);

        // sudden action 설정
        Invoke("SuddenAction", 3.0f);
    }

    void Update () {
		if(isWalk)
        {
            time += Time.deltaTime;
            if(time>=0.5)
            {
                time = 0.0f;
                score += speed * speedFactor * scoreFactor * Time.deltaTime;
                SetScoreText();
            }
            GetInput();

            MoveBackGround();

            rotation = SerialCommunication.RoadRotation;
            transform.rotation = Quaternion.Euler(new Vector3(rotation.x, transform.rotation.y, transform.rotation.z));
        }
        else
        {
            if(transform.position.y <= 40.0f)
            {
                transform.position = new Vector3(transform.position.x, 40.0f, transform.position.z);

                GetComponent<AudioSource>().clip = gameOverSound;
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().Play();

                // game over
                gameOverImage.SetActive(true);

                if(SerialCommunication.OkButton)
                {
                    SceneManager.LoadScene("Main");
                }
            }
        }

    }

    void SetScoreText()
    {
        scoreText.text = Mathf.Round(score*10)/10 + "m";
    }

    void GetInput()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {

        }
        
        if(Input.GetKey(KeyCode.RightArrow))
        {

        }

        if(Input.GetKey(KeyCode.Backspace))
        {
            isWalk = false;
            GetComponent<Animator>().SetBool("isWalk", isWalk);
            GetComponent<Animator>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void MoveBackGround()
    {
        background1.transform.Translate(new Vector3(-1, 0, 0) * speed * speedFactor * Time.deltaTime);
        background2.transform.Translate(new Vector3(-1, 0, 0) * speed * speedFactor * Time.deltaTime);

        Vector3 position1 = background1.transform.position;
        Vector3 position2 = background2.transform.position;

        if(position1.x <= minX)
        {
            background1.transform.Translate(new Vector3(1, 0, 0) * 260 * 2);
        }
        if(position2.y <= minX)
        {
            background2.transform.Translate(new Vector3(1, 0, 0) * 260 * 2);
        }
    }
}
