using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text text;
    public int score;

    public void AddScore(int _add)
    {
        Debug.Log("ADDED SCORE " + _add);
        score = score + _add;
        UpdateText();
    }
    void UpdateText()
    {
        text.text = "SCORE: " + score.ToString();
    }
}
