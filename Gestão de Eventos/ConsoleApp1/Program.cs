using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GestaoEventos
{
    class Program
    {
        static void Main(string[] args)
        {
            EventoManager eventoManager = new EventoManager();

            while (true)
            {
                Console.WriteLine("=== Sistema de Gestão de Eventos ===");
                Console.WriteLine("1. Cadastrar evento");
                Console.WriteLine("2. Listar eventos por período");
                Console.WriteLine("3. Pesquisar evento por data");
                Console.WriteLine("4. Editar informações de um evento");
                Console.WriteLine("5. Pesquisar contato cadastrado");
                Console.WriteLine("6. Excluir evento pelo ID");
                Console.WriteLine("7. Exportar dados de um evento (formato .txt)");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");
                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        try { 
                        eventoManager.CadastrarEvento();
                        }
                        catch {
                            Console.WriteLine("Algo deu errado!!!");
                        }
                        
                        break;
                    case 2:
                        eventoManager.ListarEventosPorPeriodo();
                        break;
                    case 3:
                        eventoManager.PesquisarEventoPorData();
                        break;
                    case 4:
                        eventoManager.EditarEvento();
                        break;
                    case 5:
                        eventoManager.PesquisarContatoCadastrado();
                        break;
                    case 6:
                        eventoManager.ExcluirEventoPorId();
                        break;
                    case 7:
                        eventoManager.ExportarDadosEvento();
                        break;
                    case 0:
                        Console.WriteLine("Encerrando o programa. Até mais!");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
    }

    class Evento
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string Descricao { get; set; }
        public int QuantidadePessoas { get; set; }
        public string PublicoAlvo { get; set; }
        public Contato ContatoResponsavel { get; set; }
    }

    class Contato
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }

    class EventoManager
    {
        private List<Evento> eventos = new List<Evento>();

        public void CadastrarEvento()
        {
            
            Console.WriteLine("=== Cadastro de Evento ===");

            Evento evento = new Evento();

            evento.Id = GerarIdUnico();

            Console.Write("Título: ");
            evento.Titulo = Console.ReadLine();

            Console.Write("Data e hora de início (formato: yyyy-MM-dd HH:mm): ");
            evento.DataHoraInicio = DateTime.Parse(Console.ReadLine());

            Console.Write("Data e hora de término (formato: yyyy-MM-dd HH:mm): ");
            evento.DataHoraFim = DateTime.Parse(Console.ReadLine());
            
            Console.Write("Descrição: ");
            evento.Descricao = Console.ReadLine();
            
            Console.Write("Quantidade de pessoas: ");
            evento.QuantidadePessoas = int.Parse(Console.ReadLine());
           
            Console.Write("Público alvo: ");
            evento.PublicoAlvo = Console.ReadLine();

            Console.WriteLine("=== Contato Responsável ===");

            Contato contato = new Contato();

            Console.Write("Nome: ");
            contato.Nome = Console.ReadLine();

            Console.Write("Telefone: ");
            contato.Telefone = Console.ReadLine();

            Console.Write("Email: ");
            contato.Email = Console.ReadLine();

            evento.ContatoResponsavel = contato;

            eventos.Add(evento);

            Console.WriteLine("Evento cadastrado com sucesso!");

            
         
        }

        public void ListarEventosPorPeriodo()
        {
            Console.WriteLine("=== Listar Eventos por Período ===");

            Console.Write("Data de início do período (formato: yyyy-MM-dd): ");
            DateTime dataInicio = DateTime.Parse(Console.ReadLine());

            Console.Write("Data de fim do período (formato: yyyy-MM-dd): ");
            DateTime dataFim = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("=== Eventos no Período ===");

            foreach (var evento in eventos)
            {
                if (evento.DataHoraInicio.Date >= dataInicio.Date && evento.DataHoraFim.Date <= dataFim.Date)
                {
                    Console.WriteLine($"ID: {evento.Id}");
                    Console.WriteLine($"Título: {evento.Titulo}");
                    Console.WriteLine($"Data de Início: {evento.DataHoraInicio}");
                    Console.WriteLine($"Data de Término: {evento.DataHoraFim}");
                    Console.WriteLine($"Descrição: {evento.Descricao}");
                    Console.WriteLine($"Quantidade de Pessoas: {evento.QuantidadePessoas}");
                    Console.WriteLine($"Público Alvo: {evento.PublicoAlvo}");
                    Console.WriteLine($"Contato Responsável: {evento.ContatoResponsavel.Nome} - Telefone: {evento.ContatoResponsavel.Telefone} - Email: {evento.ContatoResponsavel.Email}");
                    Console.WriteLine();
                }
            }
        }

        public void PesquisarEventoPorData()
        {
            Console.WriteLine("=== Pesquisar Evento por Data ===");

            Console.Write("Digite a data (formato: yyyy-MM-dd): ");
            DateTime dataPesquisa = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("=== Eventos na Data Específica ===");

            bool encontrado = false;

            foreach (var evento in eventos)
            {
                if (evento.DataHoraInicio.Date == dataPesquisa.Date || evento.DataHoraFim.Date == dataPesquisa.Date)
                {
                    encontrado = true;
                    Console.WriteLine($"ID: {evento.Id}");
                    Console.WriteLine($"Título: {evento.Titulo}");
                    Console.WriteLine($"Data de Início: {evento.DataHoraInicio}");
                    Console.WriteLine($"Data de Término: {evento.DataHoraFim}");
                    Console.WriteLine($"Descrição: {evento.Descricao}");
                    Console.WriteLine($"Quantidade de Pessoas: {evento.QuantidadePessoas}");
                    Console.WriteLine($"Público Alvo: {evento.PublicoAlvo}");
                    Console.WriteLine($"Contato Responsável: {evento.ContatoResponsavel.Nome} - Telefone: {evento.ContatoResponsavel.Telefone} - Email: {evento.ContatoResponsavel.Email}");
                    Console.WriteLine();
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Nenhum evento encontrado para a data especificada.");
            }
        }

        public void EditarEvento()
        {
            Console.WriteLine("=== Editar Informações de um Evento ===");

            Console.Write("Digite o ID do evento que deseja editar: ");
            string idEvento = Console.ReadLine();

            Evento evento = eventos.Find(e => e.Id == idEvento);

            if (evento == null)
            {
                Console.WriteLine("Evento não encontrado. Verifique o ID e tente novamente.");
                return;
            }

            Console.WriteLine("Selecione o que deseja editar:");
            Console.WriteLine("1. Título");
            Console.WriteLine("2. Data e Hora de Início");
            Console.WriteLine("3. Data e Hora de Término");
            Console.WriteLine("4. Descrição");
            Console.WriteLine("5. Quantidade de Pessoas");
            Console.WriteLine("6. Público Alvo");
            Console.WriteLine("7. Contato Responsável");
            Console.Write("Digite o número correspondente: ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Write("Novo Título: ");
                    evento.Titulo = Console.ReadLine();
                    break;
                case 2:
                    Console.Write("Nova Data e Hora de Início (formato: yyyy-MM-dd HH:mm): ");
                    evento.DataHoraInicio = DateTime.Parse(Console.ReadLine());
                    break;
                case 3:
                    Console.Write("Nova Data e Hora de Término (formato: yyyy-MM-dd HH:mm): ");
                    evento.DataHoraFim = DateTime.Parse(Console.ReadLine());
                    break;
                case 4:
                    Console.Write("Nova Descrição: ");
                    evento.Descricao = Console.ReadLine();
                    break;
                case 5:
                    Console.Write("Nova Quantidade de Pessoas: ");
                    evento.QuantidadePessoas = int.Parse(Console.ReadLine());
                    break;
                case 6:
                    Console.Write("Novo Público Alvo: ");
                    evento.PublicoAlvo = Console.ReadLine();
                    break;
                case 7:
                    Console.WriteLine("=== Novo Contato Responsável ===");
                    Contato novoContato = new Contato();

                    Console.Write("Nome: ");
                    novoContato.Nome = Console.ReadLine();

                    Console.Write("Telefone: ");
                    novoContato.Telefone = Console.ReadLine();

                    Console.Write("Email: ");
                    novoContato.Email = Console.ReadLine();

                    evento.ContatoResponsavel = novoContato;
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine("Informações do evento atualizadas com sucesso!");
        }

        public void PesquisarContatoCadastrado()
        {
            Console.WriteLine("=== Pesquisar Contato Cadastrado ===");

            Console.Write("Digite o nome ou email do contato: ");
            string query = Console.ReadLine();

            bool encontrado = false;

            foreach (var evento in eventos)
            {
                if (evento.ContatoResponsavel.Nome.Equals(query, StringComparison.OrdinalIgnoreCase) || evento.ContatoResponsavel.Email.Equals(query, StringComparison.OrdinalIgnoreCase))
                {
                    encontrado = true;
                    Console.WriteLine($"ID do Evento: {evento.Id}");
                    Console.WriteLine($"Título do Evento: {evento.Titulo}");
                    Console.WriteLine($"Data de Início: {evento.DataHoraInicio}");
                    Console.WriteLine($"Data de Término: {evento.DataHoraFim}");
                    Console.WriteLine($"Descrição: {evento.Descricao}");
                    Console.WriteLine($"Quantidade de Pessoas: {evento.QuantidadePessoas}");
                    Console.WriteLine($"Público Alvo: {evento.PublicoAlvo}");
                    Console.WriteLine($"Contato Responsável: {evento.ContatoResponsavel.Nome} - Telefone: {evento.ContatoResponsavel.Telefone} - Email: {evento.ContatoResponsavel.Email}");
                    Console.WriteLine();
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Contato não encontrado nos eventos cadastrados.");
            }
        }

        public void ExcluirEventoPorId()
        {
            Console.WriteLine("=== Excluir Evento por ID ===");

            Console.Write("Digite o ID do evento que deseja excluir: ");
            string idEvento = Console.ReadLine();

            Evento evento = eventos.Find(e => e.Id == idEvento);

            if (evento == null)
            {
                Console.WriteLine("Evento não encontrado. Verifique o ID e tente novamente.");
                return;
            }

            eventos.Remove(evento);

            Console.WriteLine("Evento excluído com sucesso!");

        }

        public void ExportarDadosEvento()
        {
            Console.WriteLine("=== Exportar Dados de um Evento ===");

            Console.Write("Digite o ID do evento que deseja exportar: ");
            string idEvento = Console.ReadLine();

            Evento evento = eventos.Find(e => e.Id == idEvento);

            if (evento == null)
            {
                Console.WriteLine("Evento não encontrado. Verifique o ID e tente novamente.");
                return;
            }

            string documentosPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string fileName = Path.Combine(documentosPath, $"{evento.Titulo}_{idEvento}.txt");

            using (StreamWriter file = new StreamWriter(fileName))
            {
                file.WriteLine($"ID do Evento: {evento.Id}");
                file.WriteLine($"Título do Evento: {evento.Titulo}");
                file.WriteLine($"Data de Início: {evento.DataHoraInicio}");
                file.WriteLine($"Data de Término: {evento.DataHoraFim}");
                file.WriteLine($"Descrição: {evento.Descricao}");
                file.WriteLine($"Quantidade de Pessoas: {evento.QuantidadePessoas}");
                file.WriteLine($"Público Alvo: {evento.PublicoAlvo}");
                file.WriteLine($"Contato Responsável: {evento.ContatoResponsavel.Nome} - Telefone: {evento.ContatoResponsavel.Telefone} - Email: {evento.ContatoResponsavel.Email}");
            }

            Console.WriteLine($"Dados do evento exportados com sucesso para o arquivo '{fileName}'.");
        }

        public string GerarIdUnico()
        {
            Guid guid = Guid.NewGuid();

            byte[] bytes = guid.ToByteArray();
            int number = BitConverter.ToInt32(bytes, 0);

            number = Math.Abs(number);

            string id = number.ToString("D6");

            return id;
        }
    }
}
