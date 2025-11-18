using System;

class Emprestimo
{
    public string Data { get; set; }
    public string NomePessoa { get; set; }
    public char Emprestado { get; set; } // S ou N

    public Emprestimo()
    {
        Emprestado = 'N';
        Data = "";
        NomePessoa = "";
    }
}

class Jogo
{
    public string Titulo { get; set; }
    public string Console { get; set; }
    public int Ano { get; set; }
    public int Ranking { get; set; }
    public Emprestimo InfoEmprestimo { get; set; }

    public Jogo()
    {
        InfoEmprestimo = new Emprestimo();
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Quantos jogos deseja cadastrar? ");
        int n = int.Parse(Console.ReadLine());

        Jogo[] jogos = new Jogo[n];

        for (int i = 0; i < n; i++)
        {
            jogos[i] = new Jogo();

            Console.WriteLine($"\nCadastro do jogo #{i + 1}");

            Console.Write("Título: ");
            jogos[i].Titulo = Console.ReadLine();

            Console.Write("Console: ");
            jogos[i].Console = Console.ReadLine();

            Console.Write("Ano: ");
            jogos[i].Ano = int.Parse(Console.ReadLine());

            Console.Write("Ranking (0 a 10): ");
            jogos[i].Ranking = int.Parse(Console.ReadLine());
        }

        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("===== MENU =====");
            Console.WriteLine("1 - Procurar jogo por título");
            Console.WriteLine("2 - Listar jogos por console");
            Console.WriteLine("3 - Emprestar jogo");
            Console.WriteLine("4 - Devolver jogo");
            Console.WriteLine("5 - Mostrar jogos emprestados");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");
            opcao = int.Parse(Console.ReadLine());
            Console.Clear();

            switch (opcao)
            {
                case 1:
                    ProcurarPorTitulo(jogos);
                    Console.ReadKey();
                    break;

                case 2:
                    ListarPorConsole(jogos);
                    Console.ReadKey();
                    break;

                case 3:
                    EmprestarJogo(jogos);
                    Console.ReadKey();
                    break;

                case 4:
                    DevolverJogo(jogos);
                    Console.ReadKey();
                    break;

                case 5:
                    MostrarEmprestados(jogos);
                    Console.ReadKey();
                    break;

                case 0:
                    Console.WriteLine("Saindo...");
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }

        } while (opcao != 0);
    }

    static void ProcurarPorTitulo(Jogo[] jogos)
    {
        Console.Write("Digite o título do jogo: ");
        string titulo = Console.ReadLine().ToLower();

        bool achou = false;

        foreach (var jogo in jogos)
        {
            if (jogo.Titulo.ToLower().Contains(titulo))
            {
                Console.WriteLine($"\nTítulo: {jogo.Titulo}");
                Console.WriteLine($"Console: {jogo.Console}");
                Console.WriteLine($"Ano: {jogo.Ano}");
                Console.WriteLine($"Ranking: {jogo.Ranking}");
                Console.WriteLine($"Emprestado: {jogo.InfoEmprestimo.Emprestado}");
                achou = true;
            }
        }

        if (!achou)
            Console.WriteLine("\nJogo não encontrado.");
    }

    static void ListarPorConsole(Jogo[] jogos)
    {
        Console.Write("Digite o console: ");
        string c = Console.ReadLine().ToLower();

        bool achou = false;

        foreach (var jogo in jogos)
        {
            if (jogo.Console.ToLower() == c)
            {
                Console.WriteLine($"\nTítulo: {jogo.Titulo} ({jogo.Ano}) Ranking: {jogo.Ranking}");
                achou = true;
            }
        }

        if (!achou)
            Console.WriteLine("\nNenhum jogo encontrado para esse console.");
    }

    static void EmprestarJogo(Jogo[] jogos)
    {
        Console.Write("Título do jogo a emprestar: ");
        string titulo = Console.ReadLine().ToLower();

        foreach (var jogo in jogos)
        {
            if (jogo.Titulo.ToLower() == titulo)
            {
                if (jogo.InfoEmprestimo.Emprestado == 'S')
                {
                    Console.WriteLine("Esse jogo já está emprestado.");
                    return;
                }

                Console.Write("Nome da pessoa: ");
                jogo.InfoEmprestimo.NomePessoa = Console.ReadLine();

                jogo.InfoEmprestimo.Data = DateTime.Now.ToString("dd/MM/yyyy");
                jogo.InfoEmprestimo.Emprestado = 'S';

                Console.WriteLine("Jogo emprestado com sucesso!");
                return;
            }
        }

        Console.WriteLine("Jogo não encontrado.");
    }

    static void DevolverJogo(Jogo[] jogos)
    {
        Console.Write("Título do jogo a devolver: ");
        string titulo = Console.ReadLine().ToLower();

        foreach (var jogo in jogos)
        {
            if (jogo.Titulo.ToLower() == titulo)
            {
                if (jogo.InfoEmprestimo.Emprestado == 'N')
                {
                    Console.WriteLine("Esse jogo já está disponível.");
                    return;
                }

                jogo.InfoEmprestimo.Emprestado = 'N';
                jogo.InfoEmprestimo.NomePessoa = "";
                jogo.InfoEmprestimo.Data = "";

                Console.WriteLine("Jogo devolvido com sucesso!");
                return;
            }
        }

        Console.WriteLine("Jogo não encontrado.");
    }

    static void MostrarEmprestados(Jogo[] jogos)
    {
        bool achou = false;

        foreach (var jogo in jogos)
        {
            if (jogo.InfoEmprestimo.Emprestado == 'S')
            {
                Console.WriteLine($"\n{jogo.Titulo} ({jogo.Console})");
                Console.WriteLine($"Emprestado para: {jogo.InfoEmprestimo.NomePessoa}");
                Console.WriteLine($"Data: {jogo.InfoEmprestimo.Data}");
                achou = true;
            }
        }

        if (!achou)
            Console.WriteLine("Nenhum jogo emprestado no momento.");
    }
}
