using System;
using System.Collections;
using UnityEngine;

public class CadastrarTipoAPIClient : APIClient
{
    public CadastrarTipoAPIClient(string baseUrl) : base(baseUrl) { }

    public IEnumerator CadastrarTipo(Tipo tipo, Action<string> onSuccess, Action<string> onFailure)
    {
        string jsonData = JsonUtility.ToJson(tipo);
        yield return SendRequest("/api/tipo/registrar", "POST", jsonData,
            onSuccess, onFailure);
    }
}
