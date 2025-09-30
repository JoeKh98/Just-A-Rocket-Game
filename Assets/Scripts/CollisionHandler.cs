using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 1f;
    [SerializeField] AudioClip successSFX;  
    [SerializeField] AudioClip crashSFX;  
    [SerializeField] ParticleSystem successVFX; 
    [SerializeField] ParticleSystem crashVFX;
    AudioSource audioSource;
    bool bIsControllable = true;
    bool bIsCollidable = true;  

    void Start()
    {
        // initializing necessary vars
        audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        RespondToDebugKeys(); 
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!bIsControllable || !bIsCollidable)
        {
            return;
        }

        switch(collision.gameObject.tag)
        {
            case "Friendly": 
            {
                Debug.Log("Friendly");
                break; 
            }
            case "Finish": 
            {
                Debug.Log("Finish");
                StartNextLevelSequence(); 
                break; 
            }
            case "Fuel":
            {
                Debug.Log("Fuel");
                break; 
            }
            default:
            {
                StartCrushSequence(); 
                break; 
            }
        }
    }

    private void StartNextLevelSequence()
    {
        bIsControllable = false; 
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX); 
        successVFX.Play(); 
        GetComponent<Movement>().enabled = false;  
        Invoke("LoadNextLevel", levelLoadDelay); 
    }

    private void StartCrushSequence()
    {
        bIsControllable = false; 
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);  
        crashVFX.Play(); 
        //gameObject.SetActive(false);
        GetComponent<Movement>().enabled = false; 
        Invoke("ReloadLevel", levelLoadDelay);   
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1; 
        
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0; 
        }
        
        SceneManager.LoadScene(nextScene);
        
    }
    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentScene); 
    }

    void RespondToDebugKeys()
    {
        if(Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel(); 
        }
        else if(Keyboard.current.cKey.wasPressedThisFrame)
        {
            bIsCollidable = !bIsCollidable; 
            Debug.Log("C key is pressed!"); 
        }
    } 


}
