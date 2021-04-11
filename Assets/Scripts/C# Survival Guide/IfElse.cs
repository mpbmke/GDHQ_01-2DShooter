using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfElse : MonoBehaviour
{
    //[SerializeField] int _points;
    //bool _wonGame = false;

    //[SerializeField] int _minScore = 55;
    //[SerializeField] int _maxScore = 101;
    //int numQuiz = 5;
    //int quiz01, quiz02, quiz03, quiz04, quiz05;

    //[SerializeField] GameObject _cube;

    [SerializeField] int _speed;
    [SerializeField] int _speedLimit = 20;
    bool _goFasterMsg;

    private void Start()
    {
        //_cube.GetComponent<Renderer>().material.color = Color.red;

     /* quiz01 = Random.Range(_minScore, _maxScore);
        quiz02 = Random.Range(_minScore, _maxScore);
        quiz03 = Random.Range(_minScore, _maxScore);
        quiz04 = Random.Range(_minScore, _maxScore);
        quiz05 = Random.Range(_minScore, _maxScore);
        
        int totalPoints = quiz01 + quiz02 + quiz03 + quiz04 + quiz05;
        float avgScore = totalPoints / numQuiz;
        
        if (avgScore >= 90f)
        {
            Debug.Log("The average score was an A grade: " + avgScore);
        }
        else if (avgScore >= 80f && avgScore < 90)
        {
            Debug.Log("The average score was a B grade: " + avgScore);
        }
        else if (avgScore >= 70f && avgScore < 80)
        {
            Debug.Log("The average score was a C grade: " + avgScore);
        }
        else if (avgScore >= 60f && avgScore < 70)
        {
            Debug.Log("The average score was a D grade: " + avgScore);
        }
        else if (avgScore < 60)
        {
            Debug.Log("The average score was an F grade: " + avgScore);
        }*/
    }

    void Update()
    {
        //SpaceGame();
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _cube.GetComponent<Renderer>().material.color = Color.blue;
        }*/

        SpeedControl();

        if (_speed == 0 && _goFasterMsg == false)
        {
            Debug.Log("GO FASTER!");
            _goFasterMsg = true;
        }
        else if (_speed > 0)
        {
            _goFasterMsg = false;
        }
    }

    private void SpeedControl()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _speed += 1;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && _speed >= _speedLimit)
        {
            Debug.Log("SLOW DOWN!");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && _speed > 0)
        {
            _speed -= 1;
        }

        if (_speed < 0)
        {
            _speed = 0;
        }
    }

    /*private void SpaceGame()
    {
        if (_wonGame == true)
        {
            return;
        }
        else if (_wonGame == false)
        {
            if (_points >= 50)
            {
                _wonGame = true;
                Debug.Log("Incredible! You won the easiest game ever made!");
            }
            else if (_points < 50)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _points += 10;
                }
            }
        }
    }*/
}
