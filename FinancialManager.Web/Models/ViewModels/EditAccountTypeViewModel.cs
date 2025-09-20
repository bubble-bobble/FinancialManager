using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.Web.Models.ViewModels;

public class EditAccountTypeViewModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Remote(controller: "AccountTypes", action: "ValidateAccountTypeName")]
    public string Name { get; set; }
}