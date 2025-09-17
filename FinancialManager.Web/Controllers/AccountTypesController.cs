using System.Threading.Tasks;
using AutoMapper;
using FinancialManager.Web.Models.Entities;
using FinancialManager.Web.Models.ViewModels;
using FinancialManager.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.Web.Controllers;

public class AccountTypesController(IAccountTypesRepository accountTypesRepository, IMapper mapper) : Controller
{
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAccountTypeViewModel createAccountTypeViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(createAccountTypeViewModel);
        }
        
        createAccountTypeViewModel.Sequence = 1;
        createAccountTypeViewModel.UserId = 1;
        var accountType = mapper.Map<AccountType>(createAccountTypeViewModel);
        await accountTypesRepository.InsertAccountType(accountType);
        return RedirectToAction("Index", "AccountTypes");
    }
    
    public async Task<IActionResult> ValidateAccountTypeName(string name)
    {
        var exist = await accountTypesRepository.SelectIfExistAccountType(name, 1);
        return exist ? Json($"Account type {name} already exists.") : Json(true);
    }
}