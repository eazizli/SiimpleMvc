﻿using System.ComponentModel.DataAnnotations;

namespace SiimpleMvc.AccountViewModel
{
    public class RegisterVM
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Username { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; }= null!;
        [DataType(DataType.Password)]
        public string Password { get; set; }=null!;
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPasword { get; set; }=null !;

    }
}
