using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp34
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
