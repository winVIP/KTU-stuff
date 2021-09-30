-- phpMyAdmin SQL Dump
-- version 3.4.11.1deb2+deb7u8
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Mar 05, 2019 at 08:49 AM
-- Server version: 1.0.35
-- PHP Version: 5.6.37-1~dotdeb+zts+7.1

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `vytkun`
--

-- --------------------------------------------------------

--
-- Table structure for table `Biudzetai`
--

CREATE TABLE IF NOT EXISTS `Biudzetai` (
  `id_Biudzetas` int(8) NOT NULL,
  `Esamas_biudzetas` double NOT NULL,
  `Imamas_kiekis` double NOT NULL,
  `Dedamas_kiekis` double NOT NULL,
  `Data` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id_Biudzetas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Biudzetai`
--

INSERT INTO `Biudzetai` (`id_Biudzetas`, `Esamas_biudzetas`, `Imamas_kiekis`, `Dedamas_kiekis`, `Data`) VALUES
(1, 345326, 23149, 27604, '2018-10-14 06:51:54'),
(2, 384550, 38481, 3316, '2019-02-23 16:18:06'),
(3, 391682, 45574, 43485, '2018-04-20 11:42:18'),
(4, 442739, 45563, 30179, '2019-02-28 21:00:47'),
(5, 265223, 17731, 5557, '2018-06-15 00:05:15'),
(6, 272795, 24627, 41738, '2018-11-08 11:21:48'),
(7, 343835, 33392, 37948, '2019-02-09 07:47:56'),
(8, 163177, 11767, 27186, '2018-05-05 18:56:55'),
(9, 356220, 28846, 5468, '2018-07-12 13:51:31'),
(10, 270790, 17657, 19479, '2018-10-17 08:17:45'),
(11, 497365, 31932, 13272, '2018-06-06 15:40:35'),
(12, 301377, 42158, 44449, '2018-08-27 23:55:57'),
(13, 222576, 22680, 28556, '2018-11-05 22:42:30'),
(14, 491906, 14015, 40511, '2018-06-29 18:41:27'),
(15, 207142, 21206, 13030, '2019-02-09 15:57:33'),
(16, 207495, 35267, 19515, '2019-01-25 09:13:58'),
(17, 328977, 47120, 30989, '2018-05-01 17:54:06'),
(18, 328963, 8739, 43509, '2018-04-30 20:14:55'),
(19, 451096, 38196, 39671, '2018-11-12 11:57:53'),
(20, 286556, 15859, 29341, '2018-11-25 17:14:03');

-- --------------------------------------------------------

--
-- Table structure for table `Dalyvauja`
--

CREATE TABLE IF NOT EXISTS `Dalyvauja` (
  `fk_Dalyvis` int(8) NOT NULL,
  `fk_Projektas` int(8) NOT NULL,
  PRIMARY KEY (`fk_Dalyvis`,`fk_Projektas`),
  KEY `fkc_Projektas` (`fk_Projektas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Dalyvauja`
--

INSERT INTO `Dalyvauja` (`fk_Dalyvis`, `fk_Projektas`) VALUES
(1, 16),
(2, 9),
(2, 19),
(4, 1),
(4, 2),
(4, 10),
(4, 16),
(6, 8),
(7, 7),
(7, 13),
(8, 4),
(8, 13),
(8, 14),
(9, 6),
(10, 10),
(12, 6),
(12, 7),
(12, 17),
(12, 20),
(13, 4),
(14, 13),
(15, 11),
(16, 2),
(16, 7),
(17, 10),
(17, 11),
(17, 12),
(17, 14),
(19, 4),
(19, 14),
(20, 1),
(20, 5);

-- --------------------------------------------------------

--
-- Table structure for table `Dalyviai`
--

CREATE TABLE IF NOT EXISTS `Dalyviai` (
  `ID` int(8) NOT NULL,
  `Vardas` varchar(255) NOT NULL,
  `Pavarde` varchar(255) NOT NULL,
  `El_Pastas` varchar(255) DEFAULT NULL,
  `Telefonas` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Dalyviai`
--

INSERT INTO `Dalyviai` (`ID`, `Vardas`, `Pavarde`, `El_Pastas`, `Telefonas`) VALUES
(1, 'Melli', 'Scriven', 'mscriven0@illinois.edu', '620-252-3542'),
(2, 'Margarita', 'Seathwright', 'mseathwright1@google.com.au', '368-908-7772'),
(3, 'Arvie', 'Cowell', 'acowell2@nba.com', '693-176-0461'),
(4, 'Cointon', 'Mabson', 'cmabson3@google.com', '352-548-7371'),
(5, 'Delano', 'Ullrich', 'dullrich4@ted.com', '592-189-0933'),
(6, 'Clarissa', 'Simionato', 'csimionato5@smugmug.com', '506-563-8140'),
(7, 'Maddie', 'Dytham', 'mdytham6@goodreads.com', '160-148-3537'),
(8, 'Clarie', 'Stroband', 'cstroband7@yolasite.com', '319-712-5328'),
(9, 'Velma', 'Mayte', 'vmayte8@businessweek.com', '636-685-9932'),
(10, 'Karilynn', 'Cornelisse', 'kcornelisse9@newyorker.com', '271-984-0041'),
(11, 'Nester', 'Bakhrushin', 'nbakhrushina@harvard.edu', '166-657-8292'),
(12, 'Tessi', 'Joselevitch', 'tjoselevitchb@oakley.com', '408-915-7492'),
(13, 'Vinnie', 'Godspede', 'vgodspedec@nymag.com', '753-300-9169'),
(14, 'Graham', 'Raiment', 'graimentd@themeforest.net', '729-653-8323'),
(15, 'Cristie', 'Armytage', 'carmytagee@wikipedia.org', '525-392-6609'),
(16, 'Elia', 'Crosson', 'ecrossonf@cpanel.net', '763-493-7552'),
(17, 'Egon', 'Bowen', 'eboweng@tmall.com', '602-386-8307'),
(18, 'Toby', 'Date', 'tdateh@scribd.com', '565-802-0851'),
(19, 'Stella', 'MacEvilly', 'smacevillyi@fc2.com', '280-460-2161'),
(20, 'Val', 'Byrch', 'vbyrchj@washington.edu', '485-732-8161');

-- --------------------------------------------------------

--
-- Table structure for table `Darbuotojai`
--

CREATE TABLE IF NOT EXISTS `Darbuotojai` (
  `ID` int(8) NOT NULL,
  `Vardas` varchar(255) NOT NULL,
  `Pavarde` varchar(255) NOT NULL,
  `Telefonas` varchar(50) NOT NULL,
  `El_Pastas` varchar(255) DEFAULT NULL,
  `Adresas` varchar(255) DEFAULT NULL,
  `fk_Uzduotis` int(8) NOT NULL,
  `fk_Role` int(8) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Atlieka` (`fk_Uzduotis`),
  KEY `Uzema` (`fk_Role`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Darbuotojai`
--

INSERT INTO `Darbuotojai` (`ID`, `Vardas`, `Pavarde`, `Telefonas`, `El_Pastas`, `Adresas`, `fk_Uzduotis`, `fk_Role`) VALUES
(1, 'Kipp', 'Calderon', '169-672-4688', 'kcalderon0@theguardian.com', '39 Superior Pass', 1, 1),
(2, 'Roch', 'Carnie', '998-400-7734', 'rcarnie1@forbes.com', '28402 Northport Trail', 2, 2),
(3, 'Townsend', 'Smurfitt', '420-693-3122', 'tsmurfitt2@prnewswire.com', '3 Lyons Avenue', 3, 3),
(4, 'Dalia', 'Stretton', '163-559-2652', 'dstretton3@hp.com', '53625 Coolidge Parkway', 4, 4),
(5, 'Sheri', 'Deboy', '799-640-9050', 'sdeboy4@ask.com', '7144 Pearson Street', 5, 5),
(6, 'Collie', 'MacCardle', '851-434-4935', 'cmaccardle5@amazonaws.com', '0753 Del Sol Lane', 6, 6),
(7, 'Decca', 'Greenacre', '588-440-6155', 'dgreenacre6@house.gov', '9 Moulton Place', 7, 7),
(8, 'Ambrosi', 'Bedells', '380-450-8980', 'abedells7@shop-pro.jp', '4 Crownhardt Court', 8, 8),
(9, 'Sandi', 'Von Welden', '860-347-2034', 'svonwelden8@mlb.com', '144 Morningstar Street', 9, 9),
(10, 'Vladimir', 'Brittlebank', '366-362-0220', 'vbrittlebank9@prweb.com', '840 Hudson Drive', 10, 10),
(11, 'Lianne', 'Scholer', '500-557-2446', 'lscholera@example.com', '2 Rusk Drive', 11, 11),
(12, 'Salli', 'Praundlin', '312-775-7459', 'spraundlinb@hugedomains.com', '602 Mandrake Place', 12, 12),
(13, 'Krisha', 'Briance', '169-412-2376', 'kbriancec@chicagotribune.com', '64228 Northwestern Hill', 13, 13),
(14, 'Norby', 'Weatherby', '982-517-4199', 'nweatherbyd@1688.com', '0 Steensland Alley', 14, 14),
(15, 'Winifred', 'Grigoriev', '307-725-4814', 'wgrigorieve@google.com.br', '06299 Cottonwood Park', 15, 15),
(16, 'Bartram', 'Kochlin', '502-484-8060', 'bkochlinf@comsenz.com', '71758 Springview Drive', 16, 16),
(17, 'Tiphanie', 'Hanby', '260-919-0253', 'thanbyg@uiuc.edu', '260 Hallows Center', 17, 17),
(18, 'Elton', 'Nother', '551-212-4148', 'enotherh@printfriendly.com', '31421 Arkansas Pass', 18, 18),
(19, 'Tabbitha', 'Umbers', '400-993-4745', 'tumbersi@nasa.gov', '4056 Russell Pass', 19, 19),
(20, 'Kary', 'McGaraghan', '855-194-4895', 'kmcgaraghanj@nbcnews.com', '625 Bowman Circle', 20, 20);

-- --------------------------------------------------------

--
-- Table structure for table `Grafikai`
--

CREATE TABLE IF NOT EXISTS `Grafikai` (
  `Nr` int(8) NOT NULL,
  `Pavadinimas` varchar(255) NOT NULL,
  `Nuo_kada` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Iki_kada` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `fk_Projektas` int(8) NOT NULL,
  PRIMARY KEY (`Nr`),
  KEY `Vykdomas_pagal` (`fk_Projektas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Grafikai`
--

INSERT INTO `Grafikai` (`Nr`, `Pavadinimas`, `Nuo_kada`, `Iki_kada`, `fk_Projektas`) VALUES
(1, 'sed ante', '2018-03-11 22:30:43', '2018-03-28 19:53:16', 1),
(2, 'eget', '2018-03-06 07:45:16', '2018-03-22 09:32:47', 2),
(3, 'pede', '2018-03-08 18:20:47', '2018-03-16 10:43:33', 3),
(4, 'vestibulum', '2018-03-09 19:01:21', '2018-03-19 23:15:49', 4),
(5, 'habitasse', '2018-03-07 21:50:37', '2018-03-16 11:12:36', 5),
(6, 'aliquet', '2018-03-05 09:01:58', '2018-03-25 22:56:59', 6),
(7, 'neque', '2018-03-12 11:01:10', '2018-03-29 01:25:47', 7),
(8, 'venenatis', '2018-03-13 04:05:12', '2018-03-17 00:58:18', 8),
(9, 'amet sapien', '2018-03-09 18:27:57', '2018-03-25 23:23:48', 9),
(10, 'est quam', '2018-03-10 21:56:57', '2018-03-21 04:49:53', 10),
(11, 'faucibus', '2018-03-09 04:04:28', '2018-03-25 16:05:05', 11),
(12, 'massa tempor', '2018-03-04 06:03:48', '2018-03-15 15:05:59', 12),
(13, 'lacus', '2018-03-11 22:22:05', '2018-03-19 03:29:26', 13),
(14, 'purus', '2018-03-13 08:30:43', '2018-03-29 04:10:14', 14),
(15, 'suscipit', '2018-03-05 11:26:42', '2018-03-15 06:26:19', 15),
(16, 'odio', '2018-03-12 13:50:32', '2018-03-19 21:36:02', 16),
(17, 'sed', '2018-03-10 08:37:36', '2018-03-22 07:04:16', 17),
(18, 'cursus', '2018-03-11 15:38:10', '2018-03-23 03:34:13', 18),
(19, 'luctus et', '2018-03-09 12:05:15', '2018-03-17 23:45:37', 19),
(20, 'tempor turpis', '2018-03-07 14:47:42', '2018-03-19 06:17:06', 20);

-- --------------------------------------------------------

--
-- Table structure for table `Islaidos`
--

CREATE TABLE IF NOT EXISTS `Islaidos` (
  `Nr` int(8) NOT NULL,
  `Suma` double NOT NULL,
  `Paskirtis` varchar(255) NOT NULL,
  `Data` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `fk_Biudzetas` int(8) NOT NULL,
  `fk_Uzduotis` int(8) NOT NULL,
  PRIMARY KEY (`Nr`),
  KEY `Skiriamas` (`fk_Biudzetas`),
  KEY `Reikalauja` (`fk_Uzduotis`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Islaidos`
--

INSERT INTO `Islaidos` (`Nr`, `Suma`, `Paskirtis`, `Data`, `fk_Biudzetas`, `fk_Uzduotis`) VALUES
(1, 80, 'vel augue', '2018-03-04 01:06:48', 1, 1),
(2, 441, 'ac', '2018-03-04 11:34:11', 2, 2),
(3, 179, 'odio curabitur', '2018-03-20 18:56:33', 3, 3),
(4, 411, 'potenti nullam', '2018-03-21 08:24:44', 4, 4),
(5, 462, 'dolor sit', '2018-03-04 03:33:07', 5, 5),
(6, 188, 'justo in', '2018-03-26 11:21:14', 6, 6),
(7, 163, 'potenti cras', '2018-03-09 20:58:45', 7, 7),
(8, 482, 'vehicula consequat', '2018-03-16 08:53:31', 8, 8),
(9, 271, 'condimentum curabitur', '2018-03-18 00:08:03', 9, 9),
(10, 306, 'posuere', '2018-03-23 21:36:49', 10, 10),
(11, 231, 'suscipit nulla', '2018-03-12 06:28:55', 11, 11),
(12, 121, 'a', '2018-03-19 20:16:19', 12, 12),
(13, 124, 'orci', '2018-03-23 09:56:52', 13, 13),
(14, 143, 'sapien', '2018-03-08 04:49:27', 14, 14),
(15, 396, 'nulla nisl', '2018-03-07 09:10:32', 15, 15),
(16, 476, 'primis in', '2018-03-17 19:40:48', 16, 16),
(17, 205, 'morbi vel', '2018-03-11 18:46:53', 17, 17),
(18, 421, 'nullam sit', '2018-03-09 11:40:10', 18, 18),
(19, 282, 'ultrices posuere', '2018-03-10 05:29:31', 19, 19),
(20, 496, 'nec', '2018-03-22 10:39:32', 20, 20);

-- --------------------------------------------------------

--
-- Table structure for table `Projektai`
--

CREATE TABLE IF NOT EXISTS `Projektai` (
  `Nr` int(8) NOT NULL,
  `Pavadinimas` varchar(255) NOT NULL,
  `Nuo_kada` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Iki_kada` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `fk_Uzsakovas` int(8) NOT NULL,
  PRIMARY KEY (`Nr`),
  UNIQUE KEY `fk_Uzsakovas` (`fk_Uzsakovas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Projektai`
--

INSERT INTO `Projektai` (`Nr`, `Pavadinimas`, `Nuo_kada`, `Iki_kada`, `fk_Uzsakovas`) VALUES
(1, 'posuere', '2018-03-11 07:22:12', '2018-03-20 05:21:48', 1),
(2, 'mi nulla', '2018-03-04 23:13:47', '2018-03-28 11:34:52', 2),
(3, 'ultrices', '2018-03-11 05:42:31', '2018-03-18 00:51:10', 3),
(4, 'et tempus', '2018-03-07 10:14:12', '2018-03-27 17:19:39', 4),
(5, 'dui', '2018-03-05 16:22:31', '2018-03-19 11:39:08', 5),
(6, 'bibendum imperdiet', '2018-03-05 12:48:39', '2018-03-28 07:58:55', 6),
(7, 'congue', '2018-03-07 06:31:12', '2018-03-29 17:43:48', 7),
(8, 'nunc', '2018-03-04 15:33:42', '2018-03-24 18:59:48', 8),
(9, 'neque duis', '2018-03-03 22:00:08', '2018-03-25 07:05:51', 9),
(10, 'erat nulla', '2018-03-10 12:47:33', '2018-03-18 16:03:19', 10),
(11, 'mauris', '2018-03-04 20:06:28', '2018-03-19 17:54:07', 11),
(12, 'in', '2018-03-08 06:47:53', '2018-03-21 06:19:52', 12),
(13, 'lacus', '2018-03-03 23:45:57', '2018-03-23 00:28:34', 13),
(14, 'sit amet', '2018-03-10 09:35:16', '2018-03-16 13:49:41', 14),
(15, 'velit', '2018-03-12 12:06:44', '2018-03-15 09:37:39', 15),
(16, 'tortor', '2018-03-10 13:59:10', '2018-03-27 04:40:49', 16),
(17, 'et ultrices', '2018-03-08 21:59:56', '2018-03-18 08:59:38', 17),
(18, 'est phasellus', '2018-03-08 15:12:17', '2018-03-24 18:47:00', 18),
(19, 'morbi non', '2018-03-06 10:38:34', '2018-03-18 13:54:29', 19),
(20, 'luctus et', '2018-03-06 01:56:19', '2018-03-17 23:18:26', 20);

-- --------------------------------------------------------

--
-- Table structure for table `Remejai`
--

CREATE TABLE IF NOT EXISTS `Remejai` (
  `ID` int(8) NOT NULL,
  `Kompanija` varchar(255) DEFAULT NULL,
  `El_Pastas` varchar(255) DEFAULT NULL,
  `Telefonas` varchar(50) NOT NULL,
  `Adresas` varchar(255) DEFAULT NULL,
  `Vardas` varchar(255) NOT NULL,
  `Pavarde` varchar(255) NOT NULL,
  `fk_Biudzetas` int(8) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Skiria2` (`fk_Biudzetas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Remejai`
--

INSERT INTO `Remejai` (`ID`, `Kompanija`, `El_Pastas`, `Telefonas`, `Adresas`, `Vardas`, `Pavarde`, `fk_Biudzetas`) VALUES
(1, 'massa', 'sorsi0@e-recht24.de', '433-758-7044', '55760 Utah Point', 'Sandie', 'Orsi', 1),
(2, 'justo lacinia', 'lchallin1@reddit.com', '184-982-3370', '5711 Porter Road', 'Lila', 'Challin', 2),
(3, 'lectus', 'nfrankling2@howstuffworks.com', '822-155-4143', '9532 Mandrake Avenue', 'Nollie', 'Frankling', 3),
(4, 'imperdiet nullam', 'cduffill3@nhs.uk', '824-620-8568', '44 Clemons Place', 'Cristie', 'Duffill', 4),
(5, 'scelerisque mauris', 'jlesek4@istockphoto.com', '385-810-3575', '13907 Gerald Street', 'Javier', 'Lesek', 5),
(6, 'in', 'lglanders5@123-reg.co.uk', '760-948-1481', '5 Glendale Parkway', 'Ly', 'Glanders', 6),
(7, 'mauris', 'jloud6@about.com', '717-726-9948', '447 Rockefeller Drive', 'Jacquenetta', 'Loud', 7),
(8, 'convallis', 'mtraill7@seesaa.net', '408-481-1029', '77 Sommers Street', 'Marina', 'Traill', 8),
(9, 'volutpat', 'cbroderick8@prnewswire.com', '707-137-3081', '7 Sugar Street', 'Celestia', 'Broderick', 9),
(10, 'at', 'nkinforth9@nydailynews.com', '105-700-1412', '715 Lawn Terrace', 'Nataniel', 'Kinforth', 10),
(11, 'nibh in', 'pcockera@nps.gov', '980-610-2691', '63822 Transport Plaza', 'Phillie', 'Cocker', 11),
(12, 'pede posuere', 'kbletsob@wisc.edu', '461-110-0658', '0651 Oxford Trail', 'Keelia', 'Bletso', 12),
(13, 'sed', 'ndemeadc@sohu.com', '101-357-9697', '17793 Haas Terrace', 'Norton', 'Demead', 13),
(14, 'id', 'bgoodbournd@skype.com', '670-208-6666', '54541 Westport Park', 'Bianka', 'Goodbourn', 14),
(15, 'aliquet', 'jswirese@ifeng.com', '216-321-1050', '3473 Lunder Point', 'Jena', 'Swires', 15),
(16, 'pede malesuada', 'aingrayf@ibm.com', '960-778-8236', '37918 Sunnyside Lane', 'Aeriela', 'Ingray', 16),
(17, 'cras mi', 'roxburghg@weather.com', '257-312-5334', '57679 Lien Junction', 'Rowland', 'Oxburgh', 17),
(18, 'aliquam lacus', 'smerritth@hugedomains.com', '528-962-4025', '2791 Fuller Avenue', 'Salome', 'Merritt', 18),
(19, 'lorem', 'lleachi@xing.com', '803-330-0841', '6541 Lillian Point', 'Locke', 'Leach', 19),
(20, 'odio cras', 'rcalderonj@census.gov', '260-114-0911', '4 Bunker Hill Street', 'Ruben', 'Calderon', 20);

-- --------------------------------------------------------

--
-- Table structure for table `Roles`
--

CREATE TABLE IF NOT EXISTS `Roles` (
  `Nr` int(8) NOT NULL,
  `Pavadinimas` varchar(255) NOT NULL,
  `Uzduotis` varchar(255) NOT NULL,
  `Nuo_kada` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Iki_kada` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`Nr`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Roles`
--

INSERT INTO `Roles` (`Nr`, `Pavadinimas`, `Uzduotis`, `Nuo_kada`, `Iki_kada`) VALUES
(1, 'Media Manager II', 'donec', '2018-03-07 13:36:43', '2018-03-24 21:18:06'),
(2, 'Account Executive', 'tincidunt eu', '2018-03-13 20:57:42', '2018-03-28 11:36:52'),
(3, 'Staff Accountant II', 'in', '2018-03-10 11:56:45', '2018-03-23 11:15:40'),
(4, 'Senior Sales Associate', 'cum sociis', '2018-03-08 12:17:53', '2018-03-25 06:12:03'),
(5, 'Chemical Engineer', 'nullam sit', '2018-03-11 16:12:16', '2018-03-24 02:39:35'),
(6, 'Software Engineer I', 'justo', '2018-03-08 13:58:34', '2018-03-23 00:23:19'),
(7, 'Systems Administrator IV', 'nulla', '2018-03-06 04:38:08', '2018-03-27 16:04:14'),
(8, 'Marketing Assistant', 'lobortis vel', '2018-03-11 23:12:11', '2018-03-29 17:48:06'),
(9, 'Physical Therapy Assistant', 'turpis sed', '2018-03-10 01:29:52', '2018-03-24 03:31:08'),
(10, 'Accounting Assistant IV', 'eu est', '2018-03-09 06:31:23', '2018-03-24 07:14:53'),
(11, 'Database Administrator III', 'nulla mollis', '2018-03-10 04:40:29', '2018-03-21 12:52:45'),
(12, 'Nuclear Power Engineer', 'suspendisse', '2018-03-12 11:41:29', '2018-03-26 14:30:08'),
(13, 'Quality Control Specialist', 'sit', '2018-03-08 08:19:11', '2018-03-29 01:49:05'),
(14, 'Senior Cost Accountant', 'justo nec', '2018-03-13 11:47:49', '2018-03-27 03:47:16'),
(15, 'Associate Professor', 'cubilia', '2018-03-05 19:57:27', '2018-03-15 19:29:40'),
(16, 'Registered Nurse', 'nunc rhoncus', '2018-03-08 02:36:04', '2018-03-21 10:40:33'),
(17, 'Compensation Analyst', 'vestibulum', '2018-03-07 07:16:33', '2018-03-16 04:39:57'),
(18, 'Senior Sales Associate', 'in', '2018-03-09 21:10:49', '2018-03-21 14:52:30'),
(19, 'Computer Systems Analyst I', 'a', '2018-03-10 04:41:29', '2018-03-16 13:11:58'),
(20, 'Project Manager', 'proin leo', '2018-03-06 10:45:21', '2018-03-15 07:35:57');

-- --------------------------------------------------------

--
-- Table structure for table `Uzduotys`
--

CREATE TABLE IF NOT EXISTS `Uzduotys` (
  `Nr` int(8) NOT NULL,
  `Pavadinimas` varchar(255) NOT NULL,
  `fk_Grafikas` int(8) NOT NULL,
  PRIMARY KEY (`Nr`),
  KEY `Atliekama_pagal` (`fk_Grafikas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Uzduotys`
--

INSERT INTO `Uzduotys` (`Nr`, `Pavadinimas`, `fk_Grafikas`) VALUES
(1, 'nec', 1),
(2, 'congue', 2),
(3, 'nunc rhoncus', 3),
(4, 'id', 4),
(5, 'vitae quam', 5),
(6, 'orci', 6),
(7, 'quis justo', 7),
(8, 'dui nec', 8),
(9, 'aenean', 9),
(10, 'justo nec', 10),
(11, 'lectus pellentesque', 11),
(12, 'penatibus et', 12),
(13, 'ac lobortis', 13),
(14, 'erat quisque', 14),
(15, 'venenatis', 15),
(16, 'erat', 16),
(17, 'non', 17),
(18, 'volutpat', 18),
(19, 'et', 19),
(20, 'sit amet', 20);

-- --------------------------------------------------------

--
-- Table structure for table `Uzsakovai`
--

CREATE TABLE IF NOT EXISTS `Uzsakovai` (
  `ID` int(8) NOT NULL,
  `Kompanija` varchar(255) DEFAULT NULL,
  `Vardas` varchar(255) NOT NULL,
  `Pavarde` varchar(255) NOT NULL,
  `El_Pastas` varchar(255) DEFAULT NULL,
  `Telefonas` varchar(50) NOT NULL,
  `Adresas` varchar(255) DEFAULT NULL,
  `fk_Biudzetas` int(8) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Skiria` (`fk_Biudzetas`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Uzsakovai`
--

INSERT INTO `Uzsakovai` (`ID`, `Kompanija`, `Vardas`, `Pavarde`, `El_Pastas`, `Telefonas`, `Adresas`, `fk_Biudzetas`) VALUES
(1, 'luctus', 'Errick', 'Cypler', 'ecypler0@bigcartel.com', '865-524-4011', '64 Jana Road', 1),
(2, 'accumsan odio', 'Nelie', 'Waplinton', 'nwaplinton1@drupal.org', '166-140-4754', '43 Shopko Terrace', 2),
(3, 'ut odio', 'Fin', 'Littefair', 'flittefair2@csmonitor.com', '139-290-5481', '62 Monument Lane', 3),
(4, 'venenatis tristique', 'Theodosia', 'Shreeves', 'tshreeves3@marriott.com', '814-455-9843', '18 Novick Center', 4),
(5, 'nibh in', 'Phillie', 'Thunder', 'pthunder4@symantec.com', '312-872-0465', '35857 Heath Circle', 5),
(6, 'orci', 'Yehudi', 'McKinn', 'ymckinn5@miitbeian.gov.cn', '184-319-3758', '95 Vera Avenue', 6),
(7, 'sit amet', 'Tybie', 'Attrill', 'tattrill6@cbsnews.com', '864-929-3739', '50324 Talmadge Center', 7),
(8, 'congue', 'Roxi', 'Black', 'rblack7@lulu.com', '574-907-0596', '742 Scofield Center', 8),
(9, 'nullam', 'Ferd', 'Gidney', 'fgidney8@chicagotribune.com', '261-347-0886', '6 Hazelcrest Crossing', 9),
(10, 'arcu sed', 'Norean', 'Vigurs', 'nvigurs9@reverbnation.com', '312-660-0146', '08827 Lakeland Circle', 10),
(11, 'luctus', 'Duke', 'Geddis', 'dgeddisa@berkeley.edu', '660-172-7251', '635 Crowley Alley', 11),
(12, 'habitasse platea', 'Shae', 'Loche', 'slocheb@google.pl', '208-944-5666', '121 Porter Pass', 12),
(13, 'nulla suspendisse', 'Emile', 'Tabart', 'etabartc@acquirethisname.com', '311-701-3175', '4626 Darwin Junction', 13),
(14, 'in porttitor', 'Langston', 'Veel', 'lveeld@live.com', '214-997-8339', '70 Coolidge Place', 14),
(15, 'eleifend donec', 'Elroy', 'Hinzer', 'ehinzere@mac.com', '833-142-5104', '5 Oneill Park', 15),
(16, 'phasellus', 'Guido', 'Parfett', 'gparfettf@mozilla.com', '894-309-6066', '90 Pepper Wood Trail', 16),
(17, 'vestibulum eget', 'Kipp', 'Heinonen', 'kheinoneng@phpbb.com', '943-818-1060', '69 Warrior Place', 17),
(18, 'dolor', 'Antoine', 'Strathman', 'astrathmanh@dion.ne.jp', '756-303-9379', '21566 Briar Crest Park', 18),
(19, 'leo odio', 'Ginger', 'Utterson', 'guttersoni@4shared.com', '639-266-1793', '894 Armistice Lane', 19),
(20, 'ante', 'Charlotta', 'Althorpe', 'calthorpej@cocolog-nifty.com', '304-590-8700', '8977 Surrey Street', 20);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `Dalyvauja`
--
ALTER TABLE `Dalyvauja`
  ADD CONSTRAINT `fkc_Dalyvis` FOREIGN KEY (`fk_Dalyvis`) REFERENCES `Dalyviai` (`ID`),
  ADD CONSTRAINT `fkc_Projektas` FOREIGN KEY (`fk_Projektas`) REFERENCES `Projektai` (`Nr`);

--
-- Constraints for table `Darbuotojai`
--
ALTER TABLE `Darbuotojai`
  ADD CONSTRAINT `Atlieka` FOREIGN KEY (`fk_Uzduotis`) REFERENCES `Uzduotys` (`Nr`),
  ADD CONSTRAINT `Uzema` FOREIGN KEY (`fk_Role`) REFERENCES `Roles` (`Nr`);

--
-- Constraints for table `Grafikai`
--
ALTER TABLE `Grafikai`
  ADD CONSTRAINT `Vykdomas_pagal` FOREIGN KEY (`fk_Projektas`) REFERENCES `Projektai` (`Nr`);

--
-- Constraints for table `Islaidos`
--
ALTER TABLE `Islaidos`
  ADD CONSTRAINT `Reikalauja` FOREIGN KEY (`fk_Uzduotis`) REFERENCES `Uzduotys` (`Nr`),
  ADD CONSTRAINT `Skiriamas` FOREIGN KEY (`fk_Biudzetas`) REFERENCES `Biudzetai` (`id_Biudzetas`);

--
-- Constraints for table `Projektai`
--
ALTER TABLE `Projektai`
  ADD CONSTRAINT `Uzsako` FOREIGN KEY (`fk_Uzsakovas`) REFERENCES `Uzsakovai` (`ID`);

--
-- Constraints for table `Remejai`
--
ALTER TABLE `Remejai`
  ADD CONSTRAINT `Skiria2` FOREIGN KEY (`fk_Biudzetas`) REFERENCES `Biudzetai` (`id_Biudzetas`);

--
-- Constraints for table `Uzduotys`
--
ALTER TABLE `Uzduotys`
  ADD CONSTRAINT `Atliekama_pagal` FOREIGN KEY (`fk_Grafikas`) REFERENCES `Grafikai` (`Nr`);

--
-- Constraints for table `Uzsakovai`
--
ALTER TABLE `Uzsakovai`
  ADD CONSTRAINT `Skiria` FOREIGN KEY (`fk_Biudzetas`) REFERENCES `Biudzetai` (`id_Biudzetas`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
