using UnityEngine;

public class SaidaFinal : MonoBehaviour
{
    public GameObject painelFim; 

    private bool terminou = false;

    private void OnTriggerEnter(Collider other)
    {
        if (terminou) return;
        if (!other.CompareTag("Player")) return;
        if (!DadosFaseFinal.passagemLiberada) return; 

        terminou = true;
        DialogueManager.Instance.TravarJogador(true); 
        painelFim.SetActive(true);
        DadosFaseFinal.Resetar();
        DadosBatalha.Resetar();
    }

    private void Update()
    {
        if (!terminou) return;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (Input.GetKeyDown(KeyCode.Escape)) Sair();
    }

    public void Sair()
    {
        Application.Quit();
    }
}