-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Tempo de geração: 07-Dez-2022 às 01:24
-- Versão do servidor: 8.0.27
-- versão do PHP: 7.4.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `projetosge`
--
CREATE DATABASE IF NOT EXISTS `projetosge` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `projetosge`;

-- --------------------------------------------------------

--
-- Estrutura da tabela `tbcliente`
--

DROP TABLE IF EXISTS `tbcliente`;
CREATE TABLE IF NOT EXISTS `tbcliente` (
  `IdCliente` int NOT NULL AUTO_INCREMENT,
  `nomecliente` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `cnpj` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `cpf` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `statuscliente` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`IdCliente`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `tbcliente`
--

INSERT INTO `tbcliente` (`IdCliente`, `nomecliente`, `cnpj`, `cpf`, `statuscliente`) VALUES
(1, 'João Silva', NULL, '30479775931', 'Ativo'),
(2, 'Maria Oliveira', '54761389000111', NULL, 'Ativo');


-- --------------------------------------------------------

--
-- Estrutura da tabela `tbfuncionario`
--

DROP TABLE IF EXISTS `tbfuncionario`;
CREATE TABLE IF NOT EXISTS `tbfuncionario` (
  `IdFunc` int NOT NULL AUTO_INCREMENT,
  `nomefunc` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `sobrenomefunc` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `rg` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `contato` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `email` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `cpf` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `datanascimento` date NOT NULL,
  `dataadmissao` date NOT NULL,
  `cargo` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `salario` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `prazosalario` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `statusfunc` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`IdFunc`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `tbfuncionario`
--

INSERT INTO `tbfuncionario` (`IdFunc`, `nomefunc`, `sobrenomefunc`, `rg`, `contato`, `email`, `cpf`, `datanascimento`, `dataadmissao`, `cargo`, `salario`, `prazosalario`, `statusfunc`) VALUES
(1, 'Luis', 'Sousa', '123456789', '11911111111', 'teste@gmail.com', '123.456.789-09', '2004-05-18', '2022-12-06', 'Aprendiz', '100', 'Diária', 'Ativo'),
(2, 'Rafael', 'Pozzane', '987654321', '11911111111', 'teste@gmail.com', '987.654.321-00', '2004-05-11', '2022-12-06', 'Aprendiz', '100', 'Diária', 'Ativo'),
(3, 'Mickael', 'Leite', '456789123', '11911111111', 'teste@gmail.com', '456.789.123-45', '2004-02-28', '2022-12-06', 'Aprendiz', '100', 'Diária', 'Ativo'),
(4, 'Marcos', 'Ruan', '321654987', '11911111111', 'Teste@teste.com', '321.654.987-78', '2004-03-03', '2022-12-06', 'Aprendiz', '100', 'Diária', 'Ativo'),
(5, 'Natã', 'Martins', '654321789', '11911111111', 'teste@teste.com', '654.321.789-12', '2004-09-16', '2022-12-06', 'Aprendiz', '100', 'Diária', 'Ativo');


-- --------------------------------------------------------

--
-- Estrutura da tabela `tblstservico`
--

DROP TABLE IF EXISTS `tblstservico`;
CREATE TABLE IF NOT EXISTS `tblstservico` (
  `IdLstServico` int NOT NULL AUTO_INCREMENT,
  `nomeservico` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `precoservico` varchar(6) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `status` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`IdLstServico`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `tblstservico`
--

INSERT INTO `tblstservico` (`IdLstServico`, `nomeservico`, `precoservico`, `status`) VALUES
(1, 'Instalar tomada', '15', 'Ativo'),
(2, 'Instalar Infraestrutura', '15', 'Ativo');

-- --------------------------------------------------------

--
-- Estrutura da tabela `tbmateriais`
--

DROP TABLE IF EXISTS `tbmateriais`;
CREATE TABLE IF NOT EXISTS `tbmateriais` (
  `IdMateriais` int NOT NULL AUTO_INCREMENT,
  `nomematerial` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `precomaterial` varchar(6) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `quant` int NOT NULL,
  `status` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`IdMateriais`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `tbmateriais`
--

INSERT INTO `tbmateriais` (`IdMateriais`, `nomematerial`, `precomaterial`, `quant`, `status`) VALUES
(1, 'Fio', '110', 20, 'Ativo'),
(2, 'Tomada', '10', 50, 'Ativo'),
(3, 'Eletroduto', '25', 50, 'Ativo'),
(4, 'Abraçadeira', '7', 50, 'Ativo'),
(5, 'Curva', '5', 50, 'Ativo');

-- --------------------------------------------------------

--
-- Estrutura da tabela `tbmsorcamento`
--

DROP TABLE IF EXISTS `tbmsorcamento`;
CREATE TABLE IF NOT EXISTS `tbmsorcamento` (
  `IdMSOrcamento` int NOT NULL AUTO_INCREMENT,
  `IdOrcamento` int NOT NULL,
  `IdMaterial` int DEFAULT NULL,
  `IdServico` int DEFAULT NULL,
  `nomeproduto` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `quantidade` int NOT NULL,
  `precounitario` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `precototal` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`IdMSOrcamento`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura da tabela `tbmsservico`
--

DROP TABLE IF EXISTS `tbmsservico`;
CREATE TABLE IF NOT EXISTS `tbmsservico` (
  `IdMSServico` int NOT NULL AUTO_INCREMENT,
  `IdServico` int NOT NULL,
  `IdMaterial` int DEFAULT NULL,
  `IdLstServico` int DEFAULT NULL,
  `nomeproduto` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `quantidade` int NOT NULL,
  `precounitario` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `precototal` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`IdMSServico`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura da tabela `tbobras`
--

DROP TABLE IF EXISTS `tbobras`;
CREATE TABLE IF NOT EXISTS `tbobras` (
  `IdObra` int NOT NULL AUTO_INCREMENT,
  `nomeobra` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `cliente` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `nomeresponsavel` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `contato` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `statusobra` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `cep` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `cidade` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `bairro` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `estado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `logradouro` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `numero` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `status` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`IdObra`)
) ENGINE=MyISAM AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `tbobras`
--

INSERT INTO `tbobras` (`IdObra`, `nomeobra`, `cliente`, `nomeresponsavel`, `contato`, `statusobra`, `cep`, `cidade`, `bairro`, `estado`, `logradouro`, `numero`, `status`) VALUES
(1, 'Hospital Poggers', 'Luis', 'Luis', '11911111111', 'Em progresso', '08130050', 'São Paulo', 'Cidade Kemel', 'SP', 'Rua Thyrso Burgos Lopes', '300', 'Ativo'),
(2, 'Baratão', 'Rafael', 'Rafael', '11911111111', 'Em progresso', '08568110', 'Poá', 'Jardim Nova Poá', 'SP', 'Rua Zacarias Rodrigues', '30', 'Ativo'),
(3, 'Davó', 'Michel', 'Michel', '(11) 91111-1111', 'Em progresso', '08571-068', 'Itaquaquecetuba', 'Estação', 'SP', 'Rua José Paes do Amaral', '15', 'Ativo');

-- --------------------------------------------------------

--
-- Estrutura da tabela `tborcamento`
--

DROP TABLE IF EXISTS `tborcamento`;
CREATE TABLE IF NOT EXISTS `tborcamento` (
  `IdOrcamento` int NOT NULL AUTO_INCREMENT,
  `IdCliente` int DEFAULT NULL,
  `nomecliente` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `cpf` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `cnpj` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `total` varchar(6) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `status` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`IdOrcamento`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura da tabela `tbpagamento`
--

DROP TABLE IF EXISTS `tbpagamento`;
CREATE TABLE IF NOT EXISTS `tbpagamento` (
  `IdPagamento` int NOT NULL AUTO_INCREMENT,
  `IdFunc` int DEFAULT NULL,
  `IdServ` int DEFAULT NULL,
  `tipo` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `descricao` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `vencimento` date NOT NULL,
  `valor` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `status` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`IdPagamento`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura da tabela `tbponto`
--

DROP TABLE IF EXISTS `tbponto`;
CREATE TABLE IF NOT EXISTS `tbponto` (
  `IdPonto` int NOT NULL AUTO_INCREMENT,
  `IdFunc` int NOT NULL,
  `IdServico` int NOT NULL,
  `dataponto` date NOT NULL,
  `status` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`IdPonto`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura da tabela `tbservico`
--

DROP TABLE IF EXISTS `tbservico`;
CREATE TABLE IF NOT EXISTS `tbservico` (
  `IdServico` int NOT NULL AUTO_INCREMENT,
  `IdObra` int NOT NULL,
  `turno` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `detalhes` varchar(250) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `datainicio` date NOT NULL,
  `datafinal` date DEFAULT NULL,
  `precoproduto` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `precoservico` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `precototal` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `status` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`IdServico`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura da tabela `tbuser`
--

DROP TABLE IF EXISTS `tbuser`;
CREATE TABLE IF NOT EXISTS `tbuser` (
  `IdUser` int NOT NULL AUTO_INCREMENT,
  `login` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `senha` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `nomeuser` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `nivel` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`IdUser`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `tbuser`
--

INSERT INTO `tbuser` (`IdUser`, `login`, `senha`, `nomeuser`, `nivel`) VALUES
(1, 'fohat', '123', 'fohat', 'Admin');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
