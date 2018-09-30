using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedLifeForm : LifeForm {
  
  public Life skin;

  public Form verts;
  public Form tris;
  public Form bones;

	// Use this for initialization
	public override void Create(){
    Lifes.Add(skin);
    Forms.Add(verts);
    Forms.Add(tris);
    Forms.Add(bones);

    skin._Create();

    verts._Create(verts);
    tris._Create(tris);
    bones._Create(bones);

    skin.BindPrimaryForm("_VertBuffer",verts);
    skin.BindForm("_BoneBuffer",bones);

	}


  public override void OnGestate(){

    verts._OnGestate(verts );
    tris._OnGestate( tris   );
    bones._OnGestate( bones   );
  }

  public override void WhileLiving(float v){
    skin.Live();
    ((Bones)bones).UpdateBones();
  }

}
