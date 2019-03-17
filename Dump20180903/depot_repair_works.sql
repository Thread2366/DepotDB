-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: depot
-- ------------------------------------------------------
-- Server version	5.7.20-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `repair_works`
--

DROP TABLE IF EXISTS `repair_works`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `repair_works` (
  `RW_ID` int(11) NOT NULL AUTO_INCREMENT,
  `TypeRepair` varchar(39) DEFAULT NULL COMMENT 'Тип ремонта',
  `Money` double(15,2) DEFAULT NULL COMMENT 'Стоимость ремонта',
  `Bonus` tinyint(1) DEFAULT NULL COMMENT 'Качество ремонта (отличное/нормальное)',
  `BonusPercent` int(2) DEFAULT NULL COMMENT 'Премия в процентах (общая)',
  `DateStart` date DEFAULT NULL COMMENT 'Начало ремонта',
  `DateStop` date DEFAULT NULL COMMENT 'Окончание ремонта',
  `Reason` varchar(40) DEFAULT NULL COMMENT 'Причина поступления в ремонт',
  `RailwayCarriageID` int(10) DEFAULT NULL COMMENT 'Находится на ремонте',
  `BrigadeID` int(11) DEFAULT NULL COMMENT 'Выполняют ремонт',
  PRIMARY KEY (`RW_ID`),
  UNIQUE KEY `RW_ID_UNIQUE` (`RW_ID`),
  KEY `RegNumber_idx` (`RailwayCarriageID`),
  KEY `ExecuteRepair_idx` (`BrigadeID`),
  CONSTRAINT `ExecuteRepair` FOREIGN KEY (`BrigadeID`) REFERENCES `brigades` (`B_ID`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `OnRepair` FOREIGN KEY (`RailwayCarriageID`) REFERENCES `railway_carriages` (`RegNumber`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8 COMMENT='Ремонтные работы';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `repair_works`
--

LOCK TABLES `repair_works` WRITE;
/*!40000 ALTER TABLE `repair_works` DISABLE KEYS */;
INSERT INTO `repair_works` VALUES (2,'Рем1',150000.00,1,30,'2016-08-17','2017-01-17','Пр1',11111,2),(3,'Рем2',300000.00,1,15,'2017-01-14','2017-07-14','Пр2',22223,1),(12,'Рем3',55000.00,0,NULL,'2017-02-27','2017-04-27','Пр3',11111,2);
/*!40000 ALTER TABLE `repair_works` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-09-03 15:15:16
