using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerDiamonds : MonoBehaviour
{
    public static AudioManagerDiamonds Instance;

    public AudioClip pickDiamond;             
    public AudioClip hitByRock;  
    public AudioClip loseDiamond;

    private Vector3 cameraPosition;         // position of camera

    private void Awake()
    {
        Instance = this;
        cameraPosition = Camera.main.transform.position;
    }

    // play the desired clip
    private void PlaySound(AudioClip clip)
    {
        // play the audioclip at the camera's position
        AudioSource.PlayClipAtPoint(clip, cameraPosition);
    }

    public void PlayPickDiamondClip()
    {
        PlaySound(pickDiamond);
    }

    public void PlayHitByRockClip()
    {
        PlaySound(hitByRock);
    }

    public void PlayLoseDiamondClip()
    {
        PlaySound(loseDiamond);
    }

}
