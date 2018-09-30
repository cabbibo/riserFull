using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWhenClose : MonoBehaviour {

  public ReduceLife reduce;
  public float pitch;
  public float chance;

  public AudioPlayer audio;
  public AudioClip clip;

	// Update is called once per frame
	void Update () {
	
    float r = Random.Range(0.0001f, .999f);
    if( reduce.value.x / chance > r ){
      audio.Play( clip , reduce.value.x / pitch  , .4f );
    }	
	}
}
