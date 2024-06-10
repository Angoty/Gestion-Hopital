using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Hopital.Models;
using hopital.Models;
using DinkToPdf.Contracts;
using Aspose.Pdf;
using Aspose.Pdf.Text;




namespace Hopital.Controllers;

public class PatientController : Controller
{
    private readonly ILogger<PatientController> _logger;
    private readonly ICompositeViewEngine _viewEngine;
    private readonly IConverter _pdfConverter;


    public PatientController(ILogger<PatientController> logger, ICompositeViewEngine viewEngine, IConverter pdfConverter)
    {
        _logger = logger;
        _viewEngine = viewEngine;
        _pdfConverter = pdfConverter ?? throw new ArgumentNullException(nameof(pdfConverter));
    }

    public IActionResult FormPatient()
    {
        Genre g = new Genre();
        Lieu l=new Lieu();
        List<Genre> genres= g.getAll(null);
        List<Lieu> lieux = l.getAll(null);
        var infos= new FormPersonneView(genres, lieux);
        return View(infos);
    }

    [HttpPost]
    public ActionResult GetInfo(IFormCollection form, IFormFile image){
        string? nom=form["nom"];
        string? prenom=form["prenom"];
        string? dateNaissance=form["dateNaissance"];
        string? lieuNaissance=form["lieuNaissance"];
        string? genre=form["genre"];
        string? adresse=form["adresse"];
        string? email=form["email"];
        string? numero=form["numero"];
        Console.WriteLine("adresse: "+adresse);
        Contact c = new Contact();
        c.insert(email, numero, null);
         string uploadfile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads");
        if (!Directory.Exists(uploadfile))
        {
            Directory.CreateDirectory(uploadfile);
        }
        string nom_file = Path.GetFileName(image.FileName);
        string filePath = Path.Combine(uploadfile, nom_file);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            image.CopyTo(stream);
        }    
        Personne p = new Personne();
        p.insert(nom, prenom, dateNaissance, genre, c.getLastContact(null), lieuNaissance, adresse, nom_file, null);       
        return RedirectToAction("FormPatient","Patient");    
    }
    public IActionResult Liste(int page = 1, int pageSize = 5)
    {
        Personne personne = new Personne();
        List<Personne> liste = personne.GetPaginatedList(personne.getAll(null), page, pageSize);
        int totalCount = personne.GetTotalCount(liste);
        Console.WriteLine("total: "+personne.GetTotalCount(liste));
        int totalPages = (int)Math.Ceiling((double) totalCount / pageSize);
        Console.WriteLine("page: "+totalPages);
        var model = new PaginatedListViewModel<Personne>(liste, totalCount, page, pageSize); 
        return View(model);
    }
    public IActionResult Pdf(int page, int pageSize)
    {
        // Récupérer les données pour générer le PDF
        Personne personne = new Personne();
        List<Personne> personnes = personne.GetPaginatedList(personne.getAll(null), page, pageSize);

        // Crée un document PDF
        Document document = new Document();

        // Ajoute une nouvelle page au document
        Page pdfPage = document.Pages.Add();

        // Crée une nouvelle table
        Aspose.Pdf.Table table = new Aspose.Pdf.Table();
        table.ColumnWidths = "100 100 100 100 100"; // Définir la largeur des colonnes
        table.DefaultCellPadding = new MarginInfo(1, 1, 1, 1);

        // Ajouter les en-têtes de colonne à la première ligne du tableau
        Aspose.Pdf.Row headingRow = table.Rows.Add();
        headingRow.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray); // Définir la couleur de fond de l'en-tête
        headingRow.FixedRowHeight = 20; // Définir la hauteur de la ligne d'en-tête
        headingRow.DefaultCellTextState.FontStyle = FontStyles.Bold; // Mettre en gras le texte de l'en-tête
        headingRow.Cells.Add("Nom");
        headingRow.Cells.Add("Prénom");
        headingRow.Cells.Add("Adresse");
        headingRow.Cells.Add("Contact");
        headingRow.Cells.Add("Total Facture");
        table.DefaultCellTextState.FontSize = 8;

        // Ajouter les données de votre modèle à la table
        foreach(var p in personnes)
        {
            // Crée une nouvelle ligne dans la table
            Aspose.Pdf.Row row = table.Rows.Add();

            // Ajoute les données de chaque p en tant que cellules dans la ligne
            row.Cells.Add(p.nom);
            row.Cells.Add(p.prenom);
            row.Cells.Add(p.adresse.intitule);
            row.Cells.Add(p.contact.numero + " " + p.contact.email);
            row.Cells.Add("0");

            // Ajoutez ici les boutons d'édition et de suppression si nécessaire
        }

        // Ajoute la table à la page du PDF
        pdfPage.Paragraphs.Add(table);

        // Enregistre le document PDF
        string filePath = @"F:\Angoty\Itu\L3\S6\Mme Baovola\Gestion Hopital\Application\Hopital\wwwroot\pdf\pdfGenerated-PDF.pdf";
        document.Save(filePath);

        // Retourne un fichier PDF en tant que réponse
        byte[] pdfBytes = System.IO.File.ReadAllBytes(filePath);
        return File(pdfBytes, "application/pdf", "Generated-PDF.pdf");
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






