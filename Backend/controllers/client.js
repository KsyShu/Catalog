const client = require('../models/index').client;
const crypto = require('crypto');
module.exports.list = async function (req, res) {
    const clients = await client.findAll();
    for (var cli in clients) {
        delete cli.password;
    }
    res.send(clients);
}

module.exports.info = async function (req, res) {
    const cli = await client.findByPk(req.params.id);

    delete cli.password;
    res.send(cli);
}

module.exports.add = async function (req, res) {
    cli = req.body;
    cli.password = crypto.createHash('sha256').update(req.body.password).digest('base64');

    await client.create(cli);

    res.status(200).send();
}

module.exports.update = async function (req, res) {
    if (req.body.password) {
        req.body.password = crypto.createHash('sha256').update(req.body.password).digest('base64');
    }

    const cli = await client.findByPk(req.body.id);

    if (cli == null) {
        res.status(404).send('Клиент не найден')
    } else {
        cli.set(req.body);
        await cli.save();

        res.status(200).send();
    }
}
module.exports.delete = async function (req, res) {
    await client.destroy({
        where: {
            id: req.params.id
        }
    });

    res.status(200).send();
}