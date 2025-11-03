using NaploApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NaploApp.ViewModels
{
    internal class CreateViewModel : INotifyPropertyChanged
    {
        protected void OnPropertyChanged([CallerMemberName] string name = "") 
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<Post> Posts { get; set; }

        public Command C_Create { get; set; }

        private Post selectedPost;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Post SelectedPost 
        {
            get { return selectedPost; }
            set
            {
                selectedPost = value;
                OnPropertyChanged();
            }
        }

        private void Create()
        {
            this.SelectedPost = new Post();
        }

        public CreateViewModel() 
        {
            this.Posts = new ObservableCollection<Post>();
            this.SelectedPost = this.Posts[0];
            C_Create = new Command(Create);
        }
    }
}
