using UnityEngine;

public class SoundsCash : MonoBehaviour
{
    public AudioSource AudioSource;

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource.Play();
    }

}


