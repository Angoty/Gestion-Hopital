using Npgsql;
namespace Hopital.Models;

public class Candidat{
    private int _idCandidat;
    private string? _nom;
    private string? _prenom;
    private DateTime _dateNaissance;
    private Genre? _genre;
    private Lieu? _lieuNaissance;
    private Lieu? _adresse;
    private Contact? _contact;

    public enum Genre
    {
        Homme,
        Femme,
        Autre
    }
    public int idCandidat {
        get { return _idCandidat; } 
        set { 
            if (value == null) {
                throw new Exception("La valeur de idCandidat ne peut pas être nulle.");
            } else if (value < 0) {
                throw new Exception("idCandidat invalide");
            } else {
                _idCandidat = value;
            }
        } 
    }

     public string? nom {
        get{ return _nom ;} 
        set{
            if(value.Trim().Length==0 || value==null){
                throw new Exception("Le nom du personne ne doit pas être vide");
            }
            _nom=value.Trim();
        } 
    }
    public string? prenom{
        get{ return _prenom ;} 
        set{
            if(value.Trim().Length==0 || value==null){
                throw new Exception("Le prenom ne doit pas être vide");
            }
            _prenom=value.Trim();
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
    public Lieu? lieuNaissance{
       get { return _lieuNaissance; }
        set
        {
            if (value == null){
                throw new Exception("Vous devez donner votre lieu de naissance.");
            }
            else if (!(value is Lieu)){
                throw new Exception("La valeur de lieu de naissance n'est pas du type Lieu.");
            }
            else{
                _lieuNaissance = value;
            }
        }
    }
    public Lieu? adresse{
       get { return _adresse; }
        set
        {
            if (value == null){
                throw new Exception("Vous devez donner votre adresse.");
            }
            else if (!(value is Lieu)){
                throw new Exception("La valeur de votre n'est pas du type Lieu.");
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
                throw new Exception("Vous devez donner votre contact de naissance.");
            }
            else if (!(value is Contact)){
                throw new Exception("La valeur de contact de naissance n'est pas du type contact.");
            }
            else{
                _contact = value;
            }
        }
    }
    public Candidat(){}
    public Candidat(int idCandidat, string nom, string prenom, DateTime dateNaissance, Genre g, Lieu lieu, Lieu adresse, Contact contact){
        this.idCandidat=idCandidat;
        this.nom=nom;
        this.prenom=prenom;
        this.dateNaissance=dateNaissance;
        this.genre=g;
        this.lieuNaissance=lieu;
        this.adresse=adresse;
        this.contact=contact;
    }

    public Candidat check(NpgsqlConnection con){
        bool estValid = false;
        Candidat candidat=null;
        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string query="SELECT * FROM v_personnels WHERE id_contact="+this.contact.idContact;
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int idCandidat = reader.GetInt32(reader.GetOrdinal("id_candidat"));
                string nom = reader.GetString(reader.GetOrdinal("nom"));
                string prenom = reader.GetString(reader.GetOrdinal("prenom"));
                DateTime date = reader.GetDateTime(reader.GetOrdinal("date_naissance")).Date;
                int idGenre = reader.GetInt32(reader.GetOrdinal("id_genre"));
                int idAdresse = reader.GetInt32(reader.GetOrdinal("id_adresse"));
                int idLieu = reader.GetInt32(reader.GetOrdinal("id_lieu"));
                int idContact=reader.GetInt32(reader.GetOrdinal("id_contact"));
                Candidat.Genre g = (Candidat.Genre)idGenre;
                Lieu l = Lieu.getById(idLieu);
                Lieu adresse= Lieu.getById(idAdresse);
                Contact c= Contact.getById(idContact);
                candidat = new Candidat(idCandidat, nom, prenom, date, g, l, adresse, c);
            }
            reader.Close();
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return candidat;
    }
}