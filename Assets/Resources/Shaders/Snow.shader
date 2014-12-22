Shader "Custom/Snow" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1.0,1.0,1.0,1.0)
	}
	SubShader {
		/*Tags { "Queue" = "Transparent" }*/
		Pass {
			Tags { "LightMode" = "ForwardBase" }
			CGPROGRAM
			#pragma vertex vert
	        #pragma fragment frag
	         
			uniform sampler2D _MainTex;
			uniform float4 _Color;
			
			uniform float4 _LightColor0;
			
			struct vertexInput {
	            half4 vertex : POSITION;
	            float3 normal : NORMAL;
	            half2 texcoord : TEXCOORD0;
	        };

	        struct vertexOutput {
	            half4 pos : SV_POSITION;
	            half2 tex : TEXCOORD0;
	            float4 posWorld : TEXCOORD1;
	            float3 normalDir : TEXCOORD2;
	        };

			 vertexOutput vert (vertexInput v)
	        {
	            vertexOutput o;
	            
	            o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	            o.posWorld = mul(_Object2World, v.vertex);
	            o.normalDir = normalize(mul(float4(v.normal, 0.0), _World2Object).xyz);
	            o.tex = v.texcoord;
	           	
	            return o;
	        }
	         
	        fixed4 frag (vertexOutput i) : COLOR
	        {
	        	float3 normalDirection = i.normalDir;
	        	float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				float atten;
				float3 lightDirection;
	        	
	        	if(_WorldSpaceLightPos0.w == 0.0)
	        	{
		        	atten = 1.0;
		        	lightDirection = normalize(_WorldSpaceLightPos0.xyz);
	        	}
	        	else
	        	{
	        		float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
		        	float dist = length(fragmentToLightSource);
		        	float atten = 1/dist;
		        	lightDirection = normalize(fragmentToLightSource);
	        	}
	        	
	        	fixed4 tex = tex2D(_MainTex, i.tex);
	        	
	        	float3 diffuseReflection = atten * _LightColor0.xyz * saturate(dot(normalDirection, lightDirection));
	        	
	        	return float4(diffuseReflection * tex, 1.0);
	        }
			ENDCG
		}
		Pass {
			Tags { "LightMode" = "ForwardAdd" }
			Blend One One
			CGPROGRAM
			#pragma vertex vert
	        #pragma fragment frag
	         
			uniform sampler2D _MainTex;
			uniform float4 _Color;
			
			uniform float4 _LightColor0;
			
			struct vertexInput {
	            half4 vertex : POSITION;
	            float3 normal : NORMAL;
	            half2 texcoord : TEXCOORD0;
	        };

	        struct vertexOutput {
	            half4 pos : SV_POSITION;
	            half2 tex : TEXCOORD0;
	            float4 posWorld : TEXCOORD1;
	            float3 normalDir : TEXCOORD2;
	        };

			 vertexOutput vert (vertexInput v)
	        {
	            vertexOutput o;
	            
	            o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	            o.posWorld = mul(_Object2World, v.vertex);
	            o.normalDir = normalize(mul(float4(v.normal, 0.0), _World2Object).xyz);
	            o.tex = v.texcoord;
	           	
	            return o;
	        }
	         
	        fixed4 frag (vertexOutput i) : COLOR
	        {
	        	float3 normalDirection = i.normalDir;
	        	float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				float atten;
				float3 lightDirection;
	        	
	        	if(_WorldSpaceLightPos0.w == 0.0)
	        	{
		        	atten = 1.0;
		        	lightDirection = normalize(_WorldSpaceLightPos0.xyz);
	        	}
	        	else
	        	{
	        		float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
		        	float dist = length(fragmentToLightSource);
		        	float atten = 1/dist;
		        	lightDirection = normalize(fragmentToLightSource);
	        	}
	        	
	        	fixed4 tex = tex2D(_MainTex, i.tex);
	        	
	        	float3 diffuseReflection = atten * _LightColor0.xyz * saturate(dot(normalDirection, lightDirection));
	        	
	        	return float4(diffuseReflection * tex, 1.0);
	        }
			ENDCG
		}
	} 
}
