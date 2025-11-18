using System;

class DataNascimento
{
    public int Mes { get; set; }
    public int Ano { get; set; }
}

class Gado
{
    public int Codigo { get; set; }
    public double Leite { get; set; }
    public double Alimento { get; set; }
    public DataNascimento Nasc { get; set; }
    public char Abate { get; set; }

    public Gado()
    {
        Nasc = new DataNascimento();
        Abate = 'N';
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Quantas cabeças de gado deseja cadastrar? ");
        int n = int.Parse(Console.ReadLine());

        Gado[] gado = new Gado[n];

        LerBaseDados(gado);

        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("===== MENU =====");
            Console.WriteLine("1 - Quantidade total de leite por semana");
            Console.WriteLine("2 - Quantidade total de alimento por semana");
            Console.WriteLine("3 - Listar animais destinados ao abate");
            Console.WriteLine("4 - Salvar dados em arquivo");
            Console.WriteLine("5 - Carregar dados do arquivo");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");
            opcao = int.Parse(Console.ReadLine());

            Console.Clear();

            switch (opcao)
            {
                case 1:
                    Console.WriteLine($"Total de leite: {TotalLeite(gado)} litros/semana");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.WriteLine($"Total de alimento consumido: {TotalAlimento(gado)} kg/semana");
                    Console.ReadKey();
                    break;

                case 3:
                    ListarAbate(gado);
                    Console.ReadKey();
                    break;

                case 4:
                    SalvarArquivo(gado);
                    Console.WriteLine("Dados salvos com sucesso!");
                    Console.ReadKey();
                    break;

                case 5:
                    CarregarArquivo(gado);
                    Console.WriteLine("Dados carregados com sucesso!");
                    Console.ReadKey();
                    break;

                case 0:
                    Console.WriteLine("Saindo...");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }

    static void LerBaseDados(Gado[] gado)
    {
        for (int i = 0; i < gado.Length; i++)
        {
            gado[i] = new Gado();

            Console.WriteLine($"\nCadastro #{i + 1}");

            Console.Write("Código: ");
            gado[i].Codigo = int.Parse(Console.ReadLine());

            Console.Write("Leite produzido por semana (litros): ");
            gado[i].Leite = double.Parse(Console.ReadLine());

            Console.Write("Alimento ingerido por semana (kg): ");
            gado[i].Alimento = double.Parse(Console.ReadLine());

            Console.Write("Mês de nascimento: ");
            gado[i].Nasc.Mes = int.Parse(Console.ReadLine());

            Console.Write("Ano de nascimento: ");
            gado[i].Nasc.Ano = int.Parse(Console.ReadLine());

            gado[i].Abate = VerificarAbate(gado[i]);
        }
    }

    static char VerificarAbate(Gado g)
    {
        int anoAtual = DateTime.Now.Year;
        int idade = anoAtual - g.Nasc.Ano;

        if (idade > 5 || g.Leite < 40)
            return 'S';

        return 'N';
    }

    static double TotalLeite(Gado[] gado)
    {
        double total = 0;
        foreach (var g in gado)
            total += g.Leite;
        return total;
    }

    static double TotalAlimento(Gado[] gado)
    {
        double total = 0;
        foreach (var g in gado)
            total += g.Alimento;
        return total;
    }

    static void ListarAbate(Gado[] gado)
    {
        Console.WriteLine("Animais destinados ao abate:");

        bool encontrou = false;

        foreach (var g in gado)
        {
            if (g.Abate == 'S')
            {
                Console.WriteLine($"\nCódigo: {g.Codigo}");
                Console.WriteLine($"Leite: {g.Leite} L");
                Console.WriteLine($"Alimento: {g.Alimento} kg");
                Console.WriteLine($"Nascimento: {g.Nasc.Mes}/{g.Nasc.Ano}");
                encontrou = true;
            }
        }

        if (!encontrou)
            Console.WriteLine("Nenhum animal precisa ir para o abate.");
    }

    static void SalvarArquivo(Gado[] gado)
    {
        using (StreamWriter sw = new StreamWriter("dados.txt"))
        {
            foreach (var g in gado)
            {
                sw.WriteLine($"{g.Codigo};{g.Leite};{g.Alimento};{g.Nasc.Mes};{g.Nasc.Ano};{g.Abate}");
            }
        }
    }

    static void CarregarArquivo(Gado[] gado)
    {
        if (!File.Exists("dados.txt"))
        {
            Console.WriteLine("Arquivo não encontrado!");
            return;
        }

        string[] linhas = File.ReadAllLines("dados.txt");

        for (int i = 0; i < linhas.Length && i < gado.Length; i++)
        {
            string[] campos = linhas[i].Split(';');

            gado[i] = new Gado
            {
                Codigo = int.Parse(campos[0]),
                Leite = double.Parse(campos[1]),
                Alimento = double.Parse(campos[2]),
                Nasc = new DataNascimento
                {
                    Mes = int.Parse(campos[3]),
                    Ano = int.Parse(campos[4])
                },
                Abate = campos[5][0]
            };
        }
    }
}

