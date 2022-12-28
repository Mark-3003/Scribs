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
        score += _add;
        UpdateText();
    }
    void UpdateText()
    {
        text.text = score.ToString();
    }
}
