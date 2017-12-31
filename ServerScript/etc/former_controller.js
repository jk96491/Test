let dbConn = require('../persistence/dbConnector');

module.exports = function(app){

  // 000. User Sign up
  //
  app.get('/users/',function(req,res){
    // ID unique 처리
    let ID = req.query.id;
    let PW = req.query.pw;
    let SQL = `INSERT INTO users(ID,PW) VALUES('${ID}','${PW}');`;
    SQL += 'SELECT pid,gold,level,exp FROM users WHERE pid = LAST_INSERT_ID();';

    dbConn.query(SQL,(err,rows)=>{
      if(err){
        res.send(JSON.stringify({
          msg:'Error while sign up',
          code:'000 Sign up',
        }));

        throw err;
      }

      res.send(JSON.stringify({
        msg:`User sucessfully created !`,
        result:rows[1][0]
      }));
    });
  });

  // 001. Find User (LOGIN)
  app.get('/users/:id',function(req,res){
    let ID = req.params.id;
    let SQL = `SELECT pid,gold,level,exp,nickname FROM users WHERE ID='${ID}';`;

    var users;
    // rows, results 차이 = results로 하면 서버가 에러를 캣치못하고 죽음
    dbConn.query(SQL,(err,rows)=>{
      if(err){
        res.send(JSON.stringify({
          msg:'Error while find user = '+ID,
          code:'001 Find user'
        }));

        throw err;
      }

      // 입력받은 유저가 디비에 있는 경우
      if (rows.length != 0){
        let users = rows;
        let PID = users[0].pid
        let SQL1 = `SELECT pid, level, exp FROM characters WHERE user_pid = ${PID};`;

        dbConn.query(SQL1,(err,rows)=>{
          if(err){
            throw err;
          }

        res.send(JSON.stringify({
          msg:'Found !',
          user:users,
          characters:rows
        }));
        });
    }

    // 입력받은 유저가 디비에 없는 경우
    else{
      let SQL2 = `INSERT INTO users(ID) VALUES('${ID}');`;
      SQL2 += 'SELECT pid,gold,level,exp FROM users WHERE pid = LAST_INSERT_ID();';
      dbConn.query(SQL2,(err,rows)=>{
        if(err){
          throw err;
        }

        res.send(JSON.stringify({
          msg:'Created Before Nickname',
          user:rows[1]
        }))
      });
    }

    });
  });

  // 신규회원일 경우 ID를 클라에서 입력받고 서버에 생성 후
  // 다음 UI에서 pid,nickname을 입력받고 생성된 유저튜플 갱신
  app.get('/users_nickname/', function(req,res){
    let PID = req.query.pid;
    let NICK = req.query.nick;
    let SQL = `UPDATE users SET nickname = '${NICK}' WHERE pid = ${PID};`;

    dbConn.query(SQL,(err,rows)=>{
      if(err){
        throw err;
      }

      res.send(JSON.stringify({
        msg:'Successfully Updated Nickname !'
      }));
    });
  });

  // 002. Find user and characters
  app.get('/characters/:pid',function(req,res){
    let PID = req.params.pid;
    let SQL = `SELECT pid, level, exp FROM characters WHERE user_pid = ${PID};`;

    dbConn.query(SQL,(err,rows)=>{
      if(err){
        res.send(JSON.stringify({
          msg:'Error while find user_pid = '+PID,
          code:'002 Find user and characters'
        }));

        throw err
      }

      res.send(JSON.stringify({
        msg:'User and Characters successfully  found',
        result:rows
      }));
    });
  });

};
