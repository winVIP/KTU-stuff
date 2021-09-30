-- phpMyAdmin SQL Dump
-- version 4.5.4.1deb2ubuntu2.1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Nov 24, 2019 at 02:38 PM
-- Server version: 5.7.24-0ubuntu0.16.04.1
-- PHP Version: 7.2.12-1+ubuntu16.04.1+deb.sury.org+1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `stud`
--

-- --------------------------------------------------------

--
-- Table structure for table `zinutesLAB3`
--

CREATE TABLE `zinutesLAB3` (
  `id` int(5) NOT NULL,
  `vardas` varchar(20) COLLATE utf8_lithuanian_ci NOT NULL,
  `epastas` varchar(50) COLLATE utf8_lithuanian_ci NOT NULL,
  `kam` varchar(20) COLLATE utf8_lithuanian_ci NOT NULL,
  `data` datetime NOT NULL,
  `ip` varchar(20) COLLATE utf8_lithuanian_ci NOT NULL,
  `zinute` text COLLATE utf8_lithuanian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Dumping data for table `zinutesLAB3`
--

INSERT INTO `zinutesLAB3` (`id`, `vardas`, `epastas`, `kam`, `data`, `ip`, `zinute`) VALUES
(1, 'Dovydas', 'dovsni1@ktu.lt', 'Domantas', '2019-11-03 06:25:27', '192.168.1.10', 'Sveikas, Domantai.'),
(2, 'Domantas', 'dombar2@ktu.lt', 'Dovydas', '2019-11-04 06:25:27', '192.168.2.10', 'Sveikas, Dovydai!'),
(3, 'Arnas', 'arnraz@ktu.lt', 'Laurynas', '2019-11-04 07:25:27', '192.168.3.10', 'Zdarova Lauri.'),
(4, 'Laurynas', 'laurny@ktu.lt', 'Dovydas', '2019-11-07 11:43:22', '192.168.4.11', 'Sveikas, ar padarei 3 laboratorini?'),
(5, 'Dovydas', 'dovsni1@ktu.lt', 'Laurynas', '2019-11-12 06:33:47', '192.168.1.10', 'Sveikas. Dabar darau.'),
(6, 'Laurynas', 'laurny@ktu.lt', 'Dovydas', '2019-11-17 05:26:40', '192.168.4.11', 'Okey, pranesk, kai baigsi. :)');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `zinutesLAB3`
--
ALTER TABLE `zinutesLAB3`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `zinutesLAB3`
--
ALTER TABLE `zinutesLAB3`
  MODIFY `id` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
