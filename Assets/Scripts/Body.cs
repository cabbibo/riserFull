using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : Cycle {


  [HideInInspector] public MeshRenderer render;
  [HideInInspector] public Mesh mesh;
  
  public Material material;
  public Form triangles;
  public Form verts;


  public virtual void _Create( Form verts , IndexForm triangles){
    _Create();
    print( triangles );
    Create(  verts , triangles);
    Create();
  }


  // TODO CHECK IF ALREADY CREATED!!!!
  public virtual void Create(  Form verts , IndexForm triangles ){

    material = new Material(material);
    mesh = new Mesh ();
    mesh.vertices = new Vector3[verts.count];


    int[] tris = new int[triangles.count];
    triangles.GetData(tris);
    mesh.triangles =  tris; 

    if( verts.count > 65000 ){
      mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
    }

    mesh.bounds = new Bounds (Vector3.zero, Vector3.one * 1000f);
    mesh.UploadMeshData (true);


    GameObject go = new GameObject();
    go.name = gameObject.name + " : BODY";
    go.transform.parent = gameObject.transform;
    go.AddComponent<MeshFilter>().mesh = mesh;

    render = go.AddComponent<MeshRenderer>();
    render.material = material;
    render.enabled = false;

  }

  public void Hide(){
    render.enabled = false;
  }

  public void Show(){
    if( triangles._buffer != null && verts._buffer != null ){
      render.material.SetInt("_TransferCount", verts.count);
      render.material.SetBuffer("_TransferBuffer", verts._buffer );
      render.enabled= true;
    }else{
      print("u got a null buffer! add more info to me to know which one");
    }
  }

  public override void WhileLiving(float v){
    render.material.SetInt("_TransferCount", verts.count);
    render.material.SetBuffer("_TransferBuffer", verts._buffer );
  }


}
