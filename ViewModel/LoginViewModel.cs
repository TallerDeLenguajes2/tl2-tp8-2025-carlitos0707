using System.ComponentModel.DataAnnotations;
using tl2_tp8_2025_carlitos0707.Models;


public class LoginViewModel
{
    public string Username {get; set; }
    public string Password {get; set; }
    public string ErrorMessage {get; set; }
}