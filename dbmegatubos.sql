-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.7.3-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for megatubos
DROP DATABASE IF EXISTS `megatubos`;
CREATE DATABASE IF NOT EXISTS `megatubos` /*!40100 DEFAULT CHARACTER SET utf8mb3 */;
USE `megatubos`;

-- Dumping structure for table megatubos.cliente
DROP TABLE IF EXISTS `cliente`;
CREATE TABLE IF NOT EXISTS `cliente` (
  `idCliente` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  `Telefono` varchar(12) NOT NULL,
  `Nit` varchar(15) NOT NULL,
  `Eliminada` tinyint(4) NOT NULL,
  PRIMARY KEY (`idCliente`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table megatubos.cliente: ~6 rows (approximately)
DELETE FROM `cliente`;
/*!40000 ALTER TABLE `cliente` DISABLE KEYS */;
INSERT INTO `cliente` (`idCliente`, `Nombre`, `Telefono`, `Nit`, `Eliminada`) VALUES
	(1, 'Juan Alberto 4', '44592010', '2001-20', 0),
	(2, 'Susana Del Valle', '51549488', '401580-4', 0),
	(11, 'Fernanda', '4598785', '1587595-45', 0),
	(12, 'Fernanda', '5899854', '123654', 0),
	(13, 'Claudia', '35553974', '1712508-1', 0),
	(14, 'Pedro', '15987411', '78945612', 0);
/*!40000 ALTER TABLE `cliente` ENABLE KEYS */;

-- Dumping structure for table megatubos.detalle_de_salida
DROP TABLE IF EXISTS `detalle_de_salida`;
CREATE TABLE IF NOT EXISTS `detalle_de_salida` (
  `CantidadVendida` int(11) NOT NULL,
  `Sub_Total` float NOT NULL,
  `Salidas_ID` int(11) NOT NULL,
  `Producto_Codigo` int(11) NOT NULL,
  PRIMARY KEY (`Salidas_ID`,`Producto_Codigo`),
  KEY `fk_Detalle_de_Salida_Producto1_idx` (`Producto_Codigo`),
  KEY `fk_Salida_has_Producto_Producto1_idx` (`Producto_Codigo`),
  CONSTRAINT `fk_Detalle_de_Salida_Producto1` FOREIGN KEY (`Producto_Codigo`) REFERENCES `producto` (`Codigo`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Detalle_de_Salida_Salidas1` FOREIGN KEY (`Salidas_ID`) REFERENCES `salidas` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- Dumping data for table megatubos.detalle_de_salida: ~14 rows (approximately)
DELETE FROM `detalle_de_salida`;
/*!40000 ALTER TABLE `detalle_de_salida` DISABLE KEYS */;
INSERT INTO `detalle_de_salida` (`CantidadVendida`, `Sub_Total`, `Salidas_ID`, `Producto_Codigo`) VALUES
	(10, 70, 1, 1),
	(11, 220, 1, 2),
	(4, 28, 13, 1),
	(5, 100, 13, 2),
	(4, 28, 14, 1),
	(4, 48, 14, 3),
	(4, 28, 15, 1),
	(4, 80, 15, 2),
	(2, 100, 16, 4),
	(7, 49, 20, 1),
	(2, 40, 21, 2),
	(3, 21, 22, 1),
	(2, 24, 22, 3),
	(5, 60, 23, 3);
/*!40000 ALTER TABLE `detalle_de_salida` ENABLE KEYS */;

-- Dumping structure for table megatubos.ferreteria
DROP TABLE IF EXISTS `ferreteria`;
CREATE TABLE IF NOT EXISTS `ferreteria` (
  `idFerreteria` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(45) NOT NULL,
  `Direccion` varchar(45) NOT NULL,
  `Cliente_idCliente` int(11) NOT NULL,
  PRIMARY KEY (`idFerreteria`),
  KEY `fk_Ferreteria_Cliente1_idx` (`Cliente_idCliente`),
  CONSTRAINT `fk_Ferreteria_Cliente1` FOREIGN KEY (`Cliente_idCliente`) REFERENCES `cliente` (`idCliente`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table megatubos.ferreteria: ~8 rows (approximately)
DELETE FROM `ferreteria`;
/*!40000 ALTER TABLE `ferreteria` DISABLE KEYS */;
INSERT INTO `ferreteria` (`idFerreteria`, `Nombre`, `Direccion`, `Cliente_idCliente`) VALUES
	(1, 'La Forjita', 'Xela, Guatemala', 1),
	(2, 'La Bendicion', 'Huehuetenango, Guatemala', 2),
	(3, 'La esquina', 'Zona 10 Quetzaltenango', 2),
	(4, 'la concepcion', 'zona 9, Guatemala', 12),
	(6, 'Hola', '12345', 13),
	(7, 'El sol', '24 avenida A 6-7 zona 3, Xela', 13),
	(8, 'El raton', 'Trigales', 14),
	(9, 'Random', 'Zona 3', 14);
/*!40000 ALTER TABLE `ferreteria` ENABLE KEYS */;

-- Dumping structure for table megatubos.producto
DROP TABLE IF EXISTS `producto`;
CREATE TABLE IF NOT EXISTS `producto` (
  `Codigo` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(45) NOT NULL,
  `Stock` int(11) NOT NULL,
  `PrecioCosto` float NOT NULL,
  `Eliminado` tinyint(4) NOT NULL,
  PRIMARY KEY (`Codigo`)
) ENGINE=InnoDB AUTO_INCREMENT=1434 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table megatubos.producto: ~6 rows (approximately)
DELETE FROM `producto`;
/*!40000 ALTER TABLE `producto` DISABLE KEYS */;
INSERT INTO `producto` (`Codigo`, `Descripcion`, `Stock`, `PrecioCosto`, `Eliminado`) VALUES
	(1, '1/2 pulg 315 PSI', 147, 7, 0),
	(2, 'tubo de 1" de 6m', 13, 20, 0),
	(3, '3/4 pulg 150 PSI', 40, 12, 0),
	(4, 'tubos 3/4 pulg 300 PSI', 30, 50, 0),
	(1403, 'Tubo de prueba', 25, 10.5, 0),
	(1433, 'Tubo2', 25, 2.5, 0);
/*!40000 ALTER TABLE `producto` ENABLE KEYS */;

-- Dumping structure for table megatubos.salidas
DROP TABLE IF EXISTS `salidas`;
CREATE TABLE IF NOT EXISTS `salidas` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(100) NOT NULL,
  `Fecha` date NOT NULL,
  `Total` float NOT NULL,
  `Ferreteria` varchar(50) NOT NULL,
  `Eliminada` tinyint(4) NOT NULL,
  `Vendedor_idVendedor` int(11) NOT NULL,
  `Cliente_idCliente` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `fk_Salidas_Cliente1_idx` (`Cliente_idCliente`),
  KEY `fk_Salidas_Vendedor_idx` (`Vendedor_idVendedor`),
  KEY `fk_Salida_Cliente_idx` (`Cliente_idCliente`),
  KEY `fk_Salida_Vendedor1_idx` (`Vendedor_idVendedor`),
  CONSTRAINT `fk_Salidas_Cliente1` FOREIGN KEY (`Cliente_idCliente`) REFERENCES `cliente` (`idCliente`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_Salidas_Vendedor` FOREIGN KEY (`Vendedor_idVendedor`) REFERENCES `vendedor` (`idVendedor`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table megatubos.salidas: ~10 rows (approximately)
DELETE FROM `salidas`;
/*!40000 ALTER TABLE `salidas` DISABLE KEYS */;
INSERT INTO `salidas` (`ID`, `Descripcion`, `Fecha`, `Total`, `Ferreteria`, `Eliminada`, `Vendedor_idVendedor`, `Cliente_idCliente`) VALUES
	(1, 'Se vendieron 2 productos diferentes', '2023-02-04', 290, 'Redentor', 0, 1, 1),
	(12, 'venta de 3 tubos', '2023-05-06', 47, 'la concepcion', 0, 1, 12),
	(13, 'venta de 3 tubos', '2023-05-06', 128, 'La Forjita', 0, 1, 1),
	(14, 'venta de 5 tubos', '2023-05-06', 76, 'la concepcion', 0, 1, 12),
	(15, 'venta a susana del valle por Q 108', '2023-05-06', 108, 'La Bendicion', 0, 1, 2),
	(16, 'se vendio un tubo grande', '2023-05-06', 100, 'La esquina', 0, 1, 2),
	(20, 'ventas por mayor', '2023-11-07', 49, 'El sol', 0, 1, 13),
	(21, 'prueba', '2023-11-07', 40, 'la concepcion', 0, 1, 12),
	(22, 'Venta de tubo por mayor', '2023-11-12', 45, 'la concepcion', 0, 1, 12),
	(23, 'Prueba Trigger Vendedor', '2023-11-12', 60, 'la concepcion', 0, 2, 12);
/*!40000 ALTER TABLE `salidas` ENABLE KEYS */;

-- Dumping structure for table megatubos.tipo_empleado
DROP TABLE IF EXISTS `tipo_empleado`;
CREATE TABLE IF NOT EXISTS `tipo_empleado` (
  `idTipo_Empleado` int(11) NOT NULL AUTO_INCREMENT,
  `Tipo` varchar(45) NOT NULL,
  PRIMARY KEY (`idTipo_Empleado`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table megatubos.tipo_empleado: ~4 rows (approximately)
DELETE FROM `tipo_empleado`;
/*!40000 ALTER TABLE `tipo_empleado` DISABLE KEYS */;
INSERT INTO `tipo_empleado` (`idTipo_Empleado`, `Tipo`) VALUES
	(1, 'Gerente'),
	(2, 'Vendedor'),
	(3, 'Fabricante'),
	(4, 'Admin');
/*!40000 ALTER TABLE `tipo_empleado` ENABLE KEYS */;

-- Dumping structure for table megatubos.usuario
DROP TABLE IF EXISTS `usuario`;
CREATE TABLE IF NOT EXISTS `usuario` (
  `idUsuario` varchar(40) NOT NULL,
  `Nombre` varchar(60) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `Tipo_Empleado_idTipo_Empleado` int(11) NOT NULL,
  PRIMARY KEY (`idUsuario`),
  KEY `fk_Usuario_Tipo_Empleado1_idx` (`Tipo_Empleado_idTipo_Empleado`),
  CONSTRAINT `fk_Usuario_Tipo_Empleado1` FOREIGN KEY (`Tipo_Empleado_idTipo_Empleado`) REFERENCES `tipo_empleado` (`idTipo_Empleado`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- Dumping data for table megatubos.usuario: ~3 rows (approximately)
DELETE FROM `usuario`;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` (`idUsuario`, `Nombre`, `Password`, `Tipo_Empleado_idTipo_Empleado`) VALUES
	('Admin', 'Admin', '12345', 4),
	('Andres', 'Andres Mendez', 'gerente', 1),
	('Raul', 'Raul Perez', 'raul', 2);
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;

-- Dumping structure for table megatubos.vendedor
DROP TABLE IF EXISTS `vendedor`;
CREATE TABLE IF NOT EXISTS `vendedor` (
  `idVendedor` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(45) NOT NULL,
  `Cant_Vendida` float NOT NULL,
  `Telefono` varchar(12) NOT NULL,
  `Fecha` date NOT NULL,
  `Eliminada` tinyint(4) NOT NULL,
  PRIMARY KEY (`idVendedor`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table megatubos.vendedor: ~3 rows (approximately)
DELETE FROM `vendedor`;
/*!40000 ALTER TABLE `vendedor` DISABLE KEYS */;
INSERT INTO `vendedor` (`idVendedor`, `Nombre`, `Cant_Vendida`, `Telefono`, `Fecha`, `Eliminada`) VALUES
	(1, 'Carlos', 285, '45804521', '2022-02-11', 0),
	(2, 'Raul', 60, '48785751', '2022-05-05', 0),
	(3, 'Adrian', 0, '48569842', '2022-05-10', 1);
/*!40000 ALTER TABLE `vendedor` ENABLE KEYS */;

-- Dumping structure for trigger megatubos.actualizar_producto
DROP TRIGGER IF EXISTS `actualizar_producto`;
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE Trigger actualizar_producto   
AFTER INSERT ON megatubos.detalle_de_salida FOR EACH ROW  
BEGIN  
UPDATE megatubos.producto SET producto.stock = producto.stock - new.CantidadVendida 
WHERE producto.Codigo = new.Producto_Codigo;  
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

-- Dumping structure for trigger megatubos.trigger_vendedor
DROP TRIGGER IF EXISTS `trigger_vendedor`;
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER trigger_vendedor
AFTER INSERT ON megatubos.salidas FOR EACH ROW  
BEGIN  
UPDATE megatubos.vendedor SET vendedor.Cant_Vendida = vendedor.Cant_Vendida + new.Total
WHERE vendedor.idVendedor = new.Vendedor_idVendedor;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
