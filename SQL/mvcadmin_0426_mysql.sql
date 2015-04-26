/*
Navicat MySQL Data Transfer

Source Server         : 2011-20131216BD
Source Server Version : 50077
Source Host           : localhost:3306
Source Database       : mvcadmin

Target Server Type    : MYSQL
Target Server Version : 50077
File Encoding         : 65001

Date: 2015-04-26 09:48:11
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `cms_article`
-- ----------------------------
DROP TABLE IF EXISTS `cms_article`;
CREATE TABLE `cms_article` (
  `id` int(11) NOT NULL auto_increment,
  `title` varchar(50) NOT NULL,
  `content` longtext NOT NULL,
  `addTime` datetime NOT NULL,
  `editTime` datetime NOT NULL,
  `isTop` bit(4) NOT NULL,
  `isShow` bit(4) NOT NULL,
  `isRecommend` bit(4) NOT NULL,
  `des` varchar(255) default NULL,
  `keyStr` varchar(255) default NULL,
  `author` varchar(255) default NULL,
  `source` varchar(255) default NULL,
  `rColumn` int(11) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of cms_article
-- ----------------------------
INSERT INTO `cms_article` VALUES ('1', '自定义内容区1', '内容区55', '2015-04-04 19:28:36', '2015-04-04 20:54:26', '', '', '', '222', '111', '333', '444', '13');
INSERT INTO `cms_article` VALUES ('2', '自定义内容区1', '<strong>author</strong>', '2015-04-04 20:55:08', '2015-04-11 14:24:27', '', '', '', null, '23', '1111', '2222', '14');
INSERT INTO `cms_article` VALUES ('3', '内容区2', '内容区2', '2015-04-04 21:03:04', '2015-04-04 21:03:04', '', '', '', null, null, '内容区2', '内容区2', '11');
INSERT INTO `cms_article` VALUES ('4', '内容区3', '内容区3', '2015-04-04 21:03:25', '2015-04-04 21:03:25', '', '', '', null, null, '内容区3', '内容区3', '11');
INSERT INTO `cms_article` VALUES ('5', '内容区4', '内容区4', '2015-04-04 21:03:54', '2015-04-04 21:03:54', '', '', '', null, null, '内容区4', '内容区4', '11');
INSERT INTO `cms_article` VALUES ('6', '内容区5', '内容区5', '2015-04-04 21:04:32', '2015-04-04 21:04:32', '', '', '', null, null, '内容区5', '内容区5', '11');
INSERT INTO `cms_article` VALUES ('7', '内容区6', '<strong>内容区6</strong>', '2015-04-04 21:12:46', '2015-04-04 21:12:46', '', '', '', null, null, '内容区6', '内容区6', '11');
INSERT INTO `cms_article` VALUES ('8', '内容区7', '<em>内容区7</em>', '2015-04-04 21:42:00', '2015-04-04 21:42:00', '', '', '', null, null, '内容区7', '内容区7', '11');
INSERT INTO `cms_article` VALUES ('9', '内容区8', '内容区8', '2015-04-04 21:42:45', '2015-04-04 21:42:45', '', '', '', null, null, '内容区8', '内容区8', '11');
INSERT INTO `cms_article` VALUES ('10', '内容区9', '<strong><em>内容区9</em></strong>', '2015-04-04 21:43:01', '2015-04-04 21:43:01', '', '', '', null, null, '内容区9', '内容区9', '11');
INSERT INTO `cms_article` VALUES ('11', '内容区10', '内容区10', '2015-04-04 21:43:26', '2015-04-04 21:43:26', '', '', '', null, null, '内容区10', '内容区10', '11');

-- ----------------------------
-- Table structure for `cms_basecontent`
-- ----------------------------
DROP TABLE IF EXISTS `cms_basecontent`;
CREATE TABLE `cms_basecontent` (
  `id` int(11) NOT NULL auto_increment,
  `name` varchar(20) NOT NULL,
  `des` varchar(255) NOT NULL,
  `content` longtext NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of cms_basecontent
-- ----------------------------
INSERT INTO `cms_basecontent` VALUES ('1', 'WebAdmin', 'baseContentList', '<strong>WebAdmin</strong>');
INSERT INTO `cms_basecontent` VALUES ('2', 'lww001', 'WebAdmin', '<strong><em>WebAdmin</em></strong>');

-- ----------------------------
-- Table structure for `cms_column`
-- ----------------------------
DROP TABLE IF EXISTS `cms_column`;
CREATE TABLE `cms_column` (
  `id` int(11) NOT NULL auto_increment,
  `name` varchar(20) NOT NULL,
  `isNode` bit(4) NOT NULL,
  `parentId` int(11) NOT NULL,
  `link` varchar(255) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of cms_column
-- ----------------------------
INSERT INTO `cms_column` VALUES ('11', 'lww001', '', '0', null);
INSERT INTO `cms_column` VALUES ('12', 'lww', '', '0', null);
INSERT INTO `cms_column` VALUES ('13', 'lww11111', '', '11', null);
INSERT INTO `cms_column` VALUES ('14', 'lww0210', '', '0', null);
INSERT INTO `cms_column` VALUES ('15', 'admin', '', '0', null);

-- ----------------------------
-- Table structure for `cms_link`
-- ----------------------------
DROP TABLE IF EXISTS `cms_link`;
CREATE TABLE `cms_link` (
  `id` int(11) NOT NULL auto_increment,
  `linkName` varchar(20) NOT NULL,
  `linkUrl` varchar(255) default NULL,
  `linkImg` varchar(255) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of cms_link
-- ----------------------------
INSERT INTO `cms_link` VALUES ('1', 'lww02', 'lww01', 'lww01');

-- ----------------------------
-- Table structure for `sys_account`
-- ----------------------------
DROP TABLE IF EXISTS `sys_account`;
CREATE TABLE `sys_account` (
  `id` int(11) NOT NULL auto_increment,
  `accountName` varchar(20) NOT NULL,
  `accountPwd` varchar(50) NOT NULL,
  `rAccountGroup` int(11) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of sys_account
-- ----------------------------
INSERT INTO `sys_account` VALUES ('3', 'lww001', 'e10adc3949ba59abbe56e057f20f883e', '3');
INSERT INTO `sys_account` VALUES ('4', 'lww002', 'e10adc3949ba59abbe56e057f20f883e', '2');
INSERT INTO `sys_account` VALUES ('5', 'lww003', 'e10adc3949ba59abbe56e057f20f883e', '2');
INSERT INTO `sys_account` VALUES ('6', 'lww004', 'e10adc3949ba59abbe56e057f20f883e', '2');
INSERT INTO `sys_account` VALUES ('7', 'lww005', 'e10adc3949ba59abbe56e057f20f883e', '2');
INSERT INTO `sys_account` VALUES ('8', 'lww006', 'e10adc3949ba59abbe56e057f20f883e', '2');
INSERT INTO `sys_account` VALUES ('9', 'lww007', 'e10adc3949ba59abbe56e057f20f883e', '2');
INSERT INTO `sys_account` VALUES ('10', 'lww008', 'e10adc3949ba59abbe56e057f20f883e', '2');
INSERT INTO `sys_account` VALUES ('11', 'lww009', 'e10adc3949ba59abbe56e057f20f883e', '2');
INSERT INTO `sys_account` VALUES ('12', 'lww010', 'e10adc3949ba59abbe56e057f20f883e', '2');
INSERT INTO `sys_account` VALUES ('13', 'lww011', 'e10adc3949ba59abbe56e057f20f883e', '2');

-- ----------------------------
-- Table structure for `sys_accountgroup`
-- ----------------------------
DROP TABLE IF EXISTS `sys_accountgroup`;
CREATE TABLE `sys_accountgroup` (
  `id` int(11) NOT NULL auto_increment,
  `groupName` varchar(20) NOT NULL,
  `des` varchar(100) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of sys_accountgroup
-- ----------------------------
INSERT INTO `sys_accountgroup` VALUES ('2', '分组三', '');
INSERT INTO `sys_accountgroup` VALUES ('3', '分组二', '');

-- ----------------------------
-- Table structure for `sys_accountgrouptomodule`
-- ----------------------------
DROP TABLE IF EXISTS `sys_accountgrouptomodule`;
CREATE TABLE `sys_accountgrouptomodule` (
  `id` int(11) NOT NULL auto_increment,
  `rAccountGroup` int(11) NOT NULL,
  `rModule` int(11) NOT NULL,
  `isEnable` bit(4) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of sys_accountgrouptomodule
-- ----------------------------
INSERT INTO `sys_accountgrouptomodule` VALUES ('1', '2', '1', '');
INSERT INTO `sys_accountgrouptomodule` VALUES ('2', '3', '1', '');
INSERT INTO `sys_accountgrouptomodule` VALUES ('3', '3', '2', '');
INSERT INTO `sys_accountgrouptomodule` VALUES ('4', '3', '3', '');
INSERT INTO `sys_accountgrouptomodule` VALUES ('5', '3', '4', '');
INSERT INTO `sys_accountgrouptomodule` VALUES ('6', '3', '5', '');
INSERT INTO `sys_accountgrouptomodule` VALUES ('7', '3', '6', '');
INSERT INTO `sys_accountgrouptomodule` VALUES ('8', '2', '4', '');
INSERT INTO `sys_accountgrouptomodule` VALUES ('9', '2', '5', '');
INSERT INTO `sys_accountgrouptomodule` VALUES ('10', '2', '6', '');
INSERT INTO `sys_accountgrouptomodule` VALUES ('11', '2', '2', '');
INSERT INTO `sys_accountgrouptomodule` VALUES ('12', '2', '3', '');

-- ----------------------------
-- Table structure for `sys_module`
-- ----------------------------
DROP TABLE IF EXISTS `sys_module`;
CREATE TABLE `sys_module` (
  `id` int(11) NOT NULL auto_increment,
  `moduleName` varchar(20) NOT NULL,
  `commandParameter` varchar(50) NOT NULL,
  `categoryId` int(11) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of sys_module
-- ----------------------------
INSERT INTO `sys_module` VALUES ('1', '文章管理', '/Admin/CMS/Index', '1');
INSERT INTO `sys_module` VALUES ('2', '相册管理', '/Admin/Photo/Index', '2');
INSERT INTO `sys_module` VALUES ('3', '简单统计', '/Admin/Chart/Index', '3');
INSERT INTO `sys_module` VALUES ('4', '栏目管理', '/Admin/CMS/Column', '1');
INSERT INTO `sys_module` VALUES ('5', '友情链接', '/Admin/CMS/LinkList', '1');
INSERT INTO `sys_module` VALUES ('6', '自定义区域', '/Admin/CMS/baseContentList', '1');

-- ----------------------------
-- Table structure for `sys_modulecategory`
-- ----------------------------
DROP TABLE IF EXISTS `sys_modulecategory`;
CREATE TABLE `sys_modulecategory` (
  `id` int(11) NOT NULL auto_increment,
  `categoryName` varchar(20) NOT NULL,
  `des` varchar(100) default NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=gbk;

-- ----------------------------
-- Records of sys_modulecategory
-- ----------------------------
INSERT INTO `sys_modulecategory` VALUES ('1', 'CMS管理', '新闻文章类管理');
INSERT INTO `sys_modulecategory` VALUES ('2', '图片浏览器', '用于浏览图片和上传图片');
INSERT INTO `sys_modulecategory` VALUES ('3', '图表管理', '图表管理');
INSERT INTO `sys_modulecategory` VALUES ('4', '暂未开发', '暂未开发');
