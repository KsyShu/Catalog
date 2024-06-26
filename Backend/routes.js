const express = require('express');
const multer = require('multer');

const employee = require('./controllers/employee');
const catalog = require('./controllers/catalog');
const category = require('./controllers/category');
const eating = require('./controllers/eating');
const client = require('./controllers/client');
const { login } = require('./auth/auth');

const router = express.Router();
//router.get('/',
//    function (req, res) {
//        res.render('login');
//    }
//);
const upload = multer({
    limits: { fieldSize: 10 * 1024 * 1024 } // Увеличьте значение 10 * 1024 * 1024 для увеличения размера поля данных
});
//const upload = multer({
//    dest: 'upload',
//    fileFilter: (req, file, cb) => {
//        file.originalname = Buffer.from(file.originalname, 'latin1').toString('utf8')
//        cb(null, true);
//    }
//})

const employeeRouter = express.Router();
employeeRouter.post('/add', employee.add);
employeeRouter.post('/update', employee.update);
employeeRouter.get('/info/:id', employee.info);
employeeRouter.get('/list', employee.list);
employeeRouter.get('/delete/:id', employee.delete);

const catalogRouter = express.Router();
catalogRouter.post('/add', upload.single('file'), catalog.add);
catalogRouter.post('/update', upload.single('file'), catalog.update);
catalogRouter.get('/info/:id', catalog.info);
catalogRouter.get('/list', catalog.list);
catalogRouter.get('/delete/:id', catalog.delete);

const categoryRouter = express.Router();
categoryRouter.post('/add', category.add);
categoryRouter.post('/update', category.update);
categoryRouter.get('/info/:id', category.info);
categoryRouter.get('/list', category.list);
categoryRouter.get('/delete/:id', category.delete);

const eatingRouter = express.Router();
eatingRouter.get('/info/:id', eating.info);
eatingRouter.get('/list', eating.list);
eatingRouter.get('/delete/:id', eating.delete);
eatingRouter.post('/add', upload.any(), eating.add);
eatingRouter.post('/update', upload.any(), eating.update);
module.exports = eatingRouter;

const clientRouter = express.Router();
clientRouter.post('/add', client.add);
clientRouter.post('/update', client.update);
clientRouter.get('/info/:id', client.info);
clientRouter.get('/list', client.list);
clientRouter.get('/delete/:id', client.delete);

router.use('/employee', employeeRouter);
router.use('/catalog', catalogRouter);
router.use('/category', categoryRouter);
router.use('/eating', eatingRouter);
router.use('/client', clientRouter);

router.post('/auth', login);

module.exports = router;