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
-- Table structure for table `railway_carriages`
--

DROP TABLE IF EXISTS `railway_carriages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `railway_carriages` (
  `RegNumber` int(10) NOT NULL COMMENT 'Регистрационный номер вагона',
  `RegName` varchar(60) DEFAULT NULL,
  `RegChief` varchar(20) DEFAULT NULL,
  `Type` varchar(20) DEFAULT NULL COMMENT 'Тип вагона',
  `TypeYear` int(4) DEFAULT NULL COMMENT 'Год выпуска вагона',
  `Picture` mediumblob COMMENT 'Фотография вагона',
  `External` tinyint(1) DEFAULT NULL COMMENT 'Внешняя/местная железная дорога',
  `BankExternal` varchar(60) DEFAULT NULL COMMENT 'Банк внешней железной дороги',
  `InnExternal` int(10) DEFAULT NULL COMMENT 'ИНН внешней железной дороги',
  `AddressExternal` varchar(80) DEFAULT NULL COMMENT 'Юридический адрес внешней железной дороги',
  PRIMARY KEY (`RegNumber`),
  UNIQUE KEY `RegNumber_UNIQUE` (`RegNumber`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Вагоны';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `railway_carriages`
--

LOCK TABLES `railway_carriages` WRITE;
/*!40000 ALTER TABLE `railway_carriages` DISABLE KEYS */;
INSERT INTO `railway_carriages` VALUES (11111,'Дор1','Дир1','Тип1',1990,NULL,1,'Банк1',11111,'Адрес1'),(22223,'Дор2','Дир2','Тип2',1993,NULL,0,NULL,NULL,NULL);
/*!40000 ALTER TABLE `railway_carriages` ENABLE KEYS */;
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
