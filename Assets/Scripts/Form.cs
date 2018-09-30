using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Form : Cycle {


  public int count;

  [HideInInspector] public bool intBuffer;

  [HideInInspector] public int structSize;
  [HideInInspector] public ComputeBuffer _buffer;

//  [HideInInspector] public string name;
  [HideInInspector] public string description;
  [HideInInspector] public float timeToCreate;
  [HideInInspector] public int totalMemory;  

  



  public Material debugMaterial;

  public virtual void _Create(Form parent){
    _Create();
    debugMaterial = new Material( debugMaterial );
    SetStructSize( parent );
    SetCount( parent );
    SetBufferType();
    Create();
  }

  public virtual void _OnGestate(Form parent){
    
    _OnGestate();
    _buffer = MakeBuffer();
    Embody(parent);

  }

  public virtual void Embody( Form parent ){}
  public virtual void SetCount( Form parent ){}
  public virtual void SetStructSize( Form parent ){}
  public virtual void SetBufferType(){}

  public override void Destroy(){

    ReleaseBuffer();
    created = false;

  }

  public void GetData( int[] values ){ _buffer.GetData(values); }
  public void GetData( float[] values ){ _buffer.GetData(values); }

  public void SetData( float[] values ){ _buffer.SetData( values );}
  public void SetData( int[] values ){ _buffer.SetData( values );}

  public ComputeBuffer MakeBuffer(){

    if( intBuffer == true ){
      return new ComputeBuffer( count, sizeof(int) * structSize );
    }else{
      return new ComputeBuffer( count, sizeof(float) * structSize );
    }
  }


  public void ReleaseBuffer(){
   if(_buffer != null){ _buffer.Release(); }
  }

  public override void WhileDebug(){
    debugMaterial.SetPass(0);
    debugMaterial.SetBuffer("_vertBuffer", _buffer);
    debugMaterial.SetInt("_Count",count);
    Graphics.DrawProcedural(MeshTopology.Triangles, count * 3 * 2 );
  }

}