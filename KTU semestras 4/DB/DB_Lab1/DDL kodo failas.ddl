--@(#) script.ddl

CREATE TABLE Biudzetai
(
	id_Biudzetas integer (8) NOT NULL,
	Esamas_biudzetas double precision NOT NULL,
	Imamas_kiekis double precision NOT NULL,
	Dedamas_kiekis double precision NOT NULL,
	Data timestamp NOT NULL,
	PRIMARY KEY(id_Biudzetas)
);

CREATE TABLE Dalyviai
(
	ID integer (8) NOT NULL,
	Vardas varchar (255) NOT NULL,
	Pavarde varchar (255) NOT NULL,
	El_Pastas varchar (255) NULL,
	Telefonas varchar (50) NOT NULL,
	PRIMARY KEY(ID)
);

CREATE TABLE Roles
(
	Nr integer (8) NOT NULL,
	Pavadinimas varchar (255) NOT NULL,
	Uzduotis varchar (255) NOT NULL,
	Nuo_kada timestamp NOT NULL,
	Iki_kada timestamp NOT NULL,
	PRIMARY KEY(Nr)
);

CREATE TABLE Remejai
(
	ID integer (8) NOT NULL,
	Kompanija varchar (255) NULL,
	El_Pastas varchar (255) NULL,
	Telefonas varchar (50) NOT NULL,
	Adresas varchar (255) NULL,
	Vardas varchar (255) NOT NULL,
	Pavarde varchar (255) NOT NULL,
	fk_Biudzetas integer (8) NOT NULL,
	PRIMARY KEY(ID),
	CONSTRAINT Skiria2 FOREIGN KEY(fk_Biudzetas) REFERENCES Biudzetai (id_Biudzetas)
);

CREATE TABLE Uzsakovai
(
	ID integer (8) NOT NULL,
	Kompanija varchar (255) NULL,
	Vardas varchar (255) NOT NULL,
	Pavarde varchar (255) NOT NULL,
	El_Pastas varchar (255) NULL,
	Telefonas varchar (50) NOT NULL,
	Adresas varchar (255) NULL,
	fk_Biudzetas integer (8) NOT NULL,
	PRIMARY KEY(ID),
	CONSTRAINT Skiria FOREIGN KEY(fk_Biudzetas) REFERENCES Biudzetai (id_Biudzetas)
);

CREATE TABLE Projektai
(
	Nr integer (8) NOT NULL,
	Pavadinimas varchar (255) NOT NULL,
	Nuo_kada timestamp NOT NULL,
	Iki_kada timestamp NOT NULL,
	fk_Uzsakovas integer (8) NOT NULL,
	PRIMARY KEY(Nr),
	UNIQUE(fk_Uzsakovas),
	CONSTRAINT Uzsako FOREIGN KEY(fk_Uzsakovas) REFERENCES Uzsakovai (ID)
);

CREATE TABLE Dalyvauja
(
	fk_Dalyvis integer (8) NOT NULL,
	fk_Projektas integer (8) NOT NULL,
	PRIMARY KEY(fk_Dalyvis, fk_Projektas),
	CONSTRAINT fkc_Dalyvis FOREIGN KEY(fk_Dalyvis) REFERENCES Dalyviai (ID),
	CONSTRAINT fkc_Projektas FOREIGN KEY(fk_Projektas) REFERENCES Projektai (Nr)
);

CREATE TABLE Grafikai
(
	Nr integer (8) NOT NULL,
	Pavadinimas varchar (255) NOT NULL,
	Nuo_kada timestamp NOT NULL,
	Iki_kada timestamp NOT NULL,
	fk_Projektas integer (8) NOT NULL,
	PRIMARY KEY(Nr),
	CONSTRAINT Vykdomas_pagal FOREIGN KEY(fk_Projektas) REFERENCES Projektai (Nr)
);

CREATE TABLE Uzduotys
(
	Nr integer (8) NOT NULL,
	Pavadinimas varchar (255) NOT NULL,
	fk_Grafikas integer (8) NOT NULL,
	PRIMARY KEY(Nr),
	CONSTRAINT Atliekama_pagal FOREIGN KEY(fk_Grafikas) REFERENCES Grafikai (Nr)
);

CREATE TABLE Darbuotojai
(
	ID integer (8) NOT NULL,
	Vardas varchar (255) NOT NULL,
	Pavarde varchar (255) NOT NULL,
	Telefonas varchar (50) NOT NULL,
	El_Pastas varchar (255) NULL,
	Adresas varchar (255) NULL,
	fk_Uzduotis integer (8) NOT NULL,
	fk_Role integer (8) NOT NULL,
	PRIMARY KEY(ID),
	CONSTRAINT Atlieka FOREIGN KEY(fk_Uzduotis) REFERENCES Uzduotys (Nr),
	CONSTRAINT Uzema FOREIGN KEY(fk_Role) REFERENCES Roles (Nr)
);

CREATE TABLE Islaidos
(
	Nr integer (8) NOT NULL,
	Suma double precision NOT NULL,
	Paskirtis varchar (255) NOT NULL,
	Data timestamp NOT NULL,
	fk_Biudzetas integer (8) NOT NULL,
	fk_Uzduotis integer (8) NOT NULL,
	PRIMARY KEY(Nr),
	CONSTRAINT Skiriamas FOREIGN KEY(fk_Biudzetas) REFERENCES Biudzetai (id_Biudzetas),
	CONSTRAINT Reikalauja FOREIGN KEY(fk_Uzduotis) REFERENCES Uzduotys (Nr)
);
