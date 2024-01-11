using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientController : MonoBehaviour
{
    [SerializeField]
    GameObject[]safeZones = {};
    [SerializeField]
    AudioSource audioSource;


    [SerializeField]
    AudioClip PlaceholderOne;

    [SerializeField]
    AudioClip PlaceholderTwo;

    public bool inSafeZone;
    // Start is called before the first frame update
    void Start()
    {
        inSafeZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    void doSound(string name)
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
            for (int i = 0; i < 100; i++)
            {
                audioSource.volume += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
           
          
        }
        if (increase == false)
        {
            for (int i = 0; i < 100; i++)
            {
                audioSource.volume -= 0.01f;
                yield return new WaitForSeconds(0.01f);
            }


        }

        yield return "finished";

    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject trigger in safeZones)
        {
            if (trigger == other.gameObject)
            {

                inSafeZone = true;
                doSound(trigger.name);
                audioSource.time = 0;
                Debug.Log("Trigger Enter");
                StartCoroutine(TweenVolume(true));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject trigger in safeZones)
        {
            if (trigger == other.gameObject)
            {
                inSafeZone = false;
                Debug.Log("Trigger Exit");
                StartCoroutine(TweenVolume(false));
            }
        }
    }
}
