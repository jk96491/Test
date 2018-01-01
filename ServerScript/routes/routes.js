const apiController = require('../controller/apiController');

module.exports = (app) => {

  // 000. 회원가입
  app.post('/users/', apiController.signup)

  // 001. 로그인
  // 디비에 있는 경우
  app.get('/users/:id', apiController.login)

  // 001_a. 신규회원이면 상세정보를 입력 후 갱신
  app.put('/users/:user_pk/nickname/:nickname', apiController.login_update)

  // 002. 유저의 캐릭터를 불러옴
  app.get('/characters/:user_pk', apiController.findCharacters)

  // 003. 회원가입 or 001_a 후에 캐릭터를 새로 생성함
  // app.post('')
};
