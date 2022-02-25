using System.Collections.Generic;
using UnityEngine;

public class AIListenPerception : MonoBehaviour
{
    //Maximum hearing radius
    public float maxRadius = 10f;
    //Safe hearing radius
    public float safeRadius = 5f;

    //Listening result
    public Dictionary<AudioSource, float> PerceptionResult { get; private set; }

    /// <summary>
    ///  Listen
    /// </summary>
    /// <param name="explorer">Explorer</param>
    /// <param name="targets">Sound source target set</param>
    public void Check(GameObject explorer, List<AudioSource> targets)
    {
        PerceptionResult.Clear();

        foreach (var item in targets)
        {
            //Maximum hearing range judgment
            Vector3 offset = item.transform.position - explorer.transform.position;
            if (offset.sqrMagnitude > maxRadius * maxRadius)
                continue;

            //Judgment of the probability of listening loss: Calculate the distance proportional weight of a sound source. The farther the distance, the greater the weight and the greater the probability of listening loss
            float distance = offset.magnitude;
            float weight = (distance - safeRadius) * 1.0f / (maxRadius - safeRadius);
            if (Random.value < weight)
                continue;

            //Calculate the real listening volume, which is inversely proportional to distance
            float volume = item.volume * (1 - distance / maxRadius);
            PerceptionResult.Add(item, volume);
        }
    }

    private void Start()
    {
        //test
        PerceptionResult = new Dictionary<AudioSource, float>();
        var targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
        List<AudioSource> audioSources = new List<AudioSource>();
        foreach (var item in targets)
        {
            audioSources.Add(item.GetComponent<AudioSource>());
        }
        Check(gameObject, audioSources);
        foreach (var result in PerceptionResult)
        {
            Debug.Log("audioSource : " + result.Key.gameObject.name);
            Debug.Log("volume : " + result.Value);
        }
    }
}