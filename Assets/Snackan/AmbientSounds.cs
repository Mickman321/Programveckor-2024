/*
Snäckan
2024-01-12 10:20
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSounds : MonoBehaviour
{

    AmbientController ac;

    [Header("Sounds")]

    [SerializeField]
    AudioClip[] Moving = { };
    [SerializeField]
    AudioClip[] Standstill = { };

    [Header("Config")]

    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    AudioSource audioSource;

    bool Debounce = false;
    // Start is called before the first frame update
    void Start()
    {
        ac = FindObjectOfType<AmbientController>();
        StartCoroutine(SoundLoop()); //Start the sound loop
    }

    IEnumerator TweenVolume(bool increase)
    {
        yield return new WaitForSeconds(3); //Wait until approximated end of sound
        if (increase == true)
        {
            for (int i = 0; i < 100; i++) //Repeat 100 times
            {
                audioSource.volume += 0.01f;
                yield return new WaitForSeconds(0.01f); //Smooth linear increase of volume
            }


        }
        if (increase == false)
        {
            for (int i = 0; i < 100; i++) //Repeat 100 times
            {
                audioSource.volume -= 0.01f;
                yield return new WaitForSeconds(0.01f);  //Smooth linear decrease of volume
            }


        }

        yield return "finished"; //End coroutine

    }

    IEnumerator SoundLoop()
    {
    yield return new WaitForSeconds(20f); //Initial wait to make the later parts more accurate
    MarkerA:
        if (ac.inSafeZone == false)
        {
            if (rb.velocity.magnitude == 0)
            {
                print("standstill");
                Invoke("randomStandstillSound", Random.Range(0, 10)); //Start a random sound from the "Standstill" list within 0-10 seconds

               
            }
            else
            {
                print("moving");
                Invoke("randomWalkSound", Random.Range(10, 30)); //Start a random sound from the "Moving" list within 10-30 seconds
                
            }
            yield return new WaitForSeconds(10f); //Calibration; make sure sounds don't play too often
        }

        yield return new WaitUntil(() => ac.inSafeZone == false);
        goto MarkerA; //Return to start of coroutine after inSafeZone becomes false
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
        audioSource.Stop(); //Stop possible overlapping from previous sounds/safe zone audio
        audioSource.volume = 1;
        Invoke("ResetVolume", 5);
        AudioClip randomSound = Moving[Random.Range(0, Moving.Length)]; //Get a random sound
        print(randomSound);
        audioSource.PlayOneShot(randomSound);
        StartCoroutine(TweenVolume(false)); //Fade out the end of the sound
    }
    void randomStandstillSound()
    {
        audioSource.Stop(); //Stop possible overlapping from previous sounds/safe zone audio
        audioSource.volume = 1;
        Invoke("ResetVolume", 5);
        AudioClip randomSound = Standstill[Random.Range(0, Standstill.Length)]; //Get a random sound
        print(randomSound);
        audioSource.PlayOneShot(randomSound);
        StartCoroutine(TweenVolume(false)); //Fade out the end of the sound
    }
  
    // Update is called once per frame
    void Update()
    {
        /*

  */
    }
}