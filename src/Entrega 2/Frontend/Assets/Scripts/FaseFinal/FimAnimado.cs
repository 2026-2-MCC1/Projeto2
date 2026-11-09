using UnityEngine;

public class FimAnimado : MonoBehaviour
{
    public CanvasGroup grupo;     
    public Transform titulo;     
    public float duracao = 1.2f;  

    private float tempo = 0f;

    private void OnEnable()
    {
        tempo = 0f;
        if (grupo != null) grupo.alpha = 0f;
        if (titulo != null) titulo.localScale = Vector3.one * 0.5f;
    }

    private void Update()
    {
        tempo += Time.unscaledDeltaTime;
        float t = Mathf.Clamp01(tempo / duracao);
        float suave = Mathf.SmoothStep(0f, 1f, t);

        if (grupo != null) grupo.alpha = suave;
        if (titulo != null) titulo.localScale = Vector3.one * Mathf.Lerp(0.5f, 1f, suave);
    }
}