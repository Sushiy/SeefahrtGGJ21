using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastQuestPointer : MonoBehaviour
{
    public QuestSubsystem questsub;
    private Vector3 lastQuestPosition;
    private Vector3 lastQuestDirection;

    public Transform indicator;

    // Start is called before the first frame update
    void Start()
    {
        questsub.onNewQuestFinished.AddListener(SetQuestLocation);
    }

    void SetQuestLocation(Vector3 worldposition)
    {
        lastQuestPosition = worldposition;
    }

    // Update is called once per frame
    void Update()
    {
        lastQuestDirection = Vector3.Normalize(lastQuestPosition - transform.position);
        indicator.position = transform.position + lastQuestDirection * 15.0f;
    }
}
