using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeVerts: Form {

  public int width;
  public int length;
  public int numTubes;

  public override void SetStructSize( Form parent ){ structSize = 16; }

  public override void SetCount( Form parent ){
    print( parent.count );
    numTubes = ((Hair)parent).numHairs;
    count = numTubes * width * length;
  }

}





