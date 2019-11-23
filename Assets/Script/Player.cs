using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public   GameObject  background1;
    public   GameObject  background2;
    public   Text        scoreText;
    public   float       speed = 5.0f;

    private  int         score;
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
        score = 0;
        SetScoreText();

        // time
        time = 0.0f;

        // 걷기 상태 설정
        isWalk = true;
        GetComponent<Animator>().SetBool("isWalk", isWalk);

        // sudden action 설정
        suddenAngle = 0.0f;
        Invoke("SuddenAction", 3.0f);
    }

    void Update () {
		if(isWalk)
        {
            time += Time.deltaTime;
            if(time>=0.5)
            {
                time = 0.0f;
                score += 1;
                SetScoreText();
            }
            GetInput();

            MoveBackGround();
        }

    }

    void SetScoreText()
    {
        scoreText.text = "Score : " + score;
    }

    IEnumerator SuddenAction()
    {
        yield return new WaitForSeconds(1.0f);
    }

    void GetInput()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {

        }
        
        if(Input.GetKey(KeyCode.RightArrow))
        {

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
