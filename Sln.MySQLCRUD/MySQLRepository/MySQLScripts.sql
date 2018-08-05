

--SP:
CREATE DEFINER=`root`@`localhost` PROCEDURE `SPGetbasicinfo`(IN cid INT)
BEGIN
       SELECT *
       FROM basicinfo
       WHERE basicinfoid = cid;
END

--Call MySQL SP
CALL SPGetbasicinfo (3);


-- Need to Create SP for CRUD by if numbering


-- Table `devtest`.`basicinfo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `devtest`.`basicinfo` (
  `basicinfoid` BIGINT(20) NOT NULL,
  `firstname` LONGTEXT NULL DEFAULT NULL,
  `lastname` LONGTEXT NULL DEFAULT NULL,
  `dateofbirth` DATETIME(6) NOT NULL,
  `city` LONGTEXT NULL DEFAULT NULL,
  `country` LONGTEXT NULL DEFAULT NULL,
  `mobileno` LONGTEXT NULL DEFAULT NULL,
  `nid` LONGTEXT NULL DEFAULT NULL,
  `email` LONGTEXT NULL DEFAULT NULL,
  `status` INT(11) NOT NULL,
  PRIMARY KEY (`basicinfoid`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;




