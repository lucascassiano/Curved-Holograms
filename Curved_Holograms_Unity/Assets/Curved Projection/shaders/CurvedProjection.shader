Shader "Projection Mapping/Curved Projection"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

		CGINCLUDE

#include "UnityCG.cginc"
#include "CurvedProjectionCorrection.cginc"

		sampler2D _MainTex;

	half4 frag(v2f i) : COLOR
	{
		i.uv = CurvedCorrection(i.uv);

	//if (i.uv.x>1 || i.uv.x<0 || i.uv.y>1 || i.uv.y<0) return half4(0,0,0,0);
	//else return tex2D(_MainTex, i.uv);
	/*
	
	float check = saturate(sign(i.uv.x)); //i.uv.x<0
	check *= saturate(-sign(i.uv.x - 1)); //i.uv.x>1
	check *= saturate(sign(i.uv.y)); //i.uv.y<0
	check *= saturate(-sign(i.uv.y - 1)); //i.uv.y>1
	return tex2D(_MainTex, i.uv) * check;*/
	return tex2D(_MainTex, i.uv);
	}

		ENDCG

		Subshader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog
		{
			Mode off
		}

			CGPROGRAM
#pragma fragmentoption ARB_precision_hint_fastest 
#pragma vertex vert
#pragma fragment frag
			ENDCG
		}
	}

	Fallback off
}
