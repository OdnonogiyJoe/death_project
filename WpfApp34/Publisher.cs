using System.Collections.Generic;

namespace WpfApp34
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; } // это фу
        public List<Book> Books { get; set; }
    }
}