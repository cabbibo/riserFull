using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Life : Cycle {

  [HideInInspector] public string primaryName;
  [HideInInspector] public Form primaryForm;
  public ComputeShader shader;
  public string kernelName;
  [HideInInspector] public int kernel;
  [HideInInspector] public float executionTime;

  public Dictionary<string, Form> boundForms;
  public Dictionary<string, int> boundInts;

  protected bool allBuffersSet;
  protected int numGroups;
  protected uint numThreads;

  public struct BoundAttribute {
    public string nameInShader;
    public string shaderType;
    public string attributeName;
    public System.Object boundObject;
  }

  public List<BoundAttribute> boundAttributes;


  public delegate void SetValues(ComputeShader shader, int kernel);
  public event SetValues OnSetValues;

  public override void Create(){
     boundForms = new Dictionary<string, Form>();
     boundInts = new Dictionary<string, int>();
     boundAttributes = new List<BoundAttribute>();
     FindKernel();
     GetNumThreads();
     OnCreate();
  }

  public virtual void OnCreate(){}


  public virtual void FindKernel(){
    kernel = shader.FindKernel( kernelName );
  }

  public virtual void GetNumThreads(){
    uint y; uint z;
    shader.GetKernelThreadGroupSizes(kernel, out numThreads , out y, out z);
  }

  public virtual void GetNumGroups(){
    numGroups = (primaryForm.count+((int)numThreads-1))/(int)numThreads;
  }
 
  public void BindForm( string name , Form form ){
    boundForms.Add( name ,form );
  }

   public void BindInt( string name , int form ){
    boundInts.Add( name ,form );
  }

  public void BindPrimaryForm(string name , Form form){
    primaryForm = form;
    primaryName = name;
  }




  public void Live(){

    if( OnSetValues != null ){ OnSetValues(shader,kernel); }
   
    GetNumGroups();
    SetShaderValues();
    BindAttributes();

    // set this true every frame, 
    // and allow each buffer to make 
    // untrue as needed
    allBuffersSet = true;

    _SetInternal();

    foreach(KeyValuePair<string,Form> form in boundForms){
      SetBuffer(form.Key , form.Value);
    }

    foreach(KeyValuePair<string,int> form in boundInts){
      shader.SetInt(form.Key , form.Value);
    }

    SetBuffer( primaryName , primaryForm );

    // if its still true than we can dispatch
    if ( allBuffersSet ){
      if( debug ) print( "name : " + kernelName + " Num groups : " + numGroups );
      shader.Dispatch( kernel,numGroups ,1,1);
    }

    AfterDispatch();

  }

  public virtual void _SetInternal(){
    
    shader.SetFloat("_Time", Time.time);
    shader.SetFloat("_Delta", Time.deltaTime);

  }

  public virtual void AfterDispatch(){}

  public virtual void SetShaderValues(){

  }

  private void SetBuffer(string name , Form form){
      if( form._buffer != null ){
        shader.SetBuffer( kernel , name , form._buffer);
        shader.SetInt(name+"_COUNT" , form.count );
      }else{
        allBuffersSet = false;
        print("YOUR BUFFER : " + name +  " IS NULL!");
      }
  }

  public void BindAttribute( string nameInShader, string type , string attributeName , System.Object obj ){
    BoundAttribute a = new BoundAttribute();

    a.nameInShader = nameInShader;
    a.shaderType = type;
    a.attributeName = attributeName;
    a.boundObject = obj;

    boundAttributes.Add(a);
  }

  public void BindAttributes(){
    foreach(  BoundAttribute b in boundAttributes ){


      string s = "";
      if( debug == true ){
        s +="UNIFORM : "  + b.nameInShader;
      }
      if( b.shaderType == "float" ){
        float value = (float)b.boundObject.GetType().GetField(b.attributeName).GetValue(b.boundObject);
        if( debug == true ){ print( s + " || VALUE : " + value);}
        shader.SetFloat(b.nameInShader,value);
      }else if( b.shaderType == "floats" ){
        float[] value = (float[])b.boundObject.GetType().GetField(b.attributeName).GetValue(b.boundObject);
        if( debug == true ){ print( s + " || VALUE : " + value);}
        shader.SetFloats(b.nameInShader,value);
      }else if( b.shaderType == "int" ){
        int value = (int)b.boundObject.GetType().GetField(b.attributeName).GetValue(b.boundObject);
        if( debug == true ){ print( s + " || VALUE : " + value);}
        shader.SetInt(b.nameInShader,value);
      }else if( b.shaderType == "Vector3" ){
        Vector3 value = (Vector3)b.boundObject.GetType().GetField(b.attributeName).GetValue(b.boundObject);
        if( debug == true ){ print( s + " || VALUE : " + value);}
        shader.SetVector(b.nameInShader,value);
      }else if( b.shaderType == "vector" ){
        Vector3 value = (Vector3)b.boundObject.GetType().GetField(b.attributeName).GetValue(b.boundObject);
        if( debug == true ){ print( s + " || VALUE : " + value);}
        shader.SetVector(b.nameInShader,value);
      }else if( b.shaderType == "Texture" ){
        Texture value = (Texture)b.boundObject.GetType().GetField(b.attributeName).GetValue(b.boundObject);
        if( debug == true ){ print( s + " || VALUE : " + value);}
        shader.SetTexture(kernel,b.nameInShader,value);
      }else if( b.shaderType == "Buffer" ){
        ComputeBuffer value = (ComputeBuffer)b.boundObject.GetType().GetField(b.attributeName).GetValue(b.boundObject);
        if( debug == true ){ print( s + " || VALUE : " + value);}
        shader.SetBuffer(kernel,b.nameInShader,value);
      }

    }
  }

}

