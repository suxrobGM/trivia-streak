-- phpMyAdmin SQL Dump
-- version 3.3.9
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Sep 09, 2017 at 11:25 PM
-- Server version: 5.5.8
-- PHP Version: 5.3.5

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `trivia`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

CREATE TABLE IF NOT EXISTS `admin` (
  `admin_id` int(11) NOT NULL AUTO_INCREMENT,
  `admin_user` varchar(250) NOT NULL,
  `admin_pass` varchar(250) NOT NULL,
  PRIMARY KEY (`admin_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`admin_id`, `admin_user`, `admin_pass`) VALUES
(1, 'admin', 'admin');

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE IF NOT EXISTS `categories` (
  `cat_id` int(11) NOT NULL AUTO_INCREMENT,
  `cat_name` varchar(250) DEFAULT NULL,
  `cat_image` varchar(250) DEFAULT NULL,
  `cat_status` enum('Y','N') DEFAULT NULL,
  `cat_created_on` date DEFAULT NULL,
  PRIMARY KEY (`cat_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `categories`
--


-- --------------------------------------------------------

--
-- Table structure for table `game_for_prize`
--

CREATE TABLE IF NOT EXISTS `game_for_prize` (
  `t_game_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `game_user_id` varchar(255) NOT NULL,
  `game_user_type` enum('OWN','FB') NOT NULL,
  `pruchase_game_count` int(11) NOT NULL,
  PRIMARY KEY (`t_game_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `game_for_prize`
--


-- --------------------------------------------------------

--
-- Table structure for table `game_fun_leaderboard`
--

CREATE TABLE IF NOT EXISTS `game_fun_leaderboard` (
  `game_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `game_user_id` varchar(255) NOT NULL,
  `game_user_type` enum('OWN','FB') NOT NULL,
  `game_points` bigint(20) NOT NULL,
  `game_date` date NOT NULL,
  `cat_id` int(11) NOT NULL,
  `sec_cat_id` int(11) NOT NULL,
  `th_cat_id` int(11) NOT NULL,
  `prize_id` int(11) NOT NULL,
  PRIMARY KEY (`game_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `game_fun_leaderboard`
--


-- --------------------------------------------------------

--
-- Table structure for table `game_lifeline`
--

CREATE TABLE IF NOT EXISTS `game_lifeline` (
  `lifeline_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `lifeline_user_id` bigint(20) NOT NULL,
  `lifeline_type` enum('eliminate','change','add') NOT NULL,
  `life_line_count` int(11) NOT NULL,
  PRIMARY KEY (`lifeline_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `game_lifeline`
--


-- --------------------------------------------------------

--
-- Table structure for table `game_prize`
--

CREATE TABLE IF NOT EXISTS `game_prize` (
  `prize_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `prize_title` varchar(255) NOT NULL,
  `prize_image` varchar(255) NOT NULL,
  `prize_status` enum('Y','N') NOT NULL,
  `prize_date` date NOT NULL,
  PRIMARY KEY (`prize_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `game_prize`
--


-- --------------------------------------------------------

--
-- Table structure for table `game_prize_leaderboard`
--

CREATE TABLE IF NOT EXISTS `game_prize_leaderboard` (
  `game_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `game_user_id` bigint(20) NOT NULL,
  `game_points` bigint(20) NOT NULL,
  `game_date` date NOT NULL,
  PRIMARY KEY (`game_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `game_prize_leaderboard`
--


-- --------------------------------------------------------

--
-- Table structure for table `game_winner`
--

CREATE TABLE IF NOT EXISTS `game_winner` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `game_user_id` varchar(255) NOT NULL,
  `game_user_type` enum('OWN','FB') NOT NULL,
  `prize_id` int(11) NOT NULL,
  `winner_points` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `game_winner`
--


-- --------------------------------------------------------

--
-- Table structure for table `questions`
--

CREATE TABLE IF NOT EXISTS `questions` (
  `qu_id` int(11) NOT NULL AUTO_INCREMENT,
  `qu_cat_id` int(11) NOT NULL,
  `qu_sec_cat_id` int(11) NOT NULL,
  `qu_th_cat_id` int(11) NOT NULL,
  `qu_text` text NOT NULL,
  `qu_difficulty` enum('EASY','MEDIUM','DIFFICULT') NOT NULL,
  `qu_option1` varchar(250) NOT NULL,
  `qu_option2` varchar(250) NOT NULL,
  `qu_option3` varchar(250) NOT NULL,
  `qu_option4` varchar(250) NOT NULL,
  `qu_answer` enum('1','2','3','4') NOT NULL,
  `qu_status` enum('Y','N') DEFAULT NULL,
  `qu_created_on` date DEFAULT NULL,
  PRIMARY KEY (`qu_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Dumping data for table `questions`
--


-- --------------------------------------------------------

--
-- Table structure for table `second_categories`
--

CREATE TABLE IF NOT EXISTS `second_categories` (
  `sec_cat_id` int(11) NOT NULL AUTO_INCREMENT,
  `sec_cat_parent` int(11) NOT NULL DEFAULT '0',
  `sec_cat_name` varchar(250) DEFAULT NULL,
  `sec_cat_image` varchar(250) DEFAULT NULL,
  `sec_cat_status` enum('Y','N') DEFAULT NULL,
  `sec_created_on` date DEFAULT NULL,
  PRIMARY KEY (`sec_cat_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=8 ;

--
-- Dumping data for table `second_categories`
--

INSERT INTO `second_categories` (`sec_cat_id`, `sec_cat_parent`, `sec_cat_name`, `sec_cat_image`, `sec_cat_status`, `sec_created_on`) VALUES
(6, 1, 'Football', 'category2-football.png', 'Y', '2014-07-26'),
(7, 2, 'Basball', 'baseball_ico.png', 'Y', '2014-10-25');

-- --------------------------------------------------------

--
-- Table structure for table `settings`
--

CREATE TABLE IF NOT EXISTS `settings` (
  `setting_id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user_define_field` varchar(255) NOT NULL,
  `value` int(11) NOT NULL,
  PRIMARY KEY (`setting_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Dumping data for table `settings`
--

INSERT INTO `settings` (`setting_id`, `user_define_field`, `value`) VALUES
(1, 'prize_announce_days', 30);

-- --------------------------------------------------------

--
-- Table structure for table `third_categories`
--

CREATE TABLE IF NOT EXISTS `third_categories` (
  `th_cat_id` int(11) NOT NULL AUTO_INCREMENT,
  `th_cat_parent` int(11) NOT NULL DEFAULT '0',
  `th_cat_grand_parent` int(11) NOT NULL DEFAULT '0',
  `th_cat_name` varchar(250) DEFAULT NULL,
  `th_cat_image` varchar(250) DEFAULT NULL,
  `th_cat_status` enum('Y','N') DEFAULT NULL,
  `th_created_on` date DEFAULT NULL,
  PRIMARY KEY (`th_cat_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `third_categories`
--

INSERT INTO `third_categories` (`th_cat_id`, `th_cat_parent`, `th_cat_grand_parent`, `th_cat_name`, `th_cat_image`, `th_cat_status`, `th_created_on`) VALUES
(3, 6, 13, 'FIFA', 'category2-basketball.png', 'Y', '2014-07-26');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `user_id` bigint(11) NOT NULL AUTO_INCREMENT,
  `user_email` varchar(250) NOT NULL,
  `user_name` varchar(250) NOT NULL,
  `user_pass` varchar(250) NOT NULL,
  `user_image` varchar(250) DEFAULT NULL,
  `user_dob` date DEFAULT NULL,
  `user_country` varchar(250) DEFAULT NULL,
  `user_city` varchar(250) DEFAULT NULL,
  `user_type` enum('OWN','FB') DEFAULT NULL,
  `user_membership` enum('FREE','PREMIUM') DEFAULT NULL,
  `user_status` enum('Y','N') DEFAULT 'Y',
  `user_created_on` date DEFAULT NULL,
  `ismusic` int(11) NOT NULL DEFAULT '1',
  `issound` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=32 ;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `user_email`, `user_name`, `user_pass`, `user_image`, `user_dob`, `user_country`, `user_city`, `user_type`, `user_membership`, `user_status`, `user_created_on`, `ismusic`, `issound`) VALUES
(15, 'saqi@yahoo.com', 'saqi', 'asd', NULL, NULL, 'antigua and barbuda', NULL, 'OWN', NULL, 'Y', NULL, 1, 1),
(31, 'saq@gajy.con', 'www', '123', NULL, NULL, NULL, NULL, 'OWN', NULL, 'Y', NULL, 1, 1);
