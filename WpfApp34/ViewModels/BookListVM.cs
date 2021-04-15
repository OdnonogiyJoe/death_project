using System;
using System.Collections.Generic;
using System.Text;
using WpfApp34.Tools;

namespace WpfApp34.ViewModels
{
    public class BookListVM : MvvmNotify, IVM
    {
        Db db;
        private List<Book> books;

        public List<Book> Books
        {
            get => books;
            set { 
                books = value;
                SignalChanged();
            }
        }
        public Book SelectedBook { get; set; }

        public MvvmCommand AddBook { get; set; }
        public MvvmCommand EditBook { get; set; }
        public BookListVM()
        {
            db = Db.GetDb();
            Books = new List<Book>(db.Books);

            AddBook = new MvvmCommand(()=> {
                SelectedBook = new Book();
                Pager.GoTo(PageType.BookEdit);
                Pager.SendMessage(SelectedBook, PageType.BookEdit);
            });
            EditBook = new MvvmCommand(()=> {
                Pager.GoTo(PageType.BookEdit);
                Pager.SendMessage(SelectedBook, PageType.BookEdit);
            });
        }
    }
}
