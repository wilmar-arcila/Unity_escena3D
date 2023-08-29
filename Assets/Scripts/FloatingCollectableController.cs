using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FloatingCollectableController : MonoBehaviour
{
    private float rotSpeed = 60f;

    void Update()
    {
        transform.Rotate(0,0, rotSpeed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Collider: " + other.gameObject.name);
        if (other.CompareTag("Player")){
            AudioSource _audio = GetComponent<AudioSource>();
            _audio.Play();
            StartCoroutine(destroyObjectWithAudio(_audio));
        }
    }

    private IEnumerator destroyObjectWithAudio(AudioSource _audio){
      yield return new WaitUntil(() => _audio.isPlaying == false); // Se ejecuta esta línea hasta que la condición sea verdadera
      gameObject.SetActive(false);
    }
}
