using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WypozyczalniaFilmow.Models
{
    public class Film
    {
        [Key] //Identity i tworzenie primary key


        //atrybuty
        public int FilmID { get; set; }
        [DisplayName("Movie Name")]
        [Required(ErrorMessage = "Movie name is required!")]
        [StringLength(50, ErrorMessage = "Movie name no to be exceeded!")]
        [RegularExpression(@"^[a-zA-Z0-9ĄĆĘŁŃÓŚŹŻąćęłńóśźż'''-'-\s#$%@]*$", ErrorMessage = "Use correct form!")]
        public string Name { get; set; }
        [DisplayName("Movie Genre")]
        [Required(ErrorMessage = "Movie genre is required!")]
        [StringLength(20, ErrorMessage = "Movie genre no to be exceeded!")]

        public string Genre { get; set; }
        [DisplayName("Movie Director")]
        [Required(ErrorMessage = "Movie director is required!")]
        [StringLength(30, ErrorMessage = "Movie director no to be exceeded!")]
        [RegularExpression(@"^[a-zA-Z0-9ĄĆĘŁŃÓŚŹŻąćęłńóśźż'''-'\s]*$", ErrorMessage = "Use correct form!")]
        public string Director { get; set; }
        [DisplayName("Movie Availability")]
        [Required(ErrorMessage = "Movie availability information is required!")]
        public string Available { get; set; }


     
    }

}
