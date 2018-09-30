using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*


  Is god in charge of all _ ? or is that the roll of the Life Form ?
  

  for Forms, passing in 'Parent', how do I swing that??


*/

public class God :  Cycle  {
  
  public LifeForm[] lifeforms;

  public void OnEnable(){

    for( int i = 0; i < lifeforms.Length; i++ ){
      lifeforms[i]._Create();
    }

    for( int i = 0; i < lifeforms.Length; i++ ){
      lifeforms[i]._OnGestate();
    }

    for( int i = 0; i < lifeforms.Length; i++ ){
      lifeforms[i]._OnGestated();
    }

    for( int i = 0; i < lifeforms.Length; i++ ){
      lifeforms[i]._OnBirth();
    }

    for( int i = 0; i < lifeforms.Length; i++ ){
      lifeforms[i]._OnBirthed();
    }

    for( int i = 0; i < lifeforms.Length; i++ ){
      lifeforms[i]._OnLive();
    }

  }
 
  public void OnRenderObject(){
    for( int i = 0; i < lifeforms.Length; i++ ){

      if( lifeforms[i].created == true ){

        lifeforms[i]._WhileDebug();

        foreach( Form f in lifeforms[i].Forms ){
          f._WhileDebug();
        }

        foreach( Life l in lifeforms[i].Lifes ){
          l._WhileDebug();
        }

      }
    }

  }

  public void LateUpdate(){
    for( int i = 0; i < lifeforms.Length; i++ ){
      if( lifeforms[i].gestating == true ){
        lifeforms[i]._WhileGestating(1);
      }
      if( lifeforms[i].birthing == true ){
        lifeforms[i]._WhileBirthing(1);
      }
      if( lifeforms[i].living == true ){
        lifeforms[i]._WhileLiving(1);
        
      }
      if( lifeforms[i].dying == true ){
        lifeforms[i]._WhileDying(1);
      }
    }
  }

}



