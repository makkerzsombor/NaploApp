using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaploApp.Models
{
    public partial class Post : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private DateTime date;

        // képnek
        [ObservableProperty]
        private string imagestring;
    }
}
