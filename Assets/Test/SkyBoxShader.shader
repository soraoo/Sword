Shader "Test/SkyBoxShader"
{
	Properties
	{
		_SkyBoxTex ("SkyBox Texture", Cube) = "white" {}
	}
	SubShader
	{
		Cull Off
		ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float3 worldPos : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			float4 _Cornors[4];
			TextureCube _SkyBoxTex;
			SamplerState sampler_SkyBoxTex;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = v.vertex;
				o.worldPos = _Cornors[v.uv.x + v.uv.y*2].xyz;
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				float3 viewDir = normalize(i.worldPos - _WorldSpaceCameraPos);
				return _SkyBoxTex.Sample(sampler_SkyBoxTex, viewDir);
			}	
			ENDCG
		}
	}
}
