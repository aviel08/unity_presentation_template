using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Vignette : MonoBehaviour {
	#region Variables
	public float VignettePower = 1.25f;
	public float ChromaticAbberation = 3.0f;
	public float BlurDistance = 1.0f;
	public float BlurSpread = 1.0f;
	public int Downsampling = 1;
	public Shader VignetteShader;
	public Shader SeparableBlurShader;
	public Shader ChromaticAbberationShader;
	private Material VignetteMaterial;
	private Material SeparableBlurMaterial;
	private Material ChromaticAbberationMaterial;
	#endregion
	
	#region Properties
	Material VigMaterial
	{
		get
		{
			if(VignetteMaterial == null)
			{
				VignetteMaterial = new Material(VignetteShader);
				VignetteMaterial.hideFlags = HideFlags.HideAndDontSave;	
			}
			return VignetteMaterial;
		}
	}
	Material BlurMaterial
	{
		get
		{
			if(SeparableBlurMaterial == null)
			{
				SeparableBlurMaterial = new Material(SeparableBlurShader);
				SeparableBlurMaterial.hideFlags = HideFlags.HideAndDontSave;	
			}
			return SeparableBlurMaterial;
		}
	}
	Material ChromaMaterial
	{
		get
		{
			if(ChromaticAbberationMaterial == null)
			{
				ChromaticAbberationMaterial = new Material(ChromaticAbberationShader);
				ChromaticAbberationMaterial.hideFlags = HideFlags.HideAndDontSave;	
			}
			return ChromaticAbberationMaterial;
		}
	}
	#endregion
	// Use this for initialization
	void Start () 
	{
		if(!SystemInfo.supportsImageEffects)
		{
			enabled = false;
			return;
		}
	}
	
	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{
		
		//Setup
		float WidthOverHeight = (sourceTexture.width) / (sourceTexture.height);
		float OneOverBase = 1.0f / 512.0f;
			
		RenderTexture ScreenColor = null;
		RenderTexture HalfRez = null;
		RenderTexture HalfRez2 = null;
		
	 if(VignetteShader && ChromaticAbberationShader && SeparableBlurShader != null)
	{
		//OptimizationCheck
		if (Mathf.Abs(BlurDistance) > 0.0f || Mathf.Abs(VignettePower) > 0.0f);
		{
			
			ScreenColor = RenderTexture.GetTemporary (sourceTexture.width, sourceTexture.height, 0, sourceTexture.format);
			
			if(BlurDistance > 0.0f)
			{
				
				HalfRez = RenderTexture.GetTemporary (sourceTexture.width / Downsampling, sourceTexture.height / Downsampling, 0, sourceTexture.format);
				HalfRez2 = RenderTexture.GetTemporary (sourceTexture.width / Downsampling, sourceTexture.height / Downsampling, 0, sourceTexture.format);
				//Chromatic Abberation
				Graphics.Blit (sourceTexture, HalfRez, ChromaMaterial);
				//Seperable Blur (Unitys approach)
				for(int i = 0; i < 2; i++) 
				{
					BlurMaterial.SetVector ("offsets", new Vector4 (0.0f, BlurSpread * OneOverBase, 0.0f, 0.0f));
					Graphics.Blit (HalfRez, HalfRez2, BlurMaterial); 
					BlurMaterial.SetVector ("offsets", new Vector4 (BlurSpread * OneOverBase / WidthOverHeight, 0.0f, 0.0f, 0.0f));
					Graphics.Blit (HalfRez2, HalfRez, BlurMaterial);
				}			
			}
				
			VigMaterial.SetFloat("_VignettePower", VignettePower);
			VigMaterial.SetFloat("_Blur", BlurDistance);
			VigMaterial.SetTexture("_VignetteTex", HalfRez);
				
			Graphics.Blit(sourceTexture, destTexture, VigMaterial);
			ScreenColor.wrapMode = TextureWrapMode.Clamp;
			//if(ScreenColor) RenderTexture.ReleaseTemporary(ScreenColor);
			//if(HalfRez) RenderTexture.ReleaseTemporary(HalfRez);
			//if(HalfRez2) RenderTexture.ReleaseTemporary(HalfRez2);
		}
			
			ChromaMaterial.SetFloat("_AberrationOffset", ChromaticAbberation);
	 }
	
		if(ScreenColor) RenderTexture.ReleaseTemporary(ScreenColor);
		if(HalfRez) RenderTexture.ReleaseTemporary(HalfRez);
		if(HalfRez2) RenderTexture.ReleaseTemporary(HalfRez2);
	}
	
	// Update is called once per frame
	void Update () 
	{
		VignettePower = Mathf.Clamp(VignettePower, 0.0f, 5.0f);
		Downsampling = Mathf.Clamp(Downsampling, 1, 6);
	}
	
	void OnDisable ()
	{
		if(VignetteMaterial && SeparableBlurMaterial && ChromaticAbberationMaterial)
		{
			DestroyImmediate(VignetteMaterial);
			DestroyImmediate(SeparableBlurMaterial);
			DestroyImmediate(ChromaticAbberationMaterial);
		}
		
	}
	
	
}