using System;
using System.Collections.Generic;
using System.Text;
using WpfApp34.Tools;

namespace WpfApp34.ViewModels
{
    public class PublisherEditorVM : MvvmNotify, IVM
    {
        Db db;
        private List<Publisher> publishers;
        private Publisher selectedPublisher;

        public Publisher SelectedPublisher
        {
            get => selectedPublisher;
            set
            {
                selectedPublisher = value;
                SignalChanged();
            }
        }
        public List<Publisher> Publishers
        {
            get => publishers;
            set
            {
                publishers = value;
                SignalChanged();
            }
        }

        public MvvmCommand CreatePublisher { get; set; }
        public MvvmCommand SavePublisher { get; set; }
        public PublisherEditorVM()
        {
            db = Db.GetDb();
            LoadPublishers();
            CreatePublisher = new MvvmCommand(()=> {
                SelectedPublisher = new Publisher();
                db.Publishers.Add(SelectedPublisher);
                db.SaveChanges();
                LoadPublishers();
            });

            SavePublisher = new MvvmCommand(()=> {
                db.SaveChanges();
                LoadPublishers();
            }, () => SelectedPublisher != null);
        }

        private void LoadPublishers()
        {
            Publishers = new List<Publisher>(db.Publishers);
        }
    }
}
