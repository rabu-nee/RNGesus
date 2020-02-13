using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using RNGesus.Tools;
using SimpleSQL;

public class GameManager : PersistentSingleton<GameManager>
{
    public SimpleSQLManager databaseManager;
    public Emotion[] emotions = new Emotion[11];
    public static Dictionary<RNGesus.Tools.Emotions, Emotion> emotionsDict;

    private new void Awake() {
        base.Awake();

        databaseManager = GetComponentInChildren<SimpleSQLManager>();

        emotionsDict = new Dictionary<RNGesus.Tools.Emotions, Emotion>();
        foreach(Emotion emotion in emotions) {
            emotionsDict.Add(emotion.emotion, emotion);
        }
    }
}
