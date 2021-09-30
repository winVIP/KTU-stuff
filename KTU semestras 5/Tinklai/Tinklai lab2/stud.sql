-- phpMyAdmin SQL Dump
-- version 4.5.4.1deb2ubuntu2.1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Dec 03, 2019 at 11:43 AM
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
-- Table structure for table `vyteniskunickas`
--

CREATE TABLE `vyteniskunickas` (
  `id` int(5) NOT NULL,
  `vardas` varchar(50) NOT NULL,
  `epastas` varchar(50) NOT NULL,
  `zinute` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `vyteniskunickas`
--

INSERT INTO `vyteniskunickas` (`id`, `vardas`, `epastas`, `zinute`) VALUES
(1, 'Jonas', 'jonas@email.com', 'Sveiki'),
(2, 'Tomas', 'tomas@epastas.lt', 'Nesveiki'),
(3, 'Rimas', 'rimas@elpastas.lt', ' 8498484');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `vyteniskunickas`
--
ALTER TABLE `vyteniskunickas`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `vyteniskunickas`
--
ALTER TABLE `vyteniskunickas`
  MODIFY `id` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
