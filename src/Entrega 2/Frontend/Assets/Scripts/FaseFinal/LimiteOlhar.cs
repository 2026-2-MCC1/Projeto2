using UnityEngine;

public class LimiteOlhar : MonoBehaviour
{
    public Camera cam;                       
    public float distanciaTeste = 25f;       
    public float velocidadeCorrecao = 150f;  
    public LayerMask camadasParede = ~0;    

    private void LateUpdate()
    {
        if (cam == null) return;

        float passo = velocidadeCorrecao * Time.deltaTime;

        if (VeVazio(new Vector3(0f, 0.5f, 0f)))
            transform.Rotate(0f, -passo, 0f);

        if (VeVazio(new Vector3(1f, 0.5f, 0f)))
            transform.Rotate(0f, passo, 0f);
    }

    private bool VeVazio(Vector3 pontoTela)
    {
        Ray raio = cam.ViewportPointToRay(pontoTela);
        RaycastHit[] hits = Physics.RaycastAll(raio, distanciaTeste, camadasParede, QueryTriggerInteraction.Ignore);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.transform.IsChildOf(transform)) continue;
            return false;
        }
        return true;
    }
}
