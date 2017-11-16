using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Projection Mapping/Curved Projection")]
public class CurvedProjection: MonoBehaviour
{
    [HideInInspector]
    public Shader shader;
    private string version = "1.2";


    
    //[HideInInspector]
    public float intensity = 1;
    //Main Projection Distortion Attributes
    [Header("Curved Distortion")]
    public float internalRadius = 0f;
    public float externalRadius = 0f;
    [Header("Linear Distortion")]
    public float expantionAngle = 0f;

    public bool verticalFlip = false;
    public bool horizontalFlip = false;

    public float width = 1f;
    public float height = 1f;
    public float offsetX = 0f;
    public float offsetY = 0f;
   
    


    //Generic Variables
    public Vector2 a = Vector2.zero;
    public Vector2 b = Vector2.zero;
    public Vector2 c = Vector2.zero;
    public Vector2 d = Vector2.zero;

    private Material _material;

    private void Awake()
    {
        _material = new Material(shader);

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Shader oldShader = shader;
        if (shader != oldShader)
        {
            _material = new Material(shader);
        }

        intensity = Mathf.Clamp01(intensity);
        float depth;
        depth = Screen.height / (2 * Mathf.Tan(GetComponent<Camera>().fieldOfView * Mathf.Deg2Rad * 0.5f));
        _material.SetFloat("_Depth", depth);
        _material.SetFloat("_Width", Screen.width);

        

        _material.SetFloat("_InternalRadius", internalRadius);
        _material.SetFloat("_ExternalRadius", externalRadius);
        _material.SetFloat("_ExpantionAngle", expantionAngle);
        if (verticalFlip)
            _material.SetInt("_VerticalFlip", 1);
        else
            _material.SetInt("_VerticalFlip", 0);

        if (horizontalFlip)
            _material.SetInt("_HorizontalFlip", 1);
        else
            _material.SetInt("_HorizontalFlip", 0);

        _material.SetFloat("_Intensity", intensity);

        _material.SetFloat("_W", width);
        _material.SetFloat("_H", height);
        _material.SetFloat("_OffsetX", offsetX);
        _material.SetFloat("_OffsetY", offsetY);
        //Screeb Variables X
        _material.SetFloat("_Ax",a.x);
        _material.SetFloat("_Bx", b.x);
        _material.SetFloat("_Cx", c.x);
        _material.SetFloat("_Dx", d.x);
        //Screen Variables Y
        _material.SetFloat("_Ay", a.y);
        _material.SetFloat("_By", b.y);
        _material.SetFloat("_Cy", c.y);
        _material.SetFloat("_Dy", d.y);

        Graphics.Blit(source, destination, _material);
    }

    public string getVersion()
    {
        return version;
    }

    

}
