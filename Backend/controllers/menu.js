const menu = require('../models/index').menu;

module.exports.addForm = async function (req, res) {
    const menus = await menu.findAll();
    res.render('menu/edit',
        {
            isNew: true,
            menus: menus
        });
}

module.exports.updateForm = async function (req, res) {
    const id = req.params.id;
    const cat = await menu.findByPk(id);
    const eatings = await eating.findAll();
    eatings.find(x => x.id === cat.eating_id).selected = true;
    if (cat == null) {
        res.status(404).send("Прием пищи не найден");
    } else {
        res.render('menu/edit',
            {
                menu: cat,
                isNew: false,
                eatings: eatings
            });
    }
}

module.exports.list = async function (req, res) {
    const menu_items = await menu.findAll();

    res.send(menu_items);
}

module.exports.info = async function (req, res) {
    const cat = await menu.findByPk(req.params.id, { include: eating });
    res.send(cat);
}

module.exports.add = async function (req, res) {
    if (req.file) {
        req.body.file_path = req.file.filename;
        req.body.file_name = req.file.originalname;
    }
    await menu.create(req.body);

    res.status(200).send();
}

module.exports.update = async function (req, res) {
    const cat = await menu.findByPk(req.body.id);

    if (cat == null) {
        res.status(404).send('Меню не найдено')
    } else {
        if (req.file) {
            req.body.file_path = req.file.filename;
            req.body.file_name = req.file.originalname;
        }
        cat.set(req.body);
        await cat.save();
        res.status(200).send();
    }
}

module.exports.delete = async function (req, res) {
    await menu.destroy({
        where: {
            id: req.params.id
        }
    });

    res.status(200).send();
}