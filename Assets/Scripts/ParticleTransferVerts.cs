using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTransferVerts: Form {

  public override void SetStructSize( Form parent ){ structSize = 16; }

  public override void SetCount( Form parent ){
    // 0-1
    // |/|
    // 2-3
    count = parent.count * 4;
  }

}



