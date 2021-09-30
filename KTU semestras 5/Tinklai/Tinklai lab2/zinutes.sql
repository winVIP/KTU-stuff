-- phpMyAdmin SQL Dump
-- version 4.5.4.1deb2ubuntu2.1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Dec 03, 2019 at 12:04 PM
-- Server version: 5.7.24-0ubuntu0.16.04.1
-- PHP Version: 7.2.12-1+ubuntu16.04.1+deb.sury.org+1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pvz`
--

-- --------------------------------------------------------

--
-- Table structure for table `zinutes`
--

CREATE TABLE `zinutes` (
  `id` int(5) NOT NULL,
  `vardas` varchar(20) COLLATE utf8_lithuanian_ci NOT NULL,
  `epastas` varchar(50) COLLATE utf8_lithuanian_ci NOT NULL,
  `kam` varchar(20) COLLATE utf8_lithuanian_ci NOT NULL,
  `data` datetime NOT NULL,
  `ip` varchar(20) COLLATE utf8_lithuanian_ci NOT NULL,
  `zinute` text COLLATE utf8_lithuanian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Dumping data for table `zinutes`
--

INSERT INTO `zinutes` (`id`, `vardas`, `epastas`, `kam`, `data`, `ip`, `zinute`) VALUES
(1, 'Domas', 'domas@gmail.com', 'Tomas', '2019-10-16 00:00:00', '192.168.10.12', 'bla bla bla'),
(2, 'Tadukas', 'slyva525@gmail.com', 'Domantas', '2014-04-08 11:14:25', '127.0.0.1', 'ka cia sneki'),
(13, 'Domantas', 'domentertainmenthd@gmail.com', 'Arniuha', '2019-10-16 00:00:00', '127.0.0.1', 'Kvieciu i svecius'),
(14, 'Daina', 'info@ziedukalve.lt', 'Tadukas', '2019-10-01 00:00:00', '127.0.0.1', 'koks oras Palangoje?'),
(16, 'Domantas', 'domentertainmenthd@gmail.com', 'Arniuha', '2019-10-27 13:04:01', '127.0.0.1', 'test'),
(17, 'ziauru', 'ziauru@gmail.com', 'Domas', '2019-10-27 13:10:56', '127.0.0.1', 'slyva'),
(24, 'Domantas', 'domentertainmenthd@gmail.com', 'Arniuha', '2019-10-27 13:29:15', '::1', 'kada namo eini?'),
(30, 'Daina', 'info@ziedukalve.lt', 'jhf', '2019-10-27 14:31:32', '::1', 'nupirk bilieta'),
(31, 'Daina', 'info@ziedukalve.lt', 'jhf', '2019-10-27 14:31:37', '::1', 'Ar gavai bilietus?'),
(32, 'Daina', 'info@ziedukalve.lt', 'Tadukas', '2019-10-27 14:31:39', '::1', 'kur tu eini'),
(33, 'Domantas', 'domentertainmenthd@gmail.com', 'Arniuha', '2019-10-27 18:22:44', '::1', 'iki rytojaus'),
(34, 'Arniuha', 'arh@gmail.com', 'Domantas', '2019-10-27 18:22:47', '::1', 'Dar palauk');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `zinutes`
--
ALTER TABLE `zinutes`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `zinutes`
--
ALTER TABLE `zinutes`
  MODIFY `id` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=35;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
