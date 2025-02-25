using System;
using System.Collections.Generic;

[Serializable]
public class Jogador
{
    public int cod; // C�digo �nico do jogador
    public string nome; // Nome do jogador
    public string usuario; // Nome de usu�rio
    public string senha; // Senha do jogador (com hash, j� no servidor)

    // Lista de baralhos, cada baralho cont�m uma lista de cartas

    public List<List<string>> baralhos;
}
