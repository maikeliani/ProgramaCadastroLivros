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
        public int Quantity { get; set; }

        public Customer Customer { get; set; } // cliente que aluga o livro

        public bool IsBorrowed { get; set; }
        public bool WasReturned { get; set; }



        public Book(string name, string author, string publisher, string isbn, int quantity)
        {
            this.Name = name;
            this.Author = author;
            this.Publisher = publisher;
            this.Isbn = isbn;
            this.Quantity = quantity;
        }

        





        public override string ToString()
        {
            return $" Titulo: {this.Name} | Autor: {this.Author} | Editora: {this.Publisher}  |Isbn: {this.Isbn}  | Quantidade: {this.Quantity}";
        }



    }
}
