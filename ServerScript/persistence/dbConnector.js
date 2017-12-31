const mysql = require('promise-mysql');
const config = require('../config/config')

const dbConn = mysql.createConnection({
  host:config.HOST,
  user:config.USER,
  password:config.PASSWORD,
  database:config.DATABASE,
  multipleStatements:true
});

module.exports = dbConn;
