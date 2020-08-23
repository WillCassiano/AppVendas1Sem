using System;
using System.Collections.Generic;

namespace AppVendas1Sem
{
    class Program
    {
        static List<Produto> Produtos = new List<Produto>();
        static List<Venda> Vendas = new List<Venda>();
        static int CodProduto = 0;
        static void Main(string[] args)
        {
            string escolha = "";
            while (escolha != "0")
            {
                Console.WriteLine("1 - Cadastro de produto");
                Console.WriteLine("2 - Cadastro de venda");
                Console.WriteLine("3 - Vizualizar produtos");
                Console.WriteLine("4 - Vizualizar venda");

                escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        CadastrarProduto();
                        break;
                    case "2":
                        CadastrarVenda();
                        break;
                    case "3":
                        ListarProdutos();
                        break;
                    case "4":
                        MostrarDadosVenda(Vendas[0]);
                        break;
                    default:
                        break;
                }


            }            
        }

        private static void CadastrarVenda()
        {
            ListarProdutos();
            Venda venda = new Venda();
            venda.Data = DateTime.Now;
            int cod = -1;
            while (cod != 0)
            {
                Console.Write("Informe o código do produto:");
                cod = Convert.ToInt32(Console.ReadLine());

                if (cod == 0)
                {
                    return;
                }

                Produto produto = Produtos.Find(p => p.Codigo == cod);

                VendaProduto vendaProduto = new VendaProduto();

                vendaProduto.Venda = venda;
                vendaProduto.Produto = produto;

                Console.Write("Informe a quantidade:");
                vendaProduto.Quantidade = Convert.ToInt32(Console.ReadLine());

                venda.Produtos = new List<VendaProduto>();
                venda.Produtos.Add(vendaProduto);

                Vendas.Add(venda);
            }       
        }

        private static void ListarProdutos()
        {
            foreach (Produto produto in Produtos)
            {
                Console.WriteLine("{0} - {1} - {2}", produto.Codigo, produto.Nome, produto.Preco);
            }
        }

        private static void CadastrarProduto()
        {
            Console.WriteLine("Informe o nome do produto");
            string nome = Console.ReadLine();
            Console.WriteLine("Informe o preço do produto");
            double preco = Convert.ToDouble(Console.ReadLine());

            CodProduto++;
            Produtos.Add(new Produto {
                Codigo = CodProduto,
                Nome = nome,
                Preco = preco
            });
        }

        static void MostrarDadosVenda(Venda venda1)
        {
            Console.WriteLine("|====================================================|");
            Console.WriteLine("| Venda:        {0,37}|", venda1.Codigo);
            Console.WriteLine("| Data :        {0,37}|", venda1.Data.ToShortDateString());
            Console.WriteLine("| Valor Venda:  {0,37:C2}|", venda1.ValorVenda);
            Console.WriteLine("|----------------------------------------------------|");
            Console.WriteLine("| Produtos:                                          |");
            Console.WriteLine("|----------------------------------------------------|");
            Console.WriteLine("| Cod. | Descrição | Qdte | Valor Unit | Valor Total |");
            Console.WriteLine("|------|-----------|------|------------|-------------|");
            for (int i = 0; i < venda1.Produtos.Count; i++)
            {
                Console.WriteLine("|{0,6}|{1,-11}|{2,6}|{3,12:C2}|{4,13:C2}|",
                    venda1.Produtos[i].Produto.Codigo,
                    venda1.Produtos[i].Produto.Nome,
                    venda1.Produtos[i].Quantidade,
                    venda1.Produtos[i].Produto.Preco,
                    venda1.Produtos[i].ValorTotal);
            }
            Console.WriteLine("|______|___________|______|____________|_____________|");
        }
    }

    class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
    }

    class Venda
    {
        public int Codigo { get; set; }
        public DateTime Data { get; set; }
        public double ValorVenda 
        { 
            get
            {
                double valor = 0;
                for (var i = 0; i < Produtos.Count; i++)
                {
                    valor += Produtos[i].ValorTotal;
                }
                return valor;
            }
        }

        public List<VendaProduto> Produtos { get; set; }
    }

    class VendaProduto
    {
        public double Quantidade { get; set; }
        public double ValorTotal
        {
            get 
            { 
                if (Produto == null)
                {
                    return 0;
                }
                return Produto.Preco * Quantidade; 
            }
        }

        public Venda Venda { get; set; }
        public Produto Produto { get; set; }
    }
}
