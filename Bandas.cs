using System;
namespace CadastroBandas
{
    class Bandas
    {
        static void addBanda(List<Banda> listaBandas)
        {
            Banda novaBanda = new Banda();
            Console.Write("Nome:");
            novaBanda.nome = Console.ReadLine();
            Console.Write("Genero:");
            novaBanda.genero = Console.ReadLine();
            Console.Write("Integrantes:");
            novaBanda.integrantes = int.Parse(Console.ReadLine());
            Console.Write("Ranking:");
            novaBanda.ranking = int.Parse(Console.ReadLine());
            listaBandas.Add(novaBanda);
            Console.WriteLine("--------");

        }

        static void mostrarBandas(List<Banda> listaBandas)
        {
            int posicao = 1;
            for (int i = 0; i < listaBandas.Count; i++)
            {
                Console.WriteLine($"*** Banda {posicao}***");
                Console.WriteLine($"{listaBandas[i].nome} - {listaBandas[i].genero} - {listaBandas[i].integrantes} - {listaBandas[i].ranking}");
                posicao++;
            }

        }
        
        static bool buscarBanda(List<Banda> listaBandas, string nomeBusca)
        {
            foreach (Banda b in listaBandas)
            {
                if (b.nome.ToUpper().Contains(nomeBusca.ToUpper()))
                {
                    Console.WriteLine("*** Dados da banda ***");
                    Console.WriteLine($"Nome: {b.nome}");
                    Console.WriteLine($"Genero: {b.genero}");
                    Console.WriteLine($"Integrantes: {b.integrantes}");
                    Console.WriteLine($"Ranking: {b.ranking}");
                    return true;
                }

            }// fim foreach
            return false;
        }
        static int buscarIndiceBanda(List<Banda> listaBandas, string nomeBusca)
        {
            for (int i = 0; i < listaBandas.Count; i++)
            {
                if (listaBandas[i].nome.ToUpper().Equals(nomeBusca.ToUpper()))
                {
                    return i;
                }

            }// fim foreach
            return -1;
        }

        static bool atualizarBanda(List<Banda> listaBandas,
                                    string nomeBanda)
        {
            int i = buscarIndiceBanda(listaBandas, nomeBanda);
            if (i == -1)
                return false;
            Console.WriteLine("*** Dados da banda ***");
            Console.WriteLine($"{listaBandas[i].nome} - {listaBandas[i].genero} - {listaBandas[i].integrantes} - {listaBandas[i].ranking}");
            Console.WriteLine("Novos dados");
            Console.Write("Nome:");
            listaBandas[i].nome = Console.ReadLine();
            Console.Write("Genero:");
            listaBandas[i].genero = Console.ReadLine();
            Console.Write("Integrantes:");
            listaBandas[i].integrantes = int.Parse(Console.ReadLine());
            Console.Write("Ranking:");
            listaBandas[i].ranking = int.Parse(Console.ReadLine());
            return true;
        }
        static bool removerBanda(List<Banda> listaBandas,
                                   string nomeBanda)
        {
            int i = buscarIndiceBanda(listaBandas, nomeBanda);
            if (i == -1)
                return false;
            Console.Write($"Tem certeza que deseja remover a banda {nomeBanda} [1-Sim | 0- Não]? ");
            int resposta = int.Parse(Console.ReadLine());
            if (resposta == 1){
                listaBandas.RemoveAt(i);
                Console.WriteLine("Banda removida com sucesso!");
            }
            return true;   
        }

        static int menu()
        {
            int opcao;
            Console.WriteLine("*** Sistema de Cadastro de Bandas 4U***");
            Console.WriteLine("1- Adicionar Banda");
            Console.WriteLine("2- Mostrar Bandas");
            Console.WriteLine("3- Buscar Banda");
            Console.WriteLine("4- Atualizar Banda");
            Console.WriteLine("5- Remover Banda");
            Console.WriteLine("0- Sair do Sistema");
            opcao = int.Parse(Console.ReadLine());
            return opcao;
        }

        static void salvarDados(List<Banda> listaBandas, string nomeArquivo)
        {

            using (StreamWriter writer = new StreamWriter(nomeArquivo))
            {
                foreach (Banda b in listaBandas)
                {
                    writer.WriteLine($"{b.nome},{b.genero},{b.integrantes},{b.ranking}");
                }
            }
            Console.WriteLine("Dados salvos com sucesso!");


        }

        static void carregarDados(List<Banda> listaBandas, string nomeArquivo)
        {
            if (File.Exists(nomeArquivo))
            {
                string[] linhas = File.ReadAllLines(nomeArquivo);
                foreach (string linha in linhas)
                {
                    string[] campos = linha.Split(',');
                    Banda novaBanda = new Banda();
                    novaBanda.nome = campos[0];
                    novaBanda.genero = campos[1];
                    novaBanda.integrantes = int.Parse(campos[2]);
                    novaBanda.ranking = int.Parse(campos[3]);
                    listaBandas.Add(novaBanda);
                }
                Console.WriteLine("Dados carregados com sucesso!");
            }
            else
                Console.WriteLine("Arquivo não encontrado :(");

        }

        
        static void Main()
        {
            List<Banda> listaBandas = new List<Banda>();
            int opcao = 0;
            carregarDados(listaBandas, "bandas.txt");
            do
            {
                opcao = menu();
                switch (opcao)
                {
                    case 1:
                        addBanda(listaBandas);
                        break;
                    case 2:
                        mostrarBandas(listaBandas);
                        break;
                    case 3: Console.Write("Nome da banda:");
                        string nomeBanda = Console.ReadLine();
                        bool encontrado = buscarBanda(listaBandas, nomeBanda);
                        if (!encontrado)
                            Console.WriteLine("Banda não encontrada :(" );
                        break;
                    case 4: Console.Write("Nome da banda para atualizar dados:");
                         nomeBanda = Console.ReadLine();
                         encontrado = atualizarBanda(listaBandas, nomeBanda);
                        if (!encontrado)
                            Console.WriteLine("Banda não encontrada :(" );
                        break;
                    case 5: Console.Write("Nome da banda para remover:");
                         nomeBanda = Console.ReadLine();
                         encontrado = removerBanda(listaBandas, nomeBanda);
                        if (!encontrado)
                            Console.WriteLine("Banda não encontrada :(" );
                        break;
                    case 0:
                        salvarDados(listaBandas, "bandas.txt");
                        Console.WriteLine("Até mais ;)");
                        break;
                }
                Console.ReadKey();// pausa
                Console.Clear(); // limpa a tela
            } while (opcao != 0);

        }

    }

}
