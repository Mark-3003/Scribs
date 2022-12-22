using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    public string name;

    public Sentences[] sentences;
}
[System.Serializable]
public struct Sentences
{
    [TextArea(3,10)]
    public string sentence;
    public Sprite mood;
}
