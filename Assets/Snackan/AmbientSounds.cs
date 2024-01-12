using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class AmbientSounds : MonoBehaviour
{
    AmbientController ac;
    [SerializeField]
    AudioClip[] Moving = { };
    [SerializeField]
    AudioClip[] Standstill = { };
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    AudioSource audioSource;

    bool Debounce = false;
    // Start is called before the first frame update
    void Start()
    {
        ac = FindObjectOfType<AmbientController>();
        StartCoroutine(goofyuncle());
    }

    IEnumerator TweenVolume(bool increase)
    {
        yield return new WaitForSeconds(3);
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
    IEnumerator goofyuncle()
    {
    yield return new WaitForSeconds(20f); //initial wait to make the later parts more accurate
    MarkerA:
        while (ac.inSafeZone == false)
        {
            if (rb.velocity.magnitude == 0)
            {
                print("standstill");
                Invoke("randomStandstillSound", Random.Range(0, 10));
               
            }
            else
            {
                print("moving");
                Invoke("randomWalkSound", Random.Range(10, 30));
                yield return new WaitForSeconds(10f);
            }
        }
        yield return new WaitUntil(() => ac.inSafeZone == false);
        goto MarkerA;
    }
   /* void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
   */
   void ResetVolume()
    {
        audioSource.volume = 0;
    }
    void randomWalkSound()
    {
        audioSource.Stop();
        audioSource.volume = 1;
        Invoke("ResetVolume", 5);
        AudioClip randomSound = Moving[Random.Range(0, Moving.Length)];
        print(randomSound);
        audioSource.PlayOneShot(randomSound);
        StartCoroutine(TweenVolume(false));
    }
    void randomStandstillSound()
    {
        audioSource.Stop();
        audioSource.volume = 1;
        Invoke("ResetVolume", 5);
        AudioClip randomSound = Standstill[Random.Range(0, Standstill.Length)];
        print(randomSound);
        audioSource.PlayOneShot(randomSound);
        StartCoroutine(TweenVolume(false));
    }
  
    // Update is called once per frame
    void Update()
    {

     
    }
}