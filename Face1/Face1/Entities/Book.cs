using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Face1.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
