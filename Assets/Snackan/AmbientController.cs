/*
Snäckan
2024-01-12 10:13
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientController : MonoBehaviour
{
    [Header("Safe Zones")]
    [SerializeField]
    GameObject[]safeZones = {};


    [Header("Config")]
    [SerializeField]
    AudioSource audioSource;

    [Header("Audio")]
    [SerializeField]
    AudioClip PlaceholderOne;
    [SerializeField]
    AudioClip PlaceholderTwo;


    public bool inSafeZone; //Internal
    // Start is called before the first frame update
    void Start()
    {
        inSafeZone = false; //Assume the player is not in a safe zone when they spawn
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    void doSound(string name) //Play a sound based on the name of the safe zone
    {
        if (name == "SafeZone1")
        {
            audioSource.Stop();
           audioSource.PlayOneShot(PlaceholderOne);
        }
        if (name == "SafeZone2")
        {
            audioSource.Stop();
            audioSource.PlayOneShot(PlaceholderTwo);
        }
    }
    IEnumerator TweenVolume(bool increase)
    {
        if (increase == true)
        {
            for (int i = 0; i < 100; i++) //Repeat 100 times
            {
                audioSource.volume += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smooth linear increase in volume
            }
           
          
        }
        if (increase == false)
        {
            for (int i = 0; i < 100; i++) //Repeat 100 times
            {
                audioSource.volume -= 0.01f;
                yield return new WaitForSeconds(0.01f); //Smooth linear decrease in volume
            }


        }

        yield return "finished"; //End coroutine

    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject trigger in safeZones)
        {
            if (trigger == other.gameObject) //If player has collided with one of the safe zones
            {

                inSafeZone = true; //Make sure the player is considered as inside a safe zone
                doSound(trigger.name);
                audioSource.time = 0; //Reset the time position of the audio to make sure it plays from the beginning
                Debug.Log("Trigger Enter");
                StartCoroutine(TweenVolume(true)); //Tween volume to 1
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject trigger in safeZones)
        {
            if (trigger == other.gameObject) //If player has exited one of the safe zones
            {
                inSafeZone = false;
                Debug.Log("Trigger Exit");
                StartCoroutine(TweenVolume(false)); //Tween volume to 0
            }
        }
    }
}
