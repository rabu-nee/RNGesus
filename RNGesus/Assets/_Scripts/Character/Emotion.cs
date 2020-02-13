using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Emotion
{
    public RNGesus.Tools.Emotions emotion;
    public Color color;
    [Range(0,100)]
    public float chanceInPercentage;

    public Emotion() {
        color = Color.white;
        chanceInPercentage = 100f;
    }
}
