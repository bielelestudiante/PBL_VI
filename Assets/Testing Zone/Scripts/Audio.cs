using UnityEngine;

public class SoundOnTrigger : MonoBehaviour
{
    public AudioClip soundEffect;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto en colisi�n tiene el tag o el nombre adecuado
        if (other.gameObject.CompareTag("ObjetoAudio"))
        {
            // Reproduce el sonido cuando hay una colisi�n
            AudioSource.PlayClipAtPoint(soundEffect, transform.position);
        }
    }
}
