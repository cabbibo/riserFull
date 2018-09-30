using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cycle : MonoBehaviour{

  [ HideInInspector ] public bool created;
  
  [ HideInInspector ] public bool begunGestation;
  [ HideInInspector ] public bool gestating;
  [ HideInInspector ] public bool gestated;

  [ HideInInspector ] public bool begunBirth;
  [ HideInInspector ] public bool birthed;
  [ HideInInspector ] public bool birthing;

  [ HideInInspector ] public bool begunLive;
  [ HideInInspector ] public bool living;
  [ HideInInspector ] public bool lived;

  [ HideInInspector ] public bool begunDeath;
  [ HideInInspector ] public bool dying;
  [ HideInInspector ] public bool died;

  [ HideInInspector ] public bool destroyed;

  public bool debug;

/*

  Creation

*/
  public virtual void _Create(){
    Create();
    created = true;
  }
  public virtual void Create(){}

/*

  Gestation

*/

  public virtual void _OnGestate(){
    begunGestation = true;
    OnGestate();
    gestating = true;
  }
  public virtual void OnGestate(){}
  
  public virtual void _WhileGestating(float v){
    WhileGestating(v);
  }
  public virtual void WhileGestating(float v){}
  
  public virtual void _OnGestated(){
    gestating = false;
    OnGestated();
    gestated = true;
  }
  public virtual void OnGestated(){}



/*

  Birth

*/


  public virtual void _OnBirth(){
    begunBirth = true;
    OnBirth();
    birthing = true;
  }
  public virtual void OnBirth(){}
  
  public virtual void _WhileBirthing(float v){
    WhileBirthing(v);
  }
  public virtual void WhileBirthing(float v){}
  
  public virtual void _OnBirthed(){
    birthing = false;
    OnBirthed();
    birthed = true;
  }
  public virtual void OnBirthed(){}

/*

  LIVE

*/

  public virtual void _OnLive(){
    begunLive = true;
    OnLive();
    living = true;
  }
  public virtual void OnLive(){}
  
  public virtual void _WhileLiving(float v){
    WhileLiving(v);
  }
  public virtual void WhileLiving(float v){}
  
  public virtual void _OnLived(){
    living = false;
    OnLived();
    lived = true;
  }
  public virtual void OnLived(){}



/*

  DEATH

*/


  public virtual void _OnDie(){
    begunDeath = true;
    OnDie();
    dying = true;
  }
  public virtual void OnDie(){}
  
  public virtual void _WhileDying(float v){
    WhileDying(v);
  }
  public virtual void WhileDying(float v){}
  
  public virtual void _OnDied(){
    dying = false;
    OnDied();
    died = true;
  }
  public virtual void OnDied(){}



  /*
      Destroy
  */

  public virtual void _Destroy(){
    Destroy();
  }

  public virtual void Destroy(){}




  /*
        DEBUG
  */

  public virtual void _WhileDebug(){
    if( debug == true ){
      WhileDebug();
    }
  }

  public virtual void WhileDebug(){}


}
