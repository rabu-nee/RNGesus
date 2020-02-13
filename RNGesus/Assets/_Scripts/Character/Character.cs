using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RNGesus.Tools;

public class Character : MonoBehaviour
{
    public int id;
    [SerializeField]
    private SpriteRenderer moodRing;
    public Emotions emotion;
    public Relationship[] relationships = new Relationship[18];

    // Start is called before the first frame update
    void Start()
    {
        SetRandomEmotion();
    }

    public void SetRandomEmotion() {
        int randomMood = Random.Range(0, GameManager.Instance.emotions.Length);
        bool moodSet = false;
        Emotions randomEmotion = Emotions.normal;
        while (!moodSet) {
            randomEmotion = (Emotions)randomMood;

            float chance = Random.Range(0f, 100f);
            if (chance <= GameManager.emotionsDict[emotion].chanceInPercentage) {
                moodSet = true;
            }
        }
        SetEmotion(randomEmotion);
    }

    public void SetEmotion(Emotions emotion) {
        this.emotion = emotion;
        moodRing.color = GameManager.emotionsDict[emotion].color;
    }
}

[System.Serializable]
public class Relationship {
    public int partnerId;
    public RelationshipStatus relationshipStatus;
}