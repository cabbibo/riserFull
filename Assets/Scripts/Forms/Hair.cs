using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hair: Form {

  public int numVertsPerHair;
  public float length;
  public Material lineDebugMaterial;
  public int numHairs;

  public override void SetStructSize( Form parent ){ structSize = 16; }

  public override void SetCount( Form parent ){
    numHairs = parent.count;
    count = numHairs * numVertsPerHair; 
  }

  public override void WhileDebug(){
    //print(count);

    lineDebugMaterial.SetPass(0);
    lineDebugMaterial.SetBuffer("_vertBuffer", _buffer);
    lineDebugMaterial.SetInt("_Count",count);
    lineDebugMaterial.SetInt("_NumVertsPerHair",numVertsPerHair);
    Graphics.DrawProcedural(MeshTopology.Lines, count  * 2 );

    debugMaterial.SetPass(0);
    debugMaterial.SetBuffer("_vertBuffer", _buffer);
    debugMaterial.SetInt("_Count",count);
    Graphics.DrawProcedural(MeshTopology.Triangles, count * 3 * 2 );
  }


}

