const express = require('express');
const app = express();
const port = process.env.PORT || 3000;
const bodyParser = require('body-parser');
const routes = require('./routes/routes')

app.use(bodyParser.json());

routes(app);

app.use((err, req, res, next) => {
  res.status(422).send({error: err.message});
});

module.exports = app;
