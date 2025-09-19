using System.Threading.Tasks;
using AutoMapper;
using FinancialManager.Web.Models.Entities;
using FinancialManager.Web.Models.ViewModels;
using FinancialManager.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.Web.Controllers;

public class AccountTypesController(
    IAccountTypesRepository accountTypesRepository,
    IMapper mapper,
    IUsersRepository usersRepository) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = usersRepository.SelectUserId();
        var accountTypes = await accountTypesRepository.SelectAccountTypes(userId);
        return View(accountTypes);
    }

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
        createAccountTypeViewModel.UserId = usersRepository.SelectUserId();
        var accountType = mapper.Map<AccountType>(createAccountTypeViewModel);
        await accountTypesRepository.InsertAccountType(accountType);
        return RedirectToAction("Index", "AccountTypes");
    }

    public async Task<IActionResult> ValidateAccountTypeName(string name)
    {
        var userId = usersRepository.SelectUserId();
        var exist = await accountTypesRepository.SelectIfExistAccountType(name, userId);
        return exist ? Json($"Account type {name} already exists.") : Json(true);
    }
}