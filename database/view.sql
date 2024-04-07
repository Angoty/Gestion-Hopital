CREATE OR REPLACE VIEW v_province_lieu AS 
    SELECT p.*, l.id_lieu, (l.intitule) lieu
        FROM Province p JOIN Lieu l ON p.id_province=l.id_province;
    
CREATE OR REPLACE VIEW v_contact_admin AS  
    SELECT c.*
        FROM Contact c JOIN Admin a ON c.id_contact=a.id_contact;
                       

CREATE OR REPLACE VIEW v_admins AS --ADMIN
    SELECT pa.intitule, a.*, v.email, v.numero
        FROM ProfilAdmins pa JOIN Admin a ON pa.id_profil=a.id_profilD
                             JOIN v_contact_admin v ON a.id_contact=v.id_contact;

CREATE OR REPLACE VIEW v_personnels AS  --PERSONNEL
    SELECT pp.*, p.id_personnel, p.mot_de_passe, c.*
        FROM ProfilPersonnels pp JOIN Personnel p ON pp.id_profil=p.id_profilP
                                 JOIN Candidat c ON p.id_candidat=c.id_candidat;

