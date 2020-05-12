using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThruster : MonoBehaviour
{
    public float force = 10.0f;
    public ForceMode forceMode;
    public string buttonName = "Fire1";

    [Header("Required Scene References")]
    public Transform playerTransform;
    public AudioSource playerThrusterAudio;
    public ParticleSystem playerParticles;

    [Header("Required Component References")]
    public new Rigidbody rigidbody;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton(buttonName))
        {
            //We apply the force to the player's downward transform as we are thrusting the level
            rigidbody.AddForce(-playerTransform.up*force,forceMode);
            if(!playerThrusterAudio.isPlaying)
            {
                playerThrusterAudio.Play();
                playerParticles.Play();
            }
        }
        else
        {
            playerThrusterAudio.Stop();
            playerParticles.Stop();
        }
    }

    void OnValidate()
    {
        if(rigidbody == null)
        {
            rigidbody = GetComponentInChildren<Rigidbody>();
        }
    }
}
