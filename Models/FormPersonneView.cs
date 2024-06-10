using hopital.Models;

namespace hopital.Models;
public class FormPersonneView {
    public List<Genre> genres { get; set; }
    public List<Lieu> lieux { get; set; }

    public FormPersonneView(List<Genre> genres, List<Lieu> lieux){
        this.genres=genres;
        this.lieux=lieux;
    }
}