using System;

class Program
{
    static void Main(string[] args)
    {
        Banco banco = new Banco();

        int opcao = -1;
        while (opcao != 0)
        {
            Console.WriteLine("\n=== SapiensBank ===");
            Console.WriteLine("1 - Inserir conta");
            Console.WriteLine("2 - Listar contas");
            Console.WriteLine("3 - Sacar");
           Console.WriteLine("4 - Depositar");
            Console.WriteLine("5 - Aumentar limite");
            Console.WriteLine("6 - Diminuir limite");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida.");
                continue;
            }

            Console.WriteLine();

            switch (opcao)
            {
                case 1:
                    InserirConta(banco);
                    break;

                case 2:
                    ListarContas(banco);
                    break;

                case 3:
                    Sacar(banco);
                    break;

                case 4:
                    Depositar(banco);
                    break;

                case 5:
                    AumentarLimite(banco);
                    break;

                case 6:
                    DiminuirLimite(banco);
                    break;

                case 0:
                    Console.WriteLine("Saindo...");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void InserirConta(Banco banco)
    {
        Console.Write("Número da conta: ");
        int numero = int.Parse(Console.ReadLine());

        Console.Write("Cliente: ");
        string cliente = Console.ReadLine();

        Console.Write("CPF: ");
        string cpf = Console.ReadLine();

        Console.Write("Senha: ");
        string senha = Console.ReadLine();

        Console.Write("Limite inicial: ");
        decimal limite = decimal.Parse(Console.ReadLine());

        var conta = new Conta(numero, cliente, cpf, senha, limite);
        banco.Contas.Add(conta);
        banco.SaveContas();

        Console.WriteLine("Conta criada com sucesso!");
    }

    static void ListarContas(Banco banco)
    {
        Console.WriteLine("=== Lista de contas ===");

        if (!banco.Contas.Any())
        {
            Console.WriteLine("Nenhuma conta cadastrada.");
            return;
        }

        foreach (var conta in banco.Contas)
        {
            Console.WriteLine($"Conta: {conta.Numero} | Cliente: {conta.Cliente} | Saldo: {conta.Saldo} | Limite: {conta.Limite}");
        }
    }

    static void Sacar(Banco banco)
    {
        Console.Write("Número da conta: ");
        int numero = int.Parse(Console.ReadLine());

        var conta = banco.BuscarConta(numero);
        if (conta == null)
        {
            Console.WriteLine("Conta não encontrada.");
            return;
        }

        Console.Write("Valor do saque: ");
        decimal valor = decimal.Parse(Console.ReadLine());

        if (valor <= conta.SaldoDisponível)
        {
            conta.Saldo -= valor;
            banco.SaveContas();
            Console.WriteLine("Saque realizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Saldo insuficiente.");
        }
    }

    static void Depositar(Banco banco)
    {
        Console.Write("Número da conta: ");
        int numero = int.Parse(Console.ReadLine());

        var conta = banco.BuscarConta(numero);
        if (conta == null)
        {
            Console.WriteLine("Conta não encontrada.");
            return;
        }

        Console.Write("Valor do depósito: ");
        decimal valor = decimal.Parse(Console.ReadLine());

        conta.Saldo += valor;
        banco.SaveContas();

        Console.WriteLine("Depósito realizado!");
    }

    static void AumentarLimite(Banco banco)
    {
        Console.Write("Número da conta: ");
        int numero = int.Parse(Console.ReadLine());

        var conta = banco.BuscarConta(numero);
        if (conta == null)
        {
            Console.WriteLine("Conta não encontrada.");
            return;
        }

        Console.Write("Aumentar limite em: ");
        decimal valor = decimal.Parse(Console.ReadLine());

        conta.Limite += valor;
        banco.SaveContas();

        Console.WriteLine("Limite aumentado.");
    }

    static void DiminuirLimite(Banco banco)
    {
        Console.Write("Número da conta: ");
        int numero = int.Parse(Console.ReadLine());

        var conta = banco.BuscarConta(numero);
        if (conta == null)
        {
            Console.WriteLine("Conta não encontrada.");
            return;
        }

        Console.Write("Diminuir limite em: ");
        decimal valor = decimal.Parse(Console.ReadLine());

        if (valor > conta.Limite)
        {
            Console.WriteLine("Valor maior do que o limite atual.");
            return;
        }

        conta.Limite -= valor;
        banco.SaveContas();

        Console.WriteLine("Limite diminuído.");
    }
}
