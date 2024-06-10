using Microsoft.Data.SqlClient;
namespace hopital.Models;

public class Contact{
    public int idContact{get; set;}
    public string email{get; set;}
    public string numero{get; set;}

    public Contact(){}
    public Contact(int idContact, string email, string numero){
        this.idContact=idContact;
        this.email=email;
        this.numero=numero;
    }

    public void insert(string email, string numero, SqlConnection con){
        bool estValid=true;
        if(con==null){
            Connect c=new Connect();
            con=c.connectDB();
            estValid=false;
        }
        try{
            string query="INSERT INTO Contact(email, numero) VALUES('"+email+"', '"+numero+"')";
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
     public Contact getById(int idContact){
        bool estValid = false;
        SqlConnection con = null;
        Contact  contact=null;
        try{
            if(con==null){
                estValid=true;
                Connect c=new Connect();
                con=c.connectDB();
            }
            string query="SELECT * FROM Contact WHERE id_contact="+idContact;
            SqlCommand cmd=new SqlCommand(query,con);
            SqlDataReader reader=cmd.ExecuteReader();
            while(reader.Read()){
                int id=reader.GetInt32(0);
                string email=reader.GetString(1);
                string numero=reader.GetString(2);
                contact =new Contact(id,  email, numero);
            }
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return contact;
    }

    public Contact getLastContact(SqlConnection  con){
        bool estValid = false;
        Contact contact = new Contact();
        try{
            if(con==null){
                estValid=true;
                Connect c=new Connect();
                con=c.connectDB();
            }
            string query="SELECT TOP 1 * FROM contact ORDER BY id_contact DESC";
            SqlCommand cmd=new SqlCommand(query,con);
            SqlDataReader reader=cmd.ExecuteReader();
            while(reader.Read()){
                int idContact=reader.GetInt32(0);
                string email=reader.GetString(1);
                string numero=reader.GetString(2);
                contact =new Contact(idContact, email, numero);
            }
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return contact;
    }

}
