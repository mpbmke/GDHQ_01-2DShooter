using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizCalc : MonoBehaviour
{
    public int quiz01, quiz02, quiz03, quiz04, quiz05;

    void Start()
    {
        //BRUTE FORCE
        int numQuizzes = 5;
        float quizAvg;

        quiz01 = Random.Range(55, 101);
        quiz02 = Random.Range(55, 101);
        quiz03 = Random.Range(55, 101);
        quiz04 = Random.Range(55, 101);
        quiz05 = Random.Range(55, 101);

        quizAvg = ((float)quiz01 + (float)quiz02 + (float)quiz03 + (float)quiz04 + (float)quiz05) / numQuizzes;

        Debug.Log("Quiz Average: " + quizAvg);
    }
}
