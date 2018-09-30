using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkinnedTriangles : IndexForm {

  [ HideInInspector ] public int width;
  [ HideInInspector ] public int length;
  [ HideInInspector ] public int numTubes;

  private int[] values;
  public override void SetCount( Form parent ){
    SkinnedMeshRenderer mesh = ((SkinnedVerts)toIndex).mesh;
    values = mesh.sharedMesh.triangles;
    count = values.Length;
  }

  public override void Embody(Form parent){
    SetData(values);
  }

}

