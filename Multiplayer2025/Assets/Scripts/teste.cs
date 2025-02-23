using UnityEngine;

public class teste : MonoBehaviour
{
    private void Start()
    {
        // Criando uma nova instância do CadastrarTipoAPIClient
        var cadastrarTipoClient = new CadastrarTipoAPIClient("http://localhost:5213");

        // Criando o tipo que será registrado
        Tipo novoTipo = new Tipo { nome = "Fogo" };

        string json = JsonUtility.ToJson(novoTipo);
        Debug.Log("Jogador JSON: " + json);


        // Iniciando a requisição para registrar o tipo
        StartCoroutine(cadastrarTipoClient.CadastrarTipo(novoTipo,
            sucesso => Debug.Log("Tipo registrado com sucesso: " + sucesso),
            erro => Debug.LogError("Erro ao registrar tipo: " + erro)
        ));
    }
}
