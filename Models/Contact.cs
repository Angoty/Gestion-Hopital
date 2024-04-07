using Npgsql;
namespace Hopital.Models;

public class Contact{
    private int _idContact;
    private string _email;
    private string _numero;

    public int idContact{
        get{ return _idContact;} 
        set{
            if (value == null) {
                throw new Exception("La valeur de idContact ne peut pas être nulle.");
            } else if (value < 0) {
                throw new Exception("idContact invalide");
            } else {
                _idContact = value;
            }
        }
    }
    public string email{
         get{ return _email ;} 
        set{
            if(value.Trim().Length==0 || value==null){
                throw new Exception("L'email de la  personne ne doit pas être vide");
            }
            _email=value.Trim();
        } 
    }
    public string numero{
         get{ return _numero ;} 
        set{
            if(value.Trim().Length==0 || value==null){
                throw new Exception("Le numero de la  personne ne doit pas être vide");
            }
            _numero=value.Trim();
        } 
    }

    public Contact(){}
    public Contact(int idContact, string email, string numero){
        this.idContact=idContact;
        this.email=email;
        this.numero=numero;
    }

    public static Contact? getById(int id){
        NpgsqlConnection con=null;
        Contact? c = null;
        bool estValid = false;
        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string query = "SELECT * FROM contact WHERE id_contact="+id;
            Console.WriteLine(query);
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int idContact = reader.GetInt32(0);
                string email = reader.GetString(1);
                string numero=reader.GetString(2);
                c = new Contact(idContact, email, numero);
            }
             reader.Close();
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return c;
    }

    public static Contact? getByString(string email){
        NpgsqlConnection con=null;
        Contact? c = null;
        bool estValid = false;
        try{
            if(con==null){
                estValid=true;
                con=Connect.connectDB();
            }
            string query = "SELECT * FROM contact WHERE email='"+email+"'";
            Console.WriteLine(query);
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int idContact = reader.GetInt32(0);
                string email1 = reader.GetString(1);
                string numero1=reader.GetString(2);
                c = new Contact(idContact, email1, numero1);
            }
             reader.Close();
        }catch (Exception e){
            throw e; 
        }finally{
            if(estValid) con.Close();
        }
        return c;
    }
    
}
