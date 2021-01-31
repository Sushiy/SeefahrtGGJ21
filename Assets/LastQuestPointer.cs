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
        lastQuestDirection = lastQuestPosition - transform.position;
        float distance = lastQuestDirection.magnitude;

        float t = Mathf.Clamp(distance / 15.0f, 0,1);
        float mult = Mathf.Lerp(distance * 0.25f, 15.0f, t);

        indicator.position = transform.position + lastQuestDirection.normalized * mult;
        indicator.rotation = Quaternion.LookRotation(lastQuestDirection);

    }
}
