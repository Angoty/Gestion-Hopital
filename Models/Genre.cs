using Microsoft.Data.SqlClient;
namespace hopital.Models;

public class Genre{
    public int idGenre{get; set;}
    public string intitule{get; set;}
    public Genre(){}
    public Genre(int idGenre, string i){
        this.idGenre=idGenre;
        this.intitule=i;
    }

    public List<Genre> getAll(SqlConnection con){
        bool estValid = false;
        List<Genre> genres = new List<Genre>();
        try{
            if(con==null){
                estValid=true;
                Connect c=new Connect();
                con=c.connectDB();
            }
            string query="SELECT * FROM genre";
            SqlCommand cmd=new SqlCommand(query,con);
            SqlDataReader reader=cmd.ExecuteReader();
            while(reader.Read()){
                int id=reader.GetInt32(0);
                string  intitule=reader.GetString(1);
                Genre genre =new Genre(id, intitule);
                genres.Add(genre);
            }
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return genres;
    }
    public Genre getById(int idGenre){
        bool estValid = false;
        SqlConnection con = null;
        Genre  genre=null;
        try{
            if(con==null){
                estValid=true;
                Connect c=new Connect();
                con=c.connectDB();
            }
            string query="SELECT * FROM Genre WHERE id_genre="+idGenre;
            SqlCommand cmd=new SqlCommand(query,con);
            SqlDataReader reader=cmd.ExecuteReader();
            while(reader.Read()){
                int id=reader.GetInt32(0);
                string intitule=reader.GetString(1);
                genre =new Genre(id,  intitule);
            }
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return genre;
    }
}