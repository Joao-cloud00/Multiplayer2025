[System.Serializable]
public class Carta
{
    public int cod { get; set; }
    public string nome { get; set; }
    public string imagem { get; set; }
    public int hp { get; set; }
    public int ataqueCusto { get; set; }
    public string ataqueNome { get; set; }
    public int ataqueDano { get; set; }
    public string ataqueEfeito { get; set; }
    public int recuo { get; set; }
    public string efeito { get; set; }
    public int estagio { get; set; }
    public Tipo tipo { get; set; }


}