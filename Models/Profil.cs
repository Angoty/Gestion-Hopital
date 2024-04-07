using Npgsql;
namespace Hopital.Models;

public class Profil{
    private int _idProfil;
    private string? _intitule;

    public int idProfil{
         get{ return _idProfil;} 
        set{
            if (value < 0) {
                throw new Exception("idP_idProfil invalide");
            }
            _idProfil = value;
        }
    }
    public string intitule{
        get{
            if (_intitule == null){
                throw new Exception("La propriété intitule du profil n'a pas été initialisée.");
            }
            return _intitule;
        }set{
            if (value == null || value.Trim().Length == 0){
                throw new Exception("L'intitulé du profil est invalide.");
            }
            _intitule = value.Trim();
        }
    }
    public Profil(){}
    public Profil(int idProfil, string intitule){
        this.idProfil=idProfil;
        this.intitule=intitule;
    }

    public static Profil? getById(int? id){
        if (id == null)
        {
            throw new Exception("L'identifiant du profil ne peut pas être null.");
        }
        NpgsqlConnection? con=null;
        Profil? p = null;
        bool estValid = false;
        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string sql = "SELECT * FROM profiladmins WHERE id_profil="+id;
            Console.WriteLine(sql);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int idProfil = reader.GetInt32(0);
                string intitule=reader.GetString(1);
                p = new Profil(idProfil, intitule);
            }
             reader.Close();
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid==true) con.Close();
        }
        return p;
    }
}