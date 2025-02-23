using System;

[System.Serializable]

public class Partida
{
    public int cod { get; set; }
    public DateTime data { get; set; }
    public Baralho baralho01 { get; set; }
    public Baralho baralho02 { get; set; }
}