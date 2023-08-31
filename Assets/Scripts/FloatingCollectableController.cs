using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FloatingCollectableController : MonoBehaviour
{
    [SerializeField] private bool xAxis;
    [SerializeField] private bool yAxis;
    [SerializeField] private bool zAxis;
    [Range(10f, 100f)]
    [SerializeField] private float rotSpeed = 60f;

    void Update()
    {
        if(xAxis) transform.Rotate(rotSpeed*Time.deltaTime,0,0);
        if(yAxis) transform.Rotate(0,rotSpeed*Time.deltaTime,0);
        if(zAxis) transform.Rotate(0,0,rotSpeed*Time.deltaTime);
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
