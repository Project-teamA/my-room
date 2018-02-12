using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[AddComponentMenu("UI/Effects/SoftMaskScript")]
public class bl_GallerySoftMask : MonoBehaviour
{
    Material mat;

    Canvas cachedCanvas = null;
    Transform cachedCanvasTransform = null;
    readonly Vector3[] m_WorldCorners = new Vector3[4];
    readonly Vector3[] m_CanvasCorners = new Vector3[4];

    [Tooltip("コンテナとして使用される領域")]
    public RectTransform MaskArea;

    [Tooltip("ソフトアルファを行うために使用されるテクスチャ")]
    public Texture AlphaMask;

    [Tooltip("アルファ・ミニ範囲0〜1を適用するポイント")]
    [Range(0, 1)]
    public float CutOff = 0;

    [Tooltip("カットオフに基づいてハードブレンドを実装する")]
    public bool HardBlend = false;

    [Tooltip("マスクのアルファ値を反転する")]
    public bool FlipAlphaMask = false;

    [Tooltip("異なるマスクスケーリング矩形が与えられ、この値が真である場合、マスク周辺の領域は切り捨てられません")]
    public bool DontClipMaskScalingRect = false;

    Vector2 maskOffset = Vector2.zero;
    Vector2 maskScale = Vector2.one;

    // これを初期化に使用する
    void Start()
    {
        if (MaskArea == null)
        {
            MaskArea = GetComponent<RectTransform>();
        }

        var text = GetComponent<Text>();
        if (text != null)
        {
            mat = new Material(Shader.Find("Gallery/SoftMaskShader"));
            text.material = mat;
            cachedCanvas = text.canvas;
            cachedCanvasTransform = cachedCanvas.transform;
            // 何らかの理由で、親と無効にマスクコントロールを設定すると、マウスが相互作用しなくなります
            // テクスチャレイヤは表示されません、画像には不要です
            if (transform.parent.GetComponent<Mask>() == null)
                transform.parent.gameObject.AddComponent<Mask>();

            transform.parent.GetComponent<Mask>().enabled = false;
            return;
        }

        var graphic = GetComponent<Graphic>();
        if (graphic != null)
        {
            mat = new Material(Shader.Find("Gallery/SoftMaskShader"));
            graphic.material = mat;
            cachedCanvas = graphic.canvas;
            cachedCanvasTransform = cachedCanvas.transform;
        }
    }

    void Update()
    {
        if (cachedCanvas != null)
        {
            SetMask();
        }
    }

    void SetMask()
    {
        var worldRect = GetCanvasRect();
        var size = worldRect.size;
        maskScale.Set(1.0f / size.x, 1.0f / size.y);
        maskOffset = -worldRect.min;
        maskOffset.Scale(maskScale);

        mat.SetTextureOffset("_AlphaMask", maskOffset);
        mat.SetTextureScale("_AlphaMask", maskScale);
        mat.SetTexture("_AlphaMask", AlphaMask);

        mat.SetFloat("_HardBlend", HardBlend ? 1 : 0);
        mat.SetInt("_FlipAlphaMask", FlipAlphaMask ? 1 : 0);
        mat.SetInt("_NoOuterClip", DontClipMaskScalingRect ? 1 : 0);
        mat.SetFloat("_CutOff", CutOff);
    }

    public Rect GetCanvasRect()
    {
        if (cachedCanvas == null)
            return new Rect();

        MaskArea.GetWorldCorners(m_WorldCorners);
        for (int i = 0; i < 4; ++i)
            m_CanvasCorners[i] = cachedCanvasTransform.InverseTransformPoint(m_WorldCorners[i]);

        return new Rect(m_CanvasCorners[0].x, m_CanvasCorners[0].y, m_CanvasCorners[2].x - m_CanvasCorners[0].x, m_CanvasCorners[2].y - m_CanvasCorners[0].y);
    }
}