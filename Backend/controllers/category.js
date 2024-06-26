const category = require('../models/index').category;
const catalog = require('../models/index').Catalog;

module.exports.addForm = async function (req, res) {
    const Catalogs = await catalog.findAll();
    res.render('category/edit',
        {
            isNew: true,
            Catalogs:Catalogs
        });
}
module.exports.updateForm = async function (req, res) {
    const id = req.params.id;
    const cat = await category.findByPk(id);
    const Catalogs = await catalog.findAll();
    Catalogs.find(x => x.id === cat.program).selected = true;
    if (cat == null) {
        res.status(404).send("Программа не найдена");
    } else {
        res.render('category/edit',
            {
                category: cat,
                isNew: false,
                Catalogs: Catalogs
            });
    }
}
module.exports.list = async function (req, res) {
    const categories = await category.findAll();

    res.send(categories);
}

module.exports.info = async function (req, res) {
    const cat = await category.findByPk(req.params.id);

    res.send(cat);
}
module.exports.add = async function (req, res) {
    await category.create(req.body);

    res.status(200).send();
}
module.exports.update = async function (req, res) {
    const cat = await category.findByPk(req.body.id);

    if(cat == null){
        res.status(404).send('Подписка не найдена')
    }else{
        cat.set(req.body);
        await cat.save();
        res.status(200).send();
    }   
}
module.exports.delete = async function (req, res) {
    await category.destroy({
        where: {
            id: req.params.id
        }
    });
    res.status(200).send();
}