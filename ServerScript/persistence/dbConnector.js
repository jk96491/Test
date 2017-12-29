const mysql = require('mysql');
const config = require('./config')

var dbConn = mysql.createConnection({
  host:config.HOST,
  user:config.USER,
  password:config.PASSWORD,
  database:config.DATABASE,
  multipleStatements:true
});

let TableUsersSQL = "CREATE TABLE IF NOT EXISTS users (\
  pid INT NOT NULL AUTO_INCREMENT, \
  ID VARCHAR(30) NOT NULL,\
  PW VARCHAR(30) DEFAULT 0000,\
  gold INT DEFAULT 5000,\
  level INT DEFAULT 1,\
  exp BIGINT DEFAULT 0,\
  nickname VARCHAR(12), \
  PRIMARY KEY (pid)\
  );\
";
// let sql = "11 \ 11" 로 수정

let TableCharactersSQL = "CREATE TABLE IF NOT EXISTS characters (\
  user_pid INT NOT NULL, \
  pid INT NOT NULL AUTO_INCREMENT, \
  level INT DEFAULT 1, \
  exp BIGINT DEFAULT 0,\
  PRIMARY KEY (pid),\
  FOREIGN KEY (user_pid) REFERENCES users(pid) ON DELETE CASCADE \
  );\
";


dbConn.query(TableUsersSQL,(err,rows)=>{
  if(err) throw err;
  console.log('User SQL executed !');
})

dbConn.query(TableCharactersSQL,(err,rows)=>{
  if(err) throw err;
  console.log('Character SQL executed !')
})

module.exports = dbConn;
