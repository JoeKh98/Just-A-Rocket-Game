using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation; 
    [SerializeField] float thrustStrength = 100.0f; 
    [SerializeField] float rotationStrength = 0.0f; 
    [SerializeField] AudioClip mainEngine;  
    [SerializeField] ParticleSystem rightThrustVFX;
    [SerializeField] ParticleSystem leftThrustVFX;
    [SerializeField] ParticleSystem mainThrustVFX;
    
    
    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        // initializing necessary vars
        rb = GetComponent<Rigidbody>(); 
        audioSource = GetComponent<AudioSource>(); 
    }

    void OnEnable()
    {
        // Enabling Input actions
        thrust.Enable();
        rotation.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation(); 
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting(); 
        }
        else
        {
            StopThrusting();
        }

    }

    private void StopThrusting()
    {
        mainThrustVFX.Stop();
        audioSource.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);

        if (!mainThrustVFX.isPlaying)
        {
            mainThrustVFX.Play();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput > 0)
        {
            RotateRight();
        }
        else if(rotationInput < 0)
        {
            RotateLeft();
        } 
        else
        {
            StopRotating();
        }


    }

    private void StopRotating()
    {
        rightThrustVFX.Stop();
        leftThrustVFX.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationStrength);
        if (!leftThrustVFX.isPlaying)
        {
            rightThrustVFX.Stop();
            leftThrustVFX.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationStrength);
        if (!rightThrustVFX.isPlaying)
        {
            leftThrustVFX.Stop();
            rightThrustVFX.Play();
        }
    }

    private void ApplyRotation(float rotationStrength_In)
    {
        rb.freezeRotation = true; 
        transform.Rotate(Vector3.forward * rotationStrength_In * Time.fixedDeltaTime);
        rb.freezeRotation = false; 
    }


}
