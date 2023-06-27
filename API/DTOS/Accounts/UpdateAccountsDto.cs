﻿using API.Utilities;
using System.ComponentModel.DataAnnotations;

namespace API.DTOS.Accounts
{
    public class UpdateAccountsDto
    {
        [Required]
        public Guid Guid { get; set; }
        [PasswordPolicy]
        public string Password { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public bool IsUsed { get; set; }
        [Required]
        public DateTime ExpiredDate { get; set; }
    }
}
