sampler uImage0 : register(s0); // Contents of the screen.
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;

	float4 SimpleBloomFunction(float2 uv : TEXCOORD0) : COLOR0
	{
		float4 c = tex2D(uImage0, uv);
		return saturate((c - uIntensity) / (1 - uIntensity));
	}

	technique Technique1
	{
		pass SimpleBloomPass
		{
			PixelShader = compile ps_2_0 SimpleBloomFunction();
		}
	}
