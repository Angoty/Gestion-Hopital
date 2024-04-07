using Npgsql;
namespace Hopital.Models;

public class Lieu{
    private int _idLieu;
    private string? _intitule;
    private Province? _province;

    public int idLieu{
        get{
            return _idLieu;
        }set{
            if (value<0){
                throw new Exception("L'idLieu invalide.");
            }
            _idLieu = value;
        }
    }
    public string? intitule{
        get{
            if (_intitule == null){
                throw new Exception("La propriété intitule du lieu n'a pas été initialisée.");
            }
            return _intitule;
        }set{
            if (value == null || value.Trim().Length == 0){
                throw new Exception("Le intitule du lieu est invalide.");
            }
            _intitule = value.Trim();
        }
    }
    public Province? province{
       get {  
            if (_province == null){
                throw new Exception("La propriété province du lieu n'a pas été initialisée.");
            }
            return _province;
       }set
        {
            if (value == null){
                throw new Exception("Vous devez donner votre province de naissance.");
            }
            else if (!(value is Province)){
                throw new Exception("La valeur de province  n'est pas du type province.");
            }
            _province = value;
        }
    }
    public Lieu(){}
    public Lieu(int idLieu, string? intitule, Province? p ){
        this.idLieu=idLieu;
        this.intitule=intitule;
        this.province=p;
    }

    public List<Lieu> getAll(NpgsqlConnection con){
        bool estValid = false;
        Lieu  lieu=null;
        List<Lieu> lieux = new List<Lieu>();
        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
             string query="SELECT * FROM v_province_lieu";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int idProvince=reader.GetInt32(0);
                string  intitule=reader.GetString(1);
                int idLieu=reader.GetInt32(2);
                string desc=reader.GetString(3);
                lieu =new Lieu(idLieu,  desc, new Province(idProvince, intitule));
                lieux.Add(lieu);
            }
            reader.Close();

        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return lieux;
    }

    public static Lieu? getById(int id){
        NpgsqlConnection con=null;
        Lieu? l = null;
        bool estValid = false;
        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string query = "SELECT * FROM v_province_lieu WHERE id_Lieu="+id;
            Console.WriteLine(query);
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int idProvince = reader.GetInt32(0);
                string intitule=reader.GetString(1);
                int idLieu = reader.GetInt32(2);
                string lieu = reader.GetString(3);
                l = new Lieu(idLieu, lieu, new Province(idProvince, intitule));
            }
             reader.Close();
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return l;
    }
}
