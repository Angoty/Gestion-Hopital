namespace hopital.Models;
using Microsoft.Data.SqlClient;

public class Personne{
    private int _idPersonne;
    private string? _nom;
    private string? _prenom;
    private DateTime _dateNaissance;
    private Genre? _genre;
    private Contact? _contact;
    private Lieu? _lieuNaissance;
    private Lieu? _adresse;



    public int idPersonne{
         get{ return _idPersonne; } 
        set{ 
            if(value<0){
                throw new Exception("idPersonne invalide");
            }
            _idPersonne=value;
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
    public DateTime dateNaissance{
        get{ return _dateNaissance; } 
        set{
            if(value.Date>DateTime.Today.Date){
                throw new Exception("Votre date de naissance n'est pas valide");
            }
            _dateNaissance=value;
        } 
    }
    public Genre? genre{
        get{ return _genre; } 
        set{
            if(value==null){
                throw new Exception("Vous devez donnez votre genre");
            }
            _genre=value;
        }
    }
    public Contact? contact{
        get{ return _contact; } 
        set{
            if(value==null){
                throw new Exception("Vous devez donnez votre contact");
            }
            _contact=value;
        }
    }
    public Lieu? lieuNaissance{
        get{ return _lieuNaissance; } 
        set{
            if(value==null){
                throw new Exception("Vous devez donnez votre lieu de naissance");
            }
            _lieuNaissance=value;
        }
    }
    public Lieu? adresse{
        get{ return _adresse; } 
        set{
            if(value==null){
                throw new Exception("Vous devez donnez votre adresse");
            }
            _adresse=value;
        }
    }

    public Personne[]? antecedents{get; set;}

    public Personne(){}
    public Personne(int id, string? nom, string prenom, DateTime date, Genre g, Contact c , Lieu l, Lieu a){
        this.idPersonne=id;
        this.nom=nom;
        this.prenom=prenom;
        this.dateNaissance=date;
        this.genre=g;
        this.contact=c;
        this.lieuNaissance=l;
        this.adresse=a;
    }

    public void insert(string nom, string prenom, string date, string g, Contact c, string l, string a, string image, SqlConnection con){
        bool estValid=true;
        if(con==null){
            Connect connect=new Connect();
            con=connect.connectDB();
            estValid=false;
        }
        try{
            string query="INSERT INTO Personne(nom, prenom, date_naissance, id_genre, id_contact, id_lieu, id_adresse,  id_profil, image) VALUES('"+nom+"','"+prenom+"','"+date+"', "+g+", "+c.idContact+","+l+","+a+", "+"2, '"+image+"')";
            Console.WriteLine(query);
            SqlCommand  cmd=new SqlCommand(query,con);
            cmd.ExecuteNonQuery();
        }catch(Exception e){
            throw e;
        }finally{
            if(estValid==false){
                con.Close();
            }  
        }             
    }
    

    public List<Personne> getAll(SqlConnection con){
        bool estValid = false;
        Personne  personne=null;
        List<Personne> personnes = new List<Personne>();
        try{
            if(con==null){
                estValid=true;
                Connect c=new Connect();
                con=c.connectDB();
            }
            string query="SELECT * FROM personne where id_profil=2";
            SqlCommand cmd=new SqlCommand(query,con);
            SqlDataReader reader=cmd.ExecuteReader();
            Console.WriteLine(query);
            while(reader.Read()){
                int idPersonne=reader.GetInt32(0);
                string  nom=reader.GetString(1);
                string prenom=reader.GetString(2);
                DateTime date= DateTime.Parse(reader["date_naissance"].ToString());
                int idGenre = reader.GetInt32(4);
                int idContact = reader.GetInt32(5);
                int idLieu = reader.GetInt32(6);
                int id_adresse = reader.GetInt32(7);
                Console.WriteLine("bb");
                personne =new Personne(idPersonne,  nom, prenom, date, new Genre().getById(idGenre), new Contact().getById(idContact), new Lieu().getById(idLieu), new Lieu().getById(idLieu));
                Console.WriteLine("aa");
                Console.WriteLine(personne.idPersonne);
                personnes.Add(personne);
            }
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return personnes;
    }

    public List<Personne> GetPaginatedList(List<Personne> personnes, int page, int pageSize)
    {
        int startIndex = (page - 1) * pageSize;
        int count = Math.Min(pageSize, personnes.Count - startIndex);

        return personnes.GetRange(startIndex, count);
    }

    public int GetTotalCount(List<Personne> personnes)
    {
        return personnes.Count;
    }
}