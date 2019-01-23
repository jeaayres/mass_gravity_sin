Shader "Custom/DynamicRimShader" {
	Properties {
		_MainTint ("Diffuse Tint", Color) = (1,1,1,1)
		_MainTex	("Texture", 2D)	=	"white"	{}
		_DotProduct("Rim effect",	Range(0,1))	=	0.25

		_ScrollXSpeed ("X Scroll Speed", Range(-10,10)) =	2
		_ScrollYSpeed ("Y Scroll Speed", Range(-10,10)) =	2
	}
	SubShader {
		Tags
		{
				"Queue"	=	"Transparent"
				"IgnoreProjector"	=	"True"
				"RenderType"	=	"Transparent"
		}
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma	surface	surf Lambert alpha:fade nolightmap

		fixed4	_MainTint;
		sampler2D	_MainTex;
		float	_DotProduct;
		fixed	_ScrollXSpeed;
		fixed	_ScrollYSpeed;

		struct	Input
		{
				float2	uv_MainTex;
				float3	worldNormal;
				float3	viewDir;
		};


		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutput o) 
		{
			//	Create	a	separate	variable	to	store	our	UVs	
			//	before	we	pass	them	to	the	tex2D()	function
			fixed2	scrolledUV	=	IN.uv_MainTex;
							
			//	Create	variables	that	store	the	individual	x	and	y	
			//	components	for	the	UV's	scaled	by	time
			fixed	xScrollValue	=	_ScrollXSpeed	*	_Time;
			fixed	yScrollValue	=	_ScrollYSpeed	*	_Time;
							
			//	Apply	the	final	UV	offset
			scrolledUV	+=	fixed2(xScrollValue,	yScrollValue);
							
			//	Apply	textures	and	tint
			float4	c	=	tex2D(_MainTex,	scrolledUV);
			o.Albedo	=	c.rgb	*	_MainTint;
			float3 normal = normalize(IN.worldNormal);
			float3 dir = normalize(IN.viewDir);
			float	border	=	 1 - (abs(dot(dir, normal)));
			float	alpha	=	(border	* border *	(1	-	_DotProduct)	+	_DotProduct);
			//float   alpha   = border * border *  _DotProduct;
			o.Alpha	=	c.a	*	alpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
