using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        public const double TAXA_SAQUE = 3.5; 
        public string Titular { get; set; }
        public int NumeroConta { get; set; }
        public double Saldo { get; set; }

        public ContaBancaria(int numeroConta, string titular, double saldoInicial = 0.0)
        {
            Titular = titular;
            NumeroConta = numeroConta;
            Saldo = saldoInicial;

        }

        public override string ToString()
        {
            return $"Conta {NumeroConta}, Titular: {Titular}, Saldo: $ {Saldo.ToString("F2", CultureInfo.InvariantCulture)}";
        }

        public void Deposito(double quantia)
        {
            if (quantia <= 0)
            {
                Console.WriteLine("Valor de depósito deve ser positivo.");
                return;
            }
            Saldo += quantia;
        }
        
        public void Saque(double quantia)
        {
            if (quantia <= 0)
            {
                Console.WriteLine("Valor de saque deve ser positivo.");
                return;
            }

            Saldo -= (quantia + TAXA_SAQUE);
        }
    }
}
