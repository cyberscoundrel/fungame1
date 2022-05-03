void MainLight_float(float3 WorldPos, out float3 Direction, out float3 Color, out float DistAttenuation, out float ShadowAttenuation)
{
#ifdef SHADERGRAPH_PREVIEW
	Direction = normalize(float3(0.5f, 0.5f, 0.25f));
	Color = float3(1.0f, 1.0f, 1.0f);
	DistAttenuation = 1.0f;
	ShadowAttenuation = 1.0f;
#else
#if SHADOWS_SCREEN
	float4 clipPosition = TransformWorldToHClip(WorldPos);
	float4 shadowCoordinates = ComputeScreenPos(clipPosition);
#else
	float4 shadowCoordinates = TransformWorldToShadowCoord(WorldPos);
#endif
	Light mainLight = GetMainLight(shadowCoordinates);
	Direction = mainLight.direction;
	Color = mainLight.color;
	DistAttenuation = mainLight.distanceAttenuation;
	ShadowAttenuation = mainLight.shadowAttenuation;
#endif
}


void MainLight_half(float3 WorldPos, out float3 Direction, out float3 Color, out float DistAttenuation, out float ShadowAttenuation)
{
#ifdef SHADERGRAPH_PREVIEW
	Direction = normalize(float3(0.5f, 0.5f, 0.25f));
	Color = float3(1.0f, 1.0f, 1.0f);
	DistAttenuation = 1.0f;
	ShadowAttenuation = 1.0f;
#else
	float4 shadowCoordinates = TransformWorldToShadowCoord(WorldPos);
	Light mainLight = GetMainLight(shadowCoordinates);
	Direction = mainLight.direction;
	Color = mainLight.color;
	DistAttenuation = mainLight.distanceAttenuation;
	ShadowAttenuation = mainLight.shadowAttenuation;
#endif
}

void AdditionalLight_float(float3 WorldPos, int Index, out float3 Direction, out float3 Color, out float DistAttenuation, out float ShadowAttenuation)
{
	Direction = normalize(float3(0.5f, 0.5f, 0.25f));
	Color = float3(1.0f, 1.0f, 1.0f);
	DistAttenuation = 1.0f;
	ShadowAttenuation = 1.0f;
#ifndef SHADERGRAPH_PREVIEW
	int pixelLightCount = GetAdditionalLightsCount();
	if (Index < pixelLightCount)
	{
		Light light = GetAdditionalLight(Index, WorldPos);
		Direction = light.direction;
		Color = light.color;
		DistAttenuation = light.distanceAttenuation;
		ShadowAttenuation = light.shadowAttenuation;
	}
#endif
}