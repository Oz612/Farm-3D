using UnityEngine;

public class SoundsCash : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Obtiene el componente AudioSource del objeto
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Reproduce el sonido
            audioSource.Play();
        }
    }

}


