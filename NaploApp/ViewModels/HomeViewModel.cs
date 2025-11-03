using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using NaploApp.Data;
using NaploApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaploApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly AppDbContext _db;

        public ObservableCollection<Post> Posts { get; } = new();

        public HomeViewModel(AppDbContext db)
        {
            _db = db;
        }

        public async Task LoadAsync()
        {
            var items = await _db.Posts
                .AsNoTracking()
                .OrderByDescending(p => p.Date)
                .ToListAsync();

            Posts.Clear();
            foreach (var p in items)
                Posts.Add(p);
        }
    }
}
