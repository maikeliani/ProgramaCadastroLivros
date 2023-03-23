using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using Microsoft.VisualBasic.FileIO;
using ProgramaCadastroLivros;

internal class Program
{
    private static void Main(string[] args)
    {
        List<Book> library = new List<Book>();
        List<Book> borrowedList = new List<Book>(); // livros alugados
        List<Book> returnedList = new List<Book>(); // livros devolvidos
      //  LoadBooks();    //carrega as informações salvas no arquivo para a library


        // COLOCA O CODIGO A SEGUIR DENRO DE UM METODO CARREGARDADOS() E CHAMA ESSE METODO NO INICIO DO PROGRAMA
        //obs pra depois: sr.ReadLine();Split(";"); no meu caso é "|"    ods: trocar | por ';' abre em excel, arquivo .csv
        //Split retorna vetor de string
        // entao:
        //  string[] campos = Sr.ReadLine().Split(";");
        // int id = int.Parse(campos[0]);
        //string descricao = campos[1];
        // double valor = double.Parse(campos[2]);

        //criarei o Book e dou add na library !!

        /*

        do
        {
            //  string[] campos = Sr.ReadLine().Split(";");
            // int id = int.Parse(campos[0]);
            //string descricao = campos[1];
            // double valor = double.Parse(campos[2]);


        } while (!sr.EndOfStream);
        sr.close();
        */


        //obs depois de tudo tentar fazer uma lista pro editor

        





        int Menu()
        {
            int xpto;
            Console.WriteLine("\n\nMENU DE OPÇÕES\n1- CADASTRAR LIVRO"
                + "\n2- DELETAR LIVRO\n3- BUSCAR LIVRO\n4- CONSULTA LIVROS DISPONÍVEIS\n5- EMPRESTAR UM LIVRO\n6-SAIR\n7- EDITAR\n8-DEVOLVER LIVRO\nESCOLHA UMA OPÇÃO: \n");

            while (!int.TryParse(Console.ReadLine(), out xpto))
            {
                Console.WriteLine("Insira apenas números DE 1 A 9");
                Thread.Sleep(1000);
                Console.Clear();
                Menu();
            }
            while ((xpto < 1) || (xpto > 9))
            {
                Console.Clear();
                Console.WriteLine(" Informe um valor do menu ( entre 1 e 9)");
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
                    library.Add(RegisterBook());
                    break;

                case 2:
                    Console.WriteLine(DeleteBook());
                    break;

                case 3:
                    Console.Write("Informe o nome do livro que deseja buscar: ");
                    string searchedBook = Console.ReadLine();
                    SearchBookByName(searchedBook);
                    break;

                case 4:
                    ReturnCollection();
                    break;
                case 5:
                    Console.WriteLine(BorrowBook());
                    break;

                case 6:
                    //antes de sair usa os metodos para gravar cad acoisa em cada  lista
                    System.Environment.Exit(0);
                    break;
                case 7:
                    EditFile();
                    break;
                case 8:
                    ReturnBook();
                    break;
            }
        } while (option != 6);



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




        string ReturnBook()
        {
            Console.WriteLine("Informe o livro que deseja devolver: ");
            string returned = Console.ReadLine();

            for (int x = 0; x < borrowedList.Count; x++)
            {
                if (borrowedList[x].Name.Equals(returned))
                {
                    Book aux = borrowedList[x];
                    returnedList.Add(aux); // ADD NA LISTA DE LIVROS DEVOLVIDOS

                    borrowedList.Remove(borrowedList[x]); //remove da lista de emprestados
                    WriteReturnedBook(aux);

                }
            }
            return "DEVOLVIDO";
        }



        void SearchBookByName(string isbn)
        {

            if (library.Count == 0)
            {
                Console.WriteLine(" Sem livros");
            }
            else
            {
                foreach (Book book in library)
                {
                    if (book.Isbn.Equals(isbn))
                    {

                        Console.WriteLine(book.textToDisplay());
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Não encontrado");
                    }

                }
            }


        }
        void EditFile()

        {
            StreamReader sr = new StreamReader("C:\\Users\\adm\\source\\repos\\ProgramaCadastroLivros\\ProgramaCadastroLivros\\bin\\Debug\\net6.0\\library.txt");
            string line = sr.ReadToEnd();

            string[] EditedLine = line.Split('|'); // divide em um vetor de caracteres

            foreach (var edited in EditedLine)
            {
                Console.WriteLine(edited);
            }



            Console.WriteLine(EditedLine[0]);
            sr.Close();
            Thread.Sleep(4000);
        }


        Book RegisterBook()
        {
            Console.Write("Informe o nome do livro que deseja cadastrar :");
            string name = Console.ReadLine();

            Console.Write("Informe o nome do(s) autor(es): ");
            string author = Console.ReadLine();

            Console.Write("Informe o nome da editora: ");
            string publisher = Console.ReadLine();

            Console.Write("Informe o codigo ISBN: ");
            string isbn = Console.ReadLine();

            Book book = new Book(name, author, publisher, isbn);

            WriteBook(book); // ja add o livro criado no arquivo!
            Console.WriteLine("cadastrado");
            return book;
        }



        string BorrowBook() // emprestar livro
        {
            bool isRented = false;

            Console.Write("informe o nome do livro que deseja emprestar: ");
            string bookforRent = Console.ReadLine();


            for (int x = 0; x < library.Count; x++)
            {
                if ((library[x].Name.Equals(bookforRent)))
                {


                    WriteRentedBook(library[x]);   // GRAVA LIVRO ALUGADO EM ARQUIVO PROPRIO 
                    borrowedList.Add(library[x]); // add na lista de livros emprestados
                    isRented = true;
                    return " ALUGADO!";
                }


            }

            return "Nao encontrado";
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
                    Console.WriteLine(book.textToDisplay());
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



        void AddReturnedListToFile()  //salva lista de livros devolvidos no arquivo
        {
            if (returnedList.Count() == 0)
            {
                Console.WriteLine("NENHUM LIVRO CADASTRADO");
            }
            else
            {
                foreach (Book book in returnedList)
                {
                    WriteBook(book);
                }
            }

        }







        //grava os livros alugados em arquivo DE REGISTRO!!

        void WriteRentedBook(Book book)
        {

            try
            {
                if (File.Exists("borrowed.txt"))
                {
                    var temp = ReadFile("borrowed.txt");
                    StreamWriter sw = new StreamWriter("borrowed.txt");
                    sw.WriteLine(temp + book.ToString());
                    sw.Close();

                }
                else
                {

                    StreamWriter sw = new StreamWriter("borrowed.txt");
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
                Console.WriteLine("Cadastrado no arquivo de livros emprestados com sucesso!");
                Thread.Sleep(1000);
            }

        }



        //GRAVA LIVROS DEVOLVIDISO NA  NO ARQUIVO DE DEVOLVIDOS

        void WriteReturnedBook(Book book)
        {

            try
            {
                if (File.Exists("returnedBooks.txt"))
                {
                    var temp = ReadFile("returnedBooks.txt");
                    StreamWriter sw = new StreamWriter("returnedBooks.txt");
                    sw.WriteLine(temp + book.ToString());

                    sw.Close();

                }
                else
                {

                    StreamWriter sw = new StreamWriter("returnedBooks.txt");
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
                Console.WriteLine("cadastrado na lista de Devolvido com sucesso!");
                Thread.Sleep(1000);
            }

        }



        //CADASTRA TODOS OS LIVROS DO ACERVO NO ARQUIVO DE REGISTRO 
        void WriteBook(Book book)
        {


            try
            {
                if (File.Exists("library.txt"))
                {
                    var temp = ReadFile("library.txt");
                    StreamWriter sw = new StreamWriter("library.txt");
                    sw.WriteLine(temp + book.ToString());
                    //sw.WriteLine(book.ToString());
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