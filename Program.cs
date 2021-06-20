using System;

namespace projeto_1
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio(); 
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X"){
                switch (opcaoUsuario){
                    case "1": 
                        ListarSeries();
                        break;

                    case "2": 
                        InserirSerie();
                        break;

                    case "3":
                        AtualizarSerie(); 
                        break;
                    
                    case "4": 
                        ExcluirSerie();
                        break;

                    case "5":
                        VisualizarSerie();
                        break; 

                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Até Logo!");

        }

        public static void ListarSeries(){
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0){
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            foreach ( var i in lista){
                if (!i.retornaExcluido()){
                    Console.WriteLine("#ID {0}: - {1}", i.retornaId(), i.retornaTitulo());
                }
            }
            return; 
        }

        public static void InserirSerie(){
            Console.WriteLine("Inserir nova série");
           
            repositorio.Insere(infoSerie());
        }

        public static void AtualizarSerie(){
            Console.Write("Digite o id da série: ");
            int idSerie = int.Parse(Console.ReadLine());
            
            repositorio.Atualiza(idSerie, infoSerie(idSerie));

        }

        public static void ExcluirSerie(){
            Console.Write("informe o Id da série: ");
            int idSerie = int.Parse(Console.ReadLine());

            Console.WriteLine("Todos os dados da série serão excluidos, deseja continuar(s/n)?");

            if(Console.ReadLine().ToLower() == "s"){ 
                Console.WriteLine("Os dados da série foram apagados!");
                repositorio.Exclui(idSerie);
            }else {
                Console.WriteLine("Ação Cancelada!");
            }

        }

        public static void VisualizarSerie(){
            Console.Write("Informe o id da série: ");
            int idSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(idSerie);
            if (!serie.retornaExcluido()){
                Console.WriteLine(serie);
            }
        }

        private static string ObterOpcaoUsuario(){
            Console.WriteLine();
            Console.WriteLine("Informe a opção desejada");

            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();

            return opcaoUsuario;
        }

        public static Serie infoSerie(int idSerie = -1){
            foreach(int i in Enum.GetValues(typeof(Genero))){
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o Titulo da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano do início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie serieObj = new Serie(
                id: idSerie >= 0? idSerie : repositorio.ProximoId(),
                genero: (Genero)entradaGenero,
                titulo: entradaTitulo,
                ano: entradaAno,
                descricao: entradaDescricao
            );

            return serieObj;
        }
    }
}
