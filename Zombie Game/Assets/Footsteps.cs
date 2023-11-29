using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {
    public AudioClip[] clips;
    private AudioSource source;

    void Start() {
        source = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
        Debug.Log("aa");
    }
}
