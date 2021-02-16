var createError = require('http-errors');
var express = require('express');
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');

var router = require('./routes/api');

var app = express();

//Jade motor az error page generálásához
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'jade');

// Statikus oldalak
app.use(express.static(path.join(__dirname, 'web')));

// Extra eszközök
app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(cookieParser());

// CORS engedélyezése
app.use(function (req, res, next) {
  res.header('Access-Control-Allow-Origin', '*');
  res.header('Access-Control-Allow-Headers', 'Origin, X-Requested-With, Content-Type, Accept');
  next()
});

// Router beállítása
app.use('/api', router);

// 404-es hiba elkapása & kezelése
app.use(function(req, res, next) {
  next(createError(404));
});

// Hibakezelő
app.use(function(err, req, res, next) {
  // set locals, only providing error in development
  res.locals.message = err.message;
  res.locals.error = req.app.get('env') === 'development' ? err : {};

  // render the error page
  res.status(err.status || 500);
  res.render('error');
});

module.exports = app;
