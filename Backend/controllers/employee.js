const employee = require('../models/index').employee;
const crypto = require('crypto');
module.exports.list = async function (req, res) {
    const employees = await employee.findAll();
    for (var emp in employees) {
        delete emp.password;
    }
    res.send(employees);
}

module.exports.info = async function (req, res) {
    const emp = await employee.findByPk(req.params.id);
    
    delete emp.password;
    res.send(emp);
}

module.exports.add = async function (req, res) {
    emp = req.body;
    emp.password = crypto.createHash('sha256').update(req.body.password).digest('base64');

    await employee.create(emp);

    res.status(200).send();
}

module.exports.update = async function (req, res) {
    if (req.body.password) {
        req.body.password = crypto.createHash('sha256').update(req.body.password).digest('base64');
    }

    const emp = await employee.findByPk(req.body.id);
    
    if(emp == null){
        res.status(404).send('Работник не найден')
    }else{
        emp.set(req.body);
        await emp.save();

        res.status(200).send();
    }   
}

module.exports.delete = async function (req, res) {
    await employee.destroy({
        where: {
            id: req.params.id
        }
    });

    res.status(200).send();
}