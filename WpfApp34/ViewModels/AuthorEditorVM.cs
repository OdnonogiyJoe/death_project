using System;
using System.Collections.Generic;
using System.Text;
using WpfApp34.Tools;

namespace WpfApp34.ViewModels
{
    public class AuthorEditorVM : MvvmNotify, IVM
    {
        private List<Author> authors;
        private Author selectedAuthor;

        public Author SelectedAuthor
        {
            get => selectedAuthor;
            set
            {
                selectedAuthor = value;
                SignalChanged();
            }
        }
        public List<Author> Authors
        {
            get => authors;
            set
            {
                authors = value;
                SignalChanged();
            }
        }
        Db db;

        public MvvmCommand CreateAuthor { get; set; }
        public MvvmCommand SaveAuthor { get; set; }
        public AuthorEditorVM()
        {
            db = Db.GetDb();
            LoadAuthors();
            CreateAuthor = new MvvmCommand(()=> {
                SelectedAuthor = new Author();
                db.Authors.Add(SelectedAuthor);
                db.SaveChanges();
                LoadAuthors();
            });
            SaveAuthor = new MvvmCommand(()=> {
                db.SaveChanges();
                LoadAuthors();
            }, ()=>SelectedAuthor != null);
        }

        private void LoadAuthors()
        {
            Authors = new List<Author>(db.Authors);
        }
    }
}
