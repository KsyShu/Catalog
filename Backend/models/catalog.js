'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class Catalog extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      //this.belongsTo(models.category,{
      //  foreignKey: 'category_id',
      //})
    }
  }
  Catalog.init({
    name: DataTypes.TEXT,
    /*category_id: DataTypes.INTEGER*/
  }, {
    sequelize,
    modelName: 'Catalog',
  });
  return Catalog;
};