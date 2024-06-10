using Microsoft.Data.SqlClient;
namespace hopital.Models;

public class Profil{
    public int idProfil{get; set;}
    public string intitule{get; set;}
    public Profil(){}
    public Profil(int idProfil, string i){
        this.idProfil=idProfil;
        this.intitule=i;
    }

    public Profil getById(int idProfil){
        bool estValid = false;
        SqlConnection con = null;
        Profil  profil=null;
        try{
            if(con==null){
                estValid=true;
                Connect c=new Connect();
                con=c.connectDB();
            }
            string query="SELECT * FROM profil WHERE id_profil="+idProfil;
            SqlCommand cmd=new SqlCommand(query,con);
            SqlDataReader reader=cmd.ExecuteReader();
            while(reader.Read()){
                int id=reader.GetInt32(0);
                string intitule=reader.GetString(2);
                profil =new Profil(id,  intitule);
            }
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return profil;
    }
}