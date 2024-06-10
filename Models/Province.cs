using Microsoft.Data.SqlClient;
namespace hopital.Models;

public class Province{
    public int idProvince{get; set;}
    public string intitule{get; set;}
    public Province(){}
    public Province(int idProvince, string i){
        this.idProvince=idProvince;
        this.intitule=i;
    }

    public Province getById(int idProvince){
        bool estValid = false;
        SqlConnection con = null;
        Province p =null;
        try{
            if(con==null){
                estValid=true;
                Connect c=new Connect();
                con=c.connectDB();
            }
            string query="SELECT * FROM province WHERE id_province="+idProvince;
            SqlCommand cmd=new SqlCommand(query,con);
            SqlDataReader reader=cmd.ExecuteReader();
            while(reader.Read()){
                int id=reader.GetInt32(0);
                string intitule=reader.GetString(1);
                p = new Province(id, intitule);
            }
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return p;
    }
}