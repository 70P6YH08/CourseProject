-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema replica_db
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema replica_db
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `replica_db` DEFAULT CHARACTER SET utf8 ;
SHOW WARNINGS;
USE `replica_db` ;

-- -----------------------------------------------------
-- Table `replica_db`.`users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `replica_db`.`users` (
  `user_id` BINARY(16) NOT NULL,
  `login` VARCHAR(30) NOT NULL,
  `password_hash` CHAR(60) NOT NULL,
  `email` VARCHAR(40) NOT NULL,
  `registration_date_time` DATETIME NOT NULL,
  `edited_date_time` DATETIME NULL,
  `deleted_date_time` DATETIME NULL,
  `last_action_date_time` DATETIME NULL,
  `status` ENUM('Active', 'Inactive') NOT NULL DEFAULT 'Inactive',
  PRIMARY KEY (`user_id`),
  UNIQUE INDEX `login_UNIQUE` (`login` ASC) VISIBLE,
  UNIQUE INDEX `email_UNIQUE` (`email` ASC) VISIBLE)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `replica_db`.`user_add_data`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `replica_db`.`user_add_data` (
  `data_id` BINARY(16) NOT NULL,
  `user_id` BINARY(16) NOT NULL,
  `display_name` VARCHAR(40) NULL,
  `birthday_date` DATE NULL,
  `description_profile` VARCHAR(300) NULL,
  PRIMARY KEY (`data_id`, `user_id`),
  INDEX `fk_user_add_data_users_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `fk_user_add_data_users`
    FOREIGN KEY (`user_id`)
    REFERENCES `replica_db`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `replica_db`.`chat_types`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `replica_db`.`chat_types` (
  `type_id` TINYINT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(30) NOT NULL,
  PRIMARY KEY (`type_id`),
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) VISIBLE)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `replica_db`.`chats`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `replica_db`.`chats` (
  `chat_id` BINARY(16) NOT NULL,
  `chat_type_id` TINYINT NOT NULL,
  `name` VARCHAR(60) NOT NULL,
  `description` VARCHAR(300) NULL,
  `created_date_time` DATETIME NOT NULL,
  `edited_date_time` DATETIME NULL,
  `deleted_date_time` DATETIME NULL,
  PRIMARY KEY (`chat_id`),
  INDEX `fk_chats_chat_types1_idx` (`chat_type_id` ASC) VISIBLE,
  CONSTRAINT `fk_chats_chat_types1`
    FOREIGN KEY (`chat_type_id`)
    REFERENCES `replica_db`.`chat_types` (`type_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `replica_db`.`user_roles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `replica_db`.`user_roles` (
  `role_id` TINYINT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`role_id`),
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) VISIBLE)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `replica_db`.`user_chat`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `replica_db`.`user_chat` (
  `user_id` BINARY(16) NOT NULL,
  `chat_id` BINARY(16) NOT NULL,
  `role_id` TINYINT NOT NULL,
  `joined_date_time` DATETIME NULL,
  `left_date_time` DATETIME NULL,
  PRIMARY KEY (`user_id`, `chat_id`),
  INDEX `fk_users_has_chats_chats1_idx` (`chat_id` ASC) VISIBLE,
  INDEX `fk_user_chat_user_roles1_idx` (`role_id` ASC) INVISIBLE,
  CONSTRAINT `fk_users_has_chats_users1`
    FOREIGN KEY (`user_id`)
    REFERENCES `replica_db`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_users_has_chats_chats1`
    FOREIGN KEY (`chat_id`)
    REFERENCES `replica_db`.`chats` (`chat_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_user_chat_user_roles1`
    FOREIGN KEY (`role_id`)
    REFERENCES `replica_db`.`user_roles` (`role_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `replica_db`.`delivery_message_status`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `replica_db`.`delivery_message_status` (
  `status_id` TINYINT NOT NULL,
  `name` VARCHAR(30) NOT NULL,
  PRIMARY KEY (`status_id`),
  UNIQUE INDEX `name_UNIQUE` (`name` ASC) VISIBLE)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `replica_db`.`messages`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `replica_db`.`messages` (
  `message_id` BINARY(16) NOT NULL,
  `sender_id` BINARY(16) NOT NULL,
  `chat_id` BINARY(16) NOT NULL,
  `delivery_status_id` TINYINT NOT NULL,
  `content` VARCHAR(500) NOT NULL,
  `created_date_time` DATETIME NOT NULL,
  `edited_date_time` DATETIME NULL,
  `deleted_date_time` DATETIME NULL,
  `delivered_date_time` DATETIME NULL,
  PRIMARY KEY (`message_id`),
  INDEX `fk_messages_users1_idx` (`sender_id` ASC) VISIBLE,
  INDEX `fk_messages_chats1_idx` (`chat_id` ASC) VISIBLE,
  INDEX `fk_messages_delivery_message_status1_idx` (`delivery_status_id` ASC) VISIBLE,
  CONSTRAINT `fk_messages_users1`
    FOREIGN KEY (`sender_id`)
    REFERENCES `replica_db`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_messages_chats1`
    FOREIGN KEY (`chat_id`)
    REFERENCES `replica_db`.`chats` (`chat_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_messages_delivery_message_status1`
    FOREIGN KEY (`delivery_status_id`)
    REFERENCES `replica_db`.`delivery_message_status` (`status_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `replica_db`.`messages_has_users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `replica_db`.`messages_has_users` (
  `message_id` BINARY(16) NOT NULL,
  `user_id` BINARY(16) NOT NULL,
  `read_date_time` DATETIME NOT NULL,
  PRIMARY KEY (`message_id`, `user_id`),
  INDEX `fk_messages_has_users_users1_idx` (`user_id` ASC) VISIBLE,
  INDEX `fk_messages_has_users_messages1_idx` (`message_id` ASC) VISIBLE,
  CONSTRAINT `fk_messages_has_users_messages1`
    FOREIGN KEY (`message_id`)
    REFERENCES `replica_db`.`messages` (`message_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_messages_has_users_users1`
    FOREIGN KEY (`user_id`)
    REFERENCES `replica_db`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `replica_db`.`user_code`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `replica_db`.`user_code` (
  `code_id` BINARY(16) NOT NULL,
  `user_id` BINARY(16) NOT NULL,
  `code_hash` CHAR(60) NOT NULL,
  `input_attempts` TINYINT NOT NULL,
  PRIMARY KEY (`code_id`, `user_id`),
  INDEX `fk_user_code_users1_idx` (`user_id` ASC) VISIBLE,
  CONSTRAINT `fk_user_code_users1`
    FOREIGN KEY (`user_id`)
    REFERENCES `replica_db`.`users` (`user_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
