Shader "Custom/Bloom" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1.0,1.0,1.0,1.0)
		_Sensitivity ("Sensitivity", Float) = 0.8
		_Intensity ("Intensity", Float) = 1.0
		_BlurSize ("BlurSize", Float) = 20
	}
	SubShader {
	
		// High-intensity pixel extract pass
		Pass {
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
		
			CGPROGRAM
			#pragma vertex vert
	        #pragma fragment frag

			uniform sampler2D _MainTex;
			uniform float4 _Color;
			uniform float _Sensitivity;

			struct vertexInput {
	            half4 vertex : POSITION;
	            half2 texcoord : TEXCOORD0;
	        };

	        struct vertexOutput {
	            half4 pos : SV_POSITION;
	            half2 tex : TEXCOORD0;
	        };

			 vertexOutput vert (vertexInput v)
	        {
	            vertexOutput o;
	            
	            o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	            o.tex = v.texcoord;
	           	
	            return o;
	        }
	         
	        fixed4 frag (vertexOutput i) : COLOR
	        {
	        	fixed4 tex = tex2D(_MainTex, i.tex);
	        	if((tex.r + tex.g + tex.b) / 3.0 > _Sensitivity || (tex.r >= _Sensitivity || tex.g >= _Sensitivity || tex.b >= _Sensitivity)) {
	        		return tex;
	        	}
	        	else {
	        		return fixed4(0, 0, 0, 0);
	        	}
	        }
			ENDCG
		}
		
		// Gaussian-blur pass (X axis)
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			CGPROGRAM
			#pragma vertex vert
	        #pragma fragment frag

			uniform sampler2D _MainTex;
			uniform int _BlurSize;

			struct vertexInput {
	            half4 vertex : POSITION;
	            half2 texcoord : TEXCOORD0;
	        };

	        struct vertexOutput {
	            half4 pos : SV_POSITION;
	            half2 tex : TEXCOORD0;
	        };

			 vertexOutput vert (vertexInput v)
	        {
	            vertexOutput o;
	            
	            o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	            o.tex = v.texcoord;
	            
	            return o;
	        }
	         
	        fixed4 frag (vertexOutput i) : COLOR
	        {
	        	const float blurSize = 1.0/_ScreenParams.x;
	        	fixed4 sum = fixed4(0,0,0,0);
		
				for(int ii=-_BlurSize;ii<=_BlurSize;ii++)
					sum += tex2D(_MainTex, half2(i.tex.x + ii*blurSize, i.tex.y)) * (_BlurSize+2-abs(ii)) / 100.0;
	        	
	        	return sum;
	        }
			ENDCG
		}
		
		// Gaussian-blur pass (Y axis)
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			CGPROGRAM
			#pragma vertex vert
	        #pragma fragment frag

			uniform sampler2D _MainTex;
			uniform float4 _Color;
			uniform float _Intensity;
			uniform int _BlurSize;

			struct vertexInput {
	            half4 vertex : POSITION;
	            half2 texcoord : TEXCOORD0;
	        };

	        struct vertexOutput {
	            half4 pos : SV_POSITION;
	            half2 tex : TEXCOORD0;
	        };

			 vertexOutput vert (vertexInput v)
	        {
	            vertexOutput o;
	            
	            o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	            o.tex = v.texcoord;
	            
	            return o;
	        }
	         
	        fixed4 frag (vertexOutput i) : COLOR
	        {
	        	const float blurSize = 1.0/_ScreenParams.x;
	        	fixed4 sum = fixed4(0,0,0,0);
	        	
	        	for(int ii=-_BlurSize;ii<=_BlurSize;ii++)
					sum += tex2D(_MainTex, half2(i.tex.x, i.tex.y + ii*blurSize)) * (_BlurSize+2-abs(ii)) / 100.0;
	        	
	        	return ((sum) / _Intensity) * _Color;
	        }
			ENDCG
		}
		
		// Blend pass (One One) - Blends the original renderTexture with the modified one
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			Blend One One
			CGPROGRAM
			#pragma vertex vert
	        #pragma fragment frag

			uniform sampler2D _MainTex;

			struct vertexInput {
	            half4 vertex : POSITION;
	            half2 texcoord : TEXCOORD0;
	        };

	        struct vertexOutput {
	            half4 pos : SV_POSITION;
	            half2 tex : TEXCOORD0;
	        };

			 vertexOutput vert (vertexInput v)
	        {
	            vertexOutput o;
	            
	            o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	            o.tex = v.texcoord;
	            
	            return o;
	        }
	         
	        fixed4 frag (vertexOutput i) : COLOR
	        {
	        	fixed4 tex = tex2D(_MainTex, i.tex);
	        	return tex;
	        }
			ENDCG
		}
	} 
}
