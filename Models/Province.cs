using Npgsql;
namespace Hopital.Models;

public class Province{
    private int _idProvince;
    private string? _intitule;

    public int idProvince{
        get{ return _idProvince;} 
        set{
            if (value < 0) {
                throw new Exception("idProvince invalide");
            }
            _idProvince = value;
        }
    }
    public string intitule{
        get{
            if (_intitule == null){
                throw new Exception("La propriété intitule de la province n'a pas été initialisée.");
            }
            return _intitule;
        }set{
            if (value == null || value.Trim().Length == 0){
                throw new Exception("L'intitulé de la Province est invalide.");
            }
            _intitule = value.Trim();
        }
    }
    
    public Province(){}
    public Province(int idProvince, string intitule){
        this.idProvince=idProvince;
        this.intitule=intitule;
    }
}