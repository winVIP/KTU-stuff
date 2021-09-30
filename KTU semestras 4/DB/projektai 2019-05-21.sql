-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 21, 2019 at 10:21 AM
-- Server version: 10.1.38-MariaDB
-- PHP Version: 7.3.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `projektai`
--

-- --------------------------------------------------------

--
-- Table structure for table `biudzetai`
--

CREATE TABLE `biudzetai` (
  `id_Biudzetas` int(8) NOT NULL,
  `Esamas_biudzetas` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `biudzetai`
--

INSERT INTO `biudzetai` (`id_Biudzetas`, `Esamas_biudzetas`) VALUES
(1, 976611.85),
(2, 577607.18),
(3, 390438.97),
(4, 330741.7),
(5, 596411.75),
(6, 838336.12),
(7, 515461.59),
(8, 131702.22),
(9, 251211.92),
(10, 658623.03),
(11, 991288.44),
(12, 738930.06),
(13, 118685.12),
(14, 801336.96),
(15, 658091.86),
(16, 958639),
(17, 506474.27),
(18, 352110.17),
(19, 662266.72),
(20, 887144.96);

-- --------------------------------------------------------

--
-- Table structure for table `dalyvauja`
--

CREATE TABLE `dalyvauja` (
  `fk_Dalyvis` int(8) NOT NULL,
  `fk_Projektas` int(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `dalyvauja`
--

INSERT INTO `dalyvauja` (`fk_Dalyvis`, `fk_Projektas`) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 11),
(12, 12),
(13, 13),
(14, 14),
(15, 15),
(16, 16),
(17, 17),
(18, 18),
(19, 19);

-- --------------------------------------------------------

--
-- Table structure for table `dalyviai`
--

CREATE TABLE `dalyviai` (
  `ID` int(8) NOT NULL,
  `Vardas` varchar(255) NOT NULL,
  `Pavarde` varchar(255) NOT NULL,
  `Telefonas` varchar(255) NOT NULL,
  `El_Pastas` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `dalyviai`
--

INSERT INTO `dalyviai` (`ID`, `Vardas`, `Pavarde`, `Telefonas`, `El_Pastas`) VALUES
(1, 'Melli', 'Scriven', '620-252-3542', 'mscriven0@illinois.edu'),
(2, 'Margarita', 'Seathwright', '368-908-7772', 'mseathwright1@google.com.au'),
(3, 'Arvie', 'Cowell', '693-176-0461', 'acowell2@nba.com'),
(4, 'Cointon', 'Mabson', '352-548-7371', 'cmabson3@google.com'),
(5, 'Delano', 'Ullrich', '592-189-0933', 'dullrich4@ted.com'),
(6, 'Clarissa', 'Simionato', '506-563-8140', 'csimionato5@smugmug.com'),
(7, 'Maddie', 'Dytham', '160-148-3537', 'mdytham6@goodreads.com'),
(8, 'Clarie', 'Stroband', '319-712-5328', 'cstroband7@yolasite.com'),
(9, 'Velma', 'Mayte', '636-685-9932', 'vmayte8@businessweek.com'),
(10, 'Karilynn', 'Cornelisse', '271-984-0041', 'kcornelisse9@newyorker.com'),
(11, 'Nester', 'Bakhrushin', '166-657-8292', 'nbakhrushina@harvard.edu'),
(12, 'Tessi', 'Joselevitch', '408-915-7492', 'tjoselevitchb@oakley.com'),
(13, 'Vinnie', 'Godspede', '753-300-9169', 'vgodspedec@nymag.com'),
(14, 'Graham', 'Raiment', '729-653-8323', 'graimentd@themeforest.net'),
(15, 'Cristie', 'Armytage', '525-392-6609', 'carmytagee@wikipedia.org'),
(16, 'Elia', 'Crosson', '763-493-7552', 'ecrossonf@cpanel.net'),
(17, 'Egon', 'Bowen', '602-386-8307', 'eboweng@tmall.com'),
(18, 'Toby', 'Date', '565-802-0851', 'tdateh@scribd.com'),
(19, 'Stella', 'MacEvilly', '280-460-2161', 'smacevillyi@fc2.com');

-- --------------------------------------------------------

--
-- Table structure for table `darbuotojai`
--

CREATE TABLE `darbuotojai` (
  `ID` int(8) NOT NULL,
  `Vardas` varchar(255) NOT NULL,
  `Pavarde` varchar(255) NOT NULL,
  `Telefonas` varchar(255) NOT NULL,
  `El_Pastas` varchar(255) DEFAULT NULL,
  `Adresas` varchar(255) DEFAULT NULL,
  `fk_Uzduotis2` int(8) NOT NULL,
  `fk_Role` int(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `darbuotojai`
--

INSERT INTO `darbuotojai` (`ID`, `Vardas`, `Pavarde`, `Telefonas`, `El_Pastas`, `Adresas`, `fk_Uzduotis2`, `fk_Role`) VALUES
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
(20, 'Kary', 'McGaraghan', '855-194-4895', 'kmcgaraghanj@nbcnews.com', '625 Bowman Circle', 20, 20),
(22, 'Gardner', 'Hankey', '237-948-0293', 'ghankey0@usda.gov', '085 Porter Park', 1, 1),
(23, 'Maria', 'Hardesty', '305-454-7450', 'mhardesty1@cnet.com', '3 Continental Alley', 1, 1),
(24, 'Betteann', 'Polden', '671-824-0025', 'bpolden2@live.com', '2645 Sherman Court', 1, 1),
(25, 'Georgetta', 'Gammel', '900-255-1176', 'ggammel3@xinhuanet.com', '58377 Anderson Road', 1, 1),
(26, 'Seumas', 'Dillistone', '417-169-6256', 'sdillistone4@guardian.co.uk', '18691 Holy Cross Lane', 1, 1),
(27, 'Elva', 'Egginson', '442-891-9169', 'eegginson5@opera.com', '55652 Eastlawn Alley', 1, 1),
(28, 'Bogart', 'Vahey', '692-258-2961', 'bvahey6@patch.com', '96 Larry Terrace', 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `grafikai`
--

CREATE TABLE `grafikai` (
  `Nr` int(8) NOT NULL,
  `Pavadinimas` varchar(255) NOT NULL,
  `Nuo_kada` date NOT NULL,
  `Iki_kada` date NOT NULL,
  `fk_Projektas2` int(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `grafikai`
--

INSERT INTO `grafikai` (`Nr`, `Pavadinimas`, `Nuo_kada`, `Iki_kada`, `fk_Projektas2`) VALUES
(1, 'sed ante', '2018-03-12', '2018-03-28', 1),
(2, 'eget', '2018-03-06', '2018-03-22', 2),
(3, 'pede', '2018-03-08', '2018-03-16', 3),
(4, 'vestibulum', '2018-03-09', '2018-03-20', 4),
(5, 'habitasse', '2018-03-07', '2018-03-16', 5),
(6, 'aliquet', '2018-03-05', '2018-03-26', 6),
(7, 'neque', '2018-03-12', '2018-03-29', 7),
(8, 'venenatis', '2018-03-13', '2018-03-17', 8),
(9, 'amet sapien', '2018-03-09', '2018-03-26', 9),
(10, 'est quam', '2018-03-10', '2018-03-21', 10),
(11, 'faucibus', '2018-03-09', '2018-03-25', 11),
(12, 'massa tempor', '2018-03-04', '2018-03-15', 12),
(13, 'lacus', '2018-03-12', '2018-03-19', 13),
(14, 'purus', '2018-03-13', '2018-03-29', 14),
(15, 'suscipit', '2018-03-05', '2018-03-15', 15),
(16, 'odio', '2018-03-12', '2018-03-19', 16),
(17, 'sed', '2018-03-10', '2018-03-22', 17),
(18, 'cursus', '2018-03-11', '2018-03-23', 18),
(19, 'luctus et', '2018-03-09', '2018-03-18', 19),
(20, 'tempor turpis', '2018-03-07', '2018-03-19', 20);

-- --------------------------------------------------------

--
-- Table structure for table `islaidos`
--

CREATE TABLE `islaidos` (
  `Nr` int(8) NOT NULL,
  `Suma` double NOT NULL,
  `Paskirtis` varchar(255) NOT NULL,
  `Data` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `fk_Biudzetas2` int(8) NOT NULL,
  `fk_Uzduotis` int(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `islaidos`
--

INSERT INTO `islaidos` (`Nr`, `Suma`, `Paskirtis`, `Data`, `fk_Biudzetas2`, `fk_Uzduotis`) VALUES
(1, 42335.97, 'accumsan tortor', '2018-03-15 19:18:54', 1, 1),
(2, 84098.66, 'suscipit a', '2018-03-19 15:12:58', 2, 2),
(3, 83105, 'eget eros', '2018-03-19 18:44:34', 3, 3),
(4, 71847.7, 'cras', '2018-03-17 07:04:35', 4, 4),
(5, 70766.14, 'arcu', '2018-03-28 11:53:33', 5, 5),
(6, 52634.82, 'vestibulum', '2018-03-24 19:18:22', 6, 6),
(7, 45618.84, 'arcu', '2018-03-26 12:31:56', 7, 7),
(8, 90374.43, 'diam nam', '2018-03-26 15:13:21', 8, 8),
(9, 26783.43, 'pellentesque', '2018-03-27 13:07:21', 9, 9),
(10, 64228.3, 'rhoncus', '2018-03-19 09:03:38', 10, 10),
(11, 81684.98, 'in blandit', '2018-03-19 17:51:57', 11, 11),
(12, 85427.65, 'quam', '2018-03-16 07:34:16', 12, 12),
(13, 47253, 'in lectus', '2018-03-28 13:18:37', 13, 13),
(14, 67705.04, 'convallis', '2018-03-26 13:31:20', 14, 14),
(15, 34427.9, 'quisque arcu', '2018-03-25 20:00:01', 15, 15),
(16, 79452.53, 'turpis elementum', '2018-03-20 21:08:06', 16, 16),
(17, 40505.92, 'eu', '2018-03-28 07:05:30', 17, 17),
(18, 25588.88, 'ac tellus', '2018-03-21 17:06:20', 18, 18),
(19, 60118.34, 'odio', '2018-03-21 09:41:30', 19, 19),
(20, 40207.92, 'sit amet', '2018-03-15 07:09:33', 20, 20);

-- --------------------------------------------------------

--
-- Table structure for table `lesos`
--

CREATE TABLE `lesos` (
  `Nr` int(8) NOT NULL,
  `Suma` double NOT NULL,
  `Data` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `fk_Biudzetas` int(8) NOT NULL,
  `fk_Uzsakovas2` int(8) NOT NULL,
  `fk_Remejas` int(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `lesos`
--

INSERT INTO `lesos` (`Nr`, `Suma`, `Data`, `fk_Biudzetas`, `fk_Uzsakovas2`, `fk_Remejas`) VALUES
(1, 69806.82, '2018-03-22 09:55:26', 1, 1, 1),
(2, 89924.68, '2018-03-27 14:28:25', 2, 2, 2),
(3, 39227.71, '2018-03-13 22:11:10', 3, 3, 3),
(4, 88962.05, '2018-03-21 18:42:23', 4, 4, 4),
(5, 66841.06, '2018-03-17 02:41:27', 5, 5, 5),
(6, 93412.55, '2018-03-15 18:44:13', 6, 6, 6),
(7, 75900.16, '2018-03-21 02:10:39', 7, 7, 7),
(8, 35102.73, '2018-03-14 13:35:52', 8, 8, 8),
(9, 80050.81, '2018-03-27 00:43:14', 9, 9, 9),
(10, 79683.75, '2018-03-19 08:14:28', 10, 10, 10),
(11, 83896.21, '2018-03-17 15:44:59', 11, 11, 11),
(12, 50732.43, '2018-03-21 06:53:42', 12, 12, 12),
(13, 14804.42, '2018-03-14 03:53:11', 13, 13, 13),
(14, 62529.63, '2018-03-15 08:53:08', 14, 14, 14),
(15, 76752.01, '2018-03-14 06:48:04', 15, 15, 15),
(16, 46799.42, '2018-03-20 15:00:56', 16, 16, 16),
(17, 70153.9, '2018-03-19 16:42:11', 17, 17, 17),
(18, 71226.6, '2018-03-16 20:06:49', 18, 18, 18),
(19, 82911.11, '2018-03-21 07:50:40', 19, 19, 19),
(20, 45568.9, '2018-03-21 07:43:33', 20, 20, 20);

-- --------------------------------------------------------

--
-- Table structure for table `projektai`
--

CREATE TABLE `projektai` (
  `Nr` int(8) NOT NULL,
  `Pavadinimas` varchar(255) NOT NULL,
  `Nuo_kada` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Iki_kada` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `fk_Uzsakovas` int(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `projektai`
--

INSERT INTO `projektai` (`Nr`, `Pavadinimas`, `Nuo_kada`, `Iki_kada`, `fk_Uzsakovas`) VALUES
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
-- Table structure for table `remejai`
--

CREATE TABLE `remejai` (
  `ID` int(8) NOT NULL,
  `Vardas` varchar(255) NOT NULL,
  `Pavarde` varchar(255) NOT NULL,
  `Telefonas` varchar(255) NOT NULL,
  `Kompanija` varchar(255) DEFAULT NULL,
  `El_Pastas` varchar(255) DEFAULT NULL,
  `Adresas` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `remejai`
--

INSERT INTO `remejai` (`ID`, `Vardas`, `Pavarde`, `Telefonas`, `Kompanija`, `El_Pastas`, `Adresas`) VALUES
(1, 'Brita', 'Lewsie', '667-417-9392', 'Orn-Hahn', 'blewsie0@1688.com', '651 Charing Cross Alley'),
(2, 'Kit', 'Hayller', '215-432-0851', 'Pfannerstill, Olson and Klein', 'khayller1@slashdot.org', '6 Oak Valley Circle'),
(3, 'Annabel', 'Westwood', '655-292-5894', 'Jaskolski-Walker', 'awestwood2@fotki.com', '876 Bluestem Hill'),
(4, 'Melvin', 'Robbings', '636-906-6567', 'Kunze-Stehr', 'mrobbings3@hibu.com', '41 Laurel Park'),
(5, 'Vinnie', 'Honnicott', '788-268-5719', 'Bernier-Grant', 'vhonnicott4@merriam-webster.com', '35818 Pearson Way'),
(6, 'Toma', 'Doyley', '161-668-1799', 'Williamson Group', 'tdoyley5@skype.com', '89 Meadow Valley Circle'),
(7, 'Bartholomew', 'Rivalland', '119-959-5886', 'Roberts and Sons', 'brivalland6@imgur.com', '2 Westridge Crossing'),
(8, 'Cassandry', 'Oloshkin', '970-209-0084', 'Rau, Graham and Stanton', 'coloshkin7@yale.edu', '4 Marcy Park'),
(9, 'Elnora', 'Caldecot', '395-524-6898', 'Hansen, Towne and Deckow', 'ecaldecot8@pbs.org', '664 Lawn Pass'),
(10, 'Viole', 'Beacock', '617-826-4880', 'Pfeffer Inc', 'vbeacock9@epa.gov', '02679 Rockefeller Hill'),
(11, 'Doti', 'Sorley', '907-475-9426', 'Bayer, Mayer and Friesen', 'dsorleya@amazon.de', '11 Johnson Point'),
(12, 'Errol', 'Andryszczak', '885-813-4221', 'Medhurst-Hilll', 'eandryszczakb@seesaa.net', '2 Everett Alley'),
(13, 'Vilhelmina', 'Shinton', '601-281-8912', 'Dietrich Group', 'vshintonc@digg.com', '9 Monica Drive'),
(14, 'Leonanie', 'McShirrie', '485-647-5881', 'Harris and Sons', 'lmcshirried@tinyurl.com', '870 Hayes Place'),
(15, 'Ravid', 'Mouton', '738-685-5042', 'Reichel and Sons', 'rmoutone@4shared.com', '45462 Alpine Place'),
(16, 'Binnie', 'Limbourne', '460-981-1849', 'Rice Group', 'blimbournef@vimeo.com', '68991 Kennedy Street'),
(17, 'Lynnette', 'Galiero', '711-644-9744', 'Mraz, Gusikowski and Nicolas', 'lgalierog@rambler.ru', '75 Starling Street'),
(18, 'Scotti', 'Crossdale', '845-666-3302', 'Langworth LLC', 'scrossdaleh@oaic.gov.au', '958 Moose Avenue'),
(19, 'Dieter', 'Costello', '654-662-7370', 'Braun Group', 'dcostelloi@soundcloud.com', '585 Blackbird Circle'),
(20, 'Joaquin', 'Upchurch', '553-830-8293', 'Corkery, Kris and Jacobi', 'jupchurchj@sina.com.cn', '28 Fordem Crossing');

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE `roles` (
  `Nr` int(8) NOT NULL,
  `Pavadinimas` varchar(255) NOT NULL,
  `Uzduotis` varchar(255) NOT NULL,
  `Nuo_kada` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Iki_kada` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `roles`
--

INSERT INTO `roles` (`Nr`, `Pavadinimas`, `Uzduotis`, `Nuo_kada`, `Iki_kada`) VALUES
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
-- Table structure for table `uzduotys`
--

CREATE TABLE `uzduotys` (
  `Nr` int(8) NOT NULL,
  `Pavadinimas` varchar(255) NOT NULL,
  `fk_Grafikas` int(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `uzduotys`
--

INSERT INTO `uzduotys` (`Nr`, `Pavadinimas`, `fk_Grafikas`) VALUES
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
-- Table structure for table `uzsakovai`
--

CREATE TABLE `uzsakovai` (
  `ID` int(8) NOT NULL,
  `Vardas` varchar(255) NOT NULL,
  `Pavarde` varchar(255) NOT NULL,
  `Telefonas` varchar(255) NOT NULL,
  `Kompanija` varchar(255) DEFAULT NULL,
  `El_Pastas` varchar(255) DEFAULT NULL,
  `Adresas` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `uzsakovai`
--

INSERT INTO `uzsakovai` (`ID`, `Vardas`, `Pavarde`, `Telefonas`, `Kompanija`, `El_Pastas`, `Adresas`) VALUES
(1, 'Felix', 'Baccup', '567-207-8217', 'Huels Group', 'fbaccup0@skype.com', '22 Jackson Junction'),
(2, 'Rem', 'Guynemer', '741-858-3684', 'Tromp and Sons', 'rguynemer1@amazon.de', '876 Manufacturers Hill'),
(3, 'Barton', 'Norheny', '224-952-1711', 'Fay Inc', 'bnorheny2@imdb.com', '5180 Lunder Junction'),
(4, 'Shandy', 'Halwell', '926-313-9347', 'Herman, Tromp and Feil', 'shalwell3@engadget.com', '20690 Clarendon Street'),
(5, 'Karlan', 'Dudley', '463-295-7035', 'Bruen, Bartoletti and Labadie', 'kdudley4@addthis.com', '7 Warrior Hill'),
(6, 'Titus', 'Kauffman', '650-867-5874', 'Cormier-Mann', 'tkauffman5@cam.ac.uk', '122 Hansons Lane'),
(7, 'Querida', 'Reddyhoff', '551-744-0367', 'Goodwin, Dickens and Bayer', 'qreddyhoff6@sogou.com', '9 Jana Terrace'),
(8, 'Deloria', 'Pavinese', '126-989-7334', 'Romaguera LLC', 'dpavinese7@bravesites.com', '37291 Randy Pass'),
(9, 'Wilmette', 'Burras', '199-480-1676', 'Borer Inc', 'wburras8@goo.gl', '594 Erie Hill'),
(10, 'Marion', 'Ayliff', '227-519-0864', 'Simonis-Barrows', 'mayliff9@homestead.com', '95548 Elka Trail'),
(11, 'Elsinore', 'Kaysor', '148-362-1976', 'Hyatt, Heathcote and Harvey', 'ekaysora@rediff.com', '624 Pleasure Junction'),
(12, 'Henka', 'Gudahy', '403-756-9482', 'Kerluke-West', 'hgudahyb@twitter.com', '89967 Menomonie Crossing'),
(13, 'Mark', 'Deinert', '779-333-6979', 'Fisher-Wiegand', 'mdeinertc@harvard.edu', '86928 Crest Line Center'),
(14, 'Annabell', 'Covet', '302-404-6391', 'Schultz, Stanton and Zboncak', 'acovetd@hostgator.com', '8830 David Lane'),
(15, 'Gabriel', 'Benson', '790-730-1762', 'Quigley, Nolan and Block', 'gbensone@ovh.net', '75813 Golden Leaf Avenue'),
(16, 'Doyle', 'Frotton', '206-332-9476', 'Hansen, Hodkiewicz and King', 'dfrottonf@goo.gl', '20118 Shasta Drive'),
(17, 'Derk', 'Thatcham', '721-996-2532', 'Shields-Little', 'dthatchamg@youku.com', '862 Spenser Hill'),
(18, 'Veradis', 'Sellack', '365-324-5450', 'Murphy and Sons', 'vsellackh@devhub.com', '822 Valley Edge Terrace'),
(19, 'Carmelina', 'Balassa', '156-980-1866', 'Pollich LLC', 'cbalassai@ning.com', '1 Stang Circle'),
(20, 'Kate', 'Murch', '718-699-9401', 'Reichel-Langosh', 'kmurchj@liveinternet.ru', '1681 Kensington Plaza'),
(21, 'Jonas', 'Jonaitis', '+37064642136', 'Kerpetos', 'jonas@gmail.com', 'baltu 3');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `biudzetai`
--
ALTER TABLE `biudzetai`
  ADD PRIMARY KEY (`id_Biudzetas`);

--
-- Indexes for table `dalyvauja`
--
ALTER TABLE `dalyvauja`
  ADD PRIMARY KEY (`fk_Dalyvis`,`fk_Projektas`),
  ADD KEY `fkc_Projektas` (`fk_Projektas`);

--
-- Indexes for table `dalyviai`
--
ALTER TABLE `dalyviai`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `darbuotojai`
--
ALTER TABLE `darbuotojai`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkc_Uzduotis2` (`fk_Uzduotis2`),
  ADD KEY `fkc_Role` (`fk_Role`);

--
-- Indexes for table `grafikai`
--
ALTER TABLE `grafikai`
  ADD PRIMARY KEY (`Nr`),
  ADD KEY `fkc_Projektas2` (`fk_Projektas2`);

--
-- Indexes for table `islaidos`
--
ALTER TABLE `islaidos`
  ADD PRIMARY KEY (`Nr`),
  ADD KEY `fkc_Biudzetas2` (`fk_Biudzetas2`),
  ADD KEY `fkc_Uzduotis` (`fk_Uzduotis`);

--
-- Indexes for table `lesos`
--
ALTER TABLE `lesos`
  ADD PRIMARY KEY (`Nr`),
  ADD KEY `fkc_Biudzetas` (`fk_Biudzetas`),
  ADD KEY `fkc_Uzsakovas2` (`fk_Uzsakovas2`),
  ADD KEY `fkc_Remejas` (`fk_Remejas`);

--
-- Indexes for table `projektai`
--
ALTER TABLE `projektai`
  ADD PRIMARY KEY (`Nr`),
  ADD UNIQUE KEY `fk_Uzsakovas` (`fk_Uzsakovas`);

--
-- Indexes for table `remejai`
--
ALTER TABLE `remejai`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`Nr`);

--
-- Indexes for table `uzduotys`
--
ALTER TABLE `uzduotys`
  ADD PRIMARY KEY (`Nr`),
  ADD KEY `fkc_Grafikas` (`fk_Grafikas`);

--
-- Indexes for table `uzsakovai`
--
ALTER TABLE `uzsakovai`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `biudzetai`
--
ALTER TABLE `biudzetai`
  MODIFY `id_Biudzetas` int(8) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `dalyviai`
--
ALTER TABLE `dalyviai`
  MODIFY `ID` int(8) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `darbuotojai`
--
ALTER TABLE `darbuotojai`
  MODIFY `ID` int(8) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT for table `grafikai`
--
ALTER TABLE `grafikai`
  MODIFY `Nr` int(8) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `islaidos`
--
ALTER TABLE `islaidos`
  MODIFY `Nr` int(8) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `lesos`
--
ALTER TABLE `lesos`
  MODIFY `Nr` int(8) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `projektai`
--
ALTER TABLE `projektai`
  MODIFY `Nr` int(8) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `remejai`
--
ALTER TABLE `remejai`
  MODIFY `ID` int(8) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `roles`
--
ALTER TABLE `roles`
  MODIFY `Nr` int(8) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `uzduotys`
--
ALTER TABLE `uzduotys`
  MODIFY `Nr` int(8) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `uzsakovai`
--
ALTER TABLE `uzsakovai`
  MODIFY `ID` int(8) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `dalyvauja`
--
ALTER TABLE `dalyvauja`
  ADD CONSTRAINT `fkc_Dalyvis` FOREIGN KEY (`fk_Dalyvis`) REFERENCES `dalyviai` (`ID`),
  ADD CONSTRAINT `fkc_Projektas` FOREIGN KEY (`fk_Projektas`) REFERENCES `projektai` (`Nr`);

--
-- Constraints for table `darbuotojai`
--
ALTER TABLE `darbuotojai`
  ADD CONSTRAINT `fkc_Role` FOREIGN KEY (`fk_Role`) REFERENCES `roles` (`Nr`),
  ADD CONSTRAINT `fkc_Uzduotis2` FOREIGN KEY (`fk_Uzduotis2`) REFERENCES `uzduotys` (`Nr`);

--
-- Constraints for table `grafikai`
--
ALTER TABLE `grafikai`
  ADD CONSTRAINT `fkc_Projektas2` FOREIGN KEY (`fk_Projektas2`) REFERENCES `projektai` (`Nr`);

--
-- Constraints for table `islaidos`
--
ALTER TABLE `islaidos`
  ADD CONSTRAINT `fkc_Biudzetas2` FOREIGN KEY (`fk_Biudzetas2`) REFERENCES `biudzetai` (`id_Biudzetas`),
  ADD CONSTRAINT `fkc_Uzduotis` FOREIGN KEY (`fk_Uzduotis`) REFERENCES `uzduotys` (`Nr`);

--
-- Constraints for table `lesos`
--
ALTER TABLE `lesos`
  ADD CONSTRAINT `fkc_Biudzetas` FOREIGN KEY (`fk_Biudzetas`) REFERENCES `biudzetai` (`id_Biudzetas`),
  ADD CONSTRAINT `fkc_Remejas` FOREIGN KEY (`fk_Remejas`) REFERENCES `remejai` (`ID`),
  ADD CONSTRAINT `fkc_Uzsakovas2` FOREIGN KEY (`fk_Uzsakovas2`) REFERENCES `uzsakovai` (`ID`);

--
-- Constraints for table `projektai`
--
ALTER TABLE `projektai`
  ADD CONSTRAINT `fkc_Uzsakovas` FOREIGN KEY (`fk_Uzsakovas`) REFERENCES `uzsakovai` (`ID`);

--
-- Constraints for table `uzduotys`
--
ALTER TABLE `uzduotys`
  ADD CONSTRAINT `fkc_Grafikas` FOREIGN KEY (`fk_Grafikas`) REFERENCES `grafikai` (`Nr`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
