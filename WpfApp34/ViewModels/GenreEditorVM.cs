using System;
using System.Collections.Generic;
using System.Text;
using WpfApp34.Tools;

namespace WpfApp34.ViewModels
{
    public class GenreEditorVM : MvvmNotify, IVM
    {
        Db db;

        private List<Genre> genres;

        public List<Genre> Genres
        {
            get => genres; 
            set {
                genres = value;
                SignalChanged();
            }
        }
        private Genre selectedGenre;

        public Genre SelectedGenre
        {
            get => selectedGenre; 
            set {
                selectedGenre = value;
                SignalChanged();
            }
        }

        public MvvmCommand CreateGenre { get; set; }
        public MvvmCommand SaveGenre { get; set; }

        public GenreEditorVM()
        {
            db = Db.GetDb();
            LoadGenres();
            CreateGenre = new MvvmCommand(()=> {
                SelectedGenre = new Genre();
                db.Genres.Add(SelectedGenre);
                db.SaveChanges();
                LoadGenres();
            });
            SaveGenre = new MvvmCommand(()=> {
                db.SaveChanges();
                LoadGenres();
            }, ()=> SelectedGenre != null);
        }

        private void LoadGenres()
        {
            Genres = new List<Genre>(db.Genres);
        }
    }
}
