using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using WpfApp34.Tools;

namespace WpfApp34.ViewModels
{
    public class MainVM : MvvmNotify
    {
        private Page currentPage;

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                SignalChanged();
            }
        }
        public MvvmCommand OpenPublisherEditor { get; set; }
        public MvvmCommand OpenGenreEditor { get; set; }
        public MvvmCommand OpenAuthorEditor { get; set; }
        
        public MvvmCommand OpenBookEditor { get; set; }
        public MainVM()
        {
            Pager.RegisterPages();
            Pager.RegisterPageChanger(
                page => { 
                    CurrentPage = Pager.GetPageByType(page);
                });
            OpenPublisherEditor = new MvvmCommand(
                () => {
                    Pager.GoTo(PageType.PublisherEditor); 
                });
            OpenGenreEditor = new MvvmCommand(
                () => {
                    Pager.GoTo(PageType.GenreEditor);
                });
            OpenAuthorEditor = new MvvmCommand(
                () => {
                    Pager.GoTo(PageType.AuthorEditor);
                });

            OpenBookEditor = new MvvmCommand(() => { Pager.GoTo(PageType.BookList); });
        }
    }
}
