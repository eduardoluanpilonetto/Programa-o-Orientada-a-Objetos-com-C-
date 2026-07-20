using System;
// ===========================
// Exemplo Procedural
// ===========================
string modelo = "Fusca";
int velocidade = 0;

Console.WriteLine("=== Exemplo Procedural ===");
Console.WriteLine($"Modelo: {modelo}");
Console.WriteLine($"Velocidade inicial: {velocidade}");

void Acelerar()
{
    velocidade += 10;
}

Acelerar();
Acelerar();

Console.WriteLine($"Velocidade após acelerar duas vezes: {velocidade}");


// ===========================
// Exemplo Orientado a Objetos
// ===========================

Console.WriteLine("\n=== Exemplo Orientado a Objetos ===");

Veiculo possante = new Veiculo();
possante.modelo = "Meu Possante";
possante.velocidade = 10;
possante.acelerar();
possante.acelerar();
possante.frear();
possante.verVelocidade();

Veiculo opala = new Veiculo();
opala.modelo = "Opala V8 bebum";
opala.velocidade = 0;
opala.acelerar();
opala.acelerar();
opala.acelerar();
opala.acelerar();
opala.acelerar();
opala.acelerar();
opala.acelerar();
opala.verVelocidade();
opala.acelerar();
opala.acelerar();


// ===========================
// Classe
// ===========================

class Veiculo
{
    public int velocidade = 0;
    public string modelo = "";

    public void acelerar()
    {
        velocidade += 10;
    }

    public void frear()
    {
        velocidade -= 10;
    }

    public void verVelocidade()
    {
        Console.WriteLine($"Modelo: {modelo}");
        Console.WriteLine($"A velocidade atual do veículo é {velocidade} km/h");
    }
}