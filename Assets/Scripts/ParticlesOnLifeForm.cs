using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnLifeForm: LifeForm {
  


  public Vector3 cameraUp;
  public Vector3 cameraLeft;
  public float radius;

  public Life place;

  public SkinnedLifeForm skin;

  public Form verts;

  public ParticleTransferVerts bodyVerts;
  public ParticleTransferTris bodyTris;
  public Life bodyTransfer;
  public Body body;

  public float whirlwindSpeed;
  public int whirlwindState;

  // Use this for initialization
  public override void Create(){
    Lifes.Add(place);
    Lifes.Add(bodyTransfer);
    Forms.Add(skin.verts);
    Forms.Add(verts);
    Forms.Add(bodyVerts);
    Forms.Add(bodyTris);



    
    verts._Create(skin.verts);
    
    bodyVerts._Create(verts);
    bodyTris._Create(bodyVerts);



  place._Create();
  bodyTransfer._Create();

    place.BindPrimaryForm("_VertBuffer",verts);
    place.BindForm("_SkinnedBuffer",skin.verts);
    place.BindAttribute("_Whirlwind", "int" , "whirlwindState" , this);
    place.BindAttribute("_WhirlwindSpeed", "float" , "whirlwindSpeed" , this);

    bodyTransfer.BindAttribute("_CameraUp" , "vector" , "cameraUp" , this );
    bodyTransfer.BindAttribute("_CameraLeft" , "vector" , "cameraLeft" , this );
    bodyTransfer.BindAttribute("_Radius" , "float" , "radius" , this );


    bodyTransfer.BindPrimaryForm("_VertBuffer", bodyVerts);
    bodyTransfer.BindForm("_ParticleBuffer", verts); 


 

  }


  public override void OnGestate(){
    verts._OnGestate(verts );
    bodyTris._OnGestate( verts );
    bodyVerts._OnGestate( verts );
    body._Create( bodyVerts , bodyTris );

  }
  public override void OnBirth(){
    body.Show();
  }

  public override void WhileLiving(float v){


//    print(Camera.main);
    cameraLeft = -Camera.main.transform.right;
    cameraUp = Camera.main.transform.up;

    place.Live();
    bodyTransfer.Live();
    body.WhileLiving(1);
  }

}
