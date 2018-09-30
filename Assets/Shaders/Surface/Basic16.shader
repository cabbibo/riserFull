Shader "PostTransfer/Basic16" {
  Properties {

    _Tex("", 2D) = "white" {}




  }

	SubShader {
		// COLOR PASS

		Pass {
			Tags{ "LightMode" = "ForwardBase" }
			Cull Front

			CGPROGRAM
			#pragma target 4.5
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
      #include "../Chunks/hsv.cginc"
			#include "../Chunks/noise.cginc"


			sampler2D _Tex;
			struct Transfer {
			  float3 pos;
			  float3 vel;
			  float3 nor;
			  float3 tangent;
			  float2 uv;
			  float2  debug;
			};

			StructuredBuffer<Transfer> _TransferBuffer;

			struct varyings {
				float4 pos 		: SV_POSITION;
				float3 nor 		: TEXCOORD0;
				float2 uv  		: TEXCOORD1;
				float3 eye      : TEXCOORD5;
				float3 worldPos : TEXCOORD6;
				float3 debug    : TEXCOORD7;
				float3 closest    : TEXCOORD8;
				UNITY_SHADOW_COORDS(2)
			};

			varyings vert(uint id : SV_VertexID) {

				float3 fPos = _TransferBuffer[id].pos;
				float3 fNor = _TransferBuffer[id].nor;
        float2 fUV = _TransferBuffer[id].uv;
				float2 debug = _TransferBuffer[id].debug;



				//fPos = fPos;float3( sin(id*10) , fPos.y, sin( id * 3));
				varyings o;

				UNITY_INITIALIZE_OUTPUT(varyings, o);

				o.pos = mul(UNITY_MATRIX_VP, float4(fPos,1));
				o.worldPos = fPos;
				o.eye = _WorldSpaceCameraPos - fPos;
				o.nor = fNor;
				o.uv =  fUV;
				o.debug = float3(debug.x,debug.y,0);

				UNITY_TRANSFER_SHADOW(o,o.worldPos);

				return o;
			}

			float4 frag(varyings v) : COLOR {
		
				fixed shadow = UNITY_SHADOW_ATTENUATION(v,v.worldPos -v.nor ) * .9 + .1 ;

        float3 refl = reflect( v.eye , v.nor );
        float m = dot( normalize(refl) , float3(0,1,0));

				float3 col = hsv(v.uv.x * .5 + sin( v.debug.x * .1 ) * .1 + sin(_Time.y+ v.debug) * .2 , .7,1) *3*( normalize(refl) * .5 + .5);//v.nor * 1.5 + .5;//float3( 1,1,1);
				
        col *= hsv( m * m * m ,.5,1);
        col *= hsv(v.debug.y*100,.5,1);

        if(sin(v.uv.x*100) + noise(float3(v.uv,_Time.x) *30) < 0 ){ discard;}
        return float4( col * shadow, 1.);
			}

			ENDCG
		}


        Pass {
      Tags{ "LightMode" = "ForwardBase" }
      Cull Back

      CGPROGRAM
      #pragma target 4.5
      #pragma vertex vert
      #pragma fragment frag
      #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight

      #include "UnityCG.cginc"
      #include "AutoLight.cginc"
      #include "../Chunks/hsv.cginc"
      #include "../Chunks/noise.cginc"


      sampler2D _Tex;
      struct Transfer {
        float3 pos;
        float3 vel;
        float3 nor;
        float3 tangent;
        float2 uv;
        float2  debug;
      };

      StructuredBuffer<Transfer> _TransferBuffer;

      struct varyings {
        float4 pos    : SV_POSITION;
        float3 nor    : TEXCOORD0;
        float2 uv     : TEXCOORD1;
        float3 eye      : TEXCOORD5;
        float3 worldPos : TEXCOORD6;
        float3 debug    : TEXCOORD7;
        float3 closest    : TEXCOORD8;
        UNITY_SHADOW_COORDS(2)
      };

      varyings vert(uint id : SV_VertexID) {

        float3 fPos = _TransferBuffer[id].pos;
        float3 fNor = _TransferBuffer[id].nor;
        float2 fUV = _TransferBuffer[id].uv;
        float2 debug = _TransferBuffer[id].debug;



        //fPos = fPos;float3( sin(id*10) , fPos.y, sin( id * 3));
        varyings o;

        UNITY_INITIALIZE_OUTPUT(varyings, o);

        o.pos = mul(UNITY_MATRIX_VP, float4(fPos,1));
        o.worldPos = fPos;
        o.eye = _WorldSpaceCameraPos - fPos;
        o.nor = fNor;
        o.uv =  fUV;
        o.debug = float3(debug.x,debug.y,0);

        UNITY_TRANSFER_SHADOW(o,o.worldPos);

        return o;
      }

      float4 frag(varyings v) : COLOR {
    
        fixed shadow = UNITY_SHADOW_ATTENUATION(v,v.worldPos -v.nor ) * .9 + .1 ;

        float3 refl = reflect( v.eye , v.nor );
        float m = dot( normalize(refl) , float3(0,1,0));

        float3 col = hsv(v.uv.x * .5 + sin( v.debug.x * .1 ) * .1 + sin(_Time.y+ v.debug) * .2 , .7,1) *3*( normalize(refl) * .5 + .5);//v.nor * 1.5 + .5;//float3( 1,1,1);
        
        col *= hsv( m * m * m ,.5,1);
        col *= hsv(v.debug.y*100,.5,1);

        if(sin(v.uv.x*100+ v.debug.x) + noise(float3(v.uv,_Time.x) *30) > .4 ){ 
        col = float3(1,1,1)-col;}
        col *= 2;
        if(sin(v.uv.x*100+ v.debug.x) + noise(float3(v.uv,_Time.x) *30) <0  ){ discard;}
        return float4( col * shadow, 1.);
      }

      ENDCG
    }
   // S
   // SHADOW PASS

    Pass
    {
      Tags{ "LightMode" = "ShadowCaster" }


      Fog{ Mode Off }
      ZWrite On
      ZTest LEqual
      Cull Off
      Offset 1, 1
      CGPROGRAM

      #pragma target 4.5
      #pragma vertex vert
      #pragma fragment frag
      #pragma multi_compile_shadowcaster
      #pragma fragmentoption ARB_precision_hint_fastest
      #include "UnityCG.cginc"

      struct Transfer {
        float3 pos;
        float3 vel;
        float3 nor;
        float3 tangent;
        float2 uv;
        float2  debug;
      };

      StructuredBuffer<Transfer> _TransferBuffer;
      struct v2f {
        V2F_SHADOW_CASTER;
      };


      v2f vert(appdata_base v, uint id : SV_VertexID)
      {
        v2f o;
        o.pos = mul(UNITY_MATRIX_VP, float4(_TransferBuffer[id].pos, 1));
        return o;
      }

      float4 frag(v2f i) : COLOR
      {
        SHADOW_CASTER_FRAGMENT(i)
      }
      ENDCG
    }
  


	}

}
