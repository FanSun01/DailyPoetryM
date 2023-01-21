using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DailyPoetryM.Models
{
    [Table(name: "works")]
    public class Poetry
    {
        [Column(name: "id")]
        public int Id { get; set; }

        [Column(name: "name")]
        public string Name { get; set; } = string.Empty;

        [Column(name: "author_name")]
        public string Author { get; set; } = string.Empty;

        [Column(name: "dynasty")]
        public string Dynasty { get; set; } = string.Empty;

        [Column(name: "Content")]
        public string Content { get; set; } = string.Empty;

        private string _snippet;

        [Ignore]
        public string Snippet => _snippet ??= Content.Split('。')[0];

    }
}