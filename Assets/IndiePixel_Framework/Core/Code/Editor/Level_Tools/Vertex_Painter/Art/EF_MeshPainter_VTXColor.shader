// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Emortal/MeshPainter/GG_MeshPainter_VTXColor"
{
	Properties{}

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 vertexColor : COLOR;
			};

			
			v2f vert (appdata v)
			{
				v2f o = (v2f)0;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.vertexColor = v.color;

				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				return i.vertexColor;
			}
			ENDCG
		}
	}
}
