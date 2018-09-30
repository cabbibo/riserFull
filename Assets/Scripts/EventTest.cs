using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour {

  public EventManager events;
	// Use this for initialization
	void Start () {

    EventManager.OnGripDown += Hello;
		
	}

  void Hello( GameObject t ){
    print("hello");
  }
	
	// Update is called once per frame
	void Update () {
		
	}
}
