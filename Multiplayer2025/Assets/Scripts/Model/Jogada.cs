[System.Serializable]

public class Jogada
{
    public int cod { get; set; }
    public Item item { get; set; }
    public Partida partida { get; set; }
    public int localCarta { get; set; }
    public int dano { get; set; }
    public int energia { get; set; }
}