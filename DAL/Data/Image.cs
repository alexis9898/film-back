using System.Collections.Generic;

namespace DAL.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int FilmId { get; set; }
        public Film Films { get; set; }
    }
}
