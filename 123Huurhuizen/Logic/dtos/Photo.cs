using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.dtos
{
    public class Photo
    {
        public int Id { get; set; }
        public string PhotoLink { get; set; }
        public Photo(int id, string photolink)
        {
            Id = id;
            PhotoLink = photolink;
        }
    }
}
