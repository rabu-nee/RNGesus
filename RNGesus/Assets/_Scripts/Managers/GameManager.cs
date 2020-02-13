using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using RNGesus.Tools;

public class GameManager : PersistentSingleton<GameManager>
{
    public Emotion[] emotions = new Emotion[11];
    public static Dictionary<RNGesus.Tools.Emotions, Emotion> emotionsDict;

    private new void Awake() {
        base.Awake();

        emotionsDict = new Dictionary<RNGesus.Tools.Emotions, Emotion>();
        foreach(Emotion emotion in emotions) {
            emotionsDict.Add(emotion.emotion, emotion);
        }
    }
}
