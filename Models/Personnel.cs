using Npgsql;
namespace Hopital.Models;

public class Personnel{
    private string? _idPersonnel;
    private string? _motDePasse;
    private Candidat? _candidat;
    private Profil? _profil;


     public string? idPersonnel {
        get{
            if (_idPersonnel == null){
                throw new Exception("La propriété idPersonnel du personnel n'a pas été initialisée.");
            }
            return _idPersonnel;
        }set{
            if (value == null || value.Trim().Length == 0){
                throw new Exception("Le idPersonnel du personnel est invalide.");
            }
            _idPersonnel = value.Trim();
        }
    }
    public string? motDePasse{
       get{
            if (_motDePasse == null){
                throw new Exception("La propriété mot de passe du personnel n'a pas été initialisée.");
            }
            return _motDePasse;
        }set{
            if (value == null || value.Trim().Length == 0){
                throw new Exception("Le motDePasse du personnel est invalide.");
            }
            _motDePasse = value.Trim();
        } 
    }
    public Candidat? candidat{
       get { return _candidat; }
        set
        {
            if (value == null){
                throw new Exception("Vous devez donner votre candidat de naissance.");
            }
            else if (!(value is Candidat)){
                throw new Exception("La valeur de candidat de naissance n'est pas du type candidat.");
            }
            else{
                _candidat = value;
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
                throw new Exception("La valeur de profil de naissance n'est pas du type profil.");
            }
            else{
                _profil = value;
            }
        }
    }
    public Personnel(){}
    public Personnel(string id, string mdp, Candidat c, Profil p){
        this.idPersonnel=id;
        this.motDePasse=mdp;
        this.candidat=c;
        this.profil=p;
    }

    public List<Personnel> getAll(NpgsqlConnection con){
        bool estValid = false;
        Personnel?  personnel=null;
        List<Personnel> personnels = new List<Personnel>();
        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string query="SELECT * FROM v_personnels WHERE mot_de_passe='"+this.motDePasse+"'";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string idPersonnel = reader.GetString(reader.GetOrdinal("id_personnel"));
                int idCandidat = reader.GetInt32(reader.GetOrdinal("id_candidat"));
                string nom = reader.GetString(reader.GetOrdinal("nom"));
                string prenom = reader.GetString(reader.GetOrdinal("prenom"));
                DateTime date = reader.GetDateTime(reader.GetOrdinal("date_naissance")).Date;
                int idLieuNaissance = reader.GetInt32(reader.GetOrdinal("id_lieu_naissance"));
                int idGenre = reader.GetInt32(reader.GetOrdinal("id_genre"));
                int idAdresse = reader.GetInt32(reader.GetOrdinal("id_adresse"));
                int idLieu = reader.GetInt32(reader.GetOrdinal("id_lieu"));
                int idContact=reader.GetInt32(reader.GetOrdinal("id_contact"));
                int idProfil = reader.GetInt32(reader.GetOrdinal("id_profil"));
                string intitule = reader.GetString(reader.GetOrdinal("intitule"));
                string motDePasse= reader.GetString(reader.GetOrdinal("mot_de_passe"));
                Candidat.Genre g = (Candidat.Genre)idGenre;
                Lieu? l = Lieu.getById(idLieu);
                Lieu? adresse= Lieu.getById(idAdresse);
                Contact? c= Contact.getById(idContact);
                Candidat? candidat = new Candidat(idCandidat, nom, prenom, date, g, l, adresse, c);
                personnel = new Personnel(idPersonnel, motDePasse, candidat, new Profil(idProfil, intitule));
                personnels.Add(personnel);
            }
            reader.Close();
        }catch (Exception ex){
            throw ex; 
        }finally{
            if(estValid) con.Close();
        }
        return personnels;
    }

    public Personnel GetPersonnel(NpgsqlConnection? con){
        bool estValid = false;
        Personnel?  personnel=null;
        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string query="SELECT * FROM v_personnels WHERE id_candidat="+this.candidat.idCandidat;
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string idPersonnel = reader.GetString(reader.GetOrdinal("id_personnel"));
                int idCandidat = reader.GetInt32(reader.GetOrdinal("id_candidat"));
                string nom = reader.GetString(reader.GetOrdinal("nom"));
                string prenom = reader.GetString(reader.GetOrdinal("prenom"));
                DateTime date = reader.GetDateTime(reader.GetOrdinal("date_naissance")).Date;
                int idLieuNaissance = reader.GetInt32(reader.GetOrdinal("id_lieu_naissance"));
                int idGenre = reader.GetInt32(reader.GetOrdinal("id_genre"));
                int idAdresse = reader.GetInt32(reader.GetOrdinal("id_adresse"));
                int idLieu = reader.GetInt32(reader.GetOrdinal("id_lieu"));
                int idContact=reader.GetInt32(reader.GetOrdinal("id_contact"));
                int idProfil = reader.GetInt32(reader.GetOrdinal("id_profil"));
                string intitule = reader.GetString(reader.GetOrdinal("intitule"));
                string mdp=reader.GetString(reader.GetOrdinal("mot_de_passe"));
                Candidat.Genre g = (Candidat.Genre)idGenre;
                Lieu? l = Lieu.getById(idLieu);
                Lieu? adresse= Lieu.getById(idAdresse);
                Contact? c= Contact.getById(idContact);
                Candidat? candidat = new Candidat(idCandidat, nom, prenom, date, g, l, adresse, c);
                personnel = new Personnel(idPersonnel,  mdp, candidat, new Profil(idProfil, intitule));
            }
            reader.Close();
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return personnel;
    }

}