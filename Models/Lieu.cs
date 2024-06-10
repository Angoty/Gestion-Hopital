using Microsoft.Data.SqlClient;
namespace hopital.Models;

public class Lieu{
    public int idLieu{get; set;}
    public string intitule{get; set;}
    public Province province{get; set;}
    public Lieu(){}
    public Lieu(int idLieu, string i, Province p){
        this.idLieu=idLieu;
        this.intitule=i;
        this.province=p;
    }

    public List<Lieu> getAll(SqlConnection con){
        bool estValid = false;
        Lieu  lieu=null;
        List<Lieu> lieux = new List<Lieu>();
        try{
            if(con==null){
                estValid=true;
                Connect c=new Connect();
                con=c.connectDB();
            }
            string query="SELECT * FROM v_province_lieu";
            SqlCommand cmd=new SqlCommand(query,con);
            SqlDataReader reader=cmd.ExecuteReader();
            Console.WriteLine(query);
            while(reader.Read()){
                int idProvince=reader.GetInt32(0);
                string  intitule=reader.GetString(1);
                int idLieu=reader.GetInt32(2);
                string desc=reader.GetString(3);
                Province p = new Province();
                p=p.getById(idProvince);
                lieu =new Lieu(idLieu,  desc, p);
                lieux.Add(lieu);
            }
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return lieux;
    }
    public Lieu getById(int idLieu){
        bool estValid = false;
        SqlConnection con = null;
        Lieu  lieu=null;
        try{
            if(con==null){
                estValid=true;
                Connect c=new Connect();
                con=c.connectDB();
            }
            string query="SELECT * FROM Lieu WHERE id_lieu="+idLieu;
            SqlCommand cmd=new SqlCommand(query,con);
            SqlDataReader reader=cmd.ExecuteReader();
            while(reader.Read()){
                int id=reader.GetInt32(0);
                int  idProvince=reader.GetInt32(1);
                string intitule=reader.GetString(2);
                Province p = new Province();
                p=p.getById(idProvince);
                lieu =new Lieu(id,  intitule, p);
            }
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return lieu;
    }
}