using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceModel : MonoBehaviour {

  public GameObject model;
  public GameObject sphere1;
  public GameObject sphere2;
  public GameObject sphere3;
  public GameObject sphereRepresent1;
  public GameObject sphereRepresent2;
  public GameObject sphereRepresent3;
  public Vector3 p1;
  public Vector3 p2;
  public Vector3 p3;

  public int sphereID;
  public bool canPlace = true;
	// Use this for initialization
	void Start () {
      EventManager.OnTouchpadDown += PlaceSphere;
		  EventManager.OnTouchpadUp += CanPlace;
	}

  void PlaceSphere( GameObject t ){

    if( canPlace == true ){
    canPlace = false;
      if( sphereID == 0 ){
      print("one");
      sphereID = 1;
      p1 = t.transform.position;
      sphereRepresent1.transform.position = p1;
    }else if(sphereID==1){
      sphereID = 2;
      print("two");
      p2 = t.transform.position;
      sphereRepresent2.transform.position = p2;
    }else if(sphereID==2){
      sphereID = 0;
      print("three");
      p3 = t.transform.position;
      sphereRepresent3.transform.position = p3;
      ScaleModel();
    }}


  }

  void CanPlace(GameObject g){
    canPlace = true;
  }

 
  void ScaleModel(){

    print("hello");

    Quaternion r1 = new Quaternion();
    Quaternion r2 = new Quaternion();

    Vector3 v1 = (sphere1.transform.localPosition-sphere2.transform.localPosition).normalized;
    Vector3 s1 = (p1-p2).normalized;

    r1.SetFromToRotation(v1,s1);




    float s  = (p1-p2).magnitude / (sphere1.transform.localPosition-sphere2.transform.localPosition).magnitude ;

    Vector3 v2 = ((sphere1.transform.localPosition ) - (sphere3.transform.localPosition )).normalized;

    Vector3 s2 = (p1-p3).normalized;

  r2.SetFromToRotation( v2 , s2 );


  Quaternion r = new Quaternion();
  r = Quaternion.Slerp(r1,r2,.5f);

    model.transform.position = p1 -  (r*sphere1.transform.localPosition )*s;// * scale; ///newM.MultiplyPoint( new Vector3(0,0,0));
    model.transform.rotation = r;

    model.transform.localScale = new Vector3(s,s,s);

  }
	
	// Update is called once per frame
	void Update () {
		//ScaleModel();
	}
}
