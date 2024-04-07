using Npgsql;
namespace Hopital.Models;

public class Admin{
    private string _idAdmin;
    private string? _nom;
    private string? _prenom;
    private DateTime _dateNaissance;
    private string? _motDePasse;
    private Genre? _genre;
    private Lieu? _adresse;
    private Contact? _contact;
    private Profil? _profil;

    public enum Genre
    {
        Homme,
        Femme,
        Autre
    }
    public string idAdmin {
        get{
            if (_idAdmin == null){
                throw new Exception("La propriété idAdmin de l'admin n'a pas été initialisée.");
            }
            return _idAdmin;
        }set{
            if (value == null || value.Trim().Length == 0){
                throw new Exception("L'id de l'admin est invalide.");
            }
            _idAdmin = value.Trim();
        }
    }

     public string? nom {
       get{
            if (_nom == null){
                throw new Exception("La propriété nom de l'admin n'a pas été initialisée.");
            }
            return _nom;
        }set{
            if (value == null || value.Trim().Length == 0){
                throw new Exception("Le nom de l'admin est invalide.");
            }
            _nom = value.Trim();
        }
    }
    public string? prenom{
        get{
            if (_prenom == null){
                throw new Exception("La propriété prenom de l'admin n'a pas été initialisée.");
            }
            return _prenom;
        }set{
            if (value == null || value.Trim().Length == 0){
                throw new Exception("Le prenom de l'admin est invalide.");
            }
            _prenom = value.Trim();
        }
    }
    public DateTime dateNaissance {
        get { return _dateNaissance; } 
        set {
            if (value == DateTime.MinValue) {
                _dateNaissance = DateTime.Today;
            } else if (value.Date > DateTime.Today) {
                throw new Exception("Votre date de naissance n'est pas valide");
            } else {
                _dateNaissance = value;
            }
        } 
    }
    public string? motDePasse{
        get{ return _motDePasse ;} 
        set{
            if(value.Trim().Length==0 || value==null){
                throw new Exception("Le champ mot de passe est requise");
            }
            _motDePasse=value.Trim();
        } 
    }
    public Genre? genre{
        get { return _genre; }
        set
        {
            if (value == null){
                throw new Exception("Vous devez donner votre genre.");
            }
            else if (!Enum.IsDefined(typeof(Genre), value)){
                throw new Exception("La valeur de genre n'est pas valide.");
            }
            else{
                _genre = value;
            }
        }
    }
    public Lieu? adresse{
       get { return _adresse; }
        set
        {
            if (value == null){
                throw new Exception("Vous devez donner votre lieu de naissance.");
            }
            else if (!(value is Lieu)){
                throw new Exception("La valeur de lieu de naissance n'est pas du type Lieu.");
            }
            else{
                _adresse = value;
            }
        }
    }
    public Contact? contact{
       get { return _contact; }
        set
        {
            if (value == null){
                throw new Exception("Le champ email est requise.");
            }
            else if (!(value is Contact)){
                throw new Exception("La valeur de contact  n'est pas du type contact.");
            }
            else{
                _contact = value;
            }
        }
    }
     public Profil? profil{
       get { return _profil; }
        set
        {
            if (value == null){
                throw new Exception("Vous devez donner votre profil de naissance.");
            }
            else if (!(value is Profil)){
                throw new Exception("La valeur de profil  n'est pas du type profil.");
            }
            else{
                _profil = value;
            }
        }
    }

    public Admin(){}
    public Admin(string id, string? nom, string prenom, DateTime date, string mdp,  Genre g, Contact c , Lieu l, Profil p){
        this.idAdmin=id;
        this.nom=nom;
        this.prenom=prenom;
        this.dateNaissance=date;
        this.motDePasse=mdp;
        this.genre=g;
        this.contact=c;
        this.adresse=l;
        this.profil=p;
    }

    public List<Admin> getAll(NpgsqlConnection con){
        bool estValid = false;
        Admin  admin=null;
        List<Admin> admins = new List<Admin>();
        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string query="SELECT * FROM v_admins";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string idAdmin = reader.GetString(0);
                string nom = reader.GetString(1);
                string prenom=reader.GetString(2);
                DateTime date = reader.GetDateTime(3).Date;
                string mdp=reader.GetString(4);
                int idGenre = reader.GetInt32(5);
                int idContact = reader.GetInt32(6);
                int idLieu = reader.GetInt32(7);
                int idProfil = reader.GetInt32(8);
                Admin.Genre g= (Admin.Genre)idGenre;
                admin =new Admin(idAdmin,  nom, prenom, date, mdp, g, Contact.getById(idContact), Lieu.getById(idLieu),  Profil.getById(idProfil));
                admins.Add(admin);
            }
            reader.Close();
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return admins;
    }

    public Admin? check(){
        bool estValid = false;
        NpgsqlConnection con=null;
        Admin  admin=null;
        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string query="SELECT * FROM v_admins WHERE id_contact="+this.contact.idContact+" AND mot_de_passe ='"+this.motDePasse+"'";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            Console.WriteLine(query);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string idAdmin = reader.GetString(reader.GetOrdinal("id_admin"));
                string nom = reader.GetString(reader.GetOrdinal("nom"));
                string prenom = reader.GetString(reader.GetOrdinal("prenom"));
                DateTime date = reader.GetDateTime(reader.GetOrdinal("date_naissance"));
                Console.WriteLine(date);
                string mdp=reader.GetString(reader.GetOrdinal("mot_de_passe"));
                int idGenre = reader.GetInt32(reader.GetOrdinal("id_genre"));
                int idContact=reader.GetInt32(reader.GetOrdinal("id_contact"));
                int idLieu = reader.GetInt32(reader.GetOrdinal("id_adresse"));
                int idProfil = reader.GetInt32(reader.GetOrdinal("id_profild"));
                Admin.Genre g= (Admin.Genre)idGenre;
                admin =new Admin(idAdmin,  nom, prenom, date, mdp, g, Contact.getById(idContact), Lieu.getById(idLieu),  Profil.getById(idProfil));
                Console.WriteLine("adin0:  "+admin);
                Console.WriteLine(admin);
            }
            reader.Close();
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return admin;
    }

}