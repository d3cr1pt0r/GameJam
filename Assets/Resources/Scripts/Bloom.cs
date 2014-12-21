using UnityEngine;
using System.Collections;

public class Bloom : MonoBehaviour
{
	public float Sensitivity = 0.8f;
	public float Intensity = 1.0f;
	public Color Color = new Color(1, 1, 1, 1);
	public Shader BloomShader;
	
	private Material bloomMaterial;
	private RenderTexture rt1;
	private RenderTexture rt2;

	void Start ()
	{
		bloomMaterial = new Material (BloomShader);
		rt1 = RenderTexture.GetTemporary (Screen.width, Screen.height, 24, RenderTextureFormat.ARGBFloat);
		rt2 = RenderTexture.GetTemporary (Screen.width, Screen.height, 24, RenderTextureFormat.ARGBFloat);
	}

	void Update ()
	{
		bloomMaterial.SetFloat ("_Sensitivity", Sensitivity);
		bloomMaterial.SetFloat ("_Intensity", Intensity);
		bloomMaterial.SetColor ("_Color", Color);
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit (source, rt1, bloomMaterial, 0);
		Graphics.Blit (rt1, rt2, bloomMaterial, 1);
		Graphics.Blit (rt2, rt1, bloomMaterial, 2);
		Graphics.Blit (source, rt1, bloomMaterial, 3);
		Graphics.Blit (rt1, destination);
	}
}
