-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema brackeysbot
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema brackeysbot
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `brackeysbot` DEFAULT CHARACTER SET utf8 ;
USE `brackeysbot` ;

-- -----------------------------------------------------
-- Table `brackeysbot`.`moderation_types`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `brackeysbot`.`moderation_types` (
  `type_id` INT NOT NULL,
  `type_desc` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`type_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `brackeysbot`.`user_data`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `brackeysbot`.`user_data` (
  `user_id` BIGINT NOT NULL,
  `stars` INT NOT NULL,
  PRIMARY KEY (`user_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `brackeysbot`.`infractions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `brackeysbot`.`infractions` (
  `id` INT NOT NULL,
  `date` DATETIME NOT NULL,
  `reason` VARCHAR(250) NOT NULL,
  `additional_info` VARCHAR(250) NULL,
  `moderation_types_type_id` INT NOT NULL,
  `target_user_id` INT NOT NULL,
  `moderator_user_id` BIGINT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_infractions_moderation_types_idx` (`moderation_types_type_id` ASC) ,
  INDEX `fk_infractions_user_data1_idx` (`target_user_id` ASC) ,
  INDEX `fk_infractions_user_data2_idx` (`moderator_user_id` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `brackeysbot`.`audit_log`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `brackeysbot`.`audit_log` (
  `log_id` INT NOT NULL,
  `date` DATETIME NOT NULL,
  `infractions_id` INT NULL,
  `moderation_types_type_id` INT NULL,
  `description` VARCHAR(250) NOT NULL,
  `user_data_user_id` BIGINT NULL,
  PRIMARY KEY (`log_id`),
  INDEX `fk_audit_log_infractions1_idx` (`infractions_id` ASC) ,
  INDEX `fk_audit_log_moderation_types1_idx` (`moderation_types_type_id` ASC) ,
  INDEX `fk_audit_log_user_data1_idx` (`user_data_user_id` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `brackeysbot`.`temporary_infractions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `brackeysbot`.`temporary_infractions` (
  `temp_infr_id` INT NOT NULL,
  `infractions_id` INT NOT NULL,
  `end_date` DATETIME NOT NULL,
  PRIMARY KEY (`temp_infr_id`),
  INDEX `fk_temporary_infractions_infractions1_idx` (`infractions_id` ASC) )
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
