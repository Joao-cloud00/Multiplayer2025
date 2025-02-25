using System.Collections.Generic;
[System.Serializable]

public class Baralho
{

    public int cod { get; set; }
    public string nome { get; set; }
    public bool ativo { get; set; }
    public Jogador jogador { get; set; }
    public List<string> cartas { get; set; }
}
