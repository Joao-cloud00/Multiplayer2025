using System;
using System.Collections.Generic;

[Serializable]
public class Jogador
{
    public int cod; // Código único do jogador
    public string nome; // Nome do jogador
    public string usuario; // Nome de usuário
    public string senha; // Senha do jogador (com hash, já no servidor)

    // Lista de baralhos, cada baralho contém uma lista de cartas

    public List<List<string>> baralhos;
}
