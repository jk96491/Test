var bodyParser=require('body-parser');
var urlencodedParser = bodyParser.urlencoded({ extended: false });
var jsonParser = bodyParser.json();

module.exports=function(app){
app.get('/person/:id',function(req,res){
    var obj={
        ID:req.params.id,
        QSTR:req.query.qstr
    };

    // res.render('person',obj);
});

app.post('/person/',urlencodedParser, function(req,res){
    res.send('Thank you !');
    console.log(req.body.first);
    console.log(req.body.last);
});

app.post('/personjson',jsonParser,function(req,res){
    res.send('Thank you for json data !');

    console.log(req.body.first);
    console.log(req.body.last);
});

app.get('/api',function(req,res){
    res.json({first:'John',last:'Doe'});
});
};

// post -> req.body
// get local:3000/users/:id -> req.params
// get local:3000/users?id=ID -> req.query
