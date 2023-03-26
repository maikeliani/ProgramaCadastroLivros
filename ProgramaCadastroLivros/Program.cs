using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using Microsoft.VisualBasic.FileIO;
using ProgramaCadastroLivros;
using static System.Reflection.Metadata.BlobBuilder;

internal class Program
{
    private static void Main(string[] args)
    {
        List<Book> library = new List<Book>();
        List<Book> borrowedList = new List<Book>();
        List<Book> returnedList = new List<Book>();
        LoadBooksFromFile();
        LoadReturnedBooksFromFile();
        LoadBorrowedBooksFromFile();

        void LoadBooksFromFile()
        {

            string[] retorno;


            if (File.Exists("library.txt"))
            {
                StreamReader sr = new StreamReader("library.txt");

                if (sr.Read() != -1)
                {
                    do
                    {
                        retorno = sr.ReadLine().Split(';');
                        string name = retorno[0];
                        string author = retorno[1];
                        string publisher = retorno[2];
                        string isbn = retorno[3];
                        Book book = new Book(name, author, publisher, isbn);
                        library.Add(book);
                        sr.ReadLine();
                    } while (!sr.EndOfStream);
                    sr.Close();
                }

            }
            else
            {
                Console.WriteLine("Carregando arquivo....");
                Thread.Sleep(1000);
                return;
            }


        }

        void LoadBorrowedBooksFromFile()
        {

            string[] retorno;


            if (File.Exists("borrowed.txt"))
            {
                StreamReader sr = new StreamReader("borrowed.txt");

                if (sr.Read() != -1)
                {
                    do
                    {
                        retorno = sr.ReadLine().Split(';');
                        string name = retorno[0];
                        string author = retorno[1];
                        string publisher = retorno[2];
                        string isbn = retorno[3];
                        Book book = new Book(name, author, publisher, isbn);
                        borrowedList.Add(book);
                        sr.ReadLine();
                    } while (!sr.EndOfStream);
                    sr.Close();
                }
               
            }
            else
            {
                Console.WriteLine("Carregando arquivo....");
                Thread.Sleep(1000);
                return;
            }
        }


        void LoadReturnedBooksFromFile()
        {

            string[] retorno;


            if (File.Exists("returnedBooks.txt"))
            {
                StreamReader sr = new StreamReader("returnedBooks.txt");

                if (sr.Read() != -1)
                {
                    do
                    {
                        retorno = sr.ReadLine().Split(';');
                        string name = retorno[0];
                        string author = retorno[1];
                        string publisher = retorno[2];
                        string isbn = retorno[3];
                        Book book = new Book(name, author, publisher, isbn);
                        returnedList.Add(book);
                        sr.ReadLine();
                    } while (!sr.EndOfStream);
                    sr.Close();
                }
            }
            else
            {
                Console.WriteLine("Carregando arquivo....");
                Thread.Sleep(1000);
                return;
            }
        }





        int Menu()
        {
            Console.Clear();
            int xpto;
            Console.WriteLine("\n\nMENU DE OPÇÕES\n1- CADASTRAR LIVRO"
                + "\n2- DELETAR LIVRO\n3- BUSCAR LIVRO\n4- CONSULTA LIVROS DISPONÍVEIS\n5- EMPRESTAR UM LIVRO\n6- CONSULTA LIVROS DEVOLVIDOS\n7- EDITAR LIVRO\n8- DEVOLVER LIVRO\n9- CONSULTA LIVROS EMPRESTADOS" +
                "\n0- SAIR\nESCOLHA UMA OPÇÃO: \n");


            try
            {
                xpto = int.Parse(Console.ReadLine());
            }

            catch
            {
                return -1;
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
                    Console.WriteLine(RegisterBook());
                    Thread.Sleep(2000);
                    break;

                case 2:
                    Console.WriteLine(DeleteBook());
                    Thread.Sleep(2000);
                    break;

                case 3:
                    Console.WriteLine(SearchBook());
                    Thread.Sleep(2000);
                    break;

                case 4:
                    ReturnCollection();
                    Thread.Sleep(2000);
                    break;
                case 5:
                    Console.WriteLine(BorrowBook());
                    Thread.Sleep(2000);
                    break;

                case 6:

                    ConsultReturnedBooks();
                    Thread.Sleep(2000);
                    break;

                case 7:
                    Console.WriteLine(EditBook());
                    Thread.Sleep(2000);
                    break;
                case 8:
                    Console.WriteLine(ReturnBook());
                    Thread.Sleep(2000);
                    break;
                case 9:
                    ConsultBorrowedBooks();

                    break;
                case 0:
                    System.Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Informe um valor corretamente, de acordo com o menu.");
                    Console.Beep();
                    Thread.Sleep(1000);
                    break;
            }
        } while (option != 0);






        void ConsultBorrowedBooks()
        {

            if (borrowedList.Count() == 0)
            {
                Console.WriteLine("Nenhum livro emprestado.");
                Thread.Sleep(2000);
            }
            else
            {
                foreach (Book book in borrowedList)
                {
                    Console.WriteLine(book.textToDisplay());
                }
                Thread.Sleep(2000);
            }
           
        }




        string DeleteBook()
        {

            if (library.Count() == 0)
            {
                return "Impossível deletar, estoque vazio!";
            }

            Console.Write("Informe o ISBN do livro que deseja deletar: ");
            string bookToDelete = Console.ReadLine();


            for (int x = 0; x < library.Count; x++)
            {
                if (library[x].Isbn.Equals(bookToDelete))
                {
                    library.Remove(library[x]);
                    UpdateBooksFile();
                    return "Livro excluido do cadastro!";

                }
            }

            return " Livro não encontrado no sistema!";
        }




        void ConsultReturnedBooks()
        {

            if (returnedList.Count() == 0)
            {
                Console.WriteLine("Nenhum livro devolvido.");
                Thread.Sleep(2000);
            }
            else
            {
                foreach (Book book in returnedList)
                {
                    Console.WriteLine(book.textToDisplay());
                }
            }

        }





        string ReturnBook()
        {

            Console.WriteLine("Informe o livro que deseja devolver: ");
            string bookToReturn = Console.ReadLine();


            foreach (Book book in borrowedList)
            {
                if (book.Name.Equals(bookToReturn))
                {

                    returnedList.Add(book);
                    borrowedList.Remove(book); 
                    AddReturnedBooksInFile(book);
                    UpdateBorrowedBooksFile();
                    return "DEVOLVIDO";
                }
            }

            return " Livro não encontrado";

        }



        string SearchBook()
        {
            bool found = false;

            if (library.Count == 0)
            {
                return " NENHUM LIVRO CADASTRADO";

            }
            else
            {
                Console.Write("Informe o isbn do livro que deseja buscar: ");
                string isbn = Console.ReadLine();

                foreach (Book book in library)
                {
                    if (book.Isbn.Equals(isbn))
                    {
                        found = true;
                        return book.textToDisplay();

                    }
                }

                return "Não encontrado";
            }


        }



        string RegisterBook()
        {
            bool bookFound = false;
            Console.Write("Informe o isbn do livro que deseja cadastrar: ");
            string isbn = Console.ReadLine();

            foreach (Book b in library)
            {
                if (b.Isbn.Equals(isbn))
                {
                    bookFound = true;
                    return "Livro ja está cadastrado.";


                }
            }

            if (bookFound == false)
            {
                Console.Write("Informe o nome do livro que deseja cadastrar :");
                string name = Console.ReadLine();

                Console.Write("Informe o nome do(s) autor(es): ");
                string author = Console.ReadLine();

                Console.Write("Informe o nome da editora: ");
                string publisher = Console.ReadLine();

                Book book = new Book(name, author, publisher, isbn);

                library.Add(book);
                AddBooksInFile(book);
            }

            return "CADASTRADO";
        }



        string BorrowBook()
        {


            if (library.Count == 0)
            {
                return " NENHUM LIVRO CADASTRADO";
            }
            else
            {
                Console.Write("informe o nome do livro que deseja emprestar: ");
                string bookToBorrow = Console.ReadLine();

                foreach (Book b in borrowedList)
                {
                    if (b.Name.Equals(bookToBorrow))
                    {

                        return " já esta emprestado";
                    }
                }


                for (int x = 0; x < library.Count; x++)
                {
                    if ((library[x].Name.Equals(bookToBorrow)))
                    {
                        Book aux = library[x];
                        borrowedList.Add(aux);
                        AddToBorrowedBooksFile(aux);
                        return "emprestado";
                    }
                }
                return "Nao encontrado";
            }

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




        void UpdateReturnedListFile()
        {
            if (returnedList.Count() == 0)
            {
                Console.WriteLine("NENHUM LIVRO CADASTRADO");
                Thread.Sleep(2000);
            }
            else
            {
                foreach (Book book in returnedList)
                {
                    AddReturnedBooksInFile(book);
                }
            }
        }


        void AddReturnedBooksInFile(Book book)
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




        void UpdateBorrowedBooksFile() 
        {
            if (borrowedList.Count == 0)
            {
                try
                {

                    StreamWriter sw = new StreamWriter("borrowed.txt");
                    
                    sw.Close();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                foreach (Book book in borrowedList)
                {
                    RewriteBorrowBooksInFile(book);
                }
            }
           

        }

        void RewriteBorrowBooksInFile(Book book) 
        {
            try
            {
                StreamWriter sw = new StreamWriter("library.txt");
                sw.WriteLine(book.ToString());

                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }


        }


        void AddToBorrowedBooksFile(Book book) 
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

        }



        void UpdateBooksFile()
        {


            foreach (Book item in library)
            {

                RewriteBooksInFile(item);
            }

        }



        void RewriteBooksInFile(Book book)
        {
            try
            {
                StreamWriter sw = new StreamWriter("library.txt");
                sw.WriteLine(book.ToString());

                sw.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }




        void AddBooksInFile(Book book)
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


        }




        string EditBook()
        {
            if (library.Count == 0)
            {
                return " SEM LIVROS CADASTRADOS";
            }
            else
            {
                Console.WriteLine("Informe o codigo ISBN do livro que deseja editar: ");
                string isbn = Console.ReadLine();

                Book b = null;
                int option;
                bool found = false;

                for (int i = 0; i < library.Count; i++)
                {
                    if (library[i].Isbn.Equals(isbn))
                    {
                        b = library[i];
                        found = true;
                        break;
                    }
                }


                if (found == false)
                {
                    return "Impossivel editar um livro que não esta registrado.";
                }
                else
                {
                    do
                    {
                        option = EditingMenu();
                        Book aux;


                        switch (option)
                        {
                            case 1:
                                Console.Write("Informe o novo nome para o livro: ");
                                string newName = Console.ReadLine();
                                b.Name = newName;
                                aux = b;
                                library.Remove(b);
                                library.Add(aux);
                                UpdateBooksFile();
                                Thread.Sleep(1000);
                                break;

                            case 2:
                                Console.Write("Informe o novo nome para o autor: ");
                                string newAuthor = Console.ReadLine();
                                b.Author = newAuthor;
                                aux = b;
                                library.Remove(b);
                                library.Add(aux);
                                UpdateBooksFile();
                                Thread.Sleep(1000);
                                break;

                            case 3:
                                Console.Write("Informe o novo nome para a editora: ");
                                string newPublisher = Console.ReadLine();
                                b.Publisher = newPublisher;
                                aux = b;
                                library.Remove(b);
                                library.Add(aux);
                                UpdateBooksFile();
                                Thread.Sleep(1000);
                                break;

                            case 4:
                                Console.Write("Informe o novo ISBN: ");
                                string newIsbn = Console.ReadLine();
                                b.Isbn = newIsbn;
                                aux = b;
                                library.Remove(b);
                                library.Add(aux);
                                UpdateBooksFile();
                                Thread.Sleep(1000);
                                break;


                            case 0:
                                Console.WriteLine("Saindo....");
                                Thread.Sleep(2000);
                                Menu();
                                break;

                            default:
                                Console.WriteLine("Informe corretamente o número para edição, ou  digite zero '0' para sair!");
                                Thread.Sleep(2000);
                                break;
                        }
                    } while (option != 0);

                    return "EDITADO COM SUCESSO.";
                }
            }

        }




        int EditingMenu()
        {

            Console.Clear();
            int xpto;
            Console.Write(" Escolha no menu de edição se deseja editar algum item," +
                            "ou digite '0' para sair: ");
            Console.WriteLine("\n\nMENU DE EDIÇÃO\n1- EDITAR NOME"
                + "\n2- EDITAR AUTOR\n3- EDITAR EDITORA" +
                "\n4- EDITAR ISBN\n0- SAIR\nESCOLHA UMA OPÇÃO: \n");

            try
            {
                xpto = int.Parse(Console.ReadLine());
            }

            catch
            {
                return -1;
            }

            return xpto;


        }


    }
}