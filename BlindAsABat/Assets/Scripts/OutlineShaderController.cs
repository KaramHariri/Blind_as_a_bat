using UnityEngine;

[ExecuteInEditMode]
public class OutlineShaderController : MonoBehaviour
{
    public Color color = Color.white;

    private SpriteRenderer spriteRenderer;

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateOutline(true);
    }

    void OnDisable()
    {
        UpdateOutline(false);
    }

    void Update()
    {
        UpdateOutline(true);
    }

    void UpdateOutline(bool outline)
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat("_Outline", outline ? 1f : 0);
        materialPropertyBlock.SetColor("_OutlineColor", color);
        spriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}