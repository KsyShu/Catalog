const eating = require('../models/index').eating;

module.exports.list = async function (req, res) {
    const eatings = await eating.findAll();

    res.send(eatings);
}

module.exports.info = async function (req, res) {
    const cat = await eating.findByPk(req.params.id);

    res.send(cat);
}

module.exports.add = async function (req, res) {
    await eating.create(req.body);

    res.status(200).send();
}

module.exports.update = async function (req, res) {
    const cat = await eating.findByPk(req.body.id);

    if (cat == null) {
        res.status(404).send('Прием пищи не найден')
    } else {
        cat.set(req.body);
        await cat.save();
        res.status(200).send();
    }
}

module.exports.delete = async function (req, res) {
    await eating.destroy({
        where: {
            id: req.params.id
        }
    });
    res.status(200).send();
}