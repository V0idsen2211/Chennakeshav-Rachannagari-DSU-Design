using UnityEngine;

public class SoundRandomizer : MonoBehaviour
{
    private AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        source = GetComponent<AudioSource>();
        source.pitch = Random.Range(0.9f, 1.1f);
        source.Play();
        Destroy(gameObject, source.clip.length);
    }
}
    
