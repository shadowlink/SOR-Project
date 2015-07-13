-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         10.0.14-MariaDB - mariadb.org binary distribution
-- SO del servidor:              Win64
-- HeidiSQL Versión:             9.1.0.4867
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Volcando estructura de base de datos para sordb
CREATE DATABASE IF NOT EXISTS `sordb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `sordb`;


-- Volcando estructura para tabla sordb.usuarios
CREATE TABLE IF NOT EXISTS `usuarios` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) DEFAULT NULL,
  `mail` varchar(50) DEFAULT NULL,
  `password` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- Volcando datos para la tabla sordb.usuarios: ~7 rows (aproximadamente)
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` (`id`, `nombre`, `mail`, `password`) VALUES
	(1, 'lizerg', 'sss@sss.com', 'ssss'),
	(3, 'shadowlink', 'shadowfiltro@gmail.com', 'f98ea5f25147e5b5136884297fb8e60f57990ae8'),
	(5, 'slayerlink', 'shadow@gmail.com', 'f98ea5f25147e5b5136884297fb8e60f57990ae8'),
	(6, 'perrinen', 'perr@perr.com', '356a192b7913b04c54574d18c28d46e6395428ab'),
	(7, 'perrinen2', 'perr@perr.com', '356a192b7913b04c54574d18c28d46e6395428ab'),
	(8, 'a', 'a', '86f7e437faa5a7fce15d1ddcb9eaeaea377667b8'),
	(9, 'n', 'n', 'd1854cae891ec7b29161ccaf79a24b00c274bdaa');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;


-- Volcando estructura para tabla sordb.ventas
CREATE TABLE IF NOT EXISTS `ventas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `vendedor` int(11) DEFAULT NULL,
  `tipo` varchar(50) DEFAULT NULL,
  `autor` varchar(50) DEFAULT NULL,
  `año` int(11) DEFAULT NULL,
  `estado` varchar(50) DEFAULT NULL,
  `precio` int(11) DEFAULT NULL,
  `fecha_I` timestamp NULL DEFAULT NULL,
  `fecha_F` timestamp NULL DEFAULT NULL,
  `finalizada` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `fk_ventas_usuario` (`vendedor`),
  CONSTRAINT `fk_ventas_usuario` FOREIGN KEY (`vendedor`) REFERENCES `usuarios` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=268 DEFAULT CHARSET=utf8;

-- Volcando datos para la tabla sordb.ventas: ~2 rows (aproximadamente)
/*!40000 ALTER TABLE `ventas` DISABLE KEYS */;
INSERT INTO `ventas` (`id`, `vendedor`, `tipo`, `autor`, `año`, `estado`, `precio`, `fecha_I`, `fecha_F`, `finalizada`) VALUES
	(266, 8, '1', '1', 1, '1', 111, '2014-11-29 15:26:30', '2014-12-07 10:11:00', 0),
	(267, 8, '2', '2', 3, '4', 11, '2014-11-29 15:28:01', '2014-12-05 10:11:00', 0);
/*!40000 ALTER TABLE `ventas` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
