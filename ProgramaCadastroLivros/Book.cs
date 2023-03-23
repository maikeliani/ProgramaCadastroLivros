using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramaCadastroLivros
{
    internal class Book
    {
        public string Name { get; set; }
        public string Author { get; set; } // pode ser lista de autores
        public string Publisher { get; set; }
        public string Isbn { get; set; }
       

        public Customer Customer { get; set; } // cliente que aluga o livro

        public bool IsBorrowed { get; set; }
        public bool WasReturned { get; set; }



        public Book(string name, string author, string publisher, string isbn)
        {
            this.Name = name;
            this.Author = author;
            this.Publisher = publisher;
            this.Isbn = isbn;
            
        }


            // metodo para exibir ao usuario
        public  string textToDisplay()
        {
            return $"Nome: {Name} \nAutor: {Author} \nEditora: {Publisher} \nIsbn: {Isbn}";
        }


        //metodo a ser guardado em registro
        public override string ToString()
        {
            return Name + ";" +Author +";"+Publisher + ";" +Isbn;
        }



    }
}
