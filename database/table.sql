CREATE DATABASE hopital;
\c hopital;


CREATE TABLE ProfilAdmins(
    id_profil SERIAL PRIMARY KEY,
    intitule VARCHAR(20)
);
CREATE TABLE ProfilPersonnels(
    id_profil SERIAL PRIMARY KEY,
    intitule VARCHAR(20)
);
CREATE TABLE Genre(
    id_genre SERIAL PRIMARY KEY,
    intitule VARCHAR(10)
);
CREATE TABLE Contact(
    id_contact SERIAL PRIMARY KEY,
    email VARCHAR(40),
    numero VARCHAR(10) UNIQUE
);
CREATE TABLE Province(
    id_province SERIAL PRIMARY KEY,
    intitule VARCHAR(40)
);
CREATE TABLE Lieu(
    id_lieu SERIAL PRIMARY KEY,
    id_province INTEGER REFERENCES Province(id_province),
    intitule VARCHAR(40)
);
CREATE TABLE Admin(
    id_admin  VARCHAR(7) DEFAULT ('ADM' || LPAD(nextval('seq_admin')::TEXT, 4, '0')) PRIMARY KEY,
    nom VARCHAR(30),
    prenom VARCHAR(30), 
    date_naissance DATE,
    mot_de_passe VARCHAR(30),
    id_genre INTEGER  REFERENCES Genre(id_genre),
    id_adresse INTEGER REFERENCES Lieu(id_lieu),
    id_contact INTEGER REFERENCES Contact(id_contact),
    id_profilD INTEGER REFERENCES ProfilAdmins(id_profil)
);
CREATE TABLE Candidat (
    id_candidat VARCHAR(7) DEFAULT ('CDT' || LPAD(nextval('seq_candidat')::TEXT, 4, '0')) PRIMARY KEY,
    nom VARCHAR(30),
    prenom VARCHAR(30), 
    date_naissance DATE,
    id_genre INTEGER  REFERENCES Genre(id_genre),
    id_adresse INTEGER REFERENCES Lieu(id_lieu),
    id_contact INTEGER REFERENCES Contact(id_contact)
);
CREATE TABLE Personnel(
    id_personnel VARCHAR(7) DEFAULT ('PRS' || LPAD(nextval('seq_personnel')::TEXT, 4, '0')) PRIMARY KEY,
    mot_de_passe VARCHAR(40),
    id_candidat VARCHAR(30) REFERENCES Candidat(id_candidat),
    id_profilP INTEGER REFERENCES ProfilPersonnels(id_profil)
);