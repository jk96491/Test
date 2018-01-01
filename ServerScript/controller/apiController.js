const dbConn = require('../persistence/dbConnector');

// post -> req.body
// get local:3000/users/:id -> req.params
// get local:3000/users?id=ID -> req.query


module.exports = {

  // 000. 회원가입
  signup(req, res, next){
    const ID = req.body.id;
    const PW = req.body.pw;
    const NICK = req.body.nickname;

    let SQL = `INSERT INTO users(ID,PW,Nickname) VALUES('${ID}','${PW}','${NICK}');`;
    SQL += 'SELECT * FROM Users WHERE user_pk = LAST_INSERT_ID();';

    // 신규가입을 했다면
    dbConn.then(conn => conn.query(SQL))
      .then(rows => {
        res.send(JSON.stringify(rows[1]));
      })
      .catch(next);
  },

  // 001. 로그인
  // 디비에 있는 경우, 없는 경우
  login(req, res, next){
    const ID = req.params.id;

    let SQL = `SELECT * FROM users WHERE ID='${ID}';`;

    dbConn.then(conn => conn.query(SQL))
      .then(rows => {

        // 회원이 있으면 그 회원의 캐릭터 정보를 return
        if (rows.length != 0){
          const USER_INFO = rows[0]
          USER_INFO['UserParty'] = [1,2,3]
          // 원래는 UserParty 테이블에서 User_pk의 값으로 전부 불러와야 함

          const USER_PK = rows[0].user_pk;

          let SQL1 = `SELECT * FROM characters WHERE user_pk = ${USER_PK};`;

          dbConn.then(conn => conn.query(SQL1))
          .then(rows => {
            let character_info = rows;

            for(var i=0; i<character_info.length;i++){
              character_info[i]['skins']=[1,2,3]
              character_info[i]['skills']=[{
                skill_pk:1,
                level:1
              },{
                skill_pk:2,
                level:1
              },{
                skill_pk:3,
                level:1
              }];
            }
            // 원래는 skin, skill 테이블에서 다 불러와야 함
            // 설계가 덜 끝나서 궁여지책으로


            console.log(character_info)

            res.send({
              "USER":USER_INFO,
              "Characters":character_info
            });
          })
          .catch(next);
        }

        // 회원이 없으면 ID만 디비에 저장해둠
        else{
          let SQL2 = `INSERT INTO users(ID) VALUES('${ID}');`;
          SQL2 += 'SELECT * FROM users WHERE user_pk = LAST_INSERT_ID();';

          dbConn.then(conn => conn.query(SQL2))
            .then(rows => {
              res.send(JSON.stringify(rows[1]))
            })
            .catch(next);
        }
      })
      .catch(next);
  },

  // 001_a. 신규회원이면 상세정보를 입력 후 갱신
  login_update(req, res, next){
    const USER_PK = req.params.user_pk;
    const NICK = req.params.nickname;

    let SQL = `UPDATE users SET nickname = '${NICK}' WHERE user_pk = ${USER_PK};`;
    SQL += `SELECT * FROM Users WHERE user_pk = ${USER_PK};`

    dbConn.then(conn => conn.query(SQL))
      .then(rows => {
        res.send(JSON.stringify(rows[1]))
      })
      .catch(next);
  },

  // 002. 유저의 캐릭터를 불러옴
  findCharacters(req, res, next){
    const USER_PK = req.params.user_pk;

    let SQL = `SELECT * FROM characters WHERE user_pk = ${USER_PK};`;

    dbConn.then(conn => conn.query(SQL))
      .then(rows => {
        res.send(JSON.stringify(rows));
      })
      .catch(next);
  }
};
