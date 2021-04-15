using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WpfApp34.Tools;

namespace WpfApp34.ViewModels
{
    public class BookEditVM : MvvmNotify, IVM
    {
        Db db;
        private Book selectedBook;
        private Author selectedAuthor;
        private Genre selectedGenre;

        public Book SelectedBook
        {
            get => selectedBook;
            set
            {
                selectedBook = value;
                SignalChanged();
            }
        }

        public ObservableCollection<Genre> SelectedBookGenres { 
            get => new ObservableCollection<Genre>(SelectedBook.Genres); }
        public ObservableCollection<Author> SelectedBookAuthors { 
            get => new ObservableCollection<Author>(SelectedBook.Authors); }

        public List<Publisher> Publishers { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Author> Authors { get; set; }

        public Author SelectedAuthor
        {
            get => selectedAuthor;
            set
            {
                selectedAuthor = value;
                SignalChanged();
            }
        }
        public Genre SelectedGenre
        {
            get => selectedGenre;
            set
            {
                selectedGenre = value;
                SignalChanged();
            }
        }

        public MvvmCommand AddGenre { get; set; }
        public MvvmCommand RemoveGenre { get; set; }
        public MvvmCommand AddAuthor { get; set; }
        public MvvmCommand RemoveAuthor { get; set; }

        public MvvmCommand OpenListBooks { get; set; }
        public MvvmCommand SaveSelectedBook { get; set; }
        public BookEditVM()
        {
            db = Db.GetDb();

            Publishers = new List<Publisher>(db.Publishers);
            Genres = new List<Genre>(db.Genres);
            Authors = new List<Author>(db.Authors);

            AddGenre = new MvvmCommand(()=> {
                SelectedBook.Genres.Add(SelectedGenre);
                SignalChanged("SelectedBookGenres");
            },() => SelectedGenre != null);
            RemoveGenre = new MvvmCommand(() => {
                SelectedBook.Genres.Remove(SelectedGenre);
                SignalChanged("SelectedBookGenres");
            }, () => SelectedGenre != null );

            AddAuthor = new MvvmCommand(() => {                
                SelectedBook.Authors.Add(SelectedAuthor);
                SignalChanged("SelectedBookAuthors");
            }, () => selectedAuthor != null);
            RemoveAuthor = new MvvmCommand(() => {
                SelectedBook.Authors.Remove(SelectedAuthor);
                SignalChanged("SelectedBookAuthors");
            }, () => selectedAuthor != null);

            OpenListBooks = new MvvmCommand(() =>
            Pager.GoTo(PageType.BookList));

            SaveSelectedBook = new MvvmCommand(()=>
            {
                if (SelectedBook.Id == 0)
                    db.Books.Add(SelectedBook);
                db.SaveChanges();
            });
        }

        public (Type, Action<object>) RegisterTypeMessage()
        {
            return (typeof(Book), book => { 
                SelectedBook = (Book)book;

                if (SelectedBook.Genres == null)
                    SelectedBook.Genres = new List<Genre>();

                if (SelectedBook.Authors == null)
                    SelectedBook.Authors = new List<Author>();
            }
            );
        }
    }
}
