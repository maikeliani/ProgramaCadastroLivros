using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualBasic.FileIO;
using ProgramaCadastroLivros;

internal class Program
{
    private static void Main(string[] args)
    {
        List<Book> library = new List<Book>();




        int Menu()
        {
            int xpto;
            Console.WriteLine("\n\nMENU DE OPÇÕES\n1- CADASTRAR LIVRO"
                + "\n2- DELETAR LIVRO\n3- BUSCAR LIVRO POR NOME\n4- CONSULTA LIVROS DISPONÍVEIS\n5- ALUGAR UM LIVRO\n6- GRAVAR NO ARQUIVO\n7- SAIR\nESCOLHA UMA OPÇÃO: \n");

            while (!int.TryParse(Console.ReadLine(), out xpto))
            {
                Console.WriteLine("Insira apenas números DE 1 A 7");
                Thread.Sleep(1000);
                Console.Clear();
                Menu();
            }
            while ((xpto < 1) || (xpto > 7))
            {
                Console.Clear();
                Console.WriteLine(" Informe um valor do menu ( entre 1 e 7)");
                Menu();

                while (!int.TryParse(Console.ReadLine(), out xpto))
                {
                    Console.Clear();
                    Menu();
                }
            }
            return xpto;
        }

        int option;

        


        do
        {
            option = Menu();

            switch (option)
            {
                case 1:
                    RegisterBook();
                    break;

                case 2:
                    Console.WriteLine(DeleteBook());
                    break;

                case 3:
                    Console.Write("Informe o nome do livro que deseja buscar: ");
                    string searchedBook = Console.ReadLine();
                    Console.WriteLine(SearchBookByName(searchedBook));
                    break;

                case 4:
                    ReturnCollection();
                    break;
                case 5:
                    Console.WriteLine(RentBook());
                    break;
                case 6:
                    AddListToFile();
                    break;

                case 7:
                    System.Environment.Exit(0);
                    break;
            }
        } while (option != 7);




        /*
           string txt = "";
            Console.WriteLine("LISTA DE LIVROS\n");

            if(library.Count() == 0)
            {
                return " NENHUM LIVRO CADASTRADO!";
            }
            else
            {
                StreamReader sr = new("library.txt");
               
                do
                {
                    txt += sr.ReadLine() + "\n";

                } while (!sr.EndOfStream);

                return txt;
            }*/




        string DeleteBook()
        {   
            

            if (library.Count() == 0)
            {
                return "Impossível deletar, estoque vazio!";
            }

            Console.Write("Informe o ISBN do livro que deseja deletar: ");
            string bookToDelete = Console.ReadLine();
            bool deleted = false;


            for (int x = 0; x < library.Count; x++)
            {
                if (library[x].Isbn == bookToDelete)
                {
                    library.Remove(library[x]);
                    deleted = true;
                }
            }

            if (deleted)
            {
                return "Livro excluido do cadastro!";
            }
            return " Livro não encntrado no sistema!";
        }



        string SearchBookByName(string name)
        {
            foreach (Book book in library)
            {
                if (book.Name == name)
                {
                    if (book.Quantity == 0)
                    {
                        return "Livro(s) indisponivel no momento";
                    }
                    else
                    {
                        return $"No momento temos {book.Quantity} livros disponíveis!";
                    }
                }

            }
            return "Livro não cadastrado!";
        }




        string RegisterBook()
        {
            Console.Write("Informe o nome do livro que deseja cadastrar :");
            string name = Console.ReadLine();

            Console.Write("Informe o nome do(s) autor(es): ");
            string author = Console.ReadLine();

            Console.Write("Informe o nome da editora: ");
            string publisher = Console.ReadLine();

            Console.Write("Informe o codigo ISBN: ");
            string isbn = Console.ReadLine();

            Console.WriteLine("Informe a quntidade de livros a serem cadastradods no acervo");
            int quantity = int.Parse(Console.ReadLine());

            Book book = new Book(name, author, publisher, isbn, quantity);

            library.Add(book);
           // AddListToFile(book); //  add no arquivo qdo cria o livro
            return " cadastrado";
        }



        string RentBook()
        {
           
            Console.Write("informe o nome do livro que deseja alugar: ");
            string bookforRent = Console.ReadLine();

            Console.Write("Informe a quantidade desejada: ");
            int bookQuantity = int.Parse(Console.ReadLine());


            for(int x=0; x < library.Count;x++)
            {
                if ((library[x].Name.Equals(bookforRent)))
                {
                    if (library[x].Quantity >= bookQuantity)
                    {
                        library[x].Quantity -= bookQuantity;
                                                
                    }
                    else
                    {
                        return "QUANTIDADE INDISPONIVEL";
                    }
                }
                else
                {
                    return "LIVRO NÃO ENCONTRADO.";
                }
            }
            return " ALUGADO COM SUCESSO";
        }


        void ReturnCollection()
        {
            if (library.Count() == 0)
            {
                Console.WriteLine("NENHUM LIVRO CADASTRADO");
            }
            else
            {
                foreach (Book book in library)
                {
                    Console.WriteLine(book.ToString());
                }
            }


        }


        string ReadFile(string f)
        {
            StreamReader sr = new StreamReader(f);
            string text = "";
            try
            {
                text = sr.ReadToEnd();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sr.Close();
            }
            return text;
        }



        //envia todos os livros da lista para o metodo que grava no arquivo

        void AddListToFile()
        {
            if (library.Count() == 0)
            {
                Console.WriteLine("NENHUM LIVRO CADASTRADO");
            }
            else
            {
                foreach (Book book in library)
                {
                    WriteBook(book);
                }
            }
            
        }




        void WriteBook(Book book)
        {

            try
            {
                if (File.Exists("library.txt"))
                {
                    var temp = ReadFile("library.txt");
                    StreamWriter sw = new StreamWriter("library.txt");
                    sw.WriteLine(temp + book.ToString());
                    sw.Close();

                }
                else
                {
                   
                    StreamWriter sw = new StreamWriter("library.txt");
                    sw.WriteLine(book.ToString());
                    sw.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Console.WriteLine("Cadastrado no arquivo com sucesso!");
                Thread.Sleep(1000);
            }

        }



        string EditBook(Book book) // falta implementar
        {
            return null;
        }






    }
}