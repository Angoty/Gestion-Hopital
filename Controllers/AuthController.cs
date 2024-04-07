using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Hopital.Models;

namespace Hopital.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    public IActionResult LoginAdmin()
    {
        ViewBag.Layout = "_LayoutLogin";
        if (TempData.ContainsKey("ErrorMessage"))
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
        }
        return View();
    }
     public IActionResult LoginPersonnel()
    {
        ViewBag.Layout = "_LayoutLogin";
        return View();
    }

    [HttpPost]
    public IActionResult VerifyAdmin(IFormCollection form){
        string? email=form["email"];
        string? mdp=form["password"];
        Console.WriteLine("ici");
        Contact c = Contact.getByString(email);
        try{
            Admin a = new Admin{contact=c, motDePasse=mdp};
            a=a.check();
            if (a!=null){
                var str = JsonConvert.SerializeObject(a);
                HttpContext.Session.SetString("userAdmin", str);
            }
            else{
                throw new Exception("Identifiants incorrects");
            }
        }catch(Exception e){
            TempData["ErrorMessage"]=e.Message;
            return RedirectToAction("LoginAdmin","Auth");    
        }
        return RedirectToAction("Index","Home");
    }
    [HttpPost]
    public IActionResult VerifyPersonnel(IFormCollection form){
        string? email=form["email"];
        string? mdp=form["password"];
        Contact c = new Contact{email=email};
        Candidat a = new Candidat{contact=c};
        Personnel p = new Personnel{candidat=a, motDePasse=mdp};
        try
        {
            p=p.GetPersonnel(null);
            if (a!=null){
                var str = JsonConvert.SerializeObject(a);
                HttpContext.Session.SetString("userAdmin",str);
            }else{
                throw new Exception("Identifiants incorrects.");
            }
        }catch(Exception exe){
            ViewBag.ErrorMessage= exe.Message;
            return RedirectToAction("LoginAdmin","Auth");    
        }
        return RedirectToAction("Index","Home");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
