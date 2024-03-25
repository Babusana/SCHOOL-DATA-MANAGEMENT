using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APP.Models;

public partial class Auth
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; } = null!;
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
