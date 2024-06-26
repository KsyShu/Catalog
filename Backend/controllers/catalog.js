const catalog = require('../models/index').Catalog;
/*const category = require('../models/index').category;*/

module.exports.addForm = async function (req, res) {
    const categories = await category.findAll();
    res.render('catalog/edit',
        {
            isNew: true,
            categories: categories
        });
}

module.exports.updateForm = async function (req, res) {
    const id = req.params.id;
    const cat = await catalog.findByPk(id);
    const categories = await category.findAll();
    categories.find(x => x.id === cat.category_id).selected = true;
    if (cat == null) {
        res.status(404).send("Подписка не найдена");
    } else {
        res.render('catalog/edit',
            {
                catalog: cat,
                isNew: false,
                categories: categories
            });
    }
}

module.exports.list = async function (req, res) {
    const catalog_items = await catalog.findAll();

    res.send(catalog_items);
}

module.exports.info = async function (req, res) {
    const cat = await catalog.findByPk(req.params.id, { include: category });
    res.send(cat);
}
module.exports.add = async function (req, res){
    if(req.file){
        req.body.file_path = req.file.filename;
        req.body.file_name = req.file.originalname;
    }
    await catalog.create(req.body);

    res.status(200).send();
}
module.exports.update = async function (req, res){
    const cat = await catalog.findByPk(req.body.id);

    if(cat == null){
        res.status(404).send('Программа не найдена')
    }else{
        if(req.file){
            req.body.file_path = req.file.filename;
            req.body.file_name = req.file.originalname;
        }
        cat.set(req.body);
        await cat.save();
        res.status(200).send();
    }   
}
module.exports.delete = async function (req, res){
    await catalog.destroy({
        where: {
            id: req.params.id
        }
    });

    res.status(200).send();
}