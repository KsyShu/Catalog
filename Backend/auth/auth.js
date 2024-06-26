const employee = require("../models").employee
const client = require("../models").client
const crypto = require('crypto')
const { response } = require("express");

module.exports.login = async function (req, res) {
    username = req.body.username;
    password = req.body.password;

    const userEmployee = await employee.findOne({
        where: { username },
        raw: true
    })
    const userClient = await client.findOne({
        where: { username },
        raw: true
    })

    hashedPassword = crypto.createHash('sha256').update(password).digest('base64');
    const response = {};

    if (userEmployee == null && userClient == null) {
        res.status(404).send();
        return;
    }
    if (userEmployee && userEmployee.password == hashedPassword) {
        response['userEmployee'] = userEmployee;
        delete response.userEmployee.password;
    }
    if (userClient && userClient.password == hashedPassword) {
        response['userClient'] = userClient;
        delete response.userClient.password;
    }

    console.log(response);
    if (response.userEmployee == undefined && response.userClient == undefined) {
        res.status(401).send();
        return;
    }
    res.status(200).send(response);
}
