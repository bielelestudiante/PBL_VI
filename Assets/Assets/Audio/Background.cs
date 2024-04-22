using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips; // Lista de clips de audio a reproducir
    private AudioSource audioSource; // Referencia al componente AudioSource
    private AudioClip lastPlayedClip; // Referencia al último clip reproducido

    void Start()
    {
        // Obtener el componente AudioSource del GameObject actual o añadir uno nuevo si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Reproducir un clip de audio al azar de la lista
        PlayRandomAudioClip();
    }

    void PlayRandomAudioClip()
    {
        // Comprobar si hay clips de audio en la lista
        if (audioClips.Length == 0)
        {
            Debug.LogWarning("No hay clips de audio para reproducir.");
            return;
        }

        // Seleccionar un clip aleatorio que no sea el último reproducido
        AudioClip randomClip = GetRandomClipDifferentFromLast();

        // Reproducir el clip de audio seleccionado
        audioSource.clip = randomClip;
        audioSource.Play();

        // Programar la reproducción del siguiente clip
        Invoke("PlayRandomAudioClip", randomClip.length);
    }

    AudioClip GetRandomClipDifferentFromLast()
    {
        AudioClip randomClip;

        // Seleccionar un clip aleatorio que no sea el último reproducido
        do
        {
            randomClip = audioClips[Random.Range(0, audioClips.Length)];
        }
        while (randomClip == lastPlayedClip && audioClips.Length > 1); // Asegurarse de que el clip no sea el último reproducido

        // Actualizar la referencia al último clip reproducido
        lastPlayedClip = randomClip;

        return randomClip;
    }
}
