﻿Shader "Debug/IndexLine16Struct" {
	Properties {

    _Color ("Color", Color) = (1,1,1,1)
		}


  SubShader{
//        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
    Cull Off
    Pass{

      //Blend SrcAlpha OneMinusSrcAlpha // Alpha blending

      CGPROGRAM
      #pragma target 4.5

      #pragma vertex vert
      #pragma fragment frag

      #include "UnityCG.cginc"

		  uniform int _Count;
      uniform float3 _Color;

struct Vert{

  float3 pos;
  float3 vel;
  float3 nor;
  float3 tan;
  float2 uv;
  float2 debug;
};



      StructuredBuffer<Vert> _vertBuffer;
      StructuredBuffer<int> _triBuffer;


      //uniform float4x4 worldMat;

      //A simple input struct for our pixel shader step containing a position.
      struct varyings {
          float4 pos      : SV_POSITION;
      };


      //Our vertex function simply fetches a point from the buffer corresponding to the vertex index
      //which we transform with the view-projection matrix before passing to the pixel program.
      varyings vert (uint id : SV_VertexID){

        varyings o;

        int baseTri = id / 6;
        int triID = id % 6;
        int whichTri = triID/2;
        int alternate = id %2;
        if( baseTri*3+whichTri < _Count ){

        	int t1 = _triBuffer[baseTri*3+ ((whichTri+0)%3)];
        	int t2 = _triBuffer[baseTri*3+ ((whichTri+1)%3)];


        	Vert v1 = _vertBuffer[t1];
        	Vert v2 = _vertBuffer[t2];

        	

        		float3 pos;;
        		if( alternate == 0 ){
        			pos = v1.pos;
        		}else{
        			pos = v2.pos;
        		}

 
	        o.pos = mul (UNITY_MATRIX_VP, float4(pos,1.0f));
	       

       	}

        return o;

      }




      //Pixel function returns a solid color for each point.
      float4 frag (varyings v) : COLOR {
      		float3 col = float3(0,1,1);//v.debug;//normalize(v.nor) * .5 + .5;

      		//if( v.debug == 0 ){ discard;}
         
          //col = float3( v.uv.x , v.uv.y , .5);
          //return float4( _Color * length(v.nor) , 1 );
          return float4( col, 1 );

      }

      ENDCG

    }
  }

  Fallback Off


}
