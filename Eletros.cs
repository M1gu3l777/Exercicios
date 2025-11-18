using System;
using System.Collections.Generic;
using System.IO;

namespace CadastroEletro
{
    struct Eletro
    {
        public string nome;
        public double potencia;               // kW
        public double tempoMedioUsoDiario;   // horas por dia
    }

    class Eletros
    {
        static void AddEletro(List<Eletro> listaEletros)
        {
            Eletro novoEletro = new Eletro();

            Console.Write("Nome: ");
            novoEletro.nome = Console.ReadLine();

            Console.Write("Potência (kW): ");
            while (!double.TryParse(Console.ReadLine(), out novoEletro.potencia))
                Console.Write("Valor inválido! Digite novamente: ");

            Console.Write("Tempo médio de uso diário (h): ");
            while (!double.TryParse(Console.ReadLine(), out novoEletro.tempoMedioUsoDiario))
                Console.Write("Valor inválido! Digite novamente: ");

            listaEletros.Add(novoEletro);
            Console.WriteLine("Eletrodoméstico cadastrado!");
        }

        static void MostrarEletros(List<Eletro> listaEletros)
        {
            if (listaEletros.Count == 0)
            {
                Console.WriteLine("Nenhum eletrodoméstico cadastrado.");
                return;
            }

            foreach (Eletro e in listaEletros)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine($"Nome: {e.nome}");
                Console.WriteLine($"Potência: {e.potencia} kW");
                Console.WriteLine($"Tempo Médio de Uso: {e.tempoMedioUsoDiario} h");
            }
        }

        static bool BuscarEletro(List<Eletro> listaEletros, string nomeBusca)
        {
            bool encontrado = false;

            foreach (Eletro e in listaEletros)
            {
                if (e.nome.ToUpper().Contains(nomeBusca.ToUpper()))
                {
                    Console.WriteLine("*** Eletrodoméstico Encontrado ***");
                    Console.WriteLine($"Nome: {e.nome}");
                    Console.WriteLine($"Potência: {e.potencia} kW");
                    Console.WriteLine($"Tempo Médio de Uso: {e.tempoMedioUsoDiario} h");
                    encontrado = true;
                }
            }
            return encontrado;
        }

        static bool BuscarPotencia(List<Eletro> listaEletros, double potenciaBusca)
        {
            bool encontrado = false;

            foreach (Eletro e in listaEletros)
            {
                if (e.potencia >= potenciaBusca)
                {
                    Console.WriteLine("*** Eletro com Potência Encontrada ***");
                    Console.WriteLine($"Nome: {e.nome}");
                    Console.WriteLine($"Potência: {e.potencia} kW");
                    Console.WriteLine($"Uso Diário: {e.tempoMedioUsoDiario} h");
                    encontrado = true;
                }
            }

            return encontrado;
        }

        static double ConsumoTotalKwDia(List<Eletro> listaEletros)
        {
            double total = 0;

            foreach (Eletro e in listaEletros)
                total += e.potencia * e.tempoMedioUsoDiario;

            return total;
        }

        static double CustoMensalTotal(List<Eletro> listaEletros, double valorKw)
        {
            double total = 0;

            foreach (Eletro e in listaEletros)
            {
                double consumoDiario = e.potencia * e.tempoMedioUsoDiario;
                total += consumoDiario * valorKw * 30;
            }

            return total;
        }

        static void SalvarEletros(List<Eletro> listaEletros)
        {
            using (StreamWriter sw = new StreamWriter("eletros.txt"))
            {
                foreach (Eletro e in listaEletros)
                    sw.WriteLine($"{e.nome};{e.potencia};{e.tempoMedioUsoDiario}");
            }
        }

        static void CarregarEletros(List<Eletro> listaEletros)
        {
            if (File.Exists("eletros.txt"))
            {
                using (StreamReader sr = new StreamReader("eletros.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] dados = line.Split(';');
                        Eletro e = new Eletro();
                        e.nome = dados[0];
                        e.potencia = double.Parse(dados[1]);
                        e.tempoMedioUsoDiario = double.Parse(dados[2]);
                        listaEletros.Add(e);
                    }
                }
            }
        }

        static int Menu()
        {
            Console.WriteLine("\n---- MENU ----");
            Console.WriteLine("1 - Adicionar Eletrodoméstico");
            Console.WriteLine("2 - Mostrar Eletrodomésticos");
            Console.WriteLine("3 - Buscar por nome");
            Console.WriteLine("4 - Buscar por potência mínima");
            Console.WriteLine("5 - Calcular consumo diário/mensal");
            Console.WriteLine("6 - Calcular custo mensal total");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            int opcao;
            while (!int.TryParse(Console.ReadLine(), out opcao))
                Console.Write("Opção inválida! Digite novamente: ");

            return opcao;
        }

        static void Main()
        {
            List<Eletro> lista = new List<Eletro>();
            CarregarEletros(lista);

            int opcao;

            do
            {
                opcao = Menu();
                Console.Clear();

                switch (opcao)
                {
                    case 1:
                        AddEletro(lista);
                        break;

                    case 2:
                        MostrarEletros(lista);
                        break;

                    case 3:
                        Console.Write("Nome para buscar: ");
                        if (!BuscarEletro(lista, Console.ReadLine()))
                            Console.WriteLine("Nenhum eletrodoméstico encontrado.");
                        break;

                    case 4:
                        Console.Write("Potência mínima (kW): ");
                        double potencia = double.Parse(Console.ReadLine());
                        if (!BuscarPotencia(lista, potencia))
                            Console.WriteLine("Nenhum aparelho com essa potência.");
                        break;

                    case 5:
                        Console.Write("Valor do kW/h em R$: ");
                        double valorKw = double.Parse(Console.ReadLine());

                        double consumoDia = ConsumoTotalKwDia(lista);
                        double consumoMes = consumoDia * 30;

                        Console.WriteLine($"Consumo diário total: {consumoDia:F2} kWh");
                        Console.WriteLine($"Consumo mensal total: {consumoMes:F2} kWh");
                        Console.WriteLine($"Custo diário:   R$ {consumoDia * valorKw:F2}");
                        Console.WriteLine($"Custo mensal:   R$ {consumoMes * valorKw:F2}");
                        break;

                    case 6:
                        Console.Write("Valor do kW/h em R$: ");
                        double valor = double.Parse(Console.ReadLine());
                        Console.WriteLine($"Custo mensal total: R$ {CustoMensalTotal(lista, valor):F2}");
                        break;

                    case 0:
                        Console.WriteLine("Salvando... Saindo!");
                        SalvarEletros(lista);
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (opcao != 0);
        }
    }
}
