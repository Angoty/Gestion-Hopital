
INSERT INTO ProfilAdmins(intitule) 
                                VALUES('Directeur General'),('Directeur Financier'), 
                                      ('Ressources Humaines');
                    
INSERT INTO ProfilPersonnels(intitule) 
                                VALUES('Medecin'),('Infirmier'),
                                      ('Pharmacien'),('Accueil');
INSERT INTO Genre(intitule)     
                                VALUES('Homme'),('Femme');

INSERT INTO Province(intitule) 
                                VALUES('Antananarivo'),('Fianarantsoa'),('Mahajanga'),
                                      ('Toamasina'),('Toliara'),('Antsiranana');
INSERT INTO Lieu(id_province, intitule) 
                                VALUES(1, 'Itaosy'),(1, 'Mahazoarivo'),
                                      (1, 'Fenoarivo'), (2, '67Ha'), 
                                      (2, 'Analakely'), (2, 'Ampefiloha'),
                                      (3, 'Mahamasina'), (3, 'Ambanidia'), 
                                      (3, 'Ambohimanarina'), (4, 'Ampasampito'), 
                                      (4, 'Ankatso'), (4, 'Amohimiandra'),
                                      (5, 'Anosy'), (5, 'Farihy'), 
                                      (5, 'Ambodimita'), (6, 'Betafo'), 
                                      (6, 'Ambanja'), (6, 'Moramanga');
INSERT INTO Contact(email, numero) 
                                VALUES('angoty@gmail.com', '0330203010'),
                                      ('nancy@gmail.com', '0340203011'),
                                      ('arena@gmail.com', '0320203012');

INSERT INTO Admin              VALUES(DEFAULT, 'RABARIJAONA', 'Angoty Fitia', '2005-11-01', 'angoty', 2, 1, 1, 3), 
                                      (DEFAULT, 'ANDRIAMAHANINTSOA', 'Nancy Elidah', '2004-08-15', 'nancy', 2, 3, 2, 2), 
                                      (DEFAULT, 'RABESERANANA', 'Arena Gracia', '2005-09-28', 'arena', 2, 2, 3, 1);



