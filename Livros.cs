using System;

struct Livro
{
    public string Titulo;
    public string Autor;
    public int Ano;
    public string Prateleira;
}

class Livros
{
    static List<Livro> livros = new List<Livro>();

    static void Main()
    {
        int opcao;
        do
        {
            Console.WriteLine("\n--- Biblioteca ---");
            Console.WriteLine("1. Cadastrar livro");
            Console.WriteLine("2. Procurar livro por título");
            Console.WriteLine("3. Mostrar todos os livros");
            Console.WriteLine("4. Mostrar livros mais novos que um ano");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida!");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    CadastrarLivro();
                    PausarLimpar();
                    break;

                case 2:
                    ProcurarLivroPorTitulo();
                    PausarLimpar();
                    break;

                case 3:
                    MostrarTodosLivros();
                    PausarLimpar();
                    break;

                case 4:
                    MostrarLivrosMaisNovosQueAno();
                    PausarLimpar();
                    break;

                case 0:
                    Console.WriteLine("Saindo...");
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    PausarLimpar();
                    break;
            }

        } while (opcao != 0);
    }

    static void PausarLimpar()
    {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
    }

    static void CadastrarLivro()
    {
        Livro livro = new Livro();

        Console.Write("Título: ");
        livro.Titulo = Console.ReadLine();

        Console.Write("Autor: ");
        livro.Autor = Console.ReadLine();

        Console.Write("Ano: ");
        while (!int.TryParse(Console.ReadLine(), out livro.Ano))
        {
            Console.Write("Ano inválido. Digite novamente: ");
        }

        Console.Write("Prateleira: ");
        livro.Prateleira = Console.ReadLine();

        livros.Add(livro);
        Console.WriteLine("Livro cadastrado com sucesso!");
    }

    static void ProcurarLivroPorTitulo()
    {
        Console.Write("Digite o título do livro a procurar: ");
        string tituloBusca = Console.ReadLine();

        var encontrados = livros
            .Where(l => l.Titulo.Equals(tituloBusca, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (encontrados.Count > 0)
        {
            foreach (var livro in encontrados)
            {
                Console.WriteLine($"Livro: {livro.Titulo} | Prateleira: {livro.Prateleira}");
            }
        }
        else
        {
            Console.WriteLine("Livro não encontrado.");
        }
    }

    static void MostrarTodosLivros()
    {
        if (livros.Count == 0)
        {
            Console.WriteLine("Nenhum livro cadastrado.");
            return;
        }

        Console.WriteLine("\n--- Lista de Livros ---");
        foreach (var livro in livros)
        {
            Console.WriteLine(
                $"Título: {livro.Titulo} | Autor: {livro.Autor} | Ano: {livro.Ano} | Prateleira: {livro.Prateleira}"
            );
        }
    }

    static void MostrarLivrosMaisNovosQueAno()
    {
        Console.Write("Digite o ano: ");
        int ano;
        while (!int.TryParse(Console.ReadLine(), out ano))
        {
            Console.Write("Ano inválido. Digite novamente: ");
        }

        var maisNovos = livros.Where(l => l.Ano > ano).ToList();

        if (maisNovos.Count == 0)
        {
            Console.WriteLine("Nenhum livro encontrado mais novo que o ano informado.");
        }
        else
        {
            Console.WriteLine($"\n--- Livros mais novos que {ano} ---");
            foreach (var livro in maisNovos)
            {
                Console.WriteLine(
                    $"Título: {livro.Titulo} | Autor: {livro.Autor} | Ano: {livro.Ano} | Prateleira: {livro.Prateleira}"
                );
            }
        }
    }
}

