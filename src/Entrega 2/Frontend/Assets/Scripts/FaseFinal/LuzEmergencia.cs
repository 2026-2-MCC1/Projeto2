using UnityEngine;

public class LuzEmergencia : MonoBehaviour
{
    public float intensidadeMin = 1f;
    public float intensidadeMax = 4f;
    public float velocidade = 4f;

    private Light luz;
    private float semente;

    private void Awake()
    {
        luz = GetComponent<Light>();
        semente = Random.Range(0f, 100f); 
    }

    private void Update()
    {
        float ruido = Mathf.PerlinNoise(Time.time * velocidade, semente);
        luz.intensity = Mathf.Lerp(intensidadeMin, intensidadeMax, ruido);
    }
}